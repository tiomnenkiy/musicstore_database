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
    public partial class Reference : Form
    {
        NpgsqlConnection connection;
        DataSet dataSet = new DataSet();
        NpgsqlDataAdapter dataAdapter = null;
        string tableCommand;
        string reference;
        private string select_ref, select_col, search_col, select_id;
        public Reference(NpgsqlConnection c, string str)
        {
            connection = c;
            reference = str;
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Hide();
            if (select_ref == "owner" || select_ref == "performer") {
                new AddReference2(connection, select_ref).ShowDialog();
            } else if (select_ref == "company" || select_ref == "city")
            {
                new AddReference3(connection, select_ref, select_col).ShowDialog();
            } else {
                new AddReference(connection, select_ref, select_col).ShowDialog();
            }
            connection.Close();
            connection.Open();
            dataAdapter = new NpgsqlDataAdapter(tableCommand, connection);
            DataTable dataTabl = new DataTable();
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTabl = dataSet.Tables[0];
            dataGridView1.DataSource = dataTabl;
            String select_str = "SELECT COUNT(*) FROM " + select_ref;
            NpgsqlCommand count = new NpgsqlCommand(select_str, connection);
            count_textBox.Text = count.ExecuteScalar().ToString();
            pageUpDown.Maximum = (Convert.ToInt32(count_textBox.Text) / 15) + 1;
            Show();
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            string deleteRowSQL,count, deleteCount;
            DialogResult res;
            if (dataGridView1.Rows.Count > 0 )
            {
                if (radioButton1.Checked)
                {
                    if (select_ref != "license")
                    {
                        deleteRowSQL = $"delete from {select_ref} where id_{select_ref}={dataGridView1.CurrentRow.Cells[0].Value}";
                    } else
                    {
                        deleteRowSQL = $"delete from {select_ref} where {select_ref}_num={dataGridView1.CurrentRow.Cells[0].Value}";
                    }
                    
                    if (select_ref == "property_type" || select_ref == "district" || select_ref == "owner")
                    {
                        deleteCount = $"select count(*) from shop where id_{select_ref}={dataGridView1.CurrentRow.Cells[0].Value}";
                        NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                        res = MessageBox.Show($"Будет удалено {countCommand.ExecuteScalar()} магазинов.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    else if (select_ref == "country")
                    {
                        deleteCount = $"select count(*) from city where id_{select_ref}={dataGridView1.CurrentRow.Cells[0].Value}";
                        NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                        res = MessageBox.Show($"Будет удалено {countCommand.ExecuteScalar()} городов.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    else if (select_ref == "city")
                    {
                        deleteCount = $"select count(*) from company where id_{select_ref}={dataGridView1.CurrentRow.Cells[0].Value}";
                        NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                        res = MessageBox.Show($"Будет удалено {countCommand.ExecuteScalar()} компаний.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    else if (select_ref == "license")
                    {
                        deleteCount = $"select count(*) from shop where {select_ref}_num={dataGridView1.CurrentRow.Cells[0].Value}";
                        NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                        res = MessageBox.Show($"Будет удалено {countCommand.ExecuteScalar()} магазинов.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        deleteCount = $"select count(*) from album where id_{select_ref}={dataGridView1.CurrentRow.Cells[0].Value}";
                        NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                        res = MessageBox.Show($"Будет удалено {countCommand.ExecuteScalar()} альбомов.\nВы уверены?", "Предупреждение", 
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    
                    if (res == DialogResult.Yes)
                    {
                        connection.Close();
                        connection.Open();
                        NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteRowSQL, connection);
                        deleteCommand.ExecuteNonQuery();

                        LoadTable();
                    }
                }
                else if (radioButton2.Checked)
                {
                    int del_num = dataGridView1.SelectedRows.Count;
                    int del_c = 0;

                    if (select_ref == "property_type" || select_ref == "district" || select_ref == "owner")
                    {
                        for (int i = 0; i < del_num; ++i)
                        {
                            deleteCount = $"select count(*) from shop where id_{select_ref}={dataGridView1.SelectedRows[i].Cells[0].Value}";
                            NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                            del_c += Convert.ToInt32(countCommand.ExecuteScalar());
                        }

                        res = MessageBox.Show($"Будет удалено {del_c} магазинов.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    } 
                    else if (select_ref == "country")
                    {
                        for (int i = 0; i < del_num; ++i)
                        {
                            deleteCount = $"select count(*) from city where id_{select_ref}={dataGridView1.SelectedRows[i].Cells[0].Value}";
                            NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                            del_c += Convert.ToInt32(countCommand.ExecuteScalar());
                        }

                        res = MessageBox.Show($"Будет удалено {del_c} городов.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    } 
                    else if (select_ref == "city")
                    {
                        for (int i = 0; i < del_num; ++i)
                        {
                            deleteCount = $"select count(*) from company where id_{select_ref}={dataGridView1.SelectedRows[i].Cells[0].Value}";
                            NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                            del_c += Convert.ToInt32(countCommand.ExecuteScalar());
                        }

                        res = MessageBox.Show($"Будет удалено {del_c} компаний.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    else if (select_ref == "license")
                    {
                        for (int i = 0; i < del_num; ++i)
                        {
                            deleteCount = $"select count(*) from shop where {select_ref}_num={dataGridView1.SelectedRows[i].Cells[0].Value}";
                            NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                            del_c += Convert.ToInt32(countCommand.ExecuteScalar());
                        }

                        res = MessageBox.Show($"Будет удалено {del_c} магазинов.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        for (int i = 0; i < del_num; ++i)
                        {
                            deleteCount = $"select count(*) from album where id_{select_ref}={dataGridView1.SelectedRows[i].Cells[0].Value}";
                            NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                            del_c += Convert.ToInt32(countCommand.ExecuteScalar());
                        }
                        
                        res = MessageBox.Show($"Будет удалено {del_c} альбомов.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }

                    if (res == DialogResult.Yes)
                    {
                        connection.Close();
                        connection.Open();
                        if (select_ref != "license")
                        {
                            for (int i = 0; i < del_num; ++i)
                            {
                                deleteRowSQL = $"delete from {select_ref} where id_{select_ref}={dataGridView1.SelectedRows[i].Cells[0].Value}";
                                NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteRowSQL, connection);
                                deleteCommand.ExecuteNonQuery();
                            }
                        } 
                        else
                        {
                            for (int i = 0; i < del_num; ++i)
                            {
                                deleteRowSQL = $"delete from {select_ref} where {select_ref}_num={dataGridView1.SelectedRows[i].Cells[0].Value}";
                                NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteRowSQL, connection);
                                deleteCommand.ExecuteNonQuery();
                            }
                        }
                        
                        LoadTable();
                    }

                    
                }
                else
                {
                    if (search_textBox.Text == "")
                    {
                        deleteRowSQL = $"truncate {select_ref} cascade";
                        if (select_ref == "property_type" || select_ref == "district" || select_ref == "owner")
                        {
                            deleteCount = $"select count(*) from shop";
                            NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                            res = MessageBox.Show($"Будет удалено {countCommand.ExecuteScalar()} магазинов.\nВы уверены?", "Предупреждение",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else if (select_ref == "country")
                        {
                            deleteCount = $"select count(*) from city";
                            NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                            res = MessageBox.Show($"Будет удалено {countCommand.ExecuteScalar()} городов.\nВы уверены?", "Предупреждение",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else if (select_ref == "city")
                        {
                            deleteCount = $"select count(*) from company";
                            NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                            res = MessageBox.Show($"Будет удалено {countCommand.ExecuteScalar()} компаний.\nВы уверены?", "Предупреждение",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else if (select_ref == "license")
                        {
                            deleteCount = $"select count(*) from shop";
                            NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                            res = MessageBox.Show($"Будет удалено {countCommand.ExecuteScalar()} магазинов.\nВы уверены?", "Предупреждение",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            deleteCount = $"select count(*) from album";
                            NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                            res = MessageBox.Show($"Будет удалено {countCommand.ExecuteScalar()} альбомов.\nВы уверены?", "Предупреждение",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }

                        if (res == DialogResult.Yes)
                        {
                            connection.Close();
                            connection.Open();
                            NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteRowSQL, connection);
                            deleteCommand.ExecuteNonQuery();
                            LoadTable();
                        }
                        
                    } 
                    else
                    {
                        int del_c = 0;
                        List<int> id_to_delete = new List<int>();
                        deleteRowSQL = $"select id_{select_ref} from " + select_ref + " where " + search_col + " ilike '" + search_textBox.Text + "%' or "
                      + search_col + " ilike '%" + search_textBox.Text + "%'";
                        NpgsqlDataReader reader;
                        NpgsqlCommand command = new NpgsqlCommand(deleteRowSQL, connection);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            try
                            {
                                id_to_delete.Add(reader.GetInt32(0));
                            }
                            catch
                            {

                            }
                        }

                        if (select_ref == "property_type" || select_ref == "district" || select_ref == "owner")
                        {
                            for (int i = 0; i < id_to_delete.Count; ++i)
                            {
                                deleteCount = $"select count(*) from shop where id_{select_ref}={id_to_delete[i]}";
                                NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                                del_c += Convert.ToInt32(countCommand.ExecuteScalar());
                            }
                            res = MessageBox.Show($"Будет удалено {del_c} магазинов.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else if (select_ref == "country")
                        {
                            for (int i = 0; i < id_to_delete.Count; ++i)
                            {
                                deleteCount = $"select count(*) from city where id_{select_ref}={id_to_delete[i]}";
                                NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                                del_c += Convert.ToInt32(countCommand.ExecuteScalar());
                            }
                            res = MessageBox.Show($"Будет удалено {del_c} городов.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else if (select_ref == "city")
                        {
                            for (int i = 0; i < id_to_delete.Count; ++i)
                            {
                                deleteCount = $"select count(*) from company where id_{select_ref}={id_to_delete[i]}";
                                NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                                del_c += Convert.ToInt32(countCommand.ExecuteScalar());
                            }
                            res = MessageBox.Show($"Будет удалено {del_c} компаний.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else if (select_ref == "license")
                        {
                            for (int i = 0; i < id_to_delete.Count; ++i)
                            {
                                deleteCount = $"select count(*) from shop where {select_ref}_num={id_to_delete[i]}";
                                NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                                del_c += Convert.ToInt32(countCommand.ExecuteScalar());
                            }
                            res = MessageBox.Show($"Будет удалено {del_c} магазинов.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            for (int i = 0; i < id_to_delete.Count; ++i)
                            {
                                deleteCount = $"select count(*) from album where id_{select_ref}={id_to_delete[i]}";
                                NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                                del_c += Convert.ToInt32(countCommand.ExecuteScalar());
                            }
                            res = MessageBox.Show($"Будет удалено {del_c} альбомов.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }

                        if (res == DialogResult.Yes)
                        {
                            connection.Close();
                            connection.Open();
                            if (select_ref != "license")
                            {
                                for (int i = 0; i < id_to_delete.Count; ++i)
                                {
                                    deleteRowSQL = $"delete from {select_ref} where id_{select_ref}={id_to_delete[i]}";
                                    NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteRowSQL, connection);
                                    deleteCommand.ExecuteNonQuery();
                                }
                            } 
                            else
                            {
                                for (int i = 0; i < id_to_delete.Count; ++i)
                                {
                                    deleteRowSQL = $"delete from {select_ref} where {select_ref}_num={id_to_delete[i]}";
                                    NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteRowSQL, connection);
                                    deleteCommand.ExecuteNonQuery();
                                }
                            }
                            
                            LoadTable();
                        }
                    }
                    
                }
            } else
            {
                MessageBox.Show("Таблица пустая!");
            }
            
            if (dataGridView1.Rows.Count == 0)
            {
                string update_counter = $"alter sequence {select_ref}_id_{select_ref}_seq restart with 1;" +
                    $"update {select_ref} set id_{select_ref} = default;";
                NpgsqlCommand update_sequence = new NpgsqlCommand(update_counter, connection);
                update_sequence.ExecuteNonQuery();

            }
            string select_table = $"SELECT COUNT(*) FROM {select_ref}";
            NpgsqlCommand album_count = new NpgsqlCommand(select_table, connection);
            count_textBox.Text = album_count.ExecuteScalar().ToString();
        }

        private void Reference_Load(object sender, EventArgs e)
        {
            LoadTable();
        }

        public void LoadTable()
        {
            if (search_textBox.Text == "")
            {
                NpgsqlCommand count;
                switch (reference)
                {   
                    case "языки": tableCommand = $"select * from selectlanguage({pageUpDown.Value - 1})";
                        count = new NpgsqlCommand("SELECT COUNT(*) FROM language", connection);
                        count_textBox.Text = count.ExecuteScalar().ToString();
                        RefTextBox.Text = "Языки";
                        select_ref = "language";
                        select_col = "language";
                        search_col = select_col;
                        break;
                    case "типы собственности":
                        tableCommand = $"select * from selectproperty({pageUpDown.Value - 1})";
                        count = new NpgsqlCommand("SELECT COUNT(*) FROM property_type", connection);
                        count_textBox.Text = count.ExecuteScalar().ToString();
                        RefTextBox.Text = "Типы собственности";
                        select_ref = "property_type";
                        select_col = "property_type";
                        search_col = select_col;
                        break;
                    case "фирмы":
                        tableCommand = $"select * from selectcompany({pageUpDown.Value - 1})";
                        count = new NpgsqlCommand("SELECT COUNT(*) FROM company", connection);
                        count_textBox.Text = count.ExecuteScalar().ToString();
                        RefTextBox.Text = "Фирмы";
                        select_ref = "company";
                        select_col = "id_city";
                        search_col = "name";
                        break;
                    case "район города":
                        tableCommand = $"select * from selectdistrict({pageUpDown.Value - 1})";
                        count = new NpgsqlCommand("SELECT COUNT(*) FROM district", connection);
                        count_textBox.Text = count.ExecuteScalar().ToString();
                        RefTextBox.Text = "Районы города";
                        select_ref = "district";
                        select_col = "name";
                        search_col = select_col;
                        break;
                    case "города":
                        tableCommand = $"select * from selectcity({pageUpDown.Value - 1})";
                        count = new NpgsqlCommand("SELECT COUNT(*) FROM city", connection);
                        count_textBox.Text = count.ExecuteScalar().ToString();
                        RefTextBox.Text = "Города";
                        select_ref = "city";
                        select_col = "id_country";
                        search_col = "name";
                        break;
                    case "страны":
                        tableCommand = $"select * from selectcountry({pageUpDown.Value - 1})";
                        count = new NpgsqlCommand("SELECT COUNT(*) FROM country", connection);
                        count_textBox.Text = count.ExecuteScalar().ToString();
                        RefTextBox.Text = "Страны";
                        select_ref = "country";
                        select_col = "name";
                        search_col = select_col;
                        break;
                    case "жанры":
                        tableCommand = $"select * from selectgenre({pageUpDown.Value - 1})";
                        count = new NpgsqlCommand("SELECT COUNT(*) FROM genre", connection);
                        count_textBox.Text = count.ExecuteScalar().ToString();
                        RefTextBox.Text = "Жанры";
                        select_ref = "genre";
                        select_col = "type";
                        search_col = select_col;
                        break;
                    case "исполнители":
                        tableCommand = $"select * from selectperformer({pageUpDown.Value - 1})";
                        count = new NpgsqlCommand("SELECT COUNT(*) FROM performer", connection);
                        count_textBox.Text = count.ExecuteScalar().ToString();
                        RefTextBox.Text = "Исполнители";
                        select_ref = "performer";
                        select_col = "surname";
                        search_col = select_col;
                        break;
                    case "тип записи":
                        tableCommand = $"select * from selectformat({pageUpDown.Value - 1})";
                        count = new NpgsqlCommand("SELECT COUNT(*) FROM format", connection);
                        count_textBox.Text = count.ExecuteScalar().ToString();
                        RefTextBox.Text = "Типы записи";
                        select_ref = "format";
                        select_col = "type";
                        search_col = select_col;
                        break;
                    case "лицензии":
                        tableCommand = $"select * from selectlicense({pageUpDown.Value - 1})";
                        count = new NpgsqlCommand("SELECT COUNT(*) FROM license", connection);
                        count_textBox.Text = count.ExecuteScalar().ToString();
                        RefTextBox.Text = "Лицензии";
                        select_ref = "license";
                        select_col = "license_num";
                        search_col = "expiration_date";
                        break;
                    case "владельцы":
                        tableCommand = $"select * from selectowner({pageUpDown.Value - 1})";
                        count = new NpgsqlCommand("SELECT COUNT(*) FROM owner", connection);
                        count_textBox.Text = count.ExecuteScalar().ToString();
                        RefTextBox.Text = "Владельцы";
                        select_ref = "owner";
                        select_col = "surname";
                        search_col = select_col;
                        break;

                }
            } 
            else
            {
                tableCommand = "select * from " + select_ref + " where " + search_col + " ilike '" + search_textBox.Text + "%' or "
                      + search_col + " ilike '%" + search_textBox.Text + "%'";
                string search_str = "SELECT COUNT(*) FROM " + select_ref + " where " + search_col + " ilike '" + search_textBox.Text + "%' or "
                             + search_col + " ilike '%" + search_textBox.Text + "%'";
                NpgsqlCommand count = new NpgsqlCommand(search_str, connection);
                count_textBox.Text = count.ExecuteScalar().ToString();
            }
            dataAdapter = new NpgsqlDataAdapter(tableCommand, connection);
            DataTable dataTabl = new DataTable();

            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTabl = dataSet.Tables[0];
            dataGridView1.DataSource = dataTabl;
            pageUpDown.Maximum = (Convert.ToInt32(count_textBox.Text) / 15) + 1;
        }

        private void count_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pageUpDown_ValueChanged(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void RefTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        //поиск лицензий не работает
        private void search_textBox_TextChanged(object sender, EventArgs e)
        {
            String str="", search_str="";
            if (select_ref != "license")
            {
                str = "select * from " + select_ref + " where " + search_col + " ilike '" + search_textBox.Text + "%' or "
                      + search_col + " ilike '%" + search_textBox.Text + "%'";
                search_str = "SELECT COUNT(*) FROM " + select_ref + " where " + search_col + " ilike '" + search_textBox.Text + "%' or "
                             + search_col + " ilike '%" + search_textBox.Text + "%'";
            }
            else // не работает
            {
                str = "select * from " + select_ref + " where " + search_col + " ilike '" + search_textBox.Text + "%' or "
                      + search_col + " ilike '%" + search_textBox.Text + "%'";
                search_str = "SELECT COUNT(*) FROM " + select_ref + " where " + search_col + " ilike '" + search_textBox.Text + "%' or "
                             + search_col + " ilike '%" + search_textBox.Text + "%'";
            }
            
            NpgsqlCommand search_count = new NpgsqlCommand(search_str, connection);
            count_textBox.Text = search_count.ExecuteScalar().ToString();
            dataAdapter = new NpgsqlDataAdapter(str, connection);
            DataTable dataTabl = new DataTable();

            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTabl = dataSet.Tables[0];
            dataGridView1.DataSource = dataTabl;
            pageUpDown.Maximum = (Convert.ToInt32(count_textBox.Text) / 15) + 1;
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            
        }
    }
}
