using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectStep1_server
{
    public partial class Form1 : Form
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        List<Socket> clientSockets = new List<Socket>();

        bool terminating = false;
        bool listening = false;
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }



        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            File.WriteAllText(@"../../online-db.txt", String.Empty);
           
            

            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if(Int32.TryParse(textBox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                button_listen.Enabled = false;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                serverlogs.AppendText("Started listening on port: " + serverPort + ".\n");
            }

            else
            {
                serverlogs.AppendText("Check the port number!\n");
            }
        }

        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);

                    Thread receiveThread = new Thread(() => Receive(newClient));
                    receiveThread.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        serverlogs.AppendText("The socket stopped working.\n");
                    }
                }
            }
        }

        private void Receive(Socket thisClient)
        {
            bool connected = true;

            while (connected && !terminating)
            {
                bool isfound = false;

                try
                {
                    Byte[] buffer = new Byte[256];
                    thisClient.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);

                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    
                    string mType = incomingMessage.Substring(0, incomingMessage.IndexOf("-"));
                    
                    
                    if(mType == "Connect")
                    {
                        string username = incomingMessage.Substring(incomingMessage.IndexOf("-") + 1);
                        
                        foreach (string line in File.ReadLines(@"../../user-db.txt"))
                        {
                            string dbaseUsername = line;
                            
                            if (username == dbaseUsername)
                            {
                                isfound = true;
                            }
                        }
                       

                        if (isfound == true)
                        {
                            bool isOnline = false;

                            foreach (string line in File.ReadLines(@"../../online-db.txt"))
                            {
                                string onlineUserName = line;

                                if (username == onlineUserName)
                                {
                                    isOnline = true;
                                    
                                }
                            }

                            if (isOnline == false)
                            {

                                string enterStatus = username;
                                using (StreamWriter file = new StreamWriter("../../online-db.txt", append: true))
                                {
                                    file.WriteLine(enterStatus);
                                }

                                serverlogs.AppendText(username + " has connected.\n");
                                string connectedreply = "OK";

                                sendMessage(thisClient, connectedreply);
                            }

                            else if (isOnline == true)
                            {
                                serverlogs.AppendText(username + " already connected!\n");
                                string alreadyOnline = "ONLINE";
                                sendMessage(thisClient, alreadyOnline);
                            }

                          

                            
                        }

                        else
                        {
                            serverlogs.AppendText(username + " tried to connect to the server but cannot!\n");
                            string notconnectedreply = "NOTOK";

                            sendMessage(thisClient, notconnectedreply);


                        }
                        
                    }

                    else if (mType == "Disconnect")
                    {

                        
                        string username = incomingMessage.Substring(incomingMessage.IndexOf("-") + 1);
                      


                        string[] onlines = File.ReadAllLines(@"../../online-db.txt");
                        List<string> onlinesList = onlines.ToList();

                        if(onlinesList.Remove(username))
                        {
                            System.IO.File.WriteAllLines(@"../../online-db.txt", onlinesList.ToArray());
                        }
                     

                        serverlogs.AppendText(username + " has disconnected.\n");

                    }

                    else if (mType == "Post")
                    {
                        string usernamePost = incomingMessage.Substring(incomingMessage.IndexOf("-") + 1);
                        string username = usernamePost.Substring(0, usernamePost.IndexOf("-"));
                        string post = usernamePost.Substring(usernamePost.IndexOf("-") + 1);

                       

                        string[] allPosts = File.ReadAllLines(@"../../posts-db.txt");
                        List<string> allPostsList = allPosts.ToList();
                        string currentIDstr = allPostsList[0];

                        int currentID = Int32.Parse(currentIDstr);
                        


                        int postID = currentID + 1;
                        allPostsList[0] = postID.ToString();
                        

                        System.IO.File.WriteAllLines(@"../../posts-db.txt", allPostsList.ToArray());


                        string postRecord = DateTime.Now + "/" + username + "/" + postID + "/" + post;

                        using (StreamWriter file = new StreamWriter("../../posts-db.txt", append: true))
                        {
                            file.WriteLine(postRecord);
                        }

                        serverlogs.AppendText(username + " has sent a post:\n");
                        serverlogs.AppendText(post+"\n");

                        string sendback = username + ": " + post + "\n";
                        sendMessage(thisClient, sendback);
                    }

                    else if (mType == "All")
                    {
                        string username = incomingMessage.Substring(incomingMessage.IndexOf("-") + 1);

                        string reqAcc = "Showing all posts from clients:\n";
                        string sentNL = "\n";
                        sendMessage(thisClient, sentNL);
                        sendMessage(thisClient, reqAcc);
                        int counter = 0;

                        foreach (string line in File.ReadLines(@"../../posts-db.txt"))
                        {
                            if(counter == 0)
                            {

                            }

                            else
                            {
                                string dbaseDate = line.Substring(0, line.IndexOf("/"));
                                string noDate = line.Substring(line.IndexOf("/") + 1);

                                string dbaseUsername = noDate.Substring(0, noDate.IndexOf("/"));
                                string idPost = noDate.Substring(noDate.IndexOf("/") + 1);

                                string dbasePostID = idPost.Substring(0, idPost.IndexOf("/"));
                                string dbasePost = idPost.Substring(idPost.IndexOf("/") + 1);


                                if (username != dbaseUsername)
                                {


                                    string sentUser = "Username: " + dbaseUsername + "\n";
                                    string sentID = "PostID: " + dbasePostID + "\n";
                                    string sentPost = "Post: " + dbasePost + "\n";
                                    string sentTime = "Time: " + dbaseDate + "\n";



                                    sendMessage(thisClient, sentUser);
                                    sendMessage(thisClient, sentID);
                                    sendMessage(thisClient, sentPost);
                                    sendMessage(thisClient, sentTime);
                                    sendMessage(thisClient, sentNL);
                                }
                            }


                            counter++;
                            
                            
                        }
                        serverlogs.AppendText("Showed all posts for " + username + ".\n");
                    }


                    else if (mType == "FriendsPosts")
                    {
                        string username = incomingMessage.Substring(incomingMessage.IndexOf("-") + 1);

                        string reqAcc = "Showing all posts from friends:\n";
                        string sentNL = "\n";
                        sendMessage(thisClient, sentNL);
                        sendMessage(thisClient, reqAcc);
                        int counter = 0;

                        foreach (string line in File.ReadLines(@"../../posts-db.txt"))
                        {
                            if (counter == 0)
                            {

                            }

                            else
                            {
                                string dbaseDate = line.Substring(0, line.IndexOf("/"));
                                string noDate = line.Substring(line.IndexOf("/") + 1);

                                string dbaseUsername = noDate.Substring(0, noDate.IndexOf("/"));
                                string idPost = noDate.Substring(noDate.IndexOf("/") + 1);

                                string dbasePostID = idPost.Substring(0, idPost.IndexOf("/"));
                                string dbasePost = idPost.Substring(idPost.IndexOf("/") + 1);
                                bool isFriend = false;

                                string friendRecord = username + "-" + dbaseUsername;

                                foreach (string friendLine in File.ReadLines(@"../../friends-db.txt"))
                                {



                                    if (friendLine == friendRecord)
                                    {
                                        isFriend = true;
                                    }

                                }


                                if (username != dbaseUsername && isFriend == true)
                                {

                                    

                                    string sentUser = "Username: " + dbaseUsername + "\n";
                                    string sentID = "PostID: " + dbasePostID + "\n";
                                    string sentPost = "Post: " + dbasePost + "\n";
                                    string sentTime = "Time: " + dbaseDate + "\n";



                                    sendMessage(thisClient, sentUser);
                                    sendMessage(thisClient, sentID);
                                    sendMessage(thisClient, sentPost);
                                    sendMessage(thisClient, sentTime);
                                    sendMessage(thisClient, sentNL);
                                }
                            }


                            counter++;


                        }
                        serverlogs.AppendText("Showed all friend's posts for " + username + ".\n");

                    }

                    else if (mType == "MyPosts")
                    {
                        string username = incomingMessage.Substring(incomingMessage.IndexOf("-") + 1);

                        string reqAcc = "Showing your posts:\n";
                        string sentNL = "\n";
                        sendMessage(thisClient, sentNL);
                        sendMessage(thisClient, reqAcc);
                        int counter = 0;

                        foreach (string line in File.ReadLines(@"../../posts-db.txt"))
                        {
                            if (counter == 0)
                            {

                            }

                            else
                            {
                                string dbaseDate = line.Substring(0, line.IndexOf("/"));
                                string noDate = line.Substring(line.IndexOf("/") + 1);

                                string dbaseUsername = noDate.Substring(0, noDate.IndexOf("/"));
                                string idPost = noDate.Substring(noDate.IndexOf("/") + 1);

                                string dbasePostID = idPost.Substring(0, idPost.IndexOf("/"));
                                string dbasePost = idPost.Substring(idPost.IndexOf("/") + 1);
                                


                                if (username == dbaseUsername)
                                {



                                    string sentUser = "Username: " + dbaseUsername + "\n";
                                    string sentID = "PostID: " + dbasePostID + "\n";
                                    string sentPost = "Post: " + dbasePost + "\n";
                                    string sentTime = "Time: " + dbaseDate + "\n";



                                    sendMessage(thisClient, sentUser);
                                    sendMessage(thisClient, sentID);
                                    sendMessage(thisClient, sentPost);
                                    sendMessage(thisClient, sentTime);
                                    sendMessage(thisClient, sentNL);
                                }
                            }


                            counter++;


                        }
                        serverlogs.AppendText("Showed " + username + "'" + "s posts.\n");
                            
                    }

                    else if (mType == "Addf")
                    {

                        

                        bool inFriendDB = false;
                        bool isFoundDB = false; 
                        string usernames = incomingMessage.Substring(incomingMessage.IndexOf("-") + 1);
                        string username = usernames.Substring(0, usernames.IndexOf("-"));
                        string usernameFriend = usernames.Substring(usernames.IndexOf("-") + 1);

                        if (username == usernameFriend)
                        {
                            string notToYourself = "You cannot send a request to yourself!\n";
                            sendMessage(thisClient, notToYourself);
                            serverlogs.AppendText("You cannot send a request to yourself!\n");
                        }

                        else
                        {

                            foreach (string line in File.ReadLines(@"../../user-db.txt"))
                            {
                                string dbaseUsername = line;

                                if (usernameFriend == dbaseUsername)
                                {
                                    isFoundDB = true;
                                }
                            }

                            if (isFoundDB == true)
                            {

                                string friendRecord = username + "-" + usernameFriend;
                                string reverseFriendRecord = usernameFriend + "-" + username; 

                                foreach (string line in File.ReadLines(@"../../friends-db.txt"))
                                {


                                    if (line == friendRecord)
                                    {
                                        inFriendDB = true;
                                    }

                                }


                                if (inFriendDB == false)
                                {
                                    using (StreamWriter file = new StreamWriter("../../friends-db.txt", append: true))
                                    {
                                        file.WriteLine(friendRecord);
                                        file.WriteLine(reverseFriendRecord);
                                        string addedFriend = "You have added " + usernameFriend + " as a friend.\n";

                                        sendMessage(thisClient, addedFriend);

                                        serverlogs.AppendText(username + " has added " + usernameFriend + " as a friend.\n");

                                        string[] onlines = File.ReadAllLines(@"../../online-db.txt");
                                        List<string> onlinesList = onlines.ToList();
                                        int counter = 0;

                                        foreach (string name in onlinesList)
                                        {
                                            if (name == usernameFriend)
                                            {
                                                Socket friendSock = clientSockets[counter];

                                                string friendMessage = username + " has added you as a friend.\n";

                                                sendMessage(friendSock, friendMessage);
                                            }

                                            counter++;
                                        }
                                    }
                                }

                                else
                                {
                                    string alreadyFriends = "You are already friends with " + usernameFriend + "!\n";
                                    sendMessage(thisClient, alreadyFriends);
                                }

                                


                            }
                            else
                            {
                                string notInDB = "User " + usernameFriend + " does not exist in the server!\n";
                                sendMessage(thisClient, notInDB);
                                serverlogs.AppendText("User " + usernameFriend + " does not exist in the server!\n");

                            }




                        }
                        
                    }

                    else if (mType == "Removef")
                    {

                        string usernames = incomingMessage.Substring(incomingMessage.IndexOf("-") + 1);
                        string username = usernames.Substring(0, usernames.IndexOf("-"));
                        string usernameFriend = usernames.Substring(usernames.IndexOf("-") + 1);

                        string friendRecord = username + "-" + usernameFriend;
                        string friendRecordReversed = usernameFriend + "-" + username;

                        bool inFriendDB = false; 

                        foreach (string line in File.ReadLines(@"../../friends-db.txt"))
                        {


                            if (line == friendRecord)
                            {
                                inFriendDB = true;
                            }

                        }

                        if (inFriendDB == false)
                        {
                            string noFriend = "You do not have a friend named " + usernameFriend + " !\n";
                            sendMessage(thisClient, noFriend);

                            serverlogs.AppendText(username + " tried to remove " + usernameFriend + " from its friends but no such friend exists!\n");
                        }

                        else
                        {

                            string[] friends = File.ReadAllLines(@"../../friends-db.txt");
                            List<string> friendsList = friends.ToList();

                            if (friendsList.Remove(friendRecord))
                            {
                                System.IO.File.WriteAllLines(@"../../friends-db.txt", friendsList.ToArray());
                            }

                            string[] friends2 = File.ReadAllLines(@"../../friends-db.txt");
                            List<string> friendsList2 = friends2.ToList();

                            if (friendsList2.Remove(friendRecordReversed))
                            {
                                System.IO.File.WriteAllLines(@"../../friends-db.txt", friendsList2.ToArray());
                            }

                            string removedFriend = "You have removed " + usernameFriend + " from your friend list.\n";
                            sendMessage(thisClient, removedFriend);

                            serverlogs.AppendText(username + " removed " + usernameFriend + " from its friend list!\n");


                            string[] onlines = File.ReadAllLines(@"../../online-db.txt");
                            List<string> onlinesList = onlines.ToList();
                            int counter = 0;

                            foreach (string name in onlinesList)
                            {
                                if (name == usernameFriend)
                                {
                                    Socket friendSock = clientSockets[counter];

                                    string friendMessage = "You have been removed from " + username + "'" +  "s friend list.\n";

                                    sendMessage(friendSock, friendMessage);
                                }

                                counter++;
                            }
                        }

                    }

                    else if (mType == "Deletep")
                    {
                        string usernamepost = incomingMessage.Substring(incomingMessage.IndexOf("-") + 1);
                        string username = usernamepost.Substring(0, usernamepost.IndexOf("-"));
                        string postID = usernamepost.Substring(usernamepost.IndexOf("-") + 1);


                        bool doesExist = false;
                        bool doesBelong = false;
                        int counter = 0;

                        foreach (string line in File.ReadLines(@"../../posts-db.txt"))
                        {

                            if (counter == 0)
                            {

                            }

                            else
                            {
                                string dbaseDate = line.Substring(0, line.IndexOf("/"));
                                string noDate = line.Substring(line.IndexOf("/") + 1);

                                string dbaseUsername = noDate.Substring(0, noDate.IndexOf("/"));
                                string idPost = noDate.Substring(noDate.IndexOf("/") + 1);

                                string dbasePostID = idPost.Substring(0, idPost.IndexOf("/"));
                                string dbasePost = idPost.Substring(idPost.IndexOf("/") + 1);



                                if (dbasePostID == postID)
                                {
                                    doesExist = true;

                                    if(dbaseUsername == username)
                                    {
                                        doesBelong = true;
                                        
                                    }
   
                                }

                               
                            }


                            counter++;

                        }

                        if (doesExist == true && doesBelong == true)
                        {
                            string[] posts = File.ReadAllLines(@"../../posts-db.txt");
                            List<string> postsList = posts.ToList();
                            List<string> newPostsList = new List<string>();
                            int postcounter = 0;


                            foreach (string post in postsList)
                            {

                                

                                if (postcounter == 0)
                                {
                                    newPostsList.Add(post);
                                }

                                else
                                {
                                    string dbaseDate = post.Substring(0, post.IndexOf("/"));
                                    string noDate = post.Substring(post.IndexOf("/") + 1);

                                    string dbaseUsername = noDate.Substring(0, noDate.IndexOf("/"));
                                    string idPost = noDate.Substring(noDate.IndexOf("/") + 1);

                                    string dbasePostID = idPost.Substring(0, idPost.IndexOf("/"));
                                    string dbasePost = idPost.Substring(idPost.IndexOf("/") + 1);

                                    

                                    if (postID != dbasePostID)
                                    {
                                        newPostsList.Add(post);
                                        
                                    }

                                    

                                }

                                postcounter++;
                                
                            }

                            System.IO.File.WriteAllLines(@"../../posts-db.txt", newPostsList.ToArray());

                            string deletedMess = "Your post with ID " + postID + " is deleted successfully!\n";
                            sendMessage(thisClient, deletedMess);

                            serverlogs.AppendText("Post with id: " + postID + " is deleted successfully by " + username + ".\n");


                        }

                        else if (doesExist == false)
                        {
                            string noPost = "There is no post with ID:" + postID + "\n";
                            sendMessage(thisClient, noPost);

                            serverlogs.AppendText("Post with id:" + postID + " does not exist!\n");
                        }

                        else if (doesBelong == false)
                        {
                            string dontBelong = "Post with ID " + postID + " is not yours!\n";
                            sendMessage(thisClient, dontBelong);

                            serverlogs.AppendText("Post with id:" + postID + " is not " + username + "'" + "s!\n");
                        }



                    }

                    else if (mType == "Showf")
                    {
                        string username = incomingMessage.Substring(incomingMessage.IndexOf("-") + 1);
                        string showFMsg = "Friends of " + username + " are: \n";
                        sendMessage(thisClient, showFMsg);

                        foreach (string line in File.ReadLines(@"../../friends-db.txt"))
                        {
                            string usernameDB = line.Substring(0 , line.IndexOf("-"));

                            

                            if (usernameDB == username)
                            {
                                string friendDB = line.Substring(line.IndexOf("-") + 1);
                                friendDB = friendDB + "\n";
                                sendMessage(thisClient, friendDB);
                            }

                        }

                        serverlogs.AppendText("Friends of " + username + " have been shown.\n");
                    }



                }
                catch
                {
                    if (!terminating)
                    {
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = false;
                }
            }
        }

        private void sendMessage(Socket thisClient, string message)
        {
            Byte[] buffer = Encoding.Default.GetBytes(message);
            try
            {
                thisClient.Send(buffer);
            }
            catch
            {
                serverlogs.AppendText("There is a problem! Check the connection...\n");
                terminating = true;
                textBox_port.Enabled = true;
                button_listen.Enabled = true;
                serverSocket.Close();
            }
        }

    }
}
