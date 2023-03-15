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
    public partial class ShowAlbum : Form
    {
        NpgsqlConnection connection;
        DataSet dataSet = new DataSet();
        NpgsqlDataAdapter dataAdapter = null;
        DateTime cur_date = new DateTime(2030, 1, 1);
        DateTime upd_date = new DateTime();
        string name, 
               date, 
               information, 
               duration, 
               photo,
               copies_total,
               songs_total,
               company,
               format,
               performer,
               genre,
               language;
        string filename = "";
        bool collection;

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
                photo = openFileDialog2.FileName;
                pictureBox1.Image = Image.FromFile(photo);
            }
        }

        int id_album;
        int cur_copies;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(numericUpDown1.Value < cur_copies)
            {
                numericUpDown1.Value = cur_copies;
                MessageBox.Show("Нельзя уменьшать тираж!");
            } else
            {
                cur_copies = Convert.ToInt32(numericUpDown1.Value);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string rel_date = dateTimePicker1.Value.ToString();
            string[] dateTime1 = rel_date.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            rel_date = dateTime1[0];
            string[] dateTime2 = rel_date.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            int upd_day = Convert.ToInt32(dateTime2[0]),
                upd_month = Convert.ToInt32(dateTime2[1]),
                upd_year = Convert.ToInt32(dateTime2[2]);

            string kek = cur_date.ToString();
            string[] dateTime3 = kek.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            kek = dateTime3[0];
            string[] dateTime4 = kek.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            int cur_day = Convert.ToInt32(dateTime4[0]),
                cur_month = Convert.ToInt32(dateTime4[1]),
                cur_year = Convert.ToInt32(dateTime4[2]);

            upd_date = new DateTime(upd_year, upd_month, upd_day);

            //MessageBox.Show($"{upd_day}.{upd_month}.{upd_year} ^ {cur_day}.{cur_month}.{cur_year}");
            if ((upd_year > cur_year) || ((upd_year == cur_year) && (upd_month > cur_month)) || ((upd_year == cur_year) && (upd_month == cur_month) && (upd_day > cur_day)))
            {
                dateTimePicker1.Value = cur_date;
                MessageBox.Show("Нельзя приближать дату выпуска!");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) { textBox2.Enabled = true; } else { textBox2.Enabled = false; textBox2.Text = ""; }
        }

        bool state = false;

        private void Update_References()
        {
            string rel_date = dateTimePicker1.Value.ToString();
            string[] dateTime1 = rel_date.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            rel_date = dateTime1[0];

            string duration = dateTimePicker2.Value.ToString();
            string[] dateTime2 = duration.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            duration = dateTime2[1];

            int comp=1, form=1, perf=1, genr=1, lang=1;
            string str;
            NpgsqlDataReader reader2;
            NpgsqlCommand command;

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

            str = $"update album set name ='{textBox1.Text}', " +
                                          $"release_date = '{rel_date}', " +
                                          $"copies_total = {numericUpDown1.Value}, " +
                                          $"songs_total = {numericUpDown2.Value}, " +
                                          $"collection = {checkBox1.Checked}, " +
                                          $"album_inf = '{textBox2.Text}', " +
                                          $"duration = '{duration}', " +
                                          $"id_company = {comp}, " +
                                          $"id_format = {form}, " +
                                          $"id_performer = {perf}, " +
                                          $"id_genre = {genr}, " +
                                          $"id_language = {lang}, " +
                                          $"photo = '{photo}' " +
                                          $"where id_album = {id_album};";
            Console.WriteLine(str);
            command = new NpgsqlCommand(str, connection);
            connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception exp) { MessageBox.Show("Что-то пошло не так"); };

            connection.Close();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (state == false)
            {
                textBox1.Enabled = true;
                checkBox1.Enabled = true;
                if (checkBox1.Checked) { textBox2.Enabled = true; } else { textBox2.Enabled = false; }
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                comboBox5.Enabled = true;
                label13.Visible = false;
                dataGridView1.Visible = false;
                count_textBox.Visible = false;
                note_num_label.Visible = false;
                pageUpDown.Visible = false;
                page_label.Visible = false;
                button2.Visible = true;
                button1.Text = "Применить";
                cur_date = dateTimePicker1.Value;
                state = true;
            } else
            {
                Update_References();
                Load_DataGrid();
                textBox1.Enabled = false;
                checkBox1.Enabled = false;
                textBox2.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                label13.Visible = true;
                dataGridView1.Visible = true;
                count_textBox.Visible = true;
                note_num_label.Visible = true;
                pageUpDown.Visible = true;
                page_label.Visible = true;
                button2.Visible = false;
                button1.Text = "Изменить";
                state = false;
            }
            
            
        }

        private void pageUpDown_ValueChanged(object sender, EventArgs e)
        {
            LoadTable();
        }

        public ShowAlbum(NpgsqlConnection c, int id_data, string name_data, string date_data, string information_data, string duration_data, string photo_data,
            string copies_data, string songs_data, string company_data, string format_data, string performer_data, string genre_data, string language_data, bool collection_data)
        {
            name = name_data;
            date = date_data;
            information = information_data;
            duration = duration_data;
            photo = photo_data;
            filename = photo_data;
            copies_total = copies_data;
            cur_copies = Convert.ToInt32(copies_data);
            songs_total = songs_data;
            company = company_data;
            format = format_data;
            performer = performer_data;
            genre = genre_data;
            language = language_data;
            collection = collection_data;
            id_album = id_data;
            connection = c;
            InitializeComponent();
        }

        private void ShowAlbum_Load(object sender, EventArgs e)
        {
            LoadTable();
        }

        public void LoadReferences()
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

        public void LoadTable()
        {
            textBox1.Text = name;
            textBox2.Text = information;

            string[] dateTime1 = date.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            string[] dateTime11 = dateTime1[2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            dateTimePicker1.Value = new DateTime(Convert.ToInt32(dateTime11[0]), Convert.ToInt32(dateTime1[1]), Convert.ToInt32(dateTime1[0]));

            numericUpDown1.Value = Convert.ToInt32(copies_total);
            numericUpDown2.Value = Convert.ToInt32(songs_total);
            checkBox1.Checked = collection;

            string[] dateTime2 = duration.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            dateTimePicker2.Value = new DateTime(2021, 6, 2, Convert.ToInt32(dateTime2[0]), Convert.ToInt32(dateTime2[1]), Convert.ToInt32(dateTime2[2]));

            LoadReferences();

            comboBox1.SelectedItem = company;
            comboBox2.SelectedItem = format;
            comboBox3.SelectedItem = performer;
            comboBox4.SelectedItem = genre;
            comboBox5.SelectedItem = language;

            pictureBox1.Image = Image.FromFile(photo);

            Load_DataGrid();
        }

        private void Load_DataGrid()
        {
            string select_table = $"SELECT COUNT(*) FROM catalog WHERE catalog.id_album = {id_album}";
            Console.WriteLine(select_table);
            NpgsqlCommand count = new NpgsqlCommand(select_table, connection);
            connection.Close();
            connection.Open();
            int c = Convert.ToInt32(count.ExecuteScalar());
            int rowsnum = 0;
            count_textBox.Text = c.ToString();
            if (c == 0) { DataTable dataTable = new DataTable(); dataGridView1.DataSource = dataTable; return; }
            if (((pageUpDown.Value - 1) * 8 + 8) <= c) { rowsnum = 8; }
            else { rowsnum = 8 - (((Convert.ToInt32(pageUpDown.Value) - 1) * 8 + 8) - c); }

            string tableCommand = $"select * from select_album_catalog({id_album}, {pageUpDown.Value - 1}, {rowsnum})";
            Console.WriteLine(tableCommand);
            connection.Close();
            connection.Open();
            dataAdapter = new NpgsqlDataAdapter(tableCommand, connection);
            DataTable dataTabl = new DataTable();

            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTabl = dataSet.Tables[0];
            dataGridView1.DataSource = dataTabl;
            pageUpDown.Maximum = (Convert.ToInt32(count_textBox.Text) / 8 + 1);
            if (Convert.ToInt32(count_textBox.Text) % 8 == 0)
            {
                --pageUpDown.Maximum;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
