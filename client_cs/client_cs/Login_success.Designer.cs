
namespace client_cs
{
    partial class Login_success
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
            this.search_button = new System.Windows.Forms.Button();
            this.setup_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // changepass_button
            // 
            this.changepass_button.Location = new System.Drawing.Point(66, 61);
            this.changepass_button.Name = "changepass_button";
            this.changepass_button.Size = new System.Drawing.Size(117, 23);
            this.changepass_button.TabIndex = 0;
            this.changepass_button.Text = "Change password";
            this.changepass_button.UseVisualStyleBackColor = true;
            this.changepass_button.Click += new System.EventHandler(this.changepass_button_Click);
            // 
            // search_button
            // 
            this.search_button.Location = new System.Drawing.Point(66, 123);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(117, 23);
            this.search_button.TabIndex = 1;
            this.search_button.Text = "Search";
            this.search_button.UseVisualStyleBackColor = true;
            // 
            // setup_button
            // 
            this.setup_button.Location = new System.Drawing.Point(66, 186);
            this.setup_button.Name = "setup_button";
            this.setup_button.Size = new System.Drawing.Size(117, 23);
            this.setup_button.TabIndex = 2;
            this.setup_button.Text = "Setup";
            this.setup_button.UseVisualStyleBackColor = true;
            // 
            // Login_success
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 282);
            this.Controls.Add(this.setup_button);
            this.Controls.Add(this.search_button);
            this.Controls.Add(this.changepass_button);
            this.Name = "Login_success";
            this.Text = "Client";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button changepass_button;
        private System.Windows.Forms.Button search_button;
        private System.Windows.Forms.Button setup_button;
    }
}