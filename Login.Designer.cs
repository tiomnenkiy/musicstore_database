
namespace kurs
{
    partial class Login
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.avtorization_label = new System.Windows.Forms.Label();
            this.login_button = new System.Windows.Forms.Button();
            this.ip_label = new System.Windows.Forms.Label();
            this.login_label = new System.Windows.Forms.Label();
            this.pasword_label = new System.Windows.Forms.Label();
            this.ip_textBox = new System.Windows.Forms.TextBox();
            this.login_textBox = new System.Windows.Forms.TextBox();
            this.password_textBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(900, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 19);
            this.справкаToolStripMenuItem.Text = "Справка";
            this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
            // 
            // avtorization_label
            // 
            this.avtorization_label.AutoSize = true;
            this.avtorization_label.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.avtorization_label.Location = new System.Drawing.Point(368, 25);
            this.avtorization_label.Name = "avtorization_label";
            this.avtorization_label.Size = new System.Drawing.Size(161, 31);
            this.avtorization_label.TabIndex = 1;
            this.avtorization_label.Text = "Авторизация";
            // 
            // login_button
            // 
            this.login_button.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login_button.Location = new System.Drawing.Point(342, 436);
            this.login_button.Name = "login_button";
            this.login_button.Size = new System.Drawing.Size(214, 85);
            this.login_button.TabIndex = 2;
            this.login_button.Text = "Войти";
            this.login_button.UseVisualStyleBackColor = true;
            this.login_button.Click += new System.EventHandler(this.login_button_Click);
            // 
            // ip_label
            // 
            this.ip_label.AutoSize = true;
            this.ip_label.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ip_label.Location = new System.Drawing.Point(240, 130);
            this.ip_label.Name = "ip_label";
            this.ip_label.Size = new System.Drawing.Size(27, 21);
            this.ip_label.TabIndex = 3;
            this.ip_label.Text = "IP";
            // 
            // login_label
            // 
            this.login_label.AutoSize = true;
            this.login_label.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login_label.Location = new System.Drawing.Point(195, 241);
            this.login_label.Name = "login_label";
            this.login_label.Size = new System.Drawing.Size(61, 21);
            this.login_label.TabIndex = 4;
            this.login_label.Text = "Логин";
            // 
            // pasword_label
            // 
            this.pasword_label.AutoSize = true;
            this.pasword_label.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pasword_label.Location = new System.Drawing.Point(182, 354);
            this.pasword_label.Name = "pasword_label";
            this.pasword_label.Size = new System.Drawing.Size(69, 21);
            this.pasword_label.TabIndex = 5;
            this.pasword_label.Text = "Пароль";
            // 
            // ip_textBox
            // 
            this.ip_textBox.Location = new System.Drawing.Point(312, 134);
            this.ip_textBox.Name = "ip_textBox";
            this.ip_textBox.Size = new System.Drawing.Size(271, 26);
            this.ip_textBox.TabIndex = 6;
            this.ip_textBox.TextChanged += new System.EventHandler(this.ip_textBox_TextChanged);
            // 
            // login_textBox
            // 
            this.login_textBox.Location = new System.Drawing.Point(312, 246);
            this.login_textBox.Name = "login_textBox";
            this.login_textBox.Size = new System.Drawing.Size(271, 26);
            this.login_textBox.TabIndex = 7;
            this.login_textBox.TextChanged += new System.EventHandler(this.login_textBox_TextChanged);
            // 
            // password_textBox
            // 
            this.password_textBox.Location = new System.Drawing.Point(312, 358);
            this.password_textBox.Name = "password_textBox";
            this.password_textBox.PasswordChar = '*';
            this.password_textBox.Size = new System.Drawing.Size(271, 26);
            this.password_textBox.TabIndex = 8;
            this.password_textBox.UseSystemPasswordChar = true;
            this.password_textBox.TextChanged += new System.EventHandler(this.password_textBox_TextChanged);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 535);
            this.Controls.Add(this.password_textBox);
            this.Controls.Add(this.login_textBox);
            this.Controls.Add(this.ip_textBox);
            this.Controls.Add(this.pasword_label);
            this.Controls.Add(this.login_label);
            this.Controls.Add(this.ip_label);
            this.Controls.Add(this.login_button);
            this.Controls.Add(this.avtorization_label);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.Label avtorization_label;
        private System.Windows.Forms.Button login_button;
        private System.Windows.Forms.Label ip_label;
        private System.Windows.Forms.Label login_label;
        private System.Windows.Forms.Label pasword_label;
        private System.Windows.Forms.TextBox ip_textBox;
        private System.Windows.Forms.TextBox login_textBox;
        private System.Windows.Forms.TextBox password_textBox;
    }
}