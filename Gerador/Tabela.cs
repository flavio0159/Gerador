using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gerador
{
    public partial class Tabela : Form
    {
        public Tabela()
        {
            InitializeComponent();
            SQL sql = new SQL();
            sql.dataGridViewUpdate(tabelaGridView);
            tabelaGridView.Columns[4].HeaderText = "Saldo de Gols";
            tabelaGridView.Columns[6].HeaderText = "Vitórias";
        }

        private void Tabela_Load(object sender, EventArgs e)
        {

        }
    }
}
