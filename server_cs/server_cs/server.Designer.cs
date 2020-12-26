
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
            this.chat_box.HideSelection = false;
            this.chat_box.LabelWrap = false;
            this.chat_box.Location = new System.Drawing.Point(16, 15);
            this.chat_box.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chat_box.Name = "chat_box";
            this.chat_box.Size = new System.Drawing.Size(516, 310);
            this.chat_box.TabIndex = 0;
            this.chat_box.TileSize = new System.Drawing.Size(300, 15);
            this.chat_box.UseCompatibleStateImageBehavior = false;
            this.chat_box.View = System.Windows.Forms.View.Tile;
            this.chat_box.SelectedIndexChanged += new System.EventHandler(this.chat_box_SelectedIndexChanged);
            // 
            // server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 341);
            this.Controls.Add(this.chat_box);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "server";
            this.Text = "Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView chat_box;
    }
}