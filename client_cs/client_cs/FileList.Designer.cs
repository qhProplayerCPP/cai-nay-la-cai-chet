
namespace client_cs
{
    partial class FileList
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
            this.label1 = new System.Windows.Forms.Label();
            this.listboxlistfile = new System.Windows.Forms.ListBox();
            this.textboxnewname = new System.Windows.Forms.TextBox();
            this.buttonsave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "File List";
            // 
            // listboxlistfile
            // 
            this.listboxlistfile.FormattingEnabled = true;
            this.listboxlistfile.ItemHeight = 16;
            this.listboxlistfile.Location = new System.Drawing.Point(27, 53);
            this.listboxlistfile.Name = "listboxlistfile";
            this.listboxlistfile.Size = new System.Drawing.Size(329, 292);
            this.listboxlistfile.TabIndex = 0;
            // 
            // textboxnewname
            // 
            this.textboxnewname.Location = new System.Drawing.Point(27, 365);
            this.textboxnewname.Name = "textboxnewname";
            this.textboxnewname.Size = new System.Drawing.Size(329, 22);
            this.textboxnewname.TabIndex = 2;
            // 
            // buttonsave
            // 
            this.buttonsave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonsave.Location = new System.Drawing.Point(235, 403);
            this.buttonsave.Name = "buttonsave";
            this.buttonsave.Size = new System.Drawing.Size(121, 35);
            this.buttonsave.TabIndex = 1;
            this.buttonsave.Text = "Apply";
            this.buttonsave.UseVisualStyleBackColor = true;
            this.buttonsave.Click += new System.EventHandler(this.buttonsave_Click);
            // 
            // FileList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 450);
            this.Controls.Add(this.buttonsave);
            this.Controls.Add(this.textboxnewname);
            this.Controls.Add(this.listboxlistfile);
            this.Controls.Add(this.label1);
            this.Name = "FileList";
            this.Text = "FileList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListBox listboxlistfile;
        public System.Windows.Forms.TextBox textboxnewname;
        private System.Windows.Forms.Button buttonsave;
    }
}