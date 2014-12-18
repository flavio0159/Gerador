/* Info
 * 27/07/2014 - Gerador de Tabela
 * Criador: Flavio
 * Créditos ao blog: http://eduardospaki.blogspot.com.br/2009/09/embaralhando-uma-lista.html?showComment=1290701067571 por passar o Embaralhador de List.
*/

using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Gerador
{
    public partial class Form1 : Form
    {
        Partida pt = new Partida();
        Panel panel1 = new Panel();
        int par = 0;
        bool ok = true; // Retorno True/False se caso ocorrer erro

        public Form1()
        {
            InitializeComponent();
            SQL sql = new SQL();
            sql.createTable();
            panel1.Size = new Size(901, 308);
            panel1.Left = 12;
            panel1.Top = 27;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.AutoScroll = true;
            panel1.BackColor = Color.FromArgb(244, 255, 244);
            this.Controls.Add(panel1);
        }


        public void Form1_Load(object sender, EventArgs e)
        {
            quantidadeDeTimeText.Select();
        }

        private void sobreToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Criador: Flavio \nDesign: Ketbaixa", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog fileread = new OpenFileDialog();

            fileread.InitialDirectory = "Desktop";
            fileread.Filter = "Times[Tabela] (*.crp)|*.crp";
            fileread.FilterIndex = 2;
            fileread.RestoreDirectory = true;
            if (fileread.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = fileread.OpenFile()) != null)
                    {
                        using (myStream)
                        {

                            string path = fileread.FileName;
                            var filename = Path.GetFileName(fileread.FileName);
                            int lines = 0;
                            using (var linesreader = File.OpenText(path))
                            {
                                while (linesreader.ReadLine() != null)
                                {
                                    lines++;
                                }
                            }
                            pt.qtimes = lines;
                            quantidadeDeTimeText.Text = Convert.ToString(pt.qtimes);
                            using (var lineRead = new StreamReader(path))
                            {
                                if (lines % 2 == 0)
                                {
                                    for (int i = 0; i <= lines - 1; i++)
                                    {
                                        pt.Times.Add(lineRead.ReadLine());
                                    }
                                    Embaralhador.Shuffle(pt.Times);
                                }
                                else
                                {
                                    for (int i = 0; i <= lines; i++)
                                    {
                                        if (i == lines)
                                        {
                                            Embaralhador.Shuffle(pt.Times);
                                            pt.Times.Add(pt.Ghost);
                                            break;
                                        }
                                        pt.Times.Add(lineRead.ReadLine());
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Interaction.MsgBox("Falha ao abrir arquivo!", MsgBoxStyle.Critical, "Error");
                }
                quantidadeDeTimeText.Enabled = false;
                if (pt.qtimes % 2 == 0) // Se o resto de Times for 2 ( Par ), ele executa este Bloco
                {
                    pt.Par(panel1);
                }
                else // Se caso o resto for 1 ( ímpar ) executa este bloco.
                {
                    pt.Impar(panel1);
                }
                btnTimes.Enabled = false;
                btnLimpar.Enabled = true;
            }
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog filesaveDialog = new SaveFileDialog();
            filesaveDialog.Filter = "Times[Tabela] (*.crp)|*.crp";
            filesaveDialog.FilterIndex = 2;
            filesaveDialog.RestoreDirectory = true;

            if (filesaveDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = filesaveDialog.OpenFile()) != null)
                {
                    StreamWriter filewrite = new StreamWriter(myStream);
                    if (par == 2)
                    {
                        for (int i = 0; i <= pt.qtimes - 1; i++)
                        {
                            filewrite.WriteLine(pt.Times[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= pt.qtimes - 2; i++)
                        {
                            filewrite.WriteLine(pt.Times[i]);
                        }
                    }
                    filewrite.Close();
                    myStream.Close();
                }
            }
            Interaction.MsgBox("Salvo com Sucesso!", MsgBoxStyle.Information, "Salvo!");
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            // Quando clicar em Limpar, vai limpar o "programa"
            quantidadeDeTimeText.Text = ""; //Limpa a entrada de quantidade de time
            btnLimpar.Enabled = false; // Bloquear o botão novamente!
            quantidadeDeTimeText.Enabled = true;
            pt.clearList();
            panel1.Controls.Clear();
            btnInsert.Enabled = false;
            abrirToolStripMenuItem.Enabled = true;
            salvarToolStripMenuItem.Enabled = false;
        }

        private void quantidadeDeTimeText_TextChanged_1(object sender, EventArgs e)
        {
            if (quantidadeDeTimeText.Text == "" || quantidadeDeTimeText.Text == "1" || quantidadeDeTimeText.Text == "0") //Se caso o Campo de Quantidade de Times for vazio
            {
                btnTimes.Enabled = false; // O Botao irar ficar Desativado
            }
            else // Se não for
            {
                btnTimes.Enabled = true; // Ira ficar ativado
            }
        }

        private void btnTimes_Click(object sender, EventArgs e)
        {
            try // Vai tentar executar este bloco
            {
                //qtimes = Convert.ToInt32(quantidadeDeTimeText.Text);
                pt.qtimes = Convert.ToInt32(quantidadeDeTimeText.Text);
                ok = true; // Ok vai começar como true ( Necessário para repetir o código novamente sem erro )
                btnTimes.Enabled = false; // Impede da pessoa gerar novamente enquanto ela não limpar.
                btnLimpar.Enabled = true; // Permite que a pessoa limpe o "programa"              
            }
            catch // Caso ocorra qualquer erro, ira mostrar esta mensagem
            {
                Interaction.MsgBox("Digite um Valor Válido!", MsgBoxStyle.Critical, "Error"); // Se caso ocorrer erro, vai aparecer uma tela pedindo para digitar o valor válido, pois de todas as possibilidades, a única que pode dar erro é sendo um valor inválido ( Exemplo: a ).
                ok = false; // Se caso ocorrer erro, o Ok vai ser False                                                                        
            }

            if (ok == true)
            {
                if (pt.qtimes % 2 == 0)
                {
                    par = 2;
                    if (pt.setTimePar() == true)
                    {
                        salvarToolStripMenuItem.Enabled = true; // Habilita o botão salvar.
                        pt.Par(panel1);
                        btnInsert.Enabled = true;
                    }
                    else
                    {
                        btnLimpar.Enabled = false;
                        btnInsert.Enabled = false;
                        btnTimes.Enabled = true;
                    }
                }
                else if(pt.setTimesImpar() == true)
                {
                    salvarToolStripMenuItem.Enabled = true; // Habilita o botão salvar.
                    pt.Impar(panel1);
                    btnInsert.Enabled = true;
                }
                else
                {
                    btnLimpar.Enabled = false;
                    btnInsert.Enabled = false;
                    btnTimes.Enabled = true;
                }
            }

            else
            {
                pt.clearList();
                btnLimpar.Enabled = false;
                btnTimes.Enabled = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
        }

        private void panel1_Move(object sender, EventArgs e)
        {
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pt.insertValoresPar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tabela tabela = new Tabela();
            tabela.Visible = true;
        }

        private void barProgress_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void arquivoToolStripMenuItem_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void arquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SQL sql = new SQL();
            sql.insertTime("Flamengo");
        }
    }
}
