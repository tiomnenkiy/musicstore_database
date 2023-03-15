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
    public partial class AddReference3 : Form
    {
        NpgsqlConnection connection;
        private string reference, col, ref_ref, selected_rec="", rec_to_select;
        private int selected_rec_id;

        private void ColLabel_Click(object sender, EventArgs e)
        {

        }

        public AddReference3(NpgsqlConnection c, string data_reference, string data_col)
        {
            connection = c;
            reference = data_reference;
            col = data_col;
            InitializeComponent();
        }

        private void AddReference3_Load(object sender, EventArgs e)
        {
            switch (reference)
            {
                case "company":
                    NameLabel.Text = "Добавление\nфирмы";
                    label.Text += "название фирмы";
                    ColLabel.Text += "город";
                    ref_ref = "city";
                    rec_to_select = "город";
                    break;
                case "city":
                    NameLabel.Text = "Добавление\nгорода";
                    label.Text += "название города";
                    ColLabel.Text += "страну";
                    ref_ref = "country";
                    rec_to_select = "страну";
                    break;

            }

            String str = "select name from " + ref_ref + " order by name;";
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
            connection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected_rec = comboBox1.SelectedItem.ToString();
            String str = "select id_" + ref_ref + " from " + ref_ref + " where name = '" + selected_rec + "'";
            NpgsqlCommand command = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            NpgsqlDataReader reader2;
            reader2 = command.ExecuteReader();
            reader2.Read();
            try
            {
                selected_rec_id = reader2.GetInt32(0);
                //MessageBox.Show(Convert.ToString(selected_rec_id));
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
            connection.Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (AddTextBox.Text == "")
            {
                MessageBox.Show("Строка пуста. Введите данные!");
            }
            else if (selected_rec == "")
            {
                MessageBox.Show("Выберите " + rec_to_select);
            } 
            else
            {
                try
                {   
                    String str = "insert into " + reference + " (name, " + col + ") values ('" + AddTextBox.Text +
                                 "', " + selected_rec_id + ");";
                    //MessageBox.Show(str);
                    NpgsqlCommand command = new NpgsqlCommand(str, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    string message = "Запись [" + AddTextBox.Text + "][" + selected_rec + "] успешно добавлена!";
                    MessageBox.Show(message);
                    connection.Close();
                    AddTextBox.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Такая запись уже существует!");
                } 
            }
        }
    }
}
