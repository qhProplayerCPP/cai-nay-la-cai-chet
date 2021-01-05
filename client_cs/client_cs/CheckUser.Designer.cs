
namespace client_cs
{
    partial class CheckUser
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.username_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(78, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(185, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "Find User";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(78, 145);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(185, 40);
            this.button2.TabIndex = 1;
            this.button2.Text = "Check Online";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(78, 212);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(185, 39);
            this.button3.TabIndex = 2;
            this.button3.Text = "Show Date";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(78, 277);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(185, 39);
            this.button4.TabIndex = 3;
            this.button4.Text = "Show All";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // username_textBox
            // 
            this.username_textBox.AcceptsTab = true;
            this.username_textBox.Location = new System.Drawing.Point(94, 28);
            this.username_textBox.Margin = new System.Windows.Forms.Padding(4);
            this.username_textBox.Name = "username_textBox";
            this.username_textBox.Size = new System.Drawing.Size(191, 22);
            this.username_textBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Username";
            // 
            // CheckUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 340);
            this.Controls.Add(this.username_textBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "CheckUser";
            this.Text = "CheckUser";
            this.Load += new System.EventHandler(this.CheckUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox username_textBox;
        private System.Windows.Forms.Label label1;
    }
}