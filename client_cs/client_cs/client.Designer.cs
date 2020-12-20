
namespace client_cs
{
    partial class client
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
            this.chat_box = new System.Windows.Forms.ListView();
            this.type_box = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // send_button
            // 
            this.send_button.Location = new System.Drawing.Point(302, 248);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(75, 32);
            this.send_button.TabIndex = 0;
            this.send_button.Text = "Send";
            this.send_button.UseVisualStyleBackColor = true;
            this.send_button.Click += new System.EventHandler(this.send_button_Click);
            // 
            // chat_box
            // 
            this.chat_box.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.chat_box.HideSelection = false;
            this.chat_box.LabelWrap = false;
            this.chat_box.Location = new System.Drawing.Point(12, 12);
            this.chat_box.Name = "chat_box";
            this.chat_box.Size = new System.Drawing.Size(365, 230);
            this.chat_box.TabIndex = 0;
            this.chat_box.TileSize = new System.Drawing.Size(200, 15);
            this.chat_box.UseCompatibleStateImageBehavior = false;
            this.chat_box.View = System.Windows.Forms.View.Tile;
            // 
            // type_box
            // 
            this.type_box.AcceptsTab = true;
            this.type_box.Location = new System.Drawing.Point(13, 249);
            this.type_box.Multiline = true;
            this.type_box.Name = "type_box";
            this.type_box.Size = new System.Drawing.Size(283, 31);
            this.type_box.TabIndex = 0;
            // 
            // client
            // 
            this.AcceptButton = this.send_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 292);
            this.Controls.Add(this.type_box);
            this.Controls.Add(this.chat_box);
            this.Controls.Add(this.send_button);
            this.Name = "client";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button send_button;
        private System.Windows.Forms.ListView chat_box;
        private System.Windows.Forms.TextBox type_box;
    }
}

