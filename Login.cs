using System;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kurs
{
    public partial class Login : Form
    {
        string ip, login, password;
        NpgsqlConnection connection = null;
        bool connected;
        public Login()
        {
            InitializeComponent();
        }

        private async void login_button_Click(object sender, EventArgs e)
        {
            var connectionString = $"Host={ip};Username={login};Password={password};Database=postgres";
            connection = new NpgsqlConnection(connectionString);
            try
            {
                await connection.OpenAsync();
                connected = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Подключение к БД не выполнено. Проверьте параметры.");
            }

            if(connected==true)
            {
                Hide();
                new Main(connection, "album").ShowDialog();
                Environment.Exit(0);
            }
        }

        private void ip_textBox_TextChanged(object sender, EventArgs e)
        {
            ip = ip_textBox.Text;
        }

        private void login_textBox_TextChanged(object sender, EventArgs e)
        {
            login = login_textBox.Text;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            ip_textBox.Text = "127.0.0.1";
            login_textBox.Text = "postgres";
            password_textBox.Text = "Camus1913";
        }

        private void password_textBox_TextChanged(object sender, EventArgs e)
        {
            password = password_textBox.Text;
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программу выполнил студент ФКНТ группы ПИ19-Б Коржевич В.В.\nПредназначена для демонстрации работы базы данных музыкальных магазинов.");
        }
    }
}
