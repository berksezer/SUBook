using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectStep1_client
{
    public partial class Form1 : Form
    {

        bool terminating = false;
        bool connected = false;
        Socket clientSocket;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string username = textBox_username.Text;

            try
            {
                string message = "Disconnect-" + username;
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);
            }

            catch
            {
                if (!terminating)
                {
                    clientlogs.AppendText("The server has disconnected !!\n");
                    button_connect.Enabled = true;
                    button_disconnect.Enabled = false;
                    button_send.Enabled = false;
                    button_allposts.Enabled = false;
                    textBox_post.Enabled = false;
                    textBox_post.Clear();

                    button_addf.Enabled = false;
                    button_removef.Enabled = false;
                    button_showf.Enabled = false;
                    textBox_firendUsername.Enabled = false;

                    button_friendsPosts.Enabled = false;
                    button_myPosts.Enabled = false;

                    button_deletePost.Enabled = false;
                    textBox_postID.Enabled = false;


                }


            }

            connected = false;
            terminating = true;
            Environment.Exit(0);

        }

       
       
        private void button_connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text;
            int portNum;

            string nameBox = textBox_username.Text;

            if (IP == "")
            {
                clientlogs.AppendText("Please enter a IP address!\n");
            }

           

            else if (Int32.TryParse(textBox_port.Text, out portNum))
            {
                try
                {
                    clientSocket.Connect(IP, portNum);

                    

                    connected = true;
                    terminating = false;
                    

                    Thread receivethread = new Thread(Receive);
                    receivethread.Start();

                    string username = textBox_username.Text;

                    try
                    {
                        string message = "Connect-" + username;
                        Byte[] buffer = Encoding.Default.GetBytes(message);
                        clientSocket.Send(buffer);
                    }

                    catch
                    {
                        if (!terminating)
                        {
                            clientlogs.AppendText("The server has disconnected !!\n");
                            button_connect.Enabled = true;
                            button_disconnect.Enabled = false;
                            button_send.Enabled = false;
                            button_allposts.Enabled = false;
                            textBox_post.Enabled = false;
                            textBox_post.Clear();

                            button_addf.Enabled = false;
                            button_removef.Enabled = false;
                            button_showf.Enabled = false;
                            textBox_firendUsername.Enabled = false;

                            button_friendsPosts.Enabled = false;
                            button_myPosts.Enabled = false;

                            button_deletePost.Enabled = false;
                            textBox_postID.Enabled = false;


                        }

                        clientSocket.Close();
                        connected = false;
                    }


                }
                catch
                {
                    clientlogs.AppendText("Could not connect to the server!\n");
                }


            }
            else
            {
                clientlogs.AppendText("Check the port number!\n");
            }
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            string username = textBox_username.Text;

            try
            {
                string message = "Disconnect-" + username;
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);
                
            }

            catch
            {
                if (!terminating)
                {
                    clientlogs.AppendText("The server has disconnected !!\n");
                    button_connect.Enabled = true;
                    button_disconnect.Enabled = false;
                    button_send.Enabled = false;
                    button_allposts.Enabled = false;
                    textBox_post.Enabled = false;
                    textBox_post.Clear();

                    button_addf.Enabled = false;
                    button_removef.Enabled = false;
                    button_showf.Enabled = false;
                    textBox_firendUsername.Enabled = false;

                    button_friendsPosts.Enabled = false;
                    button_myPosts.Enabled = false;

                    button_deletePost.Enabled = false;
                    textBox_postID.Enabled = false;


                }

                clientSocket.Close();
                connected = false;
            }

            button_connect.Enabled = true;
            button_disconnect.Enabled = false;
            button_send.Enabled = false;
            button_allposts.Enabled = false;
            textBox_post.Enabled = false;
            textBox_post.Clear();

            button_addf.Enabled = false;
            button_removef.Enabled = false;
            button_showf.Enabled = false;
            textBox_firendUsername.Enabled = false;

            button_friendsPosts.Enabled = false;
            button_myPosts.Enabled = false;

            button_deletePost.Enabled = false;
            textBox_postID.Enabled = false;


            connected = false;
            terminating = true;
            clientSocket.Disconnect(true);

            clientlogs.AppendText("Successfuly disconnected!\n");
        }

        private void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[256];
                    clientSocket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Trim('\0');
                    

                    if(incomingMessage == "NOTOK")
                    {
                        clientlogs.AppendText("Please enter a valid username.\n");
                        button_connect.Enabled = true;
                        button_disconnect.Enabled = false;
                        button_send.Enabled = false;
                        button_allposts.Enabled = false;
                        textBox_post.Enabled = false;
                        textBox_post.Clear();

                        button_addf.Enabled = false;
                        button_removef.Enabled = false;
                        button_showf.Enabled = false;
                        textBox_firendUsername.Enabled = false;

                        button_friendsPosts.Enabled = false;
                        button_myPosts.Enabled = false;

                        button_deletePost.Enabled = false;
                        textBox_postID.Enabled = false;


                        clientSocket.Close();
                        connected = false;


                    }

                    else if (incomingMessage == "ONLINE")
                    {
                        clientlogs.AppendText("User already online!\n");
                        button_connect.Enabled = true;
                        button_disconnect.Enabled = false;
                        button_send.Enabled = false;
                        button_allposts.Enabled = false;
                        textBox_post.Enabled = false;
                        textBox_post.Clear();


                        button_addf.Enabled = false;
                        button_removef.Enabled = false;
                        button_showf.Enabled = false;
                        textBox_firendUsername.Enabled = false;

                        button_friendsPosts.Enabled = false;
                        button_myPosts.Enabled = false;

                        button_deletePost.Enabled = false;
                        textBox_postID.Enabled = false;


                        clientSocket.Close();
                        connected = false;
                    }

                    else if (incomingMessage == "OK")
                    {
                        clientlogs.AppendText("Hello " + textBox_username.Text + "! You are connected to the server.\n");

                        button_connect.Enabled = false;
                        button_disconnect.Enabled = true;
                        button_send.Enabled = true;
                        button_allposts.Enabled = true;
                        textBox_post.Enabled = true;


                        button_addf.Enabled = true;
                        button_removef.Enabled = true;
                        button_showf.Enabled = true;
                        textBox_firendUsername.Enabled = true;

                        button_friendsPosts.Enabled = true;
                        button_myPosts.Enabled = true;

                        button_deletePost.Enabled = true;
                        textBox_postID.Enabled = true;

                    }

                    else
                    {
                        clientlogs.AppendText(incomingMessage);
                    }



                }
                catch
                {
                    if (!terminating)
                    {
                        clientlogs.AppendText("The server has disconnected !!\n");
                        button_connect.Enabled = true;
                        button_disconnect.Enabled = false;
                        button_send.Enabled = false;
                        button_allposts.Enabled = false;
                        textBox_post.Enabled = false;
                        textBox_post.Clear();

                        button_addf.Enabled = false;
                        button_removef.Enabled = false;
                        button_showf.Enabled = false;
                        textBox_firendUsername.Enabled = false;

                        button_friendsPosts.Enabled = false;
                        button_myPosts.Enabled = false;

                        button_deletePost.Enabled = false;
                        textBox_postID.Enabled = false;

                    }

                    clientSocket.Close();
                    connected = false;
                }

            }

        }

        private void button_send_Click(object sender, EventArgs e)
        {
            string txtpost = textBox_post.Text;
            string username = textBox_username.Text;
            string post = "Post-" + username + "-" + txtpost;
            try
            {
                string message = post;
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);

                clientlogs.AppendText("You have successfully sent a post!\n");
                textBox_post.Clear();

            }
            catch
            {
                if(!terminating)
                {
                    clientlogs.AppendText("The server has disconnected !!\n");
                    button_connect.Enabled = true;
                    button_disconnect.Enabled = false;
                    button_send.Enabled = false;
                    button_allposts.Enabled = false;
                    textBox_post.Enabled = false;
                    textBox_post.Clear();

                    button_addf.Enabled = false;
                    button_removef.Enabled = false;
                    button_showf.Enabled = false;
                    textBox_firendUsername.Enabled = false;

                    button_friendsPosts.Enabled = false;
                    button_myPosts.Enabled = false;

                    button_deletePost.Enabled = false;
                    textBox_postID.Enabled = false;
                }
                clientSocket.Close();
                connected = false;
            }
        }

        private void button_allposts_Click(object sender, EventArgs e)
        {
           
            string reqMessage = "All-" + textBox_username.Text;
            try
            {
                string message = reqMessage;
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);

            }
            catch
            {
                if (!terminating)
                {
                    clientlogs.AppendText("The server has disconnected !!\n");
                    button_connect.Enabled = true;
                    button_disconnect.Enabled = false;
                    button_send.Enabled = false;
                    button_allposts.Enabled = false;
                    textBox_post.Enabled = false;
                    textBox_post.Clear();

                    button_addf.Enabled = false;
                    button_removef.Enabled = false;
                    button_showf.Enabled = false;
                    textBox_firendUsername.Enabled = false;

                    button_friendsPosts.Enabled = false;
                    button_myPosts.Enabled = false;

                    button_deletePost.Enabled = false;
                    textBox_postID.Enabled = false;

                }
                clientSocket.Close();
                connected = false;
            }
        }

        private void button_addf_Click(object sender, EventArgs e)
        {
            string reqMessage = "Addf-" + textBox_username.Text + "-" + textBox_firendUsername.Text;
            try
            {
                string message = reqMessage;
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);

                textBox_firendUsername.Clear();

            }
            catch
            {
                if (!terminating)
                {
                    clientlogs.AppendText("The server has disconnected !!\n");
                    button_connect.Enabled = true;
                    button_disconnect.Enabled = false;
                    button_send.Enabled = false;
                    button_allposts.Enabled = false;
                    textBox_post.Enabled = false;
                    textBox_post.Clear();

                    button_addf.Enabled = false;
                    button_removef.Enabled = false;
                    button_showf.Enabled = false;
                    textBox_firendUsername.Enabled = false;

                    button_friendsPosts.Enabled = false;
                    button_myPosts.Enabled = false;

                    button_deletePost.Enabled = false;
                    textBox_postID.Enabled = false;

                }
                clientSocket.Close();
                connected = false;
            }
        }

        private void button_showf_Click(object sender, EventArgs e)
        {
            string reqMessage = "Showf-" + textBox_username.Text;
            try
            {
                string message = reqMessage;
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);

            }
            catch
            {
                if (!terminating)
                {
                    clientlogs.AppendText("The server has disconnected !!\n");
                    button_connect.Enabled = true;
                    button_disconnect.Enabled = false;
                    button_send.Enabled = false;
                    button_allposts.Enabled = false;
                    textBox_post.Enabled = false;
                    textBox_post.Clear();

                    button_addf.Enabled = false;
                    button_removef.Enabled = false;
                    button_showf.Enabled = false;
                    textBox_firendUsername.Enabled = false;

                    button_friendsPosts.Enabled = false;
                    button_myPosts.Enabled = false;

                    button_deletePost.Enabled = false;
                    textBox_postID.Enabled = false;

                }
                clientSocket.Close();
                connected = false;
            }
        }

        private void button_friendsPosts_Click(object sender, EventArgs e)
        {
            string reqMessage = "FriendsPosts-" + textBox_username.Text;
            try
            {
                string message = reqMessage;
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);

            }
            catch
            {
                if (!terminating)
                {
                    clientlogs.AppendText("The server has disconnected !!\n");
                    button_connect.Enabled = true;
                    button_disconnect.Enabled = false;
                    button_send.Enabled = false;
                    button_allposts.Enabled = false;
                    textBox_post.Enabled = false;
                    textBox_post.Clear();

                    button_addf.Enabled = false;
                    button_removef.Enabled = false;
                    button_showf.Enabled = false;
                    textBox_firendUsername.Enabled = false;

                    button_friendsPosts.Enabled = false;
                    button_myPosts.Enabled = false;

                    button_deletePost.Enabled = false;
                    textBox_postID.Enabled = false;

                }
                clientSocket.Close();
                connected = false;
            }
        }

        private void button_myPosts_Click(object sender, EventArgs e)
        {
            string reqMessage = "MyPosts-" + textBox_username.Text;
            try
            {
                string message = reqMessage;
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);

            }
            catch
            {
                if (!terminating)
                {
                    clientlogs.AppendText("The server has disconnected !!\n");
                    button_connect.Enabled = true;
                    button_disconnect.Enabled = false;
                    button_send.Enabled = false;
                    button_allposts.Enabled = false;
                    textBox_post.Enabled = false;
                    textBox_post.Clear();

                    button_addf.Enabled = false;
                    button_removef.Enabled = false;
                    button_showf.Enabled = false;
                    textBox_firendUsername.Enabled = false;

                    button_friendsPosts.Enabled = false;
                    button_myPosts.Enabled = false;

                    button_deletePost.Enabled = false;
                    textBox_postID.Enabled = false;

                }
                clientSocket.Close();
                connected = false;
            }
        }

        private void button_removef_Click(object sender, EventArgs e)
        {
            string reqMessage = "Removef-" + textBox_username.Text + "-" + textBox_firendUsername.Text;
            try
            {
                string message = reqMessage;
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);

                textBox_firendUsername.Clear(); 

            }
            catch
            {
                if (!terminating)
                {
                    clientlogs.AppendText("The server has disconnected !!\n");
                    button_connect.Enabled = true;
                    button_disconnect.Enabled = false;
                    button_send.Enabled = false;
                    button_allposts.Enabled = false;
                    textBox_post.Enabled = false;
                    textBox_post.Clear();

                    button_addf.Enabled = false;
                    button_removef.Enabled = false;
                    button_showf.Enabled = false;
                    textBox_firendUsername.Enabled = false;

                    button_friendsPosts.Enabled = false;
                    button_myPosts.Enabled = false;

                    button_deletePost.Enabled = false;
                    textBox_postID.Enabled = false;

                }
                clientSocket.Close();
                connected = false;
            }
        }

        private void button_deletePost_Click(object sender, EventArgs e)
        {
            string reqMessage = "Deletep-" + textBox_username.Text + "-" + textBox_postID.Text;
            try
            {
                string message = reqMessage;
                Byte[] buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer);

                textBox_postID.Clear();

            }
            catch
            {
                if (!terminating)
                {
                    clientlogs.AppendText("The server has disconnected !!\n");
                    button_connect.Enabled = true;
                    button_disconnect.Enabled = false;
                    button_send.Enabled = false;
                    button_allposts.Enabled = false;
                    textBox_post.Enabled = false;
                    textBox_post.Clear();

                    button_addf.Enabled = false;
                    button_removef.Enabled = false;
                    button_showf.Enabled = false;
                    textBox_firendUsername.Enabled = false;

                    button_friendsPosts.Enabled = false;
                    button_myPosts.Enabled = false;

                    button_deletePost.Enabled = false;
                    textBox_postID.Enabled = false;

                }
                clientSocket.Close();
                connected = false;
            }
        }
    }
}
