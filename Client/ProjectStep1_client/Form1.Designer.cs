namespace ProjectStep1_client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_post = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.clientlogs = new System.Windows.Forms.RichTextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.button_send = new System.Windows.Forms.Button();
            this.button_allposts = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_firendUsername = new System.Windows.Forms.TextBox();
            this.button_addf = new System.Windows.Forms.Button();
            this.button_removef = new System.Windows.Forms.Button();
            this.button_showf = new System.Windows.Forms.Button();
            this.button_friendsPosts = new System.Windows.Forms.Button();
            this.button_myPosts = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_postID = new System.Windows.Forms.TextBox();
            this.button_deletePost = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(113, 34);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(221, 22);
            this.textBox_ip.TabIndex = 0;
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(113, 143);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(221, 22);
            this.textBox_username.TabIndex = 1;
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(113, 90);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(221, 22);
            this.textBox_port.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Username:";
            // 
            // textBox_post
            // 
            this.textBox_post.Enabled = false;
            this.textBox_post.Location = new System.Drawing.Point(95, 321);
            this.textBox_post.Name = "textBox_post";
            this.textBox_post.Size = new System.Drawing.Size(253, 22);
            this.textBox_post.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 324);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Post:";
            // 
            // clientlogs
            // 
            this.clientlogs.Location = new System.Drawing.Point(500, 34);
            this.clientlogs.Name = "clientlogs";
            this.clientlogs.ReadOnly = true;
            this.clientlogs.Size = new System.Drawing.Size(290, 413);
            this.clientlogs.TabIndex = 8;
            this.clientlogs.Text = "";
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(363, 34);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(98, 23);
            this.button_connect.TabIndex = 9;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // button_disconnect
            // 
            this.button_disconnect.Enabled = false;
            this.button_disconnect.Location = new System.Drawing.Point(363, 110);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(98, 23);
            this.button_disconnect.TabIndex = 10;
            this.button_disconnect.Text = "Disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // button_send
            // 
            this.button_send.Enabled = false;
            this.button_send.Location = new System.Drawing.Point(375, 318);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(86, 23);
            this.button_send.TabIndex = 11;
            this.button_send.Text = "Send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // button_allposts
            // 
            this.button_allposts.Enabled = false;
            this.button_allposts.Location = new System.Drawing.Point(599, 465);
            this.button_allposts.Name = "button_allposts";
            this.button_allposts.Size = new System.Drawing.Size(86, 23);
            this.button_allposts.TabIndex = 12;
            this.button_allposts.Text = "All Posts";
            this.button_allposts.UseVisualStyleBackColor = true;
            this.button_allposts.Click += new System.EventHandler(this.button_allposts_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 253);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Username:";
            // 
            // textBox_firendUsername
            // 
            this.textBox_firendUsername.Enabled = false;
            this.textBox_firendUsername.Location = new System.Drawing.Point(113, 248);
            this.textBox_firendUsername.Name = "textBox_firendUsername";
            this.textBox_firendUsername.Size = new System.Drawing.Size(221, 22);
            this.textBox_firendUsername.TabIndex = 14;
            // 
            // button_addf
            // 
            this.button_addf.Enabled = false;
            this.button_addf.Location = new System.Drawing.Point(347, 231);
            this.button_addf.Name = "button_addf";
            this.button_addf.Size = new System.Drawing.Size(129, 23);
            this.button_addf.TabIndex = 15;
            this.button_addf.Text = "Add Friend";
            this.button_addf.UseVisualStyleBackColor = true;
            this.button_addf.Click += new System.EventHandler(this.button_addf_Click);
            // 
            // button_removef
            // 
            this.button_removef.Enabled = false;
            this.button_removef.Location = new System.Drawing.Point(347, 270);
            this.button_removef.Name = "button_removef";
            this.button_removef.Size = new System.Drawing.Size(129, 23);
            this.button_removef.TabIndex = 16;
            this.button_removef.Text = "Remove Friend";
            this.button_removef.UseVisualStyleBackColor = true;
            this.button_removef.Click += new System.EventHandler(this.button_removef_Click);
            // 
            // button_showf
            // 
            this.button_showf.Enabled = false;
            this.button_showf.Location = new System.Drawing.Point(167, 197);
            this.button_showf.Name = "button_showf";
            this.button_showf.Size = new System.Drawing.Size(114, 23);
            this.button_showf.TabIndex = 17;
            this.button_showf.Text = "Show Friends";
            this.button_showf.UseVisualStyleBackColor = true;
            this.button_showf.Click += new System.EventHandler(this.button_showf_Click);
            // 
            // button_friendsPosts
            // 
            this.button_friendsPosts.Enabled = false;
            this.button_friendsPosts.Location = new System.Drawing.Point(704, 465);
            this.button_friendsPosts.Name = "button_friendsPosts";
            this.button_friendsPosts.Size = new System.Drawing.Size(108, 23);
            this.button_friendsPosts.TabIndex = 18;
            this.button_friendsPosts.Text = "Friend\'s Posts";
            this.button_friendsPosts.UseVisualStyleBackColor = true;
            this.button_friendsPosts.Click += new System.EventHandler(this.button_friendsPosts_Click);
            // 
            // button_myPosts
            // 
            this.button_myPosts.Enabled = false;
            this.button_myPosts.Location = new System.Drawing.Point(467, 465);
            this.button_myPosts.Name = "button_myPosts";
            this.button_myPosts.Size = new System.Drawing.Size(108, 23);
            this.button_myPosts.TabIndex = 19;
            this.button_myPosts.Text = "My Posts";
            this.button_myPosts.UseVisualStyleBackColor = true;
            this.button_myPosts.Click += new System.EventHandler(this.button_myPosts_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 398);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 17);
            this.label6.TabIndex = 20;
            this.label6.Text = "Post ID:";
            // 
            // textBox_postID
            // 
            this.textBox_postID.Enabled = false;
            this.textBox_postID.Location = new System.Drawing.Point(95, 395);
            this.textBox_postID.Name = "textBox_postID";
            this.textBox_postID.Size = new System.Drawing.Size(253, 22);
            this.textBox_postID.TabIndex = 21;
            // 
            // button_deletePost
            // 
            this.button_deletePost.Enabled = false;
            this.button_deletePost.Location = new System.Drawing.Point(375, 394);
            this.button_deletePost.Name = "button_deletePost";
            this.button_deletePost.Size = new System.Drawing.Size(86, 23);
            this.button_deletePost.TabIndex = 22;
            this.button_deletePost.Text = "Delete";
            this.button_deletePost.UseVisualStyleBackColor = true;
            this.button_deletePost.Click += new System.EventHandler(this.button_deletePost_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 513);
            this.Controls.Add(this.button_deletePost);
            this.Controls.Add(this.textBox_postID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button_myPosts);
            this.Controls.Add(this.button_friendsPosts);
            this.Controls.Add(this.button_showf);
            this.Controls.Add(this.button_removef);
            this.Controls.Add(this.button_addf);
            this.Controls.Add(this.textBox_firendUsername);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_allposts);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.button_disconnect);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.clientlogs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_post);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.textBox_ip);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_post;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox clientlogs;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.Button button_allposts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_firendUsername;
        private System.Windows.Forms.Button button_addf;
        private System.Windows.Forms.Button button_removef;
        private System.Windows.Forms.Button button_showf;
        private System.Windows.Forms.Button button_friendsPosts;
        private System.Windows.Forms.Button button_myPosts;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_postID;
        private System.Windows.Forms.Button button_deletePost;
    }
}

