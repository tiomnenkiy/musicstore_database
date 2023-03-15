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
    public partial class AddReference2 : Form
    {
        NpgsqlConnection connection;
        private string reference;
        public AddReference2(NpgsqlConnection c, string data_reference)
        {
            connection = c;
            reference = data_reference;
            InitializeComponent();
        }

        private void AddReference2_Load(object sender, EventArgs e)
        {
            switch (reference)
            {
                case "owner":
                    NameLabel.Text = "Добавление\nвладельца";
                    break;
                case "performer":
                    NameLabel.Text = "Добавление\nисполнителя";
                    break;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (NameTextBox.Text == "" || SurnameTextBox.Text == "" || PatronymicTextBox.Text == "")
            {
                MessageBox.Show("Вы не заполнили все данные!");
            }
            else
            {
                try
                {
                    String str = "insert into " + reference + " (name, surname, patronymic) values ('" + 
                                 NameTextBox.Text + "','" + SurnameTextBox.Text + "','" + PatronymicTextBox.Text + "');";
                    NpgsqlCommand command = new NpgsqlCommand(str, connection);
                    command.ExecuteNonQuery();
                    string message = "Запись [" + NameTextBox.Text + "][" + SurnameTextBox.Text + "][" + PatronymicTextBox.Text + "] успешно добавлена!";
                    MessageBox.Show(message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Такой человек уже сущетсвует!");
                }
            }
        }
    }
}
