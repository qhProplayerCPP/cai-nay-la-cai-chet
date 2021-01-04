
namespace client_cs
{
    partial class FileHandler
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
            this.uploadbutton = new System.Windows.Forms.Button();
            this.downloadbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uploadbutton
            // 
            this.uploadbutton.Location = new System.Drawing.Point(12, 21);
            this.uploadbutton.Name = "uploadbutton";
            this.uploadbutton.Size = new System.Drawing.Size(189, 56);
            this.uploadbutton.TabIndex = 0;
            this.uploadbutton.Text = "Upload";
            this.uploadbutton.UseVisualStyleBackColor = true;
            this.uploadbutton.Click += new System.EventHandler(this.uploadbutton_Click);
            // 
            // downloadbutton
            // 
            this.downloadbutton.Location = new System.Drawing.Point(248, 23);
            this.downloadbutton.Name = "downloadbutton";
            this.downloadbutton.Size = new System.Drawing.Size(182, 54);
            this.downloadbutton.TabIndex = 1;
            this.downloadbutton.Text = "Download";
            this.downloadbutton.UseVisualStyleBackColor = true;
            this.downloadbutton.Click += new System.EventHandler(this.downloadbutton_Click);
            // 
            // FileHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 99);
            this.Controls.Add(this.downloadbutton);
            this.Controls.Add(this.uploadbutton);
            this.Name = "FileHandler";
            this.Text = "FileHandler";
            this.Load += new System.EventHandler(this.FileHandler_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uploadbutton;
        private System.Windows.Forms.Button downloadbutton;
    }
}