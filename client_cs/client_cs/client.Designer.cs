
namespace client_cs
{
    partial class Client
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
            this.send_button = new System.Windows.Forms.Button();
            this.chatbox = new System.Windows.Forms.ListView();
            this.typebox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // send_button
            // 
            this.send_button.Location = new System.Drawing.Point(375, 275);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(71, 47);
            this.send_button.TabIndex = 1;
            this.send_button.Text = "Send";
            this.send_button.UseVisualStyleBackColor = true;
            this.send_button.Click += new System.EventHandler(this.send_button_Click);
            // 
            // chatbox
            // 
            this.chatbox.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.chatbox.HideSelection = false;
            this.chatbox.Location = new System.Drawing.Point(12, 12);
            this.chatbox.Name = "chatbox";
            this.chatbox.Size = new System.Drawing.Size(434, 257);
            this.chatbox.TabIndex = 2;
            this.chatbox.UseCompatibleStateImageBehavior = false;
            this.chatbox.View = System.Windows.Forms.View.SmallIcon;
            // 
            // typebox
            // 
            this.typebox.AcceptsTab = true;
            this.typebox.Location = new System.Drawing.Point(12, 275);
            this.typebox.Multiline = true;
            this.typebox.Name = "typebox";
            this.typebox.Size = new System.Drawing.Size(357, 47);
            this.typebox.TabIndex = 0;
            // 
            // Client
            // 
            this.AcceptButton = this.send_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 334);
            this.Controls.Add(this.typebox);
            this.Controls.Add(this.chatbox);
            this.Controls.Add(this.send_button);
            this.Name = "Client";
            this.Text = "Client";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Client_FormClosed);
            this.Load += new System.EventHandler(this.Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button send_button;
        private System.Windows.Forms.ListView chatbox;
        private System.Windows.Forms.TextBox typebox;
    }
}