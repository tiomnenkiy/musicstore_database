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
    public partial class Generation : Form
    {
        NpgsqlConnection connection;
        NpgsqlCommand GEN_SQL = new NpgsqlCommand();
        Random rand = new Random();
        DataTable data = new DataTable();
        DataTable data1 = new DataTable();
        NpgsqlDataAdapter reader;

        static string[] country = { "Россия", "Украина", "Казахстан", "Беларусь" };
        static string[] city = new string[28] { "Москва", "Санкт-Петербург", "Сочи", "Тюмень", "Пермь", "Сыктывкар", "Екатеринбург",
                                         "Киев", "Донецк", "Львов", "Днепр", "Запорожье", "Винница", "Полтава" ,
                                         "Алматы", "Нур-Султан", "Шымкент", "Актобе", "Караганда", "Актау", "Байконур" ,
                                         "Минск", "Гродно", "Полоцк", "Витебск", "Брест", "Барановичи", "Бобруйск" };
        static string[] company = {"Геометрия", "430 East", "A&M Records", "Zen", "Berliner Gramophone", "Cardiowave", "Epic Records",
            "GOOD Music", "Holywood Records", "Impulse!", "Русское радио", "Zonata Platina", "Wu Tang", "E2/C4", "Warner Brothers", "dereck kantina", "Its Electric!", 
            "Heart&Soul Records", "Оплот", "Первый канал"};
        static string[] language = { "Русский", "Английский", "Украинский", "Французский", "Беларуский", "Испанский", "Корейский" };
        static string[] format = { "CD-DA", "SACD", "DVD-Audio", "DVD-Video", "Кассета", "Пластинка", "Цифровой альбом" };
        static string[] genre = { "Поп", "Рок", "Метал", "Джаз", "Альтернатива", "Инди", "Кей-Поп", "Бибоп", "Классика", "Электро", "Индастриал" };
        static string[] name = { "Владимир", "Владислав", "Генрих", "Альфонсо", "Денис", "Даниил", "Артемий", "Апполон", "Ярослав", "Марк", "Джим" };
        static string[] surname = { "Коржевич", "Чумарин", "Решетняк", "Овсянников", "Сыч", "Леонов", "Кулагин", "Бакалин", "Лилиев", "Лебедев", "Пучинни" };
        static string[] patronymic = { "Владимирович", "Максимович", "Александрович", "Евгеньевич", "Валерьевич", "Дмитриевич", "Анатольевич",
            "Владиславович", "Юрьевич", "Абраамович", "Галактионович" };
        static string[] district = { "Ворошиловский р-н", "Кировский р-н", "Киевский р-н", "Пролетарский р-н", "Буденовский р-н", "Мирный р-н",
            "Широкий р-н", "Калининский р-н", "Куйбышевский р-н", "Петровский р-н", "Ленинский р-н"};
        static string[] property_type = { "Государственная", "Частная", "ЗАО", "ОАО", "ООО" };
        static string[] album = { "Навсегда", " RAZZMATAZ", "xx", "Вверх", "Шум", "Starboy", "I Love You", "Woodkid", "S16", "Run Boy Run",
            "I Had The Blues But I Shook Them Loose", "Direction","Thunder","Brothers","One More Time","Around The World","Harder Better Faster Stronger",
            "Musique","Random Access Memories","Время","Strange Days","Light My Fire","PRISM","Smile^_^","One By One","The Getaway","Stadium Arcadium",
            "Californication","21","25","19",};
        static string[] shop = { "MuzzLine", "JAM", "1-й Музыкальный", "MusicStore", "SoundSmart", "7-Note", "Магазин Петра Василевского", "АбырВалг" };
        static string[] address = { "Ул.Артёма", "Ул.Пушкина", "Ул.Ленина", "Ул.Челюскинцев", "Ул.Шевченко", "Ул.Куйбышева",
            "Ул.Бакинских-Коммисаров", "Ул.Хмельницкого", "Ул.Островского" };
        static string[] inf = { "Хороший", "Плохой", "Злой", "Средний", "Крутой", "Светлый", "Легендарный", "Спорный" };
        static string[] photos = { "beatles", "cover", "demon-days", "dragons", "electronic", "floyd", "get-lucky", "hendriks", "jackdaniels",
            "kino", "muse", "nirvana", "suhoe-beloe", "trapsoul", "work", "zanuda"};

        int country_len = country.Length;
        int city_len = name.Length;
        int company_len = company.Length;
        int language_len = language.Length;
        int format_len = format.Length;
        int genre_len = genre.Length;
        int name_len = name.Length;
        int surname_len = surname.Length;
        int patronymic_len = patronymic.Length;
        int district_len = district.Length;
        int property_len = property_type.Length;
        int album_len = album.Length;
        int shop_len = shop.Length;
        int address_len = address.Length;
        int inf_len = inf.Length;
        int photos_len = photos.Length;
        public Generation(NpgsqlConnection c)
        {
            connection = c;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < country_len; ++i)
            {
                GEN_SQL = new NpgsqlCommand($"insert into country (name) values ('{country[i]}');", connection);
                Console.WriteLine($"insert into country (name) values ('{country[i]}');");
                try { GEN_SQL.ExecuteNonQuery(); } catch (Exception exp) { continue; }
            }
            Console.WriteLine("Страны сгенерированы");

            int c = 0;
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 7; ++j)
                {
                    GEN_SQL = new NpgsqlCommand($"insert into city (name, id_country) values ('{city[c]}', {i + 1});", connection);
                    Console.WriteLine($"insert into city (name) values ('{city[c]}', {i + 1});");
                    ++c;
                    try { GEN_SQL.ExecuteNonQuery(); } catch (Exception exp) { continue; }
                }

            }
            Console.WriteLine("Города сгенерированы");


            for (int i = 0; i < language_len; ++i)
            {
                GEN_SQL = new NpgsqlCommand($"insert into language (language) values ('{language[i]}');", connection);
                try { GEN_SQL.ExecuteNonQuery(); } catch (Exception exp) { continue; }
            }
            Console.WriteLine("Языки сгенерированы");


            for (int i = 0; i < format_len; ++i)
            {
                GEN_SQL = new NpgsqlCommand($"insert into format (type) values ('{format[i]}');", connection);
                try { GEN_SQL.ExecuteNonQuery(); } catch (Exception exp) { continue; }
            }
            Console.WriteLine("Форматы сгенерированы");


            for (int i = 0; i < genre_len; ++i)
            {
                GEN_SQL = new NpgsqlCommand($"insert into genre (type) values ('{genre[i]}');", connection);
                try { GEN_SQL.ExecuteNonQuery(); } catch (Exception exp) { continue; }
            }
            Console.WriteLine("Жанры сгенерированы");


            
            c = 0;
            while (c < 30)
            {
                GEN_SQL = new NpgsqlCommand($"insert into owner (name, surname, patronymic) values " +
                    $"('{name[rand.Next(0, name_len)]}', '{surname[rand.Next(0, surname_len)]}', '{patronymic[rand.Next(0, patronymic_len)]}');", connection);
                try { GEN_SQL.ExecuteNonQuery(); ++c; } catch (Exception exp) { continue; }
            }
            Console.WriteLine("Владельцы сгенерированы");


            c = 0;
            while (c < 30)
            {
                GEN_SQL = new NpgsqlCommand($"insert into performer (name, surname, patronymic) values " +
                    $"('{name[rand.Next(0, name_len)]}', '{surname[rand.Next(0, surname_len)]}', '{patronymic[rand.Next(0, patronymic_len)]}');", connection);
                try { GEN_SQL.ExecuteNonQuery(); ++c; } catch (Exception exp) { continue; }
            }
            Console.WriteLine("Исполнители сгенерированы");
            
            


            for (int i = 0; i < property_len; ++i)
            {
                GEN_SQL = new NpgsqlCommand($"insert into property_type (property_type) values ('{property_type[i]}');", connection);
                try { GEN_SQL.ExecuteNonQuery(); } catch (Exception exp) { continue; }
            }
            Console.WriteLine("Типы собственности сгенерированы");


            for (int i = 0; i < district_len; ++i)
            {
                GEN_SQL = new NpgsqlCommand($"insert into district (name) values ('{district[i]}');", connection);
                try { GEN_SQL.ExecuteNonQuery(); } catch (Exception exp) { continue; }
            }
            Console.WriteLine("Области сгенерированы");


            for (int i = 0; i < 100; ++i)
            {
                GEN_SQL = new NpgsqlCommand($"insert into license (expiration_date) values ('{rand.Next(2021, 2030)}-{rand.Next(1, 13)}-{rand.Next(1, 29)}');", connection);
                try { GEN_SQL.ExecuteNonQuery(); } catch (Exception exp) { continue; }
            }
            Console.WriteLine("Лицензии сгенерированы");

            c = 0;
            while(c<20)
            {
                string exmpl = $"insert into company (name, id_city) values ('{company[c]}', {rand.Next(1, 29)});";
                GEN_SQL = new NpgsqlCommand(exmpl, connection);
                //Console.WriteLine(exmpl);
                try { GEN_SQL.ExecuteNonQuery(); ++c; } catch (Exception exp) { continue; }
            }
            Console.WriteLine("Фирмы сгенерированы");
            MessageBox.Show("Списки сгенерированы");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string select_table = $"SELECT COUNT(*) FROM company;";
            //NpgsqlCommand company_count = new NpgsqlCommand(select_table, connection);
            //company_len = Convert.ToInt32(company_count.ExecuteScalar().ToString());
            bool coll;
            string information = "";
            int i = 0;
            while (i<300)
            {
                information = "";
                coll = Convert.ToBoolean(rand.Next(0, 2));
                if (coll) { information = $"{inf[rand.Next(0, inf_len)]} альбом"; }

                string gen_str = $"insert into album (name, release_date, copies_total, songs_total, collection, " +
                    $"album_inf, duration, id_company, id_format, id_performer, id_genre, id_language, photo) " +
                    $"values ('{album[rand.Next(0, album_len)]}', '{rand.Next(1960, 2022)}-{rand.Next(1, 13)}-{rand.Next(1, 29)}', " +
                    $"{rand.Next(1, 10000)}, {rand.Next(1, 21)}, {coll}, '{information}', " +
                    $"'{rand.Next(1, 3)}:{rand.Next(1, 61)}:{rand.Next(1, 61)}', {rand.Next(1, company_len)}, " +
                    $"{rand.Next(1, format_len + 1)}, {rand.Next(1, 31)}, {rand.Next(1, genre_len + 1)}, {rand.Next(1, language_len + 1)}, " +
                    @"'C:\Users\Vladimir\Desktop\KURSACH\kurs\photos\" +
                    $"{photos[rand.Next(0, photos_len)]}.jpg');";
                Console.WriteLine(gen_str);
                GEN_SQL = new NpgsqlCommand(gen_str, connection);
                try { GEN_SQL.ExecuteNonQuery(); ++i; } catch (Exception exp) { continue; }
            }
            Console.WriteLine("Альбомы сгенерированы");

            i = 0;
            while (i<300)
            {
                string str_gen = $"insert into shop (name, opening_year, phone, address, " +
                    $"id_district, id_property_type, license_num, id_owner) " +
                    $"values ('{shop[rand.Next(0, shop_len)]}', {rand.Next(1950, 2021)}, " +
                    $"'+38(071)-{rand.Next(1, 10)}{rand.Next(1, 10)}{rand.Next(1, 10)}-{rand.Next(1, 10)}{rand.Next(1, 10)}-" +
                    $"{rand.Next(1, 10)}{rand.Next(1, 10)}', " +
                    $"'{address[rand.Next(0, address_len)]}, {rand.Next(1, 301)}/{rand.Next(1, 181)}'," +
                    $"{rand.Next(1, district_len+1)}, {rand.Next(1, property_len+1)}, {rand.Next(1, 101)}, {rand.Next(1, 31)})";
                Console.WriteLine(str_gen);
                GEN_SQL = new NpgsqlCommand(str_gen, connection);
                try { GEN_SQL.ExecuteNonQuery(); ++i; } catch (Exception exp) { continue; }
            }
            Console.WriteLine("Магазины сгенерированы");

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            int i = 0;
            while (i<10000)
            {
                GEN_SQL = new NpgsqlCommand($"insert into supplies (id_album, id_shop, arrival_date, quantity)" +
                        $"values ({rand.Next(1,301)}, {rand.Next(1,301)}, '{rand.Next(2016, 2021)}-{rand.Next(1, 13)}-{rand.Next(1, 29)}', {rand.Next(1, 101)})", connection);
                try { GEN_SQL.ExecuteNonQuery(); ++i; } catch (Exception exp) { continue; }
            }
            Console.WriteLine("Поставки сгенерированы");
            MessageBox.Show("Поставки сгенерированы");


            int company_count = 20;
            i = 0;
            while(i<2500)
            {
                int suppl = rand.Next(1, 1001);
                GEN_SQL = new NpgsqlCommand($"insert into catalog (id_album, id_shop, price, supplied_num, sold_num) " +
                    $"values ({rand.Next(1, 301)}, {rand.Next(1, 301)}, {rand.Next(5, 301)}, {suppl}, {rand.Next(1, suppl)})", connection);
                try { GEN_SQL.ExecuteNonQuery(); ++i;} catch (Exception exp) { continue; }
            }
            Console.WriteLine("Каталоги сгенерированы");
            MessageBox.Show("Каталоги сгенерированы");

            this.Close();
        }

        private void Generation_Load(object sender, EventArgs e)
        {

        }
    }
}
