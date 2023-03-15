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
    public partial class AddSupply : Form
    {
        int[] album_id, shop_id;
        int shop_year = 0, album_year = 0;
        DateTime album_date;
        NpgsqlConnection connection;
        public AddSupply(NpgsqlConnection c)
        {
            connection = c;
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = $"select release_date from album where id_album={album_id[comboBox1.SelectedIndex]};";
            NpgsqlCommand command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            NpgsqlDataReader reader;
            reader = command.ExecuteReader();
            reader.Read();
            album_date = reader.GetDateTime(0);
            album_year = album_date.Year;
            if (album_year >= shop_year)
            {
                dateTimePicker1.MinDate = album_date;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите альбом!");
            }
            else if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Выберите магазин!");
            }
            else
            {
                string str = $"insert into supplies (id_album, id_shop, arrival_date, quantity) " +
                    $"values ({album_id[comboBox1.SelectedIndex]}, {shop_id[comboBox1.SelectedIndex]}, " +
                    $"'{dateTimePicker1.Value.ToString("yyyy-MM-dd")}', {numericUpDown2.Value});";
                Console.WriteLine(str);

                NpgsqlCommand command = new NpgsqlCommand(str, connection);
                connection.Close();
                connection.Open();
                try
                {
                    command.ExecuteNonQuery(); string message = "Поставка успешно добавлена!";
                    MessageBox.Show(message);
                }
                catch (Exception exp) { MessageBox.Show("Что-то пошло не так"); };

                connection.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = $"select opening_year from shop where id_shop={shop_id[comboBox2.SelectedIndex]};";
            NpgsqlCommand command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            NpgsqlDataReader reader;
            reader = command.ExecuteReader();
            reader.Read();
            shop_year = reader.GetInt32(0);
            if (shop_year > album_year)
            {
                dateTimePicker1.MinDate = new DateTime(shop_year, 1, 1);
            }
        }

        private void AddSupply_Load(object sender, EventArgs e)
        {
            String str = "select count(*) from album;";
            NpgsqlCommand command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            NpgsqlDataReader reader;
            reader = command.ExecuteReader();
            reader.Read();
            album_id = new int[reader.GetInt32(0)];

            str = "select count(*) from shop;";
            command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            shop_id = new int[reader.GetInt32(0)];

            str = "select name, id_album from album order by name;";
            command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                try
                {
                    comboBox1.Items.Add(reader.GetString(0));
                    album_id[i] = reader.GetInt32(1);
                    ++i;
                }
                catch
                {

                }
            }

            str = "select name, id_shop from shop order by name;";
            command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = command.ExecuteReader();
            i = 0;
            while (reader.Read())
            {
                try
                {
                    comboBox2.Items.Add(reader.GetString(0));
                    shop_id[i] = reader.GetInt32(1);
                    ++i;
                }
                catch
                {

                }
            }
        }
    }
}
