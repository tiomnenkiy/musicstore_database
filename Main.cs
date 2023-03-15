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
    public partial class Main : Form
    {
        NpgsqlConnection connection;
        int id_album, pageNum = 1;
        int SearchSort;
        string UpdateID = "ALTER SEQUENCE album_id_album_seq RESTART WITH 1" + ";" +
         "UPDATE album SET id_album = DEFAULT";
        string SearchSQL = "";
        DataSet dataSet = new DataSet();
        NpgsqlDataAdapter dataAdapter = null;
        string NotesSQL;
        string name, releaseDate, copiesTotal, songsTotal, collection, albumInf, duration, id_company, id_format, id_performer, id_genre, id_language, photo;
        string tableCommand;
        string select_ref;
        int[] album_id, shop_id;
        public Main(NpgsqlConnection c, string table)
        {
            connection = c;
            select_ref = table;
            InitializeComponent();
            switch(table)
            {
                case "shop":
                    магазиныToolStripMenuItem.Text = "Альбомы";
                    каталогToolStripMenuItem1.Text = "Каталоги";
                    поставкиToolStripMenuItem.Text = "Поставки";
                    break;
                case "album":
                    магазиныToolStripMenuItem.Text = "Магазины";
                    каталогToolStripMenuItem1.Text = "Каталоги";
                    поставкиToolStripMenuItem.Text = "Поставки";
                    break;
                case "supplies":
                    магазиныToolStripMenuItem.Text = "Магазины";
                    каталогToolStripMenuItem1.Text = "Альбомы";
                    поставкиToolStripMenuItem.Text = "Каталоги";
                    break;
                case "catalog":
                    магазиныToolStripMenuItem.Text = "Магазины";
                    каталогToolStripMenuItem1.Text = "Альбомы";
                    поставкиToolStripMenuItem.Text = "Поставки";
                    break;
            }
        }

        private void pageUpDown_ValueChanged(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void районГородаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotesSQL = "адреса";
            NotesLoad();
        }

        private void каталогToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotesSQL = "языки";
            NotesLoad();
        }

        private void районГородаToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            NotesSQL = "район города";
            NotesLoad();
        }

        private void фирмыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotesSQL = "фирмы";
            NotesLoad();
        }

        private void городаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotesSQL = "города";
            NotesLoad();
        }

        private void страныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotesSQL = "страны";
            NotesLoad();
        }

        private void жанрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotesSQL = "жанры";
            NotesLoad();
        }

        private void исполнительToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotesSQL = "исполнители";
            NotesLoad();
        }

        private void типЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotesSQL = "тип записи";
            NotesLoad();
        }

        private void владельцыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotesSQL = "владельцы";
            NotesLoad();
        }

        private void лицензияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotesSQL = "лицензии";
            NotesLoad();
        }

        private void типСобственностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotesSQL = "типы собственности";
            NotesLoad();
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            NpgsqlCommand countdel1;
            DialogResult result = new DialogResult();
            string deleteRowSQL, count;
            string deleteCount, deleteCount2;
            
            if (dataGridView.Rows.Count > 0)
            {
                DialogResult res = new DialogResult();
                if (radioButton1.Checked)
                {
                    if (select_ref == "album" || select_ref == "shop")
                    {
                        deleteCount = $"select count(*) from catalog where id_{select_ref}={dataGridView.CurrentRow.Cells[0].Value}";
                        NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                        deleteCount2 = $"select count(*) from supplies where id_{select_ref}={dataGridView.CurrentRow.Cells[0].Value}";
                        NpgsqlCommand countCommand2 = new NpgsqlCommand(deleteCount2, connection);
                        res = MessageBox.Show($"Будет удалено {countCommand.ExecuteScalar()} каталогов и {countCommand2.ExecuteScalar()} поставок.\nВы уверены?", "Предупреждение",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    else if (select_ref == "catalog" || select_ref == "supplies")
                    {
                        res = MessageBox.Show($"Вы уверены?", "Предупреждение",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }

                    if (res == DialogResult.Yes)
                    {
                        deleteRowSQL = $"delete from {select_ref} where id_{select_ref}={dataGridView.CurrentRow.Cells[0].Value}";
                        NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteRowSQL, connection);
                        deleteCommand.ExecuteNonQuery();
                        LoadTable();
                    }
                    
                }
                else if (radioButton2.Checked)
                {
                    int del_num = dataGridView.SelectedRows.Count;
                    if (select_ref == "album" || select_ref == "shop")
                    {
                        int cnum = 0, snum = 0;
                        for (int i = 0; i < del_num; ++i)
                        {
                            deleteCount = $"select count(*) from catalog where id_{select_ref}={dataGridView.CurrentRow.Cells[0].Value}";
                            NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                            deleteCount2 = $"select count(*) from supplies where id_{select_ref}={dataGridView.CurrentRow.Cells[0].Value}";
                            NpgsqlCommand countCommand2 = new NpgsqlCommand(deleteCount2, connection);
                            cnum += Convert.ToInt32(countCommand.ExecuteScalar());
                            snum += Convert.ToInt32(countCommand2.ExecuteScalar());
                        }
                        
                        res = MessageBox.Show($"Будет удалено {cnum} каталогов и {snum} поставок.\nВы уверены?", "Предупреждение",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    else if (select_ref == "catalog" || select_ref == "supplies")
                    {
                        res = MessageBox.Show($"Вы уверены?", "Предупреждение",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }

                    if (res == DialogResult.Yes)
                    {
                        for (int i = 0; i < del_num; ++i)
                        {
                            deleteRowSQL = $"delete from {select_ref} where id_{select_ref}={dataGridView.SelectedRows[i].Cells[0].Value}";
                            NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteRowSQL, connection);
                            deleteCommand.ExecuteNonQuery();
                        }
                        LoadTable();
                    }
                }
                else
                {
                    if (search_textBox.Text == "")
                    {
                        if (select_ref == "album" || select_ref == "shop")
                        {
                            deleteCount = $"select count(*) from catalog";
                            NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                            deleteCount2 = $"select count(*) from supplies";
                            NpgsqlCommand countCommand2 = new NpgsqlCommand(deleteCount2, connection);
                            res = MessageBox.Show($"Будет удалено {countCommand.ExecuteScalar()} каталогов и {countCommand2.ExecuteScalar()} поставок.\nВы уверены?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        } else
                        {

                        }

                        if (res == DialogResult.Yes)
                        {
                            deleteRowSQL = $"truncate {select_ref} cascade";
                            NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteRowSQL, connection);
                            deleteCommand.ExecuteNonQuery();
                            LoadTable();
                        }
                        
                    } else
                    {
                        if (comboBox1.SelectedItem != null)
                        {
                            int del_c = 0, del_s = 0;
                            List<int> id_to_delete = new List<int>();
                            NpgsqlDataReader reader;
                            NpgsqlCommand command = new NpgsqlCommand(tableCommand, connection);
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

                            if (select_ref == "album" || select_ref == "shop")
                            {
                                connection.Close();
                                connection.Open();
                                for (int i = 0; i < id_to_delete.Count; ++i)
                                {
                                    deleteCount = $"select count(*) from catalog where id_{select_ref}={id_to_delete[i]}";
                                    NpgsqlCommand countCommand = new NpgsqlCommand(deleteCount, connection);
                                    del_c += Convert.ToInt32(countCommand.ExecuteScalar());
                                    deleteCount2 = $"select count(*) from supplies where id_{select_ref}={id_to_delete[i]}";
                                    NpgsqlCommand countCommand2 = new NpgsqlCommand(deleteCount2, connection);
                                    del_s += Convert.ToInt32(countCommand.ExecuteScalar());
                                }
                                res = MessageBox.Show($"Будет удалено {del_c} каталогов и {del_s} поставок.\nВы уверены?", "Предупреждение",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            } else
                            {
                                res = MessageBox.Show($"Вы уверены?", "Предупреждение",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            }

                            if (res == DialogResult.Yes)
                            {
                                connection.Close();
                                connection.Open();
                                for (int i = 0; i < id_to_delete.Count; ++i)
                                {
                                    deleteRowSQL = $"delete from {select_ref} where id_{select_ref}={id_to_delete[i]}";
                                    NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteRowSQL, connection);
                                    deleteCommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Таблица пустая!");
            }

            if (dataGridView.Rows.Count == 0)
            {
                string update_counter = $"alter sequence {select_ref}_id_{select_ref}_seq restart with 1;" +
                    $"update {select_ref} set id_{select_ref} = default;";
                NpgsqlCommand update_sequence = new NpgsqlCommand(update_counter, connection);
                update_sequence.ExecuteNonQuery();
            }
            LoadTable();
        }

        private void магазиныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            switch(select_ref)
            {
                case "album":
                    new Main(connection, "shop").ShowDialog();
                    break;
                case "shop":
                    new Main(connection, "album").ShowDialog();
                    break;
                case "supplies":
                    new Main(connection, "shop").ShowDialog();
                    break;
                case "catalog":
                    new Main(connection, "shop").ShowDialog();
                    break;
            }
            Environment.Exit(0);
        }

        private void каталогToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Hide();
            switch (select_ref)
            {
                case "album":
                    new Main(connection, "catalog").ShowDialog();
                    break;
                case "shop":
                    new Main(connection, "catalog").ShowDialog();
                    break;
                case "supplies":
                    new Main(connection, "album").ShowDialog();
                    break;
                case "catalog":
                    new Main(connection, "album").ShowDialog();
                    break;
            }
            Environment.Exit(0);
        }

        private void поставкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            switch (select_ref)
            {
                case "album":
                    new Main(connection, "supplies").ShowDialog();
                    break;
                case "shop":
                    new Main(connection, "supplies").ShowDialog();
                    break;
                case "supplies":
                    new Main(connection, "catalog").ShowDialog();
                    break;
                case "catalog":
                    new Main(connection, "supplies").ShowDialog();
                    break;
            }
            Environment.Exit(0);
        }

        private void управлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            new Generation(connection).ShowDialog();
            LoadTable();
            Show();
        }

        private void очиститьБазуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string deleteRowSQL = "", Update_ID = "";
            NpgsqlCommand deleteCommand;
            NpgsqlCommand updateCommand;
            deleteCommand = new NpgsqlCommand(deleteRowSQL, connection);
            updateCommand = new NpgsqlCommand(Update_ID, connection);
            // no license, city, company, album, shop, supplies
            string[] tables = { "country", "genre", "performer", "format", "language", "district", "owner", "property_type" };
            for (int i = 0; i < tables.Length; ++i)
            {
                deleteRowSQL = $"truncate {tables[i]} cascade;";
                deleteCommand = new NpgsqlCommand(deleteRowSQL, connection);
                deleteCommand.ExecuteNonQuery();
            }

            deleteRowSQL = "truncate license cascade;";
            deleteCommand = new NpgsqlCommand(deleteRowSQL, connection);
            deleteCommand.ExecuteNonQuery();

            Update_ID = "ALTER SEQUENCE license_license_num_seq RESTART WITH 1" + ";" +
                        "UPDATE license SET license_num = DEFAULT";
            updateCommand = new NpgsqlCommand(Update_ID, connection);
            updateCommand.ExecuteNonQuery();

            for (int i = 0; i < tables.Length; ++i)
            {
                Update_ID = $"ALTER SEQUENCE {tables[i]}_id_{tables[i]}_seq RESTART WITH 1" + ";" +
                        $"UPDATE {tables[i]} SET id_{tables[i]} = DEFAULT";
                updateCommand = new NpgsqlCommand(Update_ID, connection);
                updateCommand.ExecuteNonQuery();
            }

            Update_ID = $"ALTER SEQUENCE city_id_city_seq RESTART WITH 1" + ";" +
                        $"UPDATE city SET id_city = DEFAULT";
            updateCommand = new NpgsqlCommand(Update_ID, connection);
            updateCommand.ExecuteNonQuery();

            Update_ID = $"ALTER SEQUENCE company_id_company_seq RESTART WITH 1" + ";" +
                        $"UPDATE company SET id_company = DEFAULT";
            updateCommand = new NpgsqlCommand(Update_ID, connection);
            updateCommand.ExecuteNonQuery();
            /*
            Update_ID = $"ALTER SEQUENCE album_id_album_seq RESTART WITH 1" + ";" +
                        $"UPDATE album SET id_album = DEFAULT";
            updateCommand = new NpgsqlCommand(Update_ID, connection);
            updateCommand.ExecuteNonQuery();

            Update_ID = $"ALTER SEQUENCE shop_id_shop_seq RESTART WITH 1" + ";" +
                        $"UPDATE shop SET id_shop = DEFAULT";
            updateCommand = new NpgsqlCommand(Update_ID, connection);
            updateCommand.ExecuteNonQuery();

            Update_ID = $"ALTER SEQUENCE supplies_id_supplies_seq RESTART WITH 1" + ";" +
                        $"UPDATE supplies SET id_supplies = DEFAULT";
            updateCommand = new NpgsqlCommand(Update_ID, connection);
            updateCommand.ExecuteNonQuery();
            */
            LoadTable();
            MessageBox.Show("Все данные из базы удалены!");
        }

        private void запросыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            new Queries(connection).ShowDialog();
            LoadTable();
            Show();
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Hide();
            if (select_ref == "album")
            {
                new AddAlbum(connection).ShowDialog();
            }
            else if (select_ref == "shop")
            {
                new AddShop(connection).ShowDialog();
            } else if (select_ref == "catalog")
            {
                new AddCatalog(connection).ShowDialog();
            } else
            {
                new AddSupply(connection).ShowDialog();
            }
           
            LoadTable();
            Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void search_textBox_TextChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите поле поиска!");
            } else
            {
                LoadTable();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Hide();
            if (select_ref == "album")
            {
                string name = Convert.ToString(dataGridView.CurrentRow.Cells[1].Value), 
                       date = Convert.ToString(dataGridView.CurrentRow.Cells[2].Value),
                       information = Convert.ToString(dataGridView.CurrentRow.Cells[6].Value),
                       duration = Convert.ToString(dataGridView.CurrentRow.Cells[7].Value),
                       photo = Convert.ToString(dataGridView.CurrentRow.Cells[13].Value),
                       copies_total = Convert.ToString(dataGridView.CurrentRow.Cells[3].Value), 
                       songs_total = Convert.ToString(dataGridView.CurrentRow.Cells[4].Value), 
                       id_company = Convert.ToString(dataGridView.CurrentRow.Cells[8].Value),
                       id_format = Convert.ToString(dataGridView.CurrentRow.Cells[9].Value),
                       id_performer = Convert.ToString(dataGridView.CurrentRow.Cells[10].Value),
                       id_genre = Convert.ToString(dataGridView.CurrentRow.Cells[11].Value), 
                       id_language = Convert.ToString(dataGridView.CurrentRow.Cells[12].Value);
                bool collection = Convert.ToBoolean(dataGridView.CurrentRow.Cells[5].Value);
                new ShowAlbum(connection, Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value), name, date, information, duration, photo, copies_total, 
                    songs_total, id_company, id_format, id_performer, id_genre, id_language, collection).ShowDialog();
            } else if (select_ref == "shop")
            {
                //new ShowCatalog(connection).ShowDialog();
            }
            
            LoadTable();
            Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                //MessageBox.Show("Вы выбрали " + radioButton.Text);

            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                //MessageBox.Show("Вы выбрали " + radioButton.Text);

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                //MessageBox.Show("Вы выбрали " + radioButton.Text);

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (select_ref == "catalog" || select_ref == "supplies")
            {
                comboBox1.Items.Add("Альбом");
                comboBox1.Items.Add("Магазин");
                if (select_ref == "catalog")
                {
                    SearchSQL = $"select cat.id_catalog as ID, a.name as Альбом, s.name as Магазин, cat.price as Цена, cat.supplied_num as Поставлено, cat.sold_num as Продано " +
                                $"from catalog cat " +
                                $"right join album a on a.id_album = cat.id_album " +
                                $"right join shop s on s.id_shop = cat.id_shop ";
                } else
                {
                    SearchSQL = $"select sup.id_supplies as ID, a.name as Альбом, s.name as Магазин, sup.arrival_date as Дата_прибытия, sup.quantity as Количество " +
                                $"from supplies sup " +
                                $"right join album a on a.id_album = sup.id_album " +
                                $"right join shop s on s.id_shop = sup.id_shop ";
                }
            }
            else if (select_ref == "shop")
            {
                comboBox1.Items.Add("Название магазина");
                comboBox1.Items.Add("Адрес");
                comboBox1.Items.Add("Район города");
                comboBox1.Items.Add("Тип собственности");
                comboBox1.Items.Add("Фамилия владельца");
                SearchSQL = $"select shop.id_shop as ID, shop.name as Название, shop.opening_year as Год_открытия, shop.phone as Телефон, shop.address as Адрес, dist.name as Район_города, pt.property_type as Тип_собственности, " +
                    $"l.expiration_date as Лицензия_выдана_до, o.surname || ' ' || o.name || ' ' || o.patronymic as Владелец " +
                    $"from shop left join district dist on (dist.id_district = shop.id_district) " +
                    $"right join property_type pt on shop.id_property_type = pt.id_property_type " +
                    $"right join license l on shop.license_num = l.license_num " +
                    $"right join owner o on shop.id_owner = o.id_owner ";
            }
            else if (select_ref == "album")
            {
                comboBox1.Items.Add("Название альбома");
                comboBox1.Items.Add("Название компании");
                comboBox1.Items.Add("Формат");
                comboBox1.Items.Add("Фамилия исполнителя");
                comboBox1.Items.Add("Жанр");
                comboBox1.Items.Add("Язык");
                SearchSQL = $"select alb.id_album as ID, alb.name as Название, alb.release_date as Дата_выпуска, alb.copies_total as Тираж, " +
                    $"alb.songs_total as Количество_песен, alb.collection as Сборник, alb.album_inf as Информация, alb.duration as Продолжительность, " +
                    $"comp.name as Название_компании, form.type as Формат, " +
                    $"perf.surname || ' ' || perf.name || ' ' || perf.patronymic as Исполнитель, genr.type as Жанр, lang.language as Язык, alb.photo as Фото " +
                    $"from album alb " +
                    $"left join company comp on(comp.id_company=alb.id_company) " +
                    $"left join format form on(form.id_format=alb.id_format) " +
                    $"left join performer perf on(perf.id_performer=alb.id_performer) " +
                    $"left join genre genr on(genr.id_genre=alb.id_genre) " +
                    $"left join language lang on(lang.id_language=alb.id_language)";
            }
            LoadTable();
        }

        string searh_ref="";
        public void LoadTable()
        {
            string select_table = "";
            if (search_textBox.Text != "" && comboBox1.SelectedItem != null)
            {
                switch (comboBox1.SelectedItem.ToString())
                {
                    case ("Альбом"):
                        if (select_ref == "catalog")
                        {
                            tableCommand = SearchSQL +
                                $"where a.name ilike '{search_textBox.Text}%' " +
                                $"or a.name ilike '%{search_textBox.Text}%'";
                            
                        } else
                        {
                            tableCommand = SearchSQL +
                                $"where a.name ilike '{search_textBox.Text}%' " +
                                $"or a.name ilike '%{search_textBox.Text}%'";
                        }
                        break;
                    case ("Магазин"):
                        if (select_ref == "catalog")
                        {
                            tableCommand = SearchSQL +
                                $"where s.name ilike '{search_textBox.Text}%' " +
                                $"or s.name ilike '%{search_textBox.Text}%'";

                        }
                        else
                        {
                            tableCommand = SearchSQL +
                                $"where s.name ilike '{search_textBox.Text}%' " +
                                $"or s.name ilike '%{search_textBox.Text}%'";
                        }
                        break;
                    case ("Название магазина"):
                        tableCommand = SearchSQL +
                            $"where shop.name ilike '{search_textBox.Text}%' " +
                            $"or shop.name ilike '%{search_textBox.Text}%'";
                        break;
                    case ("Адрес"):
                        tableCommand = SearchSQL +
                            $"where shop.address ilike '{search_textBox.Text}%' " +
                            $"or shop.address ilike '%{search_textBox.Text}%'";
                        break;
                    case ("Район города"):
                        tableCommand = SearchSQL +
                            $"where dist.name ilike '{search_textBox.Text}%' " +
                            $"or dist.name ilike '%{search_textBox.Text}%'";
                        break;
                    case ("Тип собственности"):
                        tableCommand = SearchSQL +
                            $"where pt.property_type ilike '{search_textBox.Text}%' " +
                            $"or pt.property_type ilike '%{search_textBox.Text}%'";
                        break;
                    case ("Фамилия владельца"):
                        tableCommand = SearchSQL +
                            $"where o.surname ilike '{search_textBox.Text}%' " +
                            $"or o.surname ilike '%{search_textBox.Text}%'";
                        break;
                    case ("Название альбома"):
                        tableCommand = SearchSQL +
                            $"where alb.name ilike '{search_textBox.Text}%' " +
                            $"or alb.name ilike '%{search_textBox.Text}%'";
                        break;
                    case ("Название компании"):
                        tableCommand = SearchSQL +
                            $"where comp.name ilike '{search_textBox.Text}%' " +
                            $"or comp.name ilike '%{search_textBox.Text}%'";
                        break;
                    case ("Формат"):
                        tableCommand = SearchSQL +
                            $"where form.type ilike '{search_textBox.Text}%' " +
                            $"or form.type ilike '%{search_textBox.Text}%'";
                        break;
                    case ("Фамилия исполнителя"):
                        tableCommand = SearchSQL +
                            $"where perf.surname ilike '{search_textBox.Text}%' " +
                            $"or perf.surname ilike '%{search_textBox.Text}%'";
                        break;
                    case ("Жанр"):
                        tableCommand = SearchSQL +
                            $"where genr.type ilike '{search_textBox.Text}%' " +
                            $"or genr.type ilike '%{search_textBox.Text}%'";
                        break;
                    case ("Язык"):
                        tableCommand = SearchSQL +
                            $"where lang.language ilike '{search_textBox.Text}%' " +
                            $"or lang.language ilike '%{search_textBox.Text}%'";
                        break;
                }
                select_table = $"SELECT COUNT(*) FROM ({tableCommand}) as bar";
                NpgsqlCommand count = new NpgsqlCommand(select_table, connection);
                connection.Close();
                connection.Open();
                int c = Convert.ToInt32(count.ExecuteScalar());
                count_textBox.Text = c.ToString();
                if (c == 0) { DataTable dataTable = new DataTable(); dataGridView.DataSource = dataTable; return; }
            }
            else
            {
                if (select_ref == "catalog")
                {
                    tableCommand = $"select * from select{select_ref}({pageUpDown.Value - 1})";
                    select_table = $"SELECT COUNT(*) FROM {select_ref}";
                    NpgsqlCommand count = new NpgsqlCommand(select_table, connection);
                    connection.Close();
                    connection.Open();
                    int c = Convert.ToInt32(count.ExecuteScalar());
                    count_textBox.Text = c.ToString();
                    if (c == 0) { DataTable dataTable = new DataTable(); dataGridView.DataSource = dataTable; return; }

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
                            shop_id[i] = reader.GetInt32(1);
                            ++i;
                        }
                        catch
                        {

                        }
                    }
                    tableCommand = $"select * from selectcatalog({pageUpDown.Value - 1})";

                } else
                {
                    tableCommand = $"select * from select{select_ref}({pageUpDown.Value - 1})";
                    select_table = $"SELECT COUNT(*) FROM {select_ref}";
                    NpgsqlCommand count = new NpgsqlCommand(select_table, connection);
                    connection.Close();
                    connection.Open();
                    int c = Convert.ToInt32(count.ExecuteScalar());
                    count_textBox.Text = c.ToString();
                    if (c == 0) { DataTable dataTable = new DataTable(); dataGridView.DataSource = dataTable; return; }
                }
            }
            connection.Close();
            connection.Open();
            dataAdapter = new NpgsqlDataAdapter(tableCommand, connection);
            DataTable dataTabl = new DataTable();

            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTabl = dataSet.Tables[0];
            dataGridView.DataSource = dataTabl;
            pageUpDown.Maximum = (Convert.ToInt32(count_textBox.Text) / 24) + 1;
        }

        public void NotesLoad()
        {
            Hide();
            new Reference(connection, NotesSQL).ShowDialog();
            LoadTable();
            Show();
        }
    }
}
