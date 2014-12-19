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
            sql.eraseSaldo();
            sql.dataGridViewUpdate(tabelaGridView);        
            tabelaGridView.Columns[5].Visible = false;
            tabelaGridView.Columns[3].HeaderText = "Saldo de Gols";
            tabelaGridView.Columns[4].HeaderText = "Gols Feitos";          
            tabelaGridView.Columns[7].HeaderText = "Vitórias";
        }

        private void Tabela_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você deseja realmente deletar todos os Dados?\nEles não poderam ser recuperados!", "Deletar Dados", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SQL sql = new SQL();
                sql.resetarTabela();
                sql.dataGridViewUpdate(tabelaGridView);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {            
            int ID = tabelaGridView.CurrentCell.RowIndex+1;
            String time = tabelaGridView.Rows[ID-1].Cells[1].Value.ToString();
            if (MessageBox.Show("Você deseja realmente deletar o Time " + time + " ?\nOs dados não poderam ser recuperados!", "Deletar Time", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
            {
                SQL sql = new SQL();
                sql.deletarTime(ID);
                sql.dataGridViewUpdate(tabelaGridView);
            }
        }

        public DataGridView getDataGridView()
        {
            return this.tabelaGridView;
        }

        private void Tabela_Resize(object sender, EventArgs e)
        {
            if(this.Size.Height < 352){
                this.Size = new Size(this.Width, 352);
            }
            if(this.Size.Width < 984 || this.Size.Width > 984){
                this.Size = new Size(984,352);
            }
            if (this.Height != 352)
            {
                btnExcluir.Top = this.Height - 88;
                btnEraseTable.Top = this.Height - 88;
                tabelaGridView.Size = new Size(943,this.Height -106);
            }
        }

    }
}
