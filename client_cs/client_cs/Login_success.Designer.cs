
namespace client_cs
{
    partial class Login_success
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
            this.changepass_button = new System.Windows.Forms.Button();
            this.search_button = new System.Windows.Forms.Button();
            this.setup_button = new System.Windows.Forms.Button();
            this.chat_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.userchat_textBox = new System.Windows.Forms.TextBox();
            this.quit_button = new System.Windows.Forms.Button();
            this.buttonFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // changepass_button
            // 
            this.changepass_button.Location = new System.Drawing.Point(93, 140);
            this.changepass_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.changepass_button.Name = "changepass_button";
            this.changepass_button.Size = new System.Drawing.Size(156, 28);
            this.changepass_button.TabIndex = 0;
            this.changepass_button.Text = "Change password";
            this.changepass_button.UseVisualStyleBackColor = true;
            this.changepass_button.Click += new System.EventHandler(this.changepass_button_Click);
            // 
            // search_button
            // 
            this.search_button.Location = new System.Drawing.Point(93, 191);
            this.search_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(156, 28);
            this.search_button.TabIndex = 1;
            this.search_button.Text = "Search";
            this.search_button.UseVisualStyleBackColor = true;
            // 
            // setup_button
            // 
            this.setup_button.Location = new System.Drawing.Point(93, 242);
            this.setup_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.setup_button.Name = "setup_button";
            this.setup_button.Size = new System.Drawing.Size(156, 28);
            this.setup_button.TabIndex = 2;
            this.setup_button.Text = "Setup";
            this.setup_button.UseVisualStyleBackColor = true;
            this.setup_button.Click += new System.EventHandler(this.setup_button_Click);
            // 
            // chat_button
            // 
            this.chat_button.Location = new System.Drawing.Point(93, 84);
            this.chat_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chat_button.Name = "chat_button";
            this.chat_button.Size = new System.Drawing.Size(156, 28);
            this.chat_button.TabIndex = 0;
            this.chat_button.Text = "Chat";
            this.chat_button.UseVisualStyleBackColor = true;
            this.chat_button.Click += new System.EventHandler(this.changepass_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username";
            // 
            // userchat_textBox
            // 
            this.userchat_textBox.Location = new System.Drawing.Point(144, 46);
            this.userchat_textBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userchat_textBox.Name = "userchat_textBox";
            this.userchat_textBox.Size = new System.Drawing.Size(152, 22);
            this.userchat_textBox.TabIndex = 4;
            // 
            // quit_button
            // 
            this.quit_button.Location = new System.Drawing.Point(93, 342);
            this.quit_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.quit_button.Name = "quit_button";
            this.quit_button.Size = new System.Drawing.Size(156, 28);
            this.quit_button.TabIndex = 5;
            this.quit_button.Text = "Quit";
            this.quit_button.UseVisualStyleBackColor = true;
            this.quit_button.Click += new System.EventHandler(this.quit_button_Click);
            // 
            // buttonFile
            // 
            this.buttonFile.Location = new System.Drawing.Point(93, 289);
            this.buttonFile.Name = "buttonFile";
            this.buttonFile.Size = new System.Drawing.Size(156, 32);
            this.buttonFile.TabIndex = 6;
            this.buttonFile.Text = "File";
            this.buttonFile.UseVisualStyleBackColor = true;
            this.buttonFile.Click += new System.EventHandler(this.buttonFile_Click);
            // 
            // Login_success
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 420);
            this.Controls.Add(this.buttonFile);
            this.Controls.Add(this.quit_button);
            this.Controls.Add(this.userchat_textBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.setup_button);
            this.Controls.Add(this.search_button);
            this.Controls.Add(this.chat_button);
            this.Controls.Add(this.changepass_button);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Login_success";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button changepass_button;
        private System.Windows.Forms.Button search_button;
        private System.Windows.Forms.Button setup_button;
        private System.Windows.Forms.Button chat_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userchat_textBox;
        private System.Windows.Forms.Button quit_button;
        private System.Windows.Forms.Button buttonFile;
    }
}