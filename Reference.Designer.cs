
namespace kurs
{
    partial class Reference
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pageUpDown = new System.Windows.Forms.NumericUpDown();
            this.page_label = new System.Windows.Forms.Label();
            this.count_textBox = new System.Windows.Forms.TextBox();
            this.note_num_label = new System.Windows.Forms.Label();
            this.add_button = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.delete_button = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.RefTextBox = new System.Windows.Forms.TextBox();
            this.search_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pageUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(1066, 31);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(82, 25);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.Location = new System.Drawing.Point(14, 37);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1036, 374);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // pageUpDown
            // 
            this.pageUpDown.Location = new System.Drawing.Point(163, 492);
            this.pageUpDown.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.pageUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pageUpDown.Name = "pageUpDown";
            this.pageUpDown.Size = new System.Drawing.Size(96, 26);
            this.pageUpDown.TabIndex = 21;
            this.pageUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pageUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pageUpDown.ValueChanged += new System.EventHandler(this.pageUpDown_ValueChanged);
            // 
            // page_label
            // 
            this.page_label.AutoSize = true;
            this.page_label.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.page_label.Location = new System.Drawing.Point(81, 494);
            this.page_label.Name = "page_label";
            this.page_label.Size = new System.Drawing.Size(76, 19);
            this.page_label.TabIndex = 20;
            this.page_label.Text = "Страница";
            // 
            // count_textBox
            // 
            this.count_textBox.Location = new System.Drawing.Point(163, 438);
            this.count_textBox.Name = "count_textBox";
            this.count_textBox.ReadOnly = true;
            this.count_textBox.Size = new System.Drawing.Size(97, 26);
            this.count_textBox.TabIndex = 19;
            this.count_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.count_textBox.TextChanged += new System.EventHandler(this.count_textBox_TextChanged);
            // 
            // note_num_label
            // 
            this.note_num_label.AutoSize = true;
            this.note_num_label.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.note_num_label.Location = new System.Drawing.Point(10, 441);
            this.note_num_label.Name = "note_num_label";
            this.note_num_label.Size = new System.Drawing.Size(147, 19);
            this.note_num_label.TabIndex = 18;
            this.note_num_label.Text = "Количество записей";
            // 
            // add_button
            // 
            this.add_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_button.Location = new System.Drawing.Point(266, 439);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(240, 79);
            this.add_button.TabIndex = 17;
            this.add_button.Text = "Добавить запись";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton2.Location = new System.Drawing.Point(6, 25);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(157, 23);
            this.radioButton2.TabIndex = 13;
            this.radioButton2.Text = " Несколько записей";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // delete_button
            // 
            this.delete_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delete_button.Location = new System.Drawing.Point(573, 441);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(240, 79);
            this.delete_button.TabIndex = 22;
            this.delete_button.Text = "Удалить запись";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton1.Location = new System.Drawing.Point(6, 0);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(116, 23);
            this.radioButton1.TabIndex = 12;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = " Одну запись";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(820, 440);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 79);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton3.Location = new System.Drawing.Point(6, 50);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(106, 23);
            this.radioButton3.TabIndex = 14;
            this.radioButton3.Text = " Все записи";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // RefTextBox
            // 
            this.RefTextBox.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RefTextBox.Location = new System.Drawing.Point(393, 0);
            this.RefTextBox.Name = "RefTextBox";
            this.RefTextBox.ReadOnly = true;
            this.RefTextBox.Size = new System.Drawing.Size(268, 32);
            this.RefTextBox.TabIndex = 24;
            this.RefTextBox.Text = "Справочник";
            this.RefTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RefTextBox.TextChanged += new System.EventHandler(this.RefTextBox_TextChanged);
            // 
            // search_textBox
            // 
            this.search_textBox.Location = new System.Drawing.Point(820, 6);
            this.search_textBox.Name = "search_textBox";
            this.search_textBox.Size = new System.Drawing.Size(224, 26);
            this.search_textBox.TabIndex = 25;
            this.search_textBox.TextChanged += new System.EventHandler(this.search_textBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(752, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 21);
            this.label1.TabIndex = 26;
            this.label1.Text = "Поиск";
            // 
            // Reference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 535);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.search_textBox);
            this.Controls.Add(this.RefTextBox);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pageUpDown);
            this.Controls.Add(this.page_label);
            this.Controls.Add(this.count_textBox);
            this.Controls.Add(this.note_num_label);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Reference";
            this.Text = "Reference";
            this.Load += new System.EventHandler(this.Reference_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pageUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.NumericUpDown pageUpDown;
        private System.Windows.Forms.Label page_label;
        private System.Windows.Forms.TextBox count_textBox;
        private System.Windows.Forms.Label note_num_label;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.TextBox RefTextBox;
        private System.Windows.Forms.TextBox search_textBox;
        private System.Windows.Forms.Label label1;
    }
}