
namespace client_cs
{
    partial class ChangePass
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
            this.newpassword_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.oldpassword_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // changepass_button
            // 
            this.changepass_button.Location = new System.Drawing.Point(206, 143);
            this.changepass_button.Name = "changepass_button";
            this.changepass_button.Size = new System.Drawing.Size(94, 32);
            this.changepass_button.TabIndex = 18;
            this.changepass_button.Text = "Confirm";
            this.changepass_button.UseVisualStyleBackColor = true;
            this.changepass_button.Click += new System.EventHandler(this.changepass_button_Click);
            // 
            // newpassword_textBox
            // 
            this.newpassword_textBox.AcceptsTab = true;
            this.newpassword_textBox.Location = new System.Drawing.Point(186, 98);
            this.newpassword_textBox.Margin = new System.Windows.Forms.Padding(4);
            this.newpassword_textBox.Name = "newpassword_textBox";
            this.newpassword_textBox.PasswordChar = '*';
            this.newpassword_textBox.Size = new System.Drawing.Size(207, 22);
            this.newpassword_textBox.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 102);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "New Password";
            // 
            // oldpassword_textBox
            // 
            this.oldpassword_textBox.AcceptsTab = true;
            this.oldpassword_textBox.Location = new System.Drawing.Point(186, 36);
            this.oldpassword_textBox.Margin = new System.Windows.Forms.Padding(4);
            this.oldpassword_textBox.Name = "oldpassword_textBox";
            this.oldpassword_textBox.PasswordChar = '*';
            this.oldpassword_textBox.Size = new System.Drawing.Size(207, 22);
            this.oldpassword_textBox.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Old Password";
            // 
            // ChangePass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 204);
            this.Controls.Add(this.changepass_button);
            this.Controls.Add(this.newpassword_textBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.oldpassword_textBox);
            this.Controls.Add(this.label2);
            this.Name = "ChangePass";
            this.Text = "ChangePass";
            this.Load += new System.EventHandler(this.ChangePass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button changepass_button;
        private System.Windows.Forms.TextBox newpassword_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox oldpassword_textBox;
        private System.Windows.Forms.Label label2;
    }
}