﻿
namespace client_cs
{
    partial class Setup
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
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(68, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "Fullname";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.setupname_button_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(68, 93);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 40);
            this.button2.TabIndex = 1;
            this.button2.Text = "Date [birthday]";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.setupdate_button_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(68, 161);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(140, 40);
            this.button3.TabIndex = 2;
            this.button3.Text = "Note";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.setupnote_button_Click);
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 232);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Setup";
            this.Text = "Setup_info";
            this.Load += new System.EventHandler(this.Setup_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}