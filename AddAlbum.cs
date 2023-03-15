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
    public partial class AddAlbum : Form
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        NpgsqlConnection connection;
        private string reference, col, ref_ref, selected_rec = "", rec_to_select;
        private int selected_rec_id;
        string filename = "";

        string[] performer;
        public AddAlbum(NpgsqlConnection c)
        {
            connection = c;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.Multiselect = false;
            openFileDialog2.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            var state = openFileDialog2.ShowDialog();

            if (state == DialogResult.Cancel)
                return;

            if (state == DialogResult.OK)
            {
                filename = openFileDialog2.FileName;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox2.ReadOnly = false;
            } else
            {
                textBox2.Text = "";
                textBox2.ReadOnly = true;
            }
        }

        private void AddAlbum_Load(object sender, EventArgs e)
        {
            String str = "select name from company order by name;";
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
            
            str = "select type from format order by type;";
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

            str = "select * from performer order by name;";
            command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = command.ExecuteReader();
            
            
            while (reader.Read())
            {
                try
                {
                    comboBox3.Items.Add($"{reader.GetString(2)} {reader.GetString(1)} {reader.GetString(3)}");
                }
                catch
                {

                }
            }

            



            connection.Close();
            str = "select type from genre order by type;";
            command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    comboBox4.Items.Add(reader.GetString(0));
                }
                catch
                {

                }
            }
            connection.Close();
            str = "select language from language order by language;";
            command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    comboBox5.Items.Add(reader.GetString(0));
                }
                catch
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("Введите название альбома!");
            } else if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите фирму!");
            } else if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Выберите формат!");
            } else if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Выберите исполнителя!");
            } else if (comboBox4.SelectedItem == null)
            {
                MessageBox.Show("Выберите жанр!");
            } else if (comboBox5.SelectedItem == null)
            {
                MessageBox.Show("Выберите язык!");
            } else if (filename == "")
            {
                MessageBox.Show("Выберите фотографию!");
            } else
            {
                string rel_date = dateTimePicker1.Value.ToString();
                string[] dateTime1 = rel_date.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                rel_date = dateTime1[0];

                string duration = dateTimePicker2.Value.ToString();
                string[] dateTime2 = duration.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                duration = dateTime2[1];

                int comp=1, form=1, perf=1, genr=1, lang=1;

                NpgsqlDataReader reader2;
                NpgsqlCommand command;
                string str;

                str = "select id_company from company where name = '" + comboBox1.Text + "'";
                command = new NpgsqlCommand(str, connection);
                connection.Close();
                connection.Open();
                
                reader2 = command.ExecuteReader();
                reader2.Read();
                try
                {
                    comp = reader2.GetInt32(0);
                    //MessageBox.Show(Convert.ToString(selected_rec_id));
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так");
                }
                connection.Close();


                str = "select id_format from format where type = '" + comboBox2.Text + "'";
                command = new NpgsqlCommand(str, connection);
                connection.Close();
                connection.Open();

                reader2 = command.ExecuteReader();
                reader2.Read();
                try
                {
                    form = reader2.GetInt32(0);
                    //MessageBox.Show(Convert.ToString(selected_rec_id));
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так");
                }
                connection.Close();

                string[] FIO = comboBox3.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string name = FIO[1], surname = FIO[0], patronymic = FIO[2];
                str = $"select id_performer from performer where name = '{name}' and surname = '{surname}' and patronymic = '{patronymic}'";
                command = new NpgsqlCommand(str, connection);
                connection.Close();
                connection.Open();

                reader2 = command.ExecuteReader();
                reader2.Read();
                try
                {
                    perf = reader2.GetInt32(0);
                    //MessageBox.Show(Convert.ToString(selected_rec_id));
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так");
                }
                connection.Close();


                str = "select id_genre from genre where type = '" + comboBox4.Text + "'";
                command = new NpgsqlCommand(str, connection);
                connection.Close();
                connection.Open();

                reader2 = command.ExecuteReader();
                reader2.Read();
                try
                {
                    genr = reader2.GetInt32(0);
                    //MessageBox.Show(Convert.ToString(selected_rec_id));
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так");
                }
                connection.Close();


                str = "select id_language from language where language = '" + comboBox5.Text + "'";
                command = new NpgsqlCommand(str, connection);
                connection.Close();
                connection.Open();

                reader2 = command.ExecuteReader();
                reader2.Read();
                try
                {
                    lang = reader2.GetInt32(0);
                    //MessageBox.Show(Convert.ToString(selected_rec_id));
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так");
                }
                connection.Close();

                string gen_str = $"insert into album (name, release_date, copies_total, songs_total, collection, " +
                    $"album_inf, duration, id_company, id_format, id_performer, id_genre, id_language, photo) " +
                    $"values ('{textBox1.Text}', '{rel_date}', " +
                    $"{numericUpDown1.Value}, {numericUpDown2.Value}, {checkBox1.Checked}, '{textBox2.Text}', " +
                    $"'{duration}', {comp}, " +
                    $"{form}, {perf}, {genr}, {lang}, " +
                    $"'{openFileDialog2.FileName}');";
                Console.WriteLine(gen_str);
                command = new NpgsqlCommand(gen_str, connection);
                connection.Open();
                try { command.ExecuteNonQuery(); string message = "Альбом успешно добавлен!";
                    MessageBox.Show(message);
                } catch (Exception exp) { MessageBox.Show("Такой альбом уже сущетсвует"); };
                
                connection.Close();
                this.Close();
            }
        }
    }
}
