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
using System.Windows.Forms.DataVisualization.Charting;

namespace kurs
{
    public partial class Graphics : Form
    {
        NpgsqlConnection connection;
        string state = "";
        public Graphics(NpgsqlConnection c, string data_state)
        {
            connection = c;
            state = data_state;
            InitializeComponent();
        }

        private void Dialog()
        {
            if (state == "radial")
            {
                chart1.Series[0].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)17;
                List<string> genres = new List<string>();
                List<int> copies = new List<int>();
                chart1.Series[0].Points.Clear();
                NpgsqlDataAdapter reader = new NpgsqlDataAdapter($"select type, profit from genre " +
                            $"inner join(select id_genre, sum(cat.sold_num) as profit " +
                            $"from album alb inner join catalog cat on (cat.id_album = alb.id_album) group by id_genre) as s on(genre.id_genre = s.id_genre) " +
                            $"order by profit desc ", connection);
                DataTable data = new DataTable();
                connection.Close();
                connection.Open();
                reader.Fill(data);
                foreach (DataRow row in data.Rows)
                {
                    genres.Add(Convert.ToString(row[0]));
                }
                foreach (DataRow row in data.Rows)
                {
                    copies.Add(Convert.ToInt32(row[1]));
                }
                for (int i = 0; i < genres.Count; ++i)
                {
                    chart1.Series[0].Points.AddY(Convert.ToDouble(copies[i]));
                    chart1.Series[0].Points[i].LegendText = ($"{genres[i]} : {Convert.ToDouble(chart1.Series[0].Points[i].YValues[0])}");
                }
            } else if (state == "column")
            {
                List<string> genres = new List<string>();
                List<int> nums = new List<int>();

                NpgsqlDataAdapter reader = new NpgsqlDataAdapter($"select g.type, count(*) as count from album alb " +
                                                                $"inner join(select * from catalog cat inner join shop on (shop.id_shop = cat.id_shop)) " +
                                                                $"as foo on(foo.id_album = alb.id_album) " +
                                                                $"inner join genre g on(g.id_genre = alb.id_genre) " +
                                                                $"group by g.id_genre order by g.id_genre", connection);
                DataTable data = new DataTable();
                connection.Close();
                connection.Open();
                reader.Fill(data);
                foreach (DataRow row in data.Rows)
                {
                    genres.Add(Convert.ToString(row[0]));
                }
                foreach (DataRow row in data.Rows)
                {
                    nums.Add(Convert.ToInt32(row[1]));
                }

                chart1.Series.Remove(chart1.Series[0]);
                for (int i = 0; i < genres.Count; ++i)
                {
                    chart1.Series.Add(genres[i]);
                    chart1.Series[i].Points.AddXY(i+1, nums[i]);
                }
            } else if (state == "3D")
            {
                List<string> format, company;
                List<int> id_format, id_company, alb_num;

                connection.Close();
                connection.Open();
                NpgsqlCommand comm = new NpgsqlCommand("select type from format order by id_format", connection);
                NpgsqlDataReader rdr = comm.ExecuteReader();
                if (!rdr.HasRows)
                {
                    rdr.Close();
                    return;
                }
                chart1.Series.Remove(chart1.Series[0]);
                int c = 0;
                while(rdr.Read())
                {
                    chart1.Series.Add(rdr[0].ToString());
                    chart1.Series[c].IsValueShownAsLabel = true;
                    chart1.Series[c].Font = new Font("Microsoft Sans Serif", 12);
                    ++c;
                }
                rdr.Close();

                comm = new NpgsqlCommand("select count(*) from format", connection);
                int format_c = Convert.ToInt32(comm.ExecuteScalar());
                comm = new NpgsqlCommand("select count(*) from company", connection);
                int comp_c = Convert.ToInt32(comm.ExecuteScalar());


                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                chart1.ChartAreas[0].Area3DStyle.Enable3D = true;

                connection.Close();
                connection.Open();
                comm = new NpgsqlCommand($"select c.id_company as id, c.name as name from company c order by id_company", connection);
                rdr = comm.ExecuteReader();
                if (!rdr.HasRows)
                {
                    rdr.Close();
                    return;
                }

                NpgsqlDataReader rdr2;
                int[,] data = new int[comp_c, format_c];
                List<string> comp = new List<string>();
                while (rdr.Read())
                {
                    if (rdr[1].ToString() != "")
                    {
                        comp.Add(rdr[1].ToString());
                        Console.Write(rdr[1].ToString());
                    }
                }
                rdr.Close();
                Console.Write("\n");


                for (int i=0; i<comp_c; ++i)
                {
                    for (int j = 0; j < format_c; ++j)
                    {
                        connection.Close();
                        connection.Open();
                        comm = new NpgsqlCommand($"select f.id_format as id, f.type as type, count(*) as cnt from album a " +
                            $"inner join format f on(f.id_format = a.id_format) " +
                            $"inner join company c on(c.id_company = a.id_company and c.id_company = {i+1}) " +
                            $"group by f.id_format order by f.id_format", connection);
                        rdr2 = comm.ExecuteReader();
                        if (!rdr2.HasRows)
                        {
                            rdr2.Close();
                            return;
                        }

                        while (rdr2.Read())
                        {
                            data[i,j] = Convert.ToInt32(rdr2[2].ToString());
                            Console.Write($"{data[i,j]}");
                        }
                        Console.Write("\n");
                        rdr2.Close();
                    }
                }

                for (int l=0; l<comp_c; ++l)
                {
                    for (int j=0; j<format_c; ++j)
                    {
                        chart1.Series[j].Points.AddXY(comp[l], data[l,j]);
                        Console.Write(data[l,j]);
                    }
                    Console.Write("\n");
                }
            }
            
        }

        private void Graphics_Load(object sender, EventArgs e)
        {
            Dialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
