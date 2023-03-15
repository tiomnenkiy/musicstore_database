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
    public partial class AddCatalog : Form
    {
        NpgsqlConnection connection;
        int[] album_id, shop_id;
        public AddCatalog(NpgsqlConnection c)
        {
            connection = c;
            InitializeComponent();
        }

        private void AddCatalog_Load(object sender, EventArgs e)
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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = $"select copies_total from album where id_album={album_id[comboBox1.SelectedIndex]};";
            NpgsqlCommand command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            NpgsqlDataReader reader;
            reader = command.ExecuteReader();
            reader.Read();
            numericUpDown2.Maximum = reader.GetInt32(0);
            numericUpDown3.Maximum = reader.GetInt32(0);
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
                string str = $"insert into catalog (id_album, id_shop, price, supplied_num, sold_num) " +
                    $"values ({album_id[comboBox1.SelectedIndex]}, {shop_id[comboBox1.SelectedIndex]}, " +
                    $"{numericUpDown1.Value}, {numericUpDown2.Value}, {numericUpDown3.Value});";
                Console.WriteLine(str);

                NpgsqlCommand command = new NpgsqlCommand(str, connection);
                connection.Close();
                connection.Open();
                
                try
                {
                    command.ExecuteNonQuery();
                    string message = "Каталог успешно добавлен!";
                    MessageBox.Show(message);
                }
                catch (Exception exp) { MessageBox.Show("Такой каталог уже сущетсвует"); };

                connection.Close();
            }
        }
    }
}
