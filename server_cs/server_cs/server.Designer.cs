
namespace server_cs
{
    partial class server
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
            this.chat_box = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // chat_box
            // 
            this.chat_box.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.chat_box.AllowColumnReorder = true;
            this.chat_box.AllowDrop = true;
            this.chat_box.HideSelection = false;
            this.chat_box.Location = new System.Drawing.Point(12, 12);
            this.chat_box.Name = "chat_box";
            this.chat_box.Size = new System.Drawing.Size(465, 325);
            this.chat_box.TabIndex = 0;
            this.chat_box.TileSize = new System.Drawing.Size(400, 15);
            this.chat_box.UseCompatibleStateImageBehavior = false;
            this.chat_box.View = System.Windows.Forms.View.Tile;
            // 
            // server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 349);
            this.Controls.Add(this.chat_box);
            this.Name = "server";
            this.Text = "Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView chat_box;
    }
}