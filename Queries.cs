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
    public partial class Queries : Form
    {
        string state = "";
        string select_table = "";
        NpgsqlConnection connection;
        DataSet dataSet = new DataSet();
        NpgsqlDataAdapter dataAdapter = null;
        string command = "";
        public Queries(NpgsqlConnection c)
        {
            connection = c;
            InitializeComponent();
        }

        private void LoadGrid()
        {
            switch(state)
            {
                case ("special3"):
                    command = $"select id_performer, count(*) as alb_num, sum(cat.price*cat.sold_num) as profit " +
                $"from album alb left join catalog cat on (cat.id_album = alb.id_album) " +
                $"where alb.release_date between date('{dateTimePicker1.Value.ToString("yyyy/MM/dd")}') and date('{dateTimePicker2.Value.ToString("yyyy/MM/dd")}')" +
                $"group by id_performer order by id_performer " +
                $"OFFSET ({pageUpDown.Value - 1}*12)ROWS FETCH NEXT 12 ROWS ONLY;";
                    Console.WriteLine(command);
                    select_table = $"SELECT COUNT(*) FROM performer";
                    break;
                case ("special12"):
                    command = $"select type, profit from genre " +
                        $"inner join(select id_genre, sum(cat.sold_num) as profit " +
                        $"from album alb inner join catalog cat on (cat.id_album = alb.id_album) group by id_genre) as s on(genre.id_genre = s.id_genre) " +
                        $"order by profit desc " +
                        $"fetch next 3 rows only";
                    select_table = $"SELECT COUNT(*) FROM genre";
                    break;
                case ("special11"):
                    if (comboBox1.SelectedItem == null)
                    {
                        MessageBox.Show("Сначала выберите магазин!");
                        return;
                    } 
                    else
                    {
                        command = $"select type, profit from genre " +
                        $"inner join(select id_genre, sum(cat.sold_num) as profit " +
                        $"from album alb inner join catalog cat on (cat.id_album = alb.id_album and cat.id_shop = {shop_id[comboBox1.SelectedIndex]}) " +
                        $"group by id_genre) as s on(genre.id_genre = s.id_genre) " +
                        $"order by profit desc " +
                        $"fetch next 3 rows only";
                        select_table = $"SELECT COUNT(*) FROM genre";
                    }
                    break;
                case ("special22"):
                    if (comboBox2.SelectedItem == null)
                    {
                        MessageBox.Show("Сначала выберите район!"); 
                        return;
                    }
                    else
                    {
                        command = $"select g.type, count(*) as count from album alb " +
                              $"inner join (select *from catalog cat inner join shop on " +
                              $"(shop.id_shop = cat.id_shop and shop.id_district={district_id[comboBox2.SelectedIndex]})) " +
                              $"as foo on(foo.id_album = alb.id_album) " +
                              $"inner join genre g on(g.id_genre = alb.id_genre) " +
                              $"group by g.id_genre order by g.id_genre";
                        select_table = $"SELECT COUNT(*) FROM genre";
                    }
                    break;
                case ("special21"):
                    if (comboBox1.SelectedItem == null)
                    {
                        MessageBox.Show("Сначала выберите магазин!");
                        return;
                    }
                    else
                    {
                        command = $"select g.type, count(*) as count from album alb " +
                              $"inner join (select *from catalog cat inner join shop on " +
                              $"(shop.id_shop = cat.id_shop and shop.id_shop={shop_id[comboBox1.SelectedIndex]})) " +
                              $"as foo on(foo.id_album = alb.id_album) " +
                              $"inner join genre g on(g.id_genre = alb.id_genre) " +
                              $"group by g.id_genre order by g.id_genre";
                        select_table = $"SELECT COUNT(*) FROM genre";
                    }
                    break;
                case ("SV1"):
                    if (comboBox3.SelectedItem == null)
                    {
                        MessageBox.Show("Сначала выберите страну!");
                        return;
                    } 
                    else
                    {
                        command = $"select comp.name as Компания, city.name as Город, foo.name as Страна from company comp " +
                              $"inner join city on(city.id_city = comp.id_city) " +
                              $"inner join (select * from country where id_country={country_id[comboBox3.SelectedIndex]}) as foo on(foo.id_country = city.id_country)";
                        select_table = $"SELECT COUNT(*) FROM (select comp.name, city.name, foo.name from company comp " +
                                  $"inner join city on(city.id_city = comp.id_city)" +
                                  $"inner join (select * from country where id_country={country_id[comboBox3.SelectedIndex]}) as foo on(foo.id_country = city.id_country)) as bar";
                    }
                    break;
                case ("SV2"):
                    if (comboBox4.SelectedItem == null)
                    {
                        MessageBox.Show("Сначала выберите тип собственности!");
                        return;
                    }
                    else
                    {
                        command = $"select shop.name as Магазин, pt.property_type as Тип from shop " +
                              $"inner join property_type pt on(pt.id_property_type = shop.id_property_type and pt.id_property_type={type_id[comboBox4.SelectedIndex]})";
                        select_table = $"SELECT COUNT(*) FROM (select shop.name, pt.property_type from shop " +
                                  $"inner join property_type pt on(pt.id_property_type = shop.id_property_type and pt.id_property_type={type_id[comboBox4.SelectedIndex]})) as bar";
                    }
                    break;
                case ("SVD1"):
                    command = $"select alb.name as Альбом, alb.release_date as Год, l.language as Язык from album alb " +
                        $"inner join language l on(l.id_language = alb.id_language) " +
                        $"where alb.release_date between date('{dateTimePicker1.Value.ToString("yyyy/MM/dd")}') and date('{dateTimePicker2.Value.ToString("yyyy/MM/dd")}')";
                    select_table = $"SELECT COUNT(*) FROM (select alb.name as Альбом, alb.release_date as Год, l.language as Язык from album alb " +
                        $"inner join language l on(l.id_language = alb.id_language) " +
                        $"where alb.release_date between date('{dateTimePicker1.Value.ToString("yyyy/MM/dd")}') and date('{dateTimePicker2.Value.ToString("yyyy/MM/dd")}')) as bar";
                    break;
                case ("SVD2"):
                    command = $"select shop.name as Магазин, d.name as Район from shop " +
                        $"inner join district d on(d.id_district = shop.id_district) " +
                        $"where shop.opening_year between 1900 and 1950";
                    select_table = $"SELECT COUNT(*) FROM (select shop.name as Магазин, d.name as Район from shop " +
                        $"inner join district d on(d.id_district = shop.id_district) " +
                        $"where shop.opening_year between 1900 and 1950) as bar";
                    break;
                case ("alb"):
                    command = $"select * from selectalbum({pageUpDown.Value - 1})";
                    select_table = $"select count(*) from album";
                    break;
                case ("shop"):
                    command = $"select * from selectshop({pageUpDown.Value - 1})";
                    select_table = $"select count(*) from shop";
                    break;
                case ("SVD3"):
                    command = $"select comp.name as Компания, city.name as Город, foo.name as Страна from company comp " +
                        $"inner join city on(city.id_city = comp.id_city) " +
                        $"inner join (select* from country) as foo on(foo.id_country = city.id_country)";
                    select_table = $"select count(*) from (select comp.name as Компания, city.name as Город, foo.name as Страна from company comp " +
                        $"inner join city on(city.id_city = comp.id_city) " +
                        $"inner join (select* from country) as foo on(foo.id_country = city.id_country)) as bar";
                    break;
                case ("left"):
                    command = $"select g.type as Тип, c.name as Компания from album a " +
                        $"left join genre g on(g.id_genre = a.id_genre) " +
                        $"left join company c on(c.id_company = a.id_company) " +
                        $"where a.songs_total > 10";
                    select_table = $"select count(*) from (select g.type, c.name from album a " +
                        $"left join genre g on(g.id_genre = a.id_genre) " +
                        $"left join company c on(c.id_company = a.id_company) " +
                        $"where a.songs_total > 10) as bar";
                    break;
                case ("right"):
                    command = $"select alb.name as Альбом, s.name as Магазин, alb.album_inf from catalog cat " +
                        $"right join album alb on(alb.id_album = cat.id_album) " +
                        $"right join shop s on(s.id_shop = cat.id_shop)";
                    select_table = $"select count(*) from (select alb.name, s.name, alb.album_inf from catalog cat " +
                        $"right join album alb on(alb.id_album = cat.id_album) " +
                        $"right join shop s on(s.id_shop = cat.id_shop)) as bar";
                    break;
                case ("ZZZ"):
                    command = $"select alb.name as Магазин, alb.copies_total as Тираж, (select min(cat.price) from catalog cat) from album alb";
                    select_table = $"select count(*) from (select alb.name, alb.copies_total, (select min(cat.price) from catalog cat) from album alb) as bar";
                    break;
                case ("ZZL"):
                    command = $"select alb.name, alb.copies_total, (select sum(cat.price) from catalog cat left join album alb on(alb.id_album=cat.id_album)) from album alb";
                    select_table = $"select count(*) from (select alb.name, alb.copies_total, (select sum(cat.price) from catalog cat left join album alb on(alb.id_album=cat.id_album)) from album alb) as bar";
                    break;
                case ("IB"):
                    command = "select count(*) from catalog";
                    select_table = "select count(*) from catalog";
                    break;
                case ("ZZ"):
                    command = "select d.name as Район, avg(A.year) from district d inner join(select opening_year as year, id_district from shop) " +
                        "as A on(d.id_district = A.id_district) group by d.name";
                    select_table = "select count(*) from (select d.name, avg(A.year) from district d inner join(select opening_year as year, id_district from shop) as A on(d.id_district = A.id_district) group by d.name) as bar";
                    break;
                case ("IPD"):
                    command = "select count(*) from catalog cat inner join(select* from shop where shop.opening_year > 2000) as s on(s.id_shop = cat.id_shop) ";
                    select_table = "select count(*) from catalog cat inner join(select* from shop where shop.opening_year > 2000) as s on(s.id_shop = cat.id_shop) ";
                    break;
                case ("IPZ"):
                    if (comboBox5.SelectedItem == null)
                    {
                        MessageBox.Show("Сначала выберите компанию!");
                        return;
                    }
                    else
                    {
                        command = $"select count(*) from catalog cat inner join album alb on(alb.id_album = cat.id_album) where alb.id_company = {comp_id[comboBox5.SelectedIndex]}";
                        select_table = $"select count(*) from catalog cat inner join album alb on(alb.id_album = cat.id_album) where alb.id_company = {comp_id[comboBox5.SelectedIndex]}";
                    }
                    break;
                case ("IPDZ"):
                    if (comboBox5.SelectedItem == null)
                    {
                        MessageBox.Show("Сначала выберите компанию!");
                        return;
                    }
                    else
                    {
                        command = $"select count(*) from catalog cat inner join album alb on(alb.id_album = cat.id_album) inner join(select* from shop where shop.opening_year > 2000) as s on(s.id_shop = cat.id_shop) where alb.id_company = {comp_id[comboBox5.SelectedIndex]}";
                        select_table = $"select count(*) from catalog cat inner join album alb on(alb.id_album = cat.id_album) inner join(select* from shop where shop.opening_year > 2000) as s on(s.id_shop = cat.id_shop) where alb.id_company = {comp_id[comboBox5.SelectedIndex]}";
                    }
                    break;

            }
                
            dataAdapter = new NpgsqlDataAdapter(command, connection);
            DataTable dataTabl = new DataTable();
            
            NpgsqlCommand count = new NpgsqlCommand(select_table, connection);
            connection.Close();
            connection.Open();
            int c = Convert.ToInt32(count.ExecuteScalar());
            count_textBox.Text = c.ToString();
            if (c == 0) {dataTabl = new DataTable(); dataGridView1.DataSource = dataTabl; return; }

            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTabl = dataSet.Tables[0];
            dataGridView1.DataSource = dataTabl;
            pageUpDown.Maximum = (Convert.ToInt32(count_textBox.Text) / 12) + 1;
        }

        private void дляКаждогоИсполнителяОпределитьКоличествоАльбомовИСуммарныйДоходПоНимВУказанномПериодеВремениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "special3";
            label1.Text = "Для каждого исполнителя определить количество альбомов и суммарный доход по ним в указанном периоде времени";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void pageUpDown_ValueChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void поВсемМагазинамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "special12";
            label1.Text = "Тройка самых топовых жанров по всем магазинам";
            pageUpDown.Value = 1;
            pageUpDown.Maximum = 1;
            LoadGrid();
        }

        private void поКаждомуМагазинуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "special11";
            label1.Text = "Тройка самых топовых жанров по каждому магазину";
            pageUpDown.Value = 1;
            pageUpDown.Maximum = 1;
            LoadGrid();
        }

        int[] shop_id, district_id, country_id, type_id, comp_id;
        private void Queries_Load(object sender, EventArgs e)
        {
            NpgsqlDataReader reader;
            string str = "select count(*) from shop;";
            NpgsqlCommand comm = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = comm.ExecuteReader();
            reader.Read();
            shop_id = new int[reader.GetInt32(0)];

            str = "select name, id_shop from shop order by id_shop;";
            comm = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = comm.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                try
                {
                    comboBox1.Items.Add(reader.GetString(0));
                    shop_id[i] = reader.GetInt32(1);
                    ++i;
                }
                catch
                {

                }
            }

            str = "select count(*) from district;";
            comm = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = comm.ExecuteReader();
            reader.Read();
            district_id = new int[reader.GetInt32(0)];

            str = "select name, id_district from district order by name;";
            comm = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = comm.ExecuteReader();
            i = 0;
            while (reader.Read())
            {
                try
                {
                    comboBox2.Items.Add(reader.GetString(0));
                    district_id[i] = reader.GetInt32(1);
                    ++i;
                }
                catch
                {

                }
            }

            str = "select count(*) from country;";
            comm = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = comm.ExecuteReader();
            reader.Read();
            country_id = new int[reader.GetInt32(0)];

            str = "select name, id_country from country order by name;";
            comm = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = comm.ExecuteReader();
            i = 0;
            while (reader.Read())
            {
                try
                {
                    //MessageBox.Show($"{reader.GetString(0)} {reader.GetInt32(1)}");
                    comboBox3.Items.Add(reader.GetString(0));
                    country_id[i] = reader.GetInt32(1);
                    ++i;
                }
                catch
                {

                }
            }

            str = "select count(*) from property_type;";
            comm = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = comm.ExecuteReader();
            reader.Read();
            type_id = new int[reader.GetInt32(0)];

            str = "select property_type, id_property_type from property_type order by property_type;";
            comm = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = comm.ExecuteReader();
            i = 0;
            while (reader.Read())
            {
                try
                {
                    comboBox4.Items.Add(reader.GetString(0));
                    type_id[i] = reader.GetInt32(1);
                    ++i;
                }
                catch
                {

                }
            }

            str = "select count(*) from company;";
            comm = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = comm.ExecuteReader();
            reader.Read();
            comp_id = new int[reader.GetInt32(0)];

            str = "select name, id_company from company order by name;";
            comm = new NpgsqlCommand(str, connection);
            connection.Close();
            connection.Open();
            reader = comm.ExecuteReader();
            i = 0;
            while (reader.Read())
            {
                try
                {
                    comboBox5.Items.Add(reader.GetString(0));
                    comp_id[i] = reader.GetInt32(1);
                    ++i;
                }
                catch
                {

                }
            }
        }

        private void поКаждомуМагазинуРайонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "special21";
            label1.Text = "Определить количество альбомов по каждому жанру исполнения по магазину";
            pageUpDown.Value = 1;
            pageUpDown.Maximum = 1;
            LoadGrid();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Hide();
            new Graphics(connection, "radial").ShowDialog();
            Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Hide();
            new Graphics(connection, "column").ShowDialog();
            Show();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Hide();
            new Graphics(connection, "3D").ShowDialog();
            Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            state = "alb";
            label1.Text = "Альбомы";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void фирмыстраныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "SV1";
            label1.Text = "Фирмы из одной страны";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void магазинытипСобственностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "SV2";
            label1.Text = "Магазины одного типа собственности";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void альбомыНаИнтервалеДатToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "SVD1";
            label1.Text = "Альбомы на интервале дат";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void магазиныНаИнтервалеДатToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "SVD2";
            label1.Text = "Магазины на интервале дат";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            state = "shop";
            label1.Text = "Магазины";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            state = "SVD3";
            label1.Text = "Общая таблица фирм, городов и стран";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void левоеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "left";
            label1.Text = "Магазин и количество поставок";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void правоеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "right";
            label1.Text = "Каталог и описание альбома";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void поПринципуЛевогоСоединенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "ZZL";
            label1.Text = "Каталог и описание альбома";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void поПринципуИтоговогоЗапросаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "ZZZ";
            label1.Text = "Альбом, тираж и средняя цена";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void безУсловияToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            state = "IB";
            label1.Text = "Всего каталогов";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void запросСПодзапросомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "ZZ";
            label1.Text = "Средний год открытия магазинов в районе";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void наДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "IPD";
            label1.Text = "Количество каталогов, магазины в которых открылись после 2000 года";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void наГруппыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "IPZ";
            label1.Text = "Количество каталогов, альбомы в которых из выбранной компании";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void наДанныеИНаГрупыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "IPDZ";
            label1.Text = "Количество каталогов, магазины в которых > 2000 года, а альбомы из выбранной компании";
            pageUpDown.Value = 1;
            LoadGrid();
        }

        private void экспортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export_Excel export = new Export_Excel();
            export.Export(dataGridView1);
        }

        private void поВсемМагазинамВЦеломToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = "special22";
            label1.Text = "Определить количество альбомов по каждому жанру исполнения по всем магазинам района";
            pageUpDown.Value = 1;
            pageUpDown.Maximum = 1;
            LoadGrid();
        }
    }
}
