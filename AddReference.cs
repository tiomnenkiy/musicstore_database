using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace kurs
{
    public partial class AddReference : Form
    {
        NpgsqlConnection connection;
        private string reference, col; 
        public AddReference(NpgsqlConnection c, string data_reference, string data_col)
        {
            connection = c;
            reference = data_reference;
            col = data_col;
            InitializeComponent();
        }

        private void AddReference_Load(object sender, EventArgs e)
        {
            switch (reference)
            {
                case "language":
                    NameLabel.Text = "Добавление\nязыков";
                    break;
                case "property_type":
                    NameLabel.Text = "Добавление\nтипа собственности";
                    break;
                case "district":
                    NameLabel.Text = "Добавление\nрайона города";
                    break;
                case "country":
                    NameLabel.Text = "Добавление\nстраны";
                    break;
                case "genre":
                    NameLabel.Text = "Добавление\nжанра";
                    break;
                case "format":
                    NameLabel.Text = "Добавление\nтипа записи";
                    break;
                case "license":
                    NameLabel.Text = "Добавление\nлицензии";
                    break;

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ColLabel_Click(object sender, EventArgs e)
        {

        }

        private void AddTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (AddTextBox.Text == "")
            {
                MessageBox.Show("Строка пуста. Введите данные!");
            }
            else
            {
                try
                {
                    String str = "insert into " + reference + "(" + col + ") values ('" + AddTextBox.Text + "');";
                    NpgsqlCommand command = new NpgsqlCommand(str, connection);
                    command.ExecuteNonQuery();
                    string message = "Запись [" + AddTextBox.Text + "] успешно добавлена!";
                    MessageBox.Show(message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Такая запись уже существует!");
                }
            }
        }
    }
}
