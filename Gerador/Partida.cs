using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Gerador
{
    class Partida
    {

        private List<string> times = new List<string>(); //Criar uma Lista 

        public List<string> Times
        {
            get { return times; }
            set { times = value; }
        }
        double check; //Usado para transforma a quantidade números ímpar em par.
        private String ghost = Convert.ToString(DateTime.Now.Millisecond);

        public String Ghost
        {
            get { return ghost; }
            set { ghost = value; }
        }
        private int _qtimes; // Armazenar a Quantidade de Times digitada pela pessoa      
        private Label[][] timecasa;
        private Label[][] timefora;
        private TextBox[][] golscasa;
        private TextBox[][] golsfora;
        private TextBox[][] golscasaR;
        private TextBox[][] golsforaR;
        private int distancia = 20;

        public int qtimes
        {
            get { return _qtimes; }
            set { _qtimes = value; }
        }

        public bool setTimePar()
        {
            try
            {
                int length = 0;
                for (int i = 0; i <= _qtimes - 1; i++) // qtimes - 1, pois, como ocupada a posição 0, se caso o quantidade de Tiems fosse 12, haveria 13 times.
                {
                    String last = "";
                    times.Add(Interaction.InputBox("Digite o nome do Time:           " + "Total: " + i + "/" + _qtimes, "Times")); // Dialogo para digitar os times
                    if (times[i] == "" || String.IsNullOrEmpty(times[i]))
                    {
                        clearList();
                        return false;
                    }
                    if (times[i].Length > length)
                    {
                        last = times[i].Substring(times[i].Length - 1);
                        length = times[i].Length;
                        Graphics pixels = Graphics.FromImage(new Bitmap(1, 1));
                        SizeF WidthTimes = pixels.MeasureString(times[i], new Font("Arial", 10));
                        SizeF WidthChar = pixels.MeasureString(last, new Font("Arial", 10));
                        int lasttamanho = Convert.ToInt32(WidthChar.Width);
                        if (Char.IsLower(Convert.ToChar(last)))
                        {
                            lasttamanho += 7;
                        }
                        distancia = Convert.ToInt32(WidthTimes.Width) + lasttamanho;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Falha ao inserir times! \n\n" + e, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void insertValoresPar()
        {
            SQL sql = new SQL();
            for (int i = 0; i <= (_qtimes - 1); i++)
            {
                check = _qtimes - 1; // Se caso qtimes for até o final, em uma tábela de 8 Times, que o correto deveria ser 4 Jogos, ele vai vai ser 5 Rodadas ( Repetindo a primeira ). Então quando ele chegar -1, vai quebrar o for impedindo que conte novamente.
                if (i == check) // Faz a verificação se já chegou a qtimes -1.
                {
                    break; // Quebra o For.
                }
                for (int x = 0; x <= (_qtimes - 1) / 2; x++)
                {
                    if (String.IsNullOrEmpty(golscasa[i][x].Text))
                    {
                        golscasa[i][x].Text = "0";
                    }
                    if (String.IsNullOrEmpty(golscasaR[i][x].Text))
                    {
                        golscasaR[i][x].Text = "0";
                    }
                    if (String.IsNullOrEmpty(golsfora[i][x].Text))
                    {
                        golsfora[i][x].Text = "0";
                    }
                    if (String.IsNullOrEmpty(golsforaR[i][x].Text))
                    {
                        golsforaR[i][x].Text = "0";
                    }
                    int empatec = 0;
                    int empatef = 0;
                    int vitoriac = 0;
                    int vitoriaf = 0;
                    int derrotac = 0;
                    int derrotaf = 0;
                    if (Convert.ToInt32(golsfora[i][x].Text) > Convert.ToInt32(golscasa[i][x].Text))
                    {
                        vitoriaf = 1;
                        derrotac = 1;
                    }
                    else
                    {
                        if (Convert.ToInt32(golsfora[i][x].Text) < Convert.ToInt32(golscasa[i][x].Text))
                        {
                            vitoriac = 1;
                            derrotaf = 1;
                        }
                        else
                        {
                            empatec = 1;
                            empatef = 1;
                        }
                    }
                    sql.updateValores(Convert.ToInt32(golsfora[i][x].Text), timefora[i][x].Text, vitoriaf, derrotaf, empatef);
                    sql.updateValores(Convert.ToInt32(golscasa[i][x].Text), timecasa[i][x].Text, vitoriac, derrotac, empatec);

                    empatec = 0;
                    empatef = 0;
                    vitoriac = 0;
                    vitoriaf = 0;
                    derrotac = 0;
                    derrotaf = 0;

                    if (Convert.ToInt32(golsforaR[i][x].Text) > Convert.ToInt32(golscasaR[i][x].Text))
                    {
                        vitoriaf = 1;
                        derrotac = 1;
                    }
                    else
                    {
                        if (Convert.ToInt32(golsforaR[i][x].Text) < Convert.ToInt32(golscasaR[i][x].Text))
                        {
                            vitoriac = 1;
                            derrotaf = 1;
                        }
                        else
                        {
                            empatec = 1;
                            empatef = 1;
                        }
                    }
                    //sql.updateValores(Convert.ToInt32(golscasaR[i][x].Text), timecasa[i][x].Text, vitoriac, derrotac, empatec);
                    //sql.updateValores(Convert.ToInt32(golsforaR[i][x].Text), timefora[i][x].Text, vitoriaf, derrotaf, empatef);

                }
            }
        }

        public bool setTimesImpar()
        {
            try
            {
                int length = 0;
                String last = "";
                _qtimes = _qtimes + 1; // Adiciono a quantidade de times ímpar, +1, se tornando um número Par.
                for (int i = 0; i <= _qtimes - 1; i++) // qtimes - 1, pois, como ocupada a posição 0, se caso o quantidade de Tiems fosse 12, haveria 13 times.
                {
                    if (i == _qtimes - 1) // Quando chegar na última posição, a pessoa não vai ter o que digitar. Então automaticamente adiciona um time fantasma para não ficar um espaço null.
                    {
                        Embaralhador.Shuffle(times); // Embaralha os times para que o time fantasma fique em uma posição aleatória
                        times.Add(ghost); // Adiciona a Variável Ghost.
                        break; // Quebra o For.
                    }
                    times.Add(Interaction.InputBox("Digite o nome do Time:           " + "Total: " + i + "/" + (_qtimes - 1), "Times")); // Dialogo para digitar os times
                    if (times[i] == "" || String.IsNullOrEmpty(times[i]))
                    {
                        clearList();
                        return false;
                    }
                    if (times[i].Length > length)
                    {
                        last = times[i].Substring(times[i].Length - 1);
                        length = times[i].Length;
                        Graphics pixels = Graphics.FromImage(new Bitmap(1, 1));
                        SizeF WidthTimes = pixels.MeasureString(times[i], new Font("Arial", 10));
                        SizeF WidthChar = pixels.MeasureString(last, new Font("Arial", 10));
                        int lasttamanho = Convert.ToInt32(WidthChar.Width);
                        if (Char.IsLower(Convert.ToChar(last)))
                        {
                            lasttamanho += 7;
                        }
                        distancia = Convert.ToInt32(WidthTimes.Width) + lasttamanho;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Falha ao inserir times! \n\n" + e, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void clearList()
        {
            this.times.Clear();
        }


        public void Par(Panel panel1)
        {

            int alto = 0;
            timecasa = new Label[_qtimes][];
            timefora = new Label[_qtimes][];
            Label[] partida = new Label[_qtimes];
            golscasa = new TextBox[_qtimes][];
            golsfora = new TextBox[_qtimes][];
            golscasaR = new TextBox[_qtimes][];
            golsforaR = new TextBox[_qtimes][];
            Boolean top = true;
            Boolean alterado = false;
            Boolean calculado = false;
            int II = 0;
            int I = 0;
            int Altura = 30;
            SQL sql = new SQL();

            Embaralhador.Shuffle(times); // Embaralha os times.
            List<string> timescopy = new List<string>(times); // Faz uma copa da lista Times.
            for (int i = 0; i <= (_qtimes - 1); i++) // qtimes/2 pois, em 4 Times digitados, iria aparecer o resultado 2x. Alemanha x Holanda / Holanda x Alemanha.
            {

                timecasa[i] = new Label[_qtimes - 1];
                timefora[i] = new Label[_qtimes - 1];
                golscasa[i] = new TextBox[_qtimes - 1];
                golsfora[i] = new TextBox[_qtimes - 1];

                check = _qtimes - 1; // Se caso qtimes for até o final, em uma tábela de 8 Times, que o correto deveria ser 4 Jogos, ele vai vai ser 5 Rodadas ( Repetindo a primeira ). Então quando ele chegar -1, vai quebrar o for impedindo que conte novamente.
                if (i == check) // Faz a verificação se já chegou a qtimes -1.
                {
                    break; // Quebra o For.
                }
                for (int x = 0; x <= (_qtimes - 1) / 2; x++)
                {
                    int casa = (i + x) % (_qtimes - 1); // Faz a escolha do time.
                    int fora = (_qtimes - 1 - x + i) % (_qtimes - 1); // Faz a escolha do time.

                    if (x == 0) // Se caso x == 0, não vai ter oque embaralhar teoricamente, então fazemos que seja o total -1.
                    {
                        fora = _qtimes - 1; // Vai receber o total - 1.
                    }


                    golscasa[i][x] = new TextBox();
                    golscasa[i][x].Size = new Size(33, 22);

                    golsfora[i][x] = new TextBox();
                    golsfora[i][x].Size = new Size(33, 22);

                    timecasa[i][x] = new Label();
                    timecasa[i][x].AutoSize = true;
                    timecasa[i][x].Text = times[casa];
                    timecasa[i][x].Font = new Font("Arial", 10);

                    timefora[i][x] = new Label();
                    timefora[i][x].AutoSize = true;
                    timefora[i][x].Text = timescopy[fora];
                    timefora[i][x].Font = new Font("Arial", 10);

                    if (i == 0)
                    {

                        timecasa[i][x].Left = 42;
                        timefora[i][x].Left = 40 + distancia;

                        timecasa[i][x].Top = Altura * (x + 1);
                        timefora[i][x].Top = Altura * (x + 1);

                        golsfora[i][x].Left = timecasa[i][x].Left - 37;
                        golscasa[i][x].Left = timefora[i][x].Left + distancia;

                        golsfora[i][x].Top = timecasa[i][x].Top - 3;
                        golscasa[i][x].Top = timefora[i][x].Top - 3;
                    }
                    else
                    {
                        timecasa[i][x].Left = golscasa[i - 1][x].Left + 100;
                        timefora[i][x].Left = timecasa[i][x].Left + distancia;
                        if (x == 0)
                        {
                            timecasa[i][x].Top = Altura;
                            timefora[i][x].Top = Altura;

                            if (alterado == true)
                            {
                                timecasa[i][x].Top = timecasa[i - 1][0].Top;
                                timefora[i][x].Top = timefora[i - 1][0].Top;
                            }


                        }
                        else
                        {
                            timecasa[i][x].Top = timecasa[i][x - 1].Top + 30;
                            timefora[i][x].Top = timefora[i][x - 1].Top + 30;
                        }
                        if (golscasa[i - 1][x].Left > 650)
                        {

                            if (top == true)
                            {
                                I = i;
                                top = false;
                            }
                            timecasa[i][x].Left = 42;
                            timefora[i][x].Left = 40 + distancia;
                            if (calculado == false)
                            {
                                Altura = Altura * (_qtimes - 1) / 2;
                                calculado = true;
                            }
                            if (i % I == 0 && II != i)
                            {
                                II = i;
                                Altura *= 2;
                            }

                            if (x == 0)
                            {
                                timecasa[i][x].Top = timecasa[i - 1][(_qtimes - 1) / 2].Top + 80;
                                timefora[i][x].Top = timecasa[i - 1][(_qtimes - 1) / 2].Top + 80;
                                alterado = true;
                            }
                            else
                            {
                                timecasa[i][x].Top = timecasa[i][x - 1].Top + 30;
                                timefora[i][x].Top = timefora[i][x - 1].Top + 30;
                                alterado = true;
                            }

                        }

                        golsfora[i][x].Left = timecasa[i][x].Left - 37;
                        golscasa[i][x].Left = timefora[i][x].Left + distancia;

                        golsfora[i][x].Top = timecasa[i][x].Top - 3;
                        golscasa[i][x].Top = timefora[i][x].Top - 3;
                    }

                    if (i == _qtimes - 2)
                    {
                        alto = timecasa[i][x].Top;
                    }

                }
                partida[i] = new Label();
                partida[i].AutoSize = true;
                partida[i].Text = "Rodada " + (i + 1);
                partida[i].Width = 34;
                partida[i].Height = 14;
                partida[i].Font = new Font("Arial", 10);

                partida[i].Left = timecasa[i][0].Left + distancia - 40;
                partida[i].Top = timecasa[i][0].Top - 30;

                //partidatop[i] = partida[i].Top;

                panel1.Controls.AddRange(timefora[i]);
                panel1.Controls.AddRange(timecasa[i]);
                panel1.Controls.AddRange(golscasa[i]);
                panel1.Controls.AddRange(golsfora[i]);

                panel1.Controls.Add(partida[i]);


            }

            Label returnol = new Label();

            returnol.AutoSize = true;
            returnol.Text = "_______________________________________________________________________________________________________";
            returnol.Font = new Font("Arial", 10);

            returnol.Left = +40;
            returnol.Top = alto + 83;

            panel1.Controls.Add(returnol);

            Label Returno = new Label();
            Returno.AutoSize = true;
            Returno.Text = "RETURNO";
            Returno.Font = new Font("Arial", 10);

            Returno.Left = 426;
            Returno.Top = alto + 70;

            panel1.Controls.Add(Returno);


            Label returnol2 = new Label();
            returnol2.AutoSize = true;
            returnol2.Text = "_______________________________________________________________________________________________________";
            returnol2.Font = new Font("Arial", 10);

            returnol2.Left = +40;
            returnol2.Top = alto + 45;

            panel1.Controls.Add(returnol2);

            top = true;
            alterado = false;
            calculado = false;
            II = 0;
            I = 0;
            alto = 0;
            Altura = returnol.Top + 70;

            for (int i = 0; i <= (_qtimes - 1); i++) // qtimes/2 pois, em 4 Times digitados, iria aparecer o resultado 2x. Alemanha x Holanda / Holanda x Alemanha.
            {


                /*casaleft[i] = new int[qtimes - 1];
                casatop[i] = new int[qtimes - 1];
                foraleft[i] = new int[qtimes - 1];
                foratop[i] = new int[qtimes - 1];*/

                timecasa[i] = new Label[_qtimes - 1];
                timefora[i] = new Label[_qtimes - 1];
                golscasaR[i] = new TextBox[_qtimes - 1];
                golsforaR[i] = new TextBox[_qtimes - 1];

                check = _qtimes - 1; // Se caso qtimes for até o final, em uma tábela de 8 Times, que o correto deveria ser 4 Jogos, ele vai vai ser 5 Rodadas ( Repetindo a primeira ). Então quando ele chegar -1, vai quebrar o for impedindo que conte novamente.
                if (i == check) // Faz a verificação se já chegou a qtimes -1.
                {
                    break; // Quebra o For.
                }
                for (int x = 0; x <= (_qtimes - 1) / 2; x++)
                {
                    int casa = (i + x) % (_qtimes - 1); // Faz a escolha do time.
                    int fora = (_qtimes - 1 - x + i) % (_qtimes - 1); // Faz a escolha do time.

                    if (x == 0) // Se caso x == 0, não vai ter oque embaralhar teoricamente, então fazemos que seja o total -1.
                    {
                        fora = _qtimes - 1; // Vai receber o total - 1.
                    }


                    golscasaR[i][x] = new TextBox();
                    golscasaR[i][x].Size = new Size(33, 22);

                    golsforaR[i][x] = new TextBox();
                    golsforaR[i][x].Size = new Size(33, 22);

                    timecasa[i][x] = new Label();
                    timecasa[i][x].AutoSize = true;
                    timecasa[i][x].Text = timescopy[fora];
                    timecasa[i][x].Width = 34;
                    timecasa[i][x].Height = 14;
                    timecasa[i][x].Font = new Font("Arial", 10);

                    timefora[i][x] = new Label();
                    timefora[i][x].AutoSize = true;
                    timefora[i][x].Text = times[casa];
                    timefora[i][x].Width = 34;
                    timefora[i][x].Height = 14;
                    timefora[i][x].Font = new Font("Arial", 10);

                    if (i == 0)
                    {

                        timecasa[i][x].Left = 42;
                        timefora[i][x].Left = 40 + distancia;

                        if (x == 0)
                        {
                            timecasa[i][x].Top = Altura * (x + 1);
                            timefora[i][x].Top = Altura * (x + 1);
                        }
                        else
                        {
                            timecasa[i][x].Top = timecasa[i][x - 1].Top + 30;
                            timefora[i][x].Top = timefora[i][x - 1].Top + 30;
                        }

                        golscasaR[i][x].Left = timecasa[i][x].Left - 37;
                        golsforaR[i][x].Left = timefora[i][x].Left + distancia;

                        golscasaR[i][x].Top = timecasa[i][x].Top - 3;
                        golsforaR[i][x].Top = timefora[i][x].Top - 3;
                    }
                    else
                    {
                        timecasa[i][x].Left = golscasa[i - 1][x].Left + 100;
                        timefora[i][x].Left = timecasa[i][x].Left + distancia;
                        if (x == 0)
                        {
                            timecasa[i][x].Top = Altura;
                            timefora[i][x].Top = Altura;

                            if (alterado == true)
                            {
                                timecasa[i][x].Top = timecasa[i - 1][0].Top;
                                timefora[i][x].Top = timefora[i - 1][0].Top;
                            }


                        }
                        else
                        {
                            timecasa[i][x].Top = timecasa[i][x - 1].Top + 30;
                            timefora[i][x].Top = timefora[i][x - 1].Top + 30;

                            timecasa[i][x].Top = timecasa[i][x - 1].Top + 30;
                            timefora[i][x].Top = timefora[i][x - 1].Top + 30;
                        }
                        if (golscasa[i - 1][x].Left > 640)
                        {
                            if (top == true)
                            {
                                I = i;
                                top = false;
                            }
                            timecasa[i][x].Left = 42;
                            timefora[i][x].Left = 40 + distancia;
                            if (i % I == 0 && II != i)
                            {
                                II = i;
                                Altura *= 2;
                            }

                            if (x == 0)
                            {
                                timecasa[i][x].Top = timecasa[i - 1][(_qtimes - 1) / 2].Top + 80;
                                timefora[i][x].Top = timecasa[i - 1][(_qtimes - 1) / 2].Top + 80;
                                alterado = true;
                            }

                            else
                            {
                                timecasa[i][x].Top = timecasa[i][x - 1].Top + 30;
                                timefora[i][x].Top = timefora[i][x - 1].Top + 30;
                                alterado = true;
                            }

                        }

                        golscasaR[i][x].Left = timecasa[i][x].Left - 37;
                        golsforaR[i][x].Left = timefora[i][x].Left + distancia;

                        golscasaR[i][x].Top = timecasa[i][x].Top - 3;
                        golsforaR[i][x].Top = timefora[i][x].Top - 3;
                    }


                    if (i == _qtimes - 2)
                    {
                        alto = timecasa[i][x].Top;
                    }

                }

                partida[i] = new Label();
                partida[i].AutoSize = true;
                partida[i].Text = "Rodada " + (i + 1);
                partida[i].Width = 34;
                partida[i].Height = 14;
                partida[i].Font = new Font("Arial", 10);

                partida[i].Left = timecasa[i][0].Left + distancia - 40;
                partida[i].Top = timecasa[i][0].Top - 30;

                //partidatop[i] = partida[i].Top;

                panel1.Controls.AddRange(timefora[i]);
                panel1.Controls.AddRange(timecasa[i]);
                panel1.Controls.AddRange(golscasaR[i]);
                panel1.Controls.AddRange(golsforaR[i]);

                panel1.Controls.Add(partida[i]);


            }
        }


        public void Impar(Panel panel1)
        {
            int alto = 0;
            timecasa = new Label[_qtimes][];
            timefora = new Label[_qtimes][];
            Label[] partida = new Label[_qtimes];
            golscasa = new TextBox[_qtimes][];
            golsfora = new TextBox[_qtimes][];
            golscasaR = new TextBox[_qtimes][];
            golsforaR = new TextBox[_qtimes][];
            Boolean top = true;
            Boolean alterado = false;
            Boolean calculado = false;
            int II = 0;
            int I = 0;
            int Altura = 30;
            SQL sql = new SQL();

            Embaralhador.Shuffle(times); // Embaralha os times.
            List<string> timescopy = new List<string>(times); // Faz uma copa da lista Times.
            for (int i = 0; i <= (_qtimes - 1); i++) // qtimes/2 pois, em 4 Times digitados, iria aparecer o resultado 2x. Alemanha x Holanda / Holanda x Alemanha.
            {

                timecasa[i] = new Label[_qtimes - 1];
                timefora[i] = new Label[_qtimes - 1];
                golscasa[i] = new TextBox[_qtimes - 1];
                golsfora[i] = new TextBox[_qtimes - 1];
                int xfora = 0;

                check = _qtimes - 1; // Se caso qtimes for até o final, em uma tábela de 8 Times, que o correto deveria ser 4 Jogos, ele vai vai ser 5 Rodadas ( Repetindo a primeira ). Então quando ele chegar -1, vai quebrar o for impedindo que conte novamente.
                if (i == check) // Faz a verificação se já chegou a qtimes -1.
                {
                    break; // Quebra o For.
                }
                for (int x = 0; x <= (_qtimes - 1) / 2; x++)
                {
                    int casa = (i + x) % (_qtimes - 1); // Faz a escolha do time.
                    int fora = (_qtimes - 1 - x + i) % (_qtimes - 1); // Faz a escolha do time.

                    if (x == 0) // Se caso x == 0, não vai ter oque embaralhar teoricamente, então fazemos que seja o total -1.
                    {
                        fora = _qtimes - 1; // Vai receber o total - 1.
                    }


                    golscasa[i][x] = new TextBox();
                    golscasa[i][x].Size = new Size(33, 22);

                    golsfora[i][x] = new TextBox();
                    golsfora[i][x].Size = new Size(33, 22);

                    timecasa[i][x] = new Label();
                    timecasa[i][x].AutoSize = true;
                    timecasa[i][x].Text = times[casa];
                    timecasa[i][x].Font = new Font("Arial", 10);

                    timefora[i][x] = new Label();
                    timefora[i][x].AutoSize = true;
                    timefora[i][x].Text = timescopy[fora];
                    timefora[i][x].Font = new Font("Arial", 10);

                    if (timefora[i][x].Text == ghost || timecasa[i][x].Text == ghost)
                    {
                        xfora = x;
                    }

                    if (i == 0)
                    {

                        timecasa[i][x].Left = 42;
                        timefora[i][x].Left = 40 + distancia;

                        timecasa[i][x].Top = Altura * (x + 1);
                        timefora[i][x].Top = Altura * (x + 1);

                        golsfora[i][x].Left = timecasa[i][x].Left - 37;
                        golscasa[i][x].Left = timefora[i][x].Left + distancia;

                        golsfora[i][x].Top = timecasa[i][x].Top - 3;
                        golscasa[i][x].Top = timefora[i][x].Top - 3;
                    }
                    else
                    {
                        timecasa[i][x].Left = golscasa[i - 1][x].Left + 100;
                        timefora[i][x].Left = timecasa[i][x].Left + distancia;
                        if (x == 0)
                        {
                            timecasa[i][x].Top = Altura;
                            timefora[i][x].Top = Altura;

                            if (alterado == true)
                            {
                                timecasa[i][x].Top = timecasa[i - 1][0].Top;
                                timefora[i][x].Top = timefora[i - 1][0].Top;
                            }


                        }
                        else
                        {
                            timecasa[i][x].Top = timecasa[i][x - 1].Top + 30;
                            timefora[i][x].Top = timefora[i][x - 1].Top + 30;
                        }
                        if (golscasa[i - 1][x].Left > 650)
                        {
                            if (top == true)
                            {
                                I = i;
                                top = false;
                            }
                            timecasa[i][x].Left = 42;
                            timefora[i][x].Left = 40 + distancia;
                            if (calculado == false)
                            {
                                Altura = Altura * (_qtimes - 1) / 2;
                                calculado = true;
                            }
                            if (i % I == 0 && II != i)
                            {
                                II = i;
                                Altura *= 2;
                            }

                            if (x == 0)
                            {
                                timecasa[i][x].Top = timecasa[i - 1][(_qtimes - 1) / 2].Top + 80;
                                timefora[i][x].Top = timecasa[i - 1][(_qtimes - 1) / 2].Top + 80;
                                alterado = true;
                            }
                            else
                            {
                                timecasa[i][x].Top = timecasa[i][x - 1].Top + 30;
                                timefora[i][x].Top = timefora[i][x - 1].Top + 30;
                                alterado = true;
                            }

                        }

                        golsfora[i][x].Left = timecasa[i][x].Left - 37;
                        golscasa[i][x].Left = timefora[i][x].Left + distancia;

                        golsfora[i][x].Top = timecasa[i][x].Top - 3;
                        golscasa[i][x].Top = timefora[i][x].Top - 3;
                    }

                    if (i == _qtimes - 2)
                    {
                        alto = timecasa[i][x].Top;
                    }

                }

                for (int x = 0; x <= (_qtimes - 1) / 2; x++)
                {
                    if (x == xfora)
                    {
                        String time = "";
                        golscasa[i][x].Visible = false;
                        golscasa[i][x].Enabled = false;
                        golsfora[i][x].Enabled = false;
                        golsfora[i][x].Visible = false;
                        if (timecasa[i][x].Text == ghost)
                        {
                            time = "Descanço: " + timefora[i][x].Text;
                        }
                        if (timefora[i][x].Text == ghost)
                        {
                            time = "Descanço: " + timecasa[i][x].Text;
                        }
                        timecasa[i][x].Text = timecasa[i][(_qtimes - 1) / 2].Text;
                        timefora[i][x].Text = timefora[i][(_qtimes - 1) / 2].Text;
                        golscasa[i][(_qtimes - 1) / 2].Top = timefora[i][x].Top - 3;
                        golsfora[i][(_qtimes - 1) / 2].Top = timefora[i][x].Top - 3;
                        timefora[i][(_qtimes - 1) / 2].Visible = false;
                        timecasa[i][(_qtimes - 1) / 2].Text = time;
                        break;
                    }
                }

                partida[i] = new Label();
                partida[i].AutoSize = true;
                partida[i].Text = "Rodada " + (i + 1);
                partida[i].Width = 34;
                partida[i].Height = 14;
                partida[i].Font = new Font("Arial", 10);

                partida[i].Left = timecasa[i][0].Left + distancia - 40;
                partida[i].Top = timecasa[i][0].Top - 30;

                //partidatop[i] = partida[i].Top;

                panel1.Controls.AddRange(timefora[i]);
                panel1.Controls.AddRange(timecasa[i]);
                panel1.Controls.AddRange(golscasa[i]);
                panel1.Controls.AddRange(golsfora[i]);

                panel1.Controls.Add(partida[i]);


            }

            Label returnol = new Label();

            returnol.AutoSize = true;
            returnol.Text = "_______________________________________________________________________________________________________";
            returnol.Font = new Font("Arial", 10);

            returnol.Left = +40;
            returnol.Top = alto + 83;

            panel1.Controls.Add(returnol);

            Label Returno = new Label();
            Returno.AutoSize = true;
            Returno.Text = "RETURNO";
            Returno.Font = new Font("Arial", 10);

            Returno.Left = 456;
            Returno.Top = alto + 70;

            panel1.Controls.Add(Returno);


            Label returnol2 = new Label();
            returnol2.AutoSize = true;
            returnol2.Text = "_______________________________________________________________________________________________________";
            returnol2.Font = new Font("Arial", 10);

            returnol2.Left = +40;
            returnol2.Top = alto + 45;

            panel1.Controls.Add(returnol2);

            top = true;
            alterado = false;
            calculado = false;
            II = 0;
            I = 0;
            alto = 0;
            Altura = returnol.Top + 70;

            for (int i = 0; i <= (_qtimes - 1); i++) // qtimes/2 pois, em 4 Times digitados, iria aparecer o resultado 2x. Alemanha x Holanda / Holanda x Alemanha.
            {

                timecasa[i] = new Label[_qtimes - 1];
                timefora[i] = new Label[_qtimes - 1];
                golscasaR[i] = new TextBox[_qtimes - 1];
                golsforaR[i] = new TextBox[_qtimes - 1];
                int xfora = 0;

                check = _qtimes - 1; // Se caso qtimes for até o final, em uma tábela de 8 Times, que o correto deveria ser 4 Jogos, ele vai vai ser 5 Rodadas ( Repetindo a primeira ). Então quando ele chegar -1, vai quebrar o for impedindo que conte novamente.
                if (i == check) // Faz a verificação se já chegou a qtimes -1.
                {
                    break; // Quebra o For.
                }
                for (int x = 0; x <= (_qtimes - 1) / 2; x++)
                {
                    int casa = (i + x) % (_qtimes - 1); // Faz a escolha do time.
                    int fora = (_qtimes - 1 - x + i) % (_qtimes - 1); // Faz a escolha do time.

                    if (x == 0) // Se caso x == 0, não vai ter oque embaralhar teoricamente, então fazemos que seja o total -1.
                    {
                        fora = _qtimes - 1; // Vai receber o total - 1.
                    }


                    golscasaR[i][x] = new TextBox();
                    golscasaR[i][x].Size = new Size(33, 22);

                    golsforaR[i][x] = new TextBox();
                    golsforaR[i][x].Size = new Size(33, 22);

                    timecasa[i][x] = new Label();
                    timecasa[i][x].AutoSize = true;
                    timecasa[i][x].Text = timescopy[fora];
                    timecasa[i][x].Width = 34;
                    timecasa[i][x].Height = 14;
                    timecasa[i][x].Font = new Font("Arial", 10);

                    timefora[i][x] = new Label();
                    timefora[i][x].AutoSize = true;
                    timefora[i][x].Text = times[casa];
                    timefora[i][x].Width = 34;
                    timefora[i][x].Height = 14;
                    timefora[i][x].Font = new Font("Arial", 10);

                    if (timefora[i][x].Text == ghost || timecasa[i][x].Text == ghost)
                    {
                        xfora = x;
                    }

                    if (i == 0)
                    {

                        timecasa[i][x].Left = 42;
                        timefora[i][x].Left = 40 + distancia;

                        if (x == 0)
                        {
                            timecasa[i][x].Top = Altura * (x + 1);
                            timefora[i][x].Top = Altura * (x + 1);
                        }
                        else
                        {
                            timecasa[i][x].Top = timecasa[i][x - 1].Top + 30;
                            timefora[i][x].Top = timefora[i][x - 1].Top + 30;
                        }

                        golscasaR[i][x].Left = timecasa[i][x].Left - 37;
                        golsforaR[i][x].Left = timefora[i][x].Left + distancia;

                        golscasaR[i][x].Top = timecasa[i][x].Top - 3;
                        golsforaR[i][x].Top = timefora[i][x].Top - 3;
                    }
                    else
                    {
                        timecasa[i][x].Left = golscasa[i - 1][x].Left + 100;
                        timefora[i][x].Left = timecasa[i][x].Left + distancia;
                        if (x == 0)
                        {
                            timecasa[i][x].Top = Altura;
                            timefora[i][x].Top = Altura;

                            if (alterado == true)
                            {
                                timecasa[i][x].Top = timecasa[i - 1][0].Top;
                                timefora[i][x].Top = timefora[i - 1][0].Top;
                            }


                        }
                        else
                        {
                            timecasa[i][x].Top = timecasa[i][x - 1].Top + 30;
                            timefora[i][x].Top = timefora[i][x - 1].Top + 30;

                            timecasa[i][x].Top = timecasa[i][x - 1].Top + 30;
                            timefora[i][x].Top = timefora[i][x - 1].Top + 30;
                        }
                        if (golscasaR[i - 1][x].Left > 650)
                        {
                            if (top == true)
                            {
                                I = i;
                                top = false;
                            }
                            timecasa[i][x].Left = 42;
                            timefora[i][x].Left = 40 + distancia;
                            if (i % I == 0 && II != i)
                            {
                                II = i;
                                Altura *= 2;
                            }

                            if (x == 0)
                            {
                                timecasa[i][x].Top = timecasa[i - 1][(_qtimes - 1) / 2].Top + 80;
                                timefora[i][x].Top = timecasa[i - 1][(_qtimes - 1) / 2].Top + 80;
                                alterado = true;
                            }

                            else
                            {
                                timecasa[i][x].Top = timecasa[i][x - 1].Top + 30;
                                timefora[i][x].Top = timefora[i][x - 1].Top + 30;
                                alterado = true;
                            }

                        }

                        golscasaR[i][x].Left = timecasa[i][x].Left - 37;
                        golsforaR[i][x].Left = timefora[i][x].Left + distancia;

                        golscasaR[i][x].Top = timecasa[i][x].Top - 3;
                        golsforaR[i][x].Top = timefora[i][x].Top - 3;
                    }


                    if (i == _qtimes - 2)
                    {
                        alto = timecasa[i][x].Top;
                    }

                }

                partida[i] = new Label();
                partida[i].AutoSize = true;
                partida[i].Text = "Rodada " + (i + 1);
                partida[i].Width = 34;
                partida[i].Height = 14;
                partida[i].Font = new Font("Arial", 10);

                partida[i].Left = timecasa[i][0].Left + 46;
                partida[i].Top = timecasa[i][0].Top - 30;

                for (int x = 0; x <= (_qtimes - 1) / 2; x++)
                {
                    if (x == xfora)
                    {
                        String time = "";
                        golscasaR[i][x].Visible = false;
                        golscasaR[i][x].Enabled = false;
                        golsforaR[i][x].Enabled = false;
                        golsforaR[i][x].Visible = false;
                        if (timecasa[i][x].Text == ghost)
                        {
                            time = "Descanço: " + timefora[i][x].Text;
                        }
                        if (timefora[i][x].Text == ghost)
                        {
                            time = "Descanço: " + timecasa[i][x].Text;
                        }
                        timecasa[i][x].Text = timecasa[i][(_qtimes - 1) / 2].Text;
                        timefora[i][x].Text = timefora[i][(_qtimes - 1) / 2].Text;
                        golscasaR[i][(_qtimes - 1) / 2].Top = timefora[i][x].Top - 3;
                        golsforaR[i][(_qtimes - 1) / 2].Top = timefora[i][x].Top - 3;
                        timefora[i][(_qtimes - 1) / 2].Visible = false;
                        timecasa[i][(_qtimes - 1) / 2].Text = time;
                    }

                    //partidatop[i] = partida[i].Top;

                    panel1.Controls.AddRange(timefora[i]);
                    panel1.Controls.AddRange(timecasa[i]);
                    panel1.Controls.AddRange(golscasaR[i]);
                    panel1.Controls.AddRange(golsforaR[i]);

                    panel1.Controls.Add(partida[i]);


                }
            }
        }
    }
}


