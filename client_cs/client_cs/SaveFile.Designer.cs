
namespace client_cs
{
    partial class SaveFile
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
            this.savefilelb = new System.Windows.Forms.Label();
            this.filenametxb = new System.Windows.Forms.TextBox();
            this.acceptbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // savefilelb
            // 
            this.savefilelb.AutoSize = true;
            this.savefilelb.Location = new System.Drawing.Point(111, 26);
            this.savefilelb.Name = "savefilelb";
            this.savefilelb.Size = new System.Drawing.Size(259, 17);
            this.savefilelb.TabIndex = 0;
            this.savefilelb.Text = "Enter name of file you want to download";
            // 
            // filenametxb
            // 
            this.filenametxb.Location = new System.Drawing.Point(12, 63);
            this.filenametxb.Name = "filenametxb";
            this.filenametxb.Size = new System.Drawing.Size(420, 22);
            this.filenametxb.TabIndex = 1;
            this.filenametxb.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // acceptbtn
            // 
            this.acceptbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptbtn.Location = new System.Drawing.Point(461, 58);
            this.acceptbtn.Name = "acceptbtn";
            this.acceptbtn.Size = new System.Drawing.Size(132, 33);
            this.acceptbtn.TabIndex = 2;
            this.acceptbtn.Text = "Apply";
            this.acceptbtn.UseVisualStyleBackColor = true;
            // 
            // SaveFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 118);
            this.Controls.Add(this.acceptbtn);
            this.Controls.Add(this.filenametxb);
            this.Controls.Add(this.savefilelb);
            this.Name = "SaveFile";
            this.Text = "SaveFile";
            this.Load += new System.EventHandler(this.SaveFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label savefilelb;
        private System.Windows.Forms.Button acceptbtn;
        public System.Windows.Forms.TextBox filenametxb;
    }
}