
namespace client_cs
{
    partial class newfilename
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
            this.buttonapply = new System.Windows.Forms.Button();
            this.labelnewname = new System.Windows.Forms.Label();
            this.textboxnewname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonapply
            // 
            this.buttonapply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonapply.Location = new System.Drawing.Point(624, 29);
            this.buttonapply.Name = "buttonapply";
            this.buttonapply.Size = new System.Drawing.Size(94, 47);
            this.buttonapply.TabIndex = 2;
            this.buttonapply.Text = "Apply";
            this.buttonapply.UseVisualStyleBackColor = true;
            // 
            // labelnewname
            // 
            this.labelnewname.AutoSize = true;
            this.labelnewname.Location = new System.Drawing.Point(40, 47);
            this.labelnewname.Name = "labelnewname";
            this.labelnewname.Size = new System.Drawing.Size(102, 17);
            this.labelnewname.TabIndex = 0;
            this.labelnewname.Text = "New File Name";
            this.labelnewname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelnewname.Click += new System.EventHandler(this.labelnewname_Click);
            // 
            // textboxnewname
            // 
            this.textboxnewname.Location = new System.Drawing.Point(160, 44);
            this.textboxnewname.Name = "textboxnewname";
            this.textboxnewname.Size = new System.Drawing.Size(429, 22);
            this.textboxnewname.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "If you don\'t want to change file name, leave blank";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // newfilename
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 120);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textboxnewname);
            this.Controls.Add(this.labelnewname);
            this.Controls.Add(this.buttonapply);
            this.Name = "newfilename";
            this.Text = "newfilename";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonapply;
        private System.Windows.Forms.Label labelnewname;
        public System.Windows.Forms.TextBox textboxnewname;
        private System.Windows.Forms.Label label1;
    }
}