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
    public partial class ShowCatalog : Form
    {
        NpgsqlConnection connection;
        public ShowCatalog(NpgsqlConnection c)
        {
            connection = c;
            InitializeComponent();
        }
    }
}
