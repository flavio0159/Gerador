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
using System.Windows.Forms;

namespace Gerador
{
    public partial class Form1 : Form
    {
        Partida pt = new Partida();
        Panel panel1 = new Panel();
        int par = 0;
        bool ok = true; // Retorno True/False se caso ocorrer erro
        bool cancel = false; // Retorno caso o usuário cancele

        public Form1()
        {
            InitializeComponent();
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
            //Stream myStream = null;
            //OpenFileDialog fileread = new OpenFileDialog();

            //fileread.InitialDirectory = "Desktop";
            //fileread.Filter = "Times[Tabela] (*.crp)|*.crp";
            //fileread.FilterIndex = 2;
            //fileread.RestoreDirectory = true;
            //if (fileread.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        if ((myStream = fileread.OpenFile()) != null)
            //        {
            //            using (myStream)
            //            {

            //                string path = fileread.FileName;
            //                var filename = Path.GetFileName(fileread.FileName);
            //                int lines = 0;
            //                using (var linesreader = File.OpenText(path))
            //                {
            //                    while (linesreader.ReadLine() != null)
            //                    {
            //                        lines++;
            //                    }
            //                }
            //                qtimes = lines;
            //                quantidadeDeTimeText.Text = Convert.ToString(qtimes);
            //                using (var lineRead = new StreamReader(path))
            //                {
            //                    if (lines % 2 == 0)
            //                    {
            //                        for (int i = 0; i <= lines - 1; i++)
            //                        {
            //                            times.Add(lineRead.ReadLine());
            //                        }
            //                        Embaralhador.Shuffle(times);
            //                    }
            //                    else
            //                    {
            //                        for (int i = 0; i <= lines; i++)
            //                        {
            //                            if (i == lines)
            //                            {
            //                                Embaralhador.Shuffle(times);
            //                                times.Add(ghost);
            //                                break;
            //                            }
            //                            times.Add(lineRead.ReadLine());
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        Interaction.MsgBox("Falha ao abrir arquivo!", MsgBoxStyle.Critical, "Error");
            //    }
            //    quantidadeDeTimeText.Enabled = false;
            //    listaPartidas.Clear();
            //    if (qtimes % 2 == 0) // Se o resto de Times for 2 ( Par ), ele executa este Bloco
            //    {
            //        List<string> timescopy = new List<string>(times);
            //        for (int i = 0; i <= (qtimes - 1); i++) // qtimes/2 pois, em 4 Times digitados, iria aparecer o resultado 2x. Alemanha x Holanda / Holanda x Alemanha.
            //        {
            //            check = qtimes - 1;
            //            if (i == check)
            //            {
            //                break;
            //            }
            //            listaPartidas.Items.Add(i + 1 + "º Rodada: ");
            //            for (int x = 0; x <= (qtimes - 1) / 2; x++)
            //            {
            //                int casa = (i + x) % (qtimes - 1);
            //                int fora = (qtimes - 1 - x + i) % (qtimes - 1);
            //                if (x == 0)
            //                {
            //                    fora = qtimes - 1;
            //                }
            //                listaPartidas.Items.Add(times[casa] + "  -x-  " + timescopy[fora]); // Adiciona os times na ListView.
            //            }
            //            listaPartidas.Items.Add("");
            //            returno = i + 1; ;
            //        }
            //        listaPartidas.Items.Add("///// RETURNO /////");
            //        listaPartidas.Items.Add("");
            //        for (int i = 0; i <= qtimes - 1; i++) // qtimes/2 pois, em 4 Times digitados, iria aparecer o resultado 2x. Alemanha x Holanda / Holanda x Alemanha.
            //        {
            //            check = qtimes - 1;
            //            if (i == check)
            //            {
            //                break;
            //            }
            //            listaPartidas.Items.Add(returno + 1 + "º Returno: ");
            //            returno++;
            //            for (int x = 0; x <= (qtimes - 1) / 2; x++)
            //            {
            //                int casa = (i + x) % (qtimes - 1);
            //                int fora = (qtimes - 1 - x + i) % (qtimes - 1);
            //                if (x == 0)
            //                {
            //                    fora = qtimes - 1;
            //                }
            //                listaPartidas.Items.Add(timescopy[fora] + "  -x-  " + times[casa]); // Adiciona os times na ListView.
            //            }
            //            listaPartidas.Items.Add("");
            //        }
            //    }
            //    else // Se caso o resto for 1 ( ímpar ) executa este bloco.
            //    {
            //        qtimes = qtimes + 1;
            //        List<string> timescopy = new List<string>(times);
            //        for (int i = 0; i <= (qtimes - 1); i++) // qtimes/2 pois, em 4 Times digitados, iria aparecer o resultado 2x. Alemanha x Holanda / Holanda x Alemanha.
            //        {
            //            check = qtimes - 1;
            //            if (i == check)
            //            {
            //                break;
            //            }
            //            listaPartidas.Items.Add(i + 1 + "º Rodada: ");
            //            for (int x = 0; x <= (qtimes - 1) / 2; x++)
            //            {
            //                int casa = (i + x) % (qtimes - 1);
            //                int fora = (qtimes - 1 - x + i) % (qtimes - 1);
            //                if (x == 0)
            //                {
            //                    fora = qtimes - 1;
            //                }
            //                if (timescopy[fora] == ghost || times[casa] == ghost)
            //                {
            //                    if (timescopy[fora] == ghost)
            //                    {
            //                        listaPartidas.Items.Add("Descanço: " + times[casa]);
            //                    }
            //                    if (times[casa] == ghost)
            //                    {
            //                        listaPartidas.Items.Add("///Folga: " + timescopy[casa] + "///");
            //                        listaPartidas.Items.Add("");
            //                    }
            //                }
            //                else
            //                {
            //                    listaPartidas.Items.Add(times[casa] + "  -x-  " + timescopy[fora]); // Adiciona os times na ListView.
            //                }
            //            }
            //            listaPartidas.Items.Add("");
            //            returno = i + 1;
            //        }
            //        listaPartidas.Items.Add("///// RETURNO /////");
            //        listaPartidas.Items.Add("");
            //        for (int i = 0; i <= (qtimes - 1); i++) // qtimes/2 pois, em 4 Times digitados, iria aparecer o resultado 2x. Alemanha x Holanda / Holanda x Alemanha.
            //        {
            //            check = qtimes - 1;
            //            if (i == check)
            //            {
            //                break;
            //            }
            //            listaPartidas.Items.Add(returno + 1 + "º Rodada: ");
            //            returno++;
            //            for (int x = 0; x <= (qtimes - 1) / 2; x++)
            //            {
            //                int casa = (i + x) % (qtimes - 1);
            //                int fora = (qtimes - 1 - x + i) % (qtimes - 1);
            //                if (x == 0)
            //                {
            //                    fora = qtimes - 1;
            //                }
            //                if (timescopy[fora] == ghost || times[casa] == ghost)
            //                {
            //                    if (timescopy[fora] == ghost)
            //                    {
            //                        listaPartidas.Items.Add("Descanço: " + times[casa]);
            //                    }
            //                    if (times[casa] == ghost)
            //                    {
            //                        listaPartidas.Items.Add("///Folga: " + timescopy[casa] + "///");
            //                        listaPartidas.Items.Add("");
            //                    }
            //                }
            //                else
            //                {
            //                    listaPartidas.Items.Add(timescopy[fora] + "  -x-  " + times[casa]); // Adiciona os times na ListView.
            //                }
            //            }
            //            listaPartidas.Items.Add("");
            //        }
            //    }
            //    btnTimes.Enabled = false;
            //    btnLimpar.Enabled = true;
            //}
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stream myStream;
            //SaveFileDialog filesaveDialog = new SaveFileDialog();
            //filesaveDialog.Filter = "Times[Tabela] (*.crp)|*.crp";
            //filesaveDialog.FilterIndex = 2;
            //filesaveDialog.RestoreDirectory = true;

            //if (filesaveDialog.ShowDialog() == DialogResult.OK)
            //{
            //    if ((myStream = filesaveDialog.OpenFile()) != null)
            //    {
            //        StreamWriter filewrite = new StreamWriter(myStream);
            //        if (par == 2)
            //        {
            //            for (int i = 0; i <= qtimes - 1; i++)
            //            {
            //                filewrite.WriteLine(times[i]);
            //            }
            //        }
            //        else
            //        {
            //            for (int i = 0; i <= qtimes - 2; i++)
            //            {
            //                filewrite.WriteLine(times[i]);
            //            }
            //        }
            //        filewrite.Close();
            //        myStream.Close();
            //    }
            //}
            //Interaction.MsgBox("Salvo com Sucesso!", MsgBoxStyle.Information, "Salvo!");
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            // Quando clicar em Limpar, vai limpar o "programa"
            quantidadeDeTimeText.Text = ""; //Limpa a entrada de quantidade de time
            labelProgress.Text = null;
            barProgress.Value = 0;
            btnLimpar.Enabled = false; // Bloquear o botão novamente!
            quantidadeDeTimeText.Enabled = true;
            pt.clearList();
            panel1.Controls.Clear();
            abrirToolStripMenuItem.Enabled = true;
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
                        pt.Par(panel1,barProgress, labelProgress);
                        salvarToolStripMenuItem.Enabled = true; // Habilita o botão salvar.
                    }

                    else
                    {
                        btnLimpar.Enabled = false;
                        btnTimes.Enabled = false;
                    }
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
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void barProgress_Click(object sender, EventArgs e)
        {
        }
    }
}
