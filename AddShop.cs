using Npgsql;
using System;
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
    public partial class AddShop : Form
    {
        NpgsqlConnection connection;
        public AddShop(NpgsqlConnection c)
        {
            connection = c;
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AddCatalog_Load(object sender, EventArgs e)
        {
            String str = "select name from district order by name;";
            NpgsqlCommand command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            NpgsqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    comboBox1.Items.Add(reader.GetString(0));
                }
                catch
                {

                }
            }

            str = "select property_type from property_type order by property_type;";
            command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    comboBox2.Items.Add(reader.GetString(0));
                }
                catch
                {

                }
            }

            str = "select license_num from license order by license_num;";
            command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = command.ExecuteReader();


            while (reader.Read())
            {
                try
                {
                    comboBox3.Items.Add(reader.GetInt32(0));
                }
                catch
                {

                }
            }

            str = "select * from owner order by surname;";
            command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = command.ExecuteReader();


            while (reader.Read())
            {
                try
                {
                    comboBox4.Items.Add($"{reader.GetString(1)} {reader.GetString(2)} {reader.GetString(3)}");
                }
                catch
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string phone = maskedTextBox1.Text.Replace(" ", "");
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите название магазина!");
            }
            else if (phone.Length != 17)
            {
                MessageBox.Show("Введите номер телефона!");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Введите улицу!");
            }
            else if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите район города!");
            }
            else if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип собственности!");
            }
            else if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Выберите номер лицензии!");
            }
            else if (comboBox4.SelectedItem == null)
            {
                MessageBox.Show("Выберите владельца!");
            }
            else
            {
                string address = $"ул.{textBox2.Text}, {numericUpDown2.Value}/{numericUpDown3.Value}";
                int distr = 1, prop = 1, own = 1, lic = 1;

                NpgsqlDataReader reader2;
                NpgsqlCommand command;
                string str;

                str = "select id_district from district where name = '" + comboBox1.Text + "'";
                command = new NpgsqlCommand(str, connection);
                connection.Close();
                connection.Open();

                reader2 = command.ExecuteReader();
                reader2.Read();
                try
                {
                    distr = reader2.GetInt32(0);
                    //MessageBox.Show(Convert.ToString(selected_rec_id));
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так");
                }
                connection.Close();

                str = "select id_property_type from property_type where property_type = '" + comboBox2.Text + "'";
                command = new NpgsqlCommand(str, connection);
                connection.Close();
                connection.Open();

                reader2 = command.ExecuteReader();
                reader2.Read();
                try
                {
                    prop = reader2.GetInt32(0);
                    //MessageBox.Show(Convert.ToString(selected_rec_id));
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так");
                }
                connection.Close();



                string[] FIO = comboBox4.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string name = FIO[1], surname = FIO[0], patronymic = FIO[2];

                str = $"select id_owner from owner where name = '{name}' and surname = '{surname}' and patronymic = '{patronymic}'";
                command = new NpgsqlCommand(str, connection);
                connection.Close();
                connection.Open();

                reader2 = command.ExecuteReader();
                reader2.Read();
                try
                {
                    own = reader2.GetInt32(0);
                    //MessageBox.Show(Convert.ToString(selected_rec_id));
                }
                catch
                {
                    MessageBox.Show("Ошибка получения id_owner");
                }
                connection.Close();

                connection.Open();
                string str_gen = $"insert into shop (name, opening_year, phone, address, " +
                    $"id_district, id_property_type, license_num, id_owner) " +
                    $"values ('{textBox1.Text}', {numericUpDown1.Value}, " +
                    $"'{phone}', " +
                    $"'{address}'," +
                    $"{distr}, {prop}, {comboBox3.Text}, {own})";
                Console.WriteLine(str_gen);
                command = new NpgsqlCommand(str_gen, connection);
                try { command.ExecuteNonQuery(); MessageBox.Show("Магазин успешно добавлен"); } catch (Exception exp) { MessageBox.Show("Такой магазин уже сущетсвует"); }
            }
        }
    }
}
