
namespace kurs
{
    partial class AddReference3
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
            this.AddTextBox = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.ColLabel = new System.Windows.Forms.Label();
            this.AddButton = new System.Windows.Forms.Button();
            this.NameLabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // AddTextBox
            // 
            this.AddTextBox.Location = new System.Drawing.Point(99, 119);
            this.AddTextBox.Name = "AddTextBox";
            this.AddTextBox.Size = new System.Drawing.Size(175, 26);
            this.AddTextBox.TabIndex = 16;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label.Location = new System.Drawing.Point(95, 97);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(69, 19);
            this.label.TabIndex = 15;
            this.label.Text = "Введите ";
            // 
            // ColLabel
            // 
            this.ColLabel.AutoSize = true;
            this.ColLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ColLabel.Location = new System.Drawing.Point(95, 180);
            this.ColLabel.Name = "ColLabel";
            this.ColLabel.Size = new System.Drawing.Size(80, 19);
            this.ColLabel.TabIndex = 13;
            this.ColLabel.Text = "Выберите ";
            this.ColLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ColLabel.Click += new System.EventHandler(this.ColLabel_Click);
            // 
            // AddButton
            // 
            this.AddButton.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddButton.Location = new System.Drawing.Point(99, 291);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(176, 48);
            this.AddButton.TabIndex = 12;
            this.AddButton.Text = "Добавить";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // NameLabel
            // 
            this.NameLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameLabel.Location = new System.Drawing.Point(88, 9);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.NameLabel.Size = new System.Drawing.Size(130, 25);
            this.NameLabel.TabIndex = 11;
            this.NameLabel.Text = "Добавление ";
            this.NameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(99, 202);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(175, 27);
            this.comboBox1.TabIndex = 18;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // AddReference3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 355);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.AddTextBox);
            this.Controls.Add(this.label);
            this.Controls.Add(this.ColLabel);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.NameLabel);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "AddReference3";
            this.Load += new System.EventHandler(this.AddReference3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AddTextBox;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label ColLabel;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}