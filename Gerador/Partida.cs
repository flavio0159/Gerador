using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Gerador
{
    class Partida
    {
        List<string> times = new List<string>(); //Criar uma Lista 
        double check; //Usado para transforma a quantidade números ímpar em par.
        String ghost = Convert.ToString(DateTime.Now.Millisecond);
        private int _qtimes; // Armazenar a Quantidade de Times digitada pela pessoa      

        public int qtimes
        {
            get { return _qtimes; }
            set { _qtimes = value; }
        }

        public bool setTimePar()
        {
            try
            {
                int index = 0;
                for (int i = 0; i <= _qtimes - 1; i++) // qtimes - 1, pois, como ocupada a posição 0, se caso o quantidade de Tiems fosse 12, haveria 13 times.
                {
                    times.Add(Interaction.InputBox("Digite o nome do Time:           " + "Total: " + i + "/" + _qtimes, "Times")); // Dialogo para digitar os times
                    if (times[i].Length > 1 && times[i].Length > index)
                    {
                        index = times[i].Length;
                    }
                    if (times[i] == "")
                    { // Se caso a pessoa clicar em cancelar, mesmo clicando a variável recebe um valor em branco. Caso seja branco, logo entende que foi cancelado.
                        break; // Quebra o For.
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

        public void setTimesImpar()
        {
            for (int i = 0; i <= _qtimes - 1; i++) // qtimes - 1, pois, como ocupada a posição 0, se caso o quantidade de Tiems fosse 12, haveria 13 times.
            {
                if (i == _qtimes - 1) // Quando chegar na última posição, a pessoa não vai ter o que digitar. Então automaticamente adiciona um time fantasma para não ficar um espaço null.
                {
                    Embaralhador.Shuffle(times); // Embaralha os times para que o time fantasma fique em uma posição aleatória
                    times.Add(ghost); // Adiciona a Variável Ghost.
                    break; // Quebra o For.
                }
                times.Add(Interaction.InputBox("Digite o nome do Time:           " + "Total: " + i + "/" + (_qtimes - 1), "Times")); // Dialogo para digitar os times
                if (times[i] == "")
                { // Se caso a pessoa clicar em cancelar, mesmo clicando a variável recebe um valor em branco. Caso seja branco, logo entende que foi cancelado.

                }
            }
        }

        public void clearList()
        {
            this.times.Clear();
        }

        public void Par(Panel panel1, ProgressBar barProgress, Label labelProgress)
        {
            int alto = 0;
            Label[][] timecasa = new Label[_qtimes][];
            Label[][] timefora = new Label[_qtimes][];
            Label[] partida = new Label[_qtimes];
            TextBox[][] golscasa = new TextBox[_qtimes][];
            TextBox[][] golsfora = new TextBox[_qtimes][];
            TextBox[][] golscasaR = new TextBox[_qtimes][];
            TextBox[][] golsforaR = new TextBox[_qtimes][];
            Boolean top = true;
            Boolean alterado = false;
            Boolean calculado = false;
            int II = 0;
            int I = 0;
            int Altura = 30;
            SQL sql = new SQL();

            barProgress.Increment(20);
            for (int i = 0; i <= _qtimes - 1; i++)
            {
                sql.consultarTime(times[i]);                
                labelProgress.Left = barProgress.Left -3 ;
                labelProgress.Text = "Verificando se o time '"+times[i]+"' existe no Banco...";
            }

            barProgress.Increment(40);
            labelProgress.Text = "Gerando tabela...";
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
                        timefora[i][x].Left = 130;

                        timecasa[i][x].Top = Altura * (x + 1);
                        timefora[i][x].Top = Altura * (x + 1);

                        golscasa[i][x].Left = timecasa[i][x].Left - 37;
                        golsfora[i][x].Left = timefora[i][x].Left + 80;

                        golscasa[i][x].Top = timecasa[i][x].Top - 3;
                        golsfora[i][x].Top = timefora[i][x].Top - 3;
                    }
                    else
                    {
                        timecasa[i][x].Left = timecasa[i - 1][x].Left + 310;
                        timefora[i][x].Left = timefora[i - 1][x].Left + 310;
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
                        if (timecasa[i][x].Left > 880 || timefora[i][x].Left > 880)
                        {
                            if (top == true)
                            {
                                I = i;
                                top = false;
                            }
                            timecasa[i][x].Left = 42;
                            timefora[i][x].Left = 130;
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

                        golscasa[i][x].Left = timecasa[i][x].Left - 37;
                        golsfora[i][x].Left = timefora[i][x].Left + 80;

                        golscasa[i][x].Top = timecasa[i][x].Top - 3;
                        golsfora[i][x].Top = timefora[i][x].Top - 3;
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

            barProgress.Increment(40);
            labelProgress.Text = "Fazendo Returno...";
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
                        timefora[i][x].Left = 130;

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
                        golsforaR[i][x].Left = timefora[i][x].Left + 80;

                        golscasaR[i][x].Top = timecasa[i][x].Top - 3;
                        golsforaR[i][x].Top = timefora[i][x].Top - 3;
                    }
                    else
                    {
                        timecasa[i][x].Left = timecasa[i - 1][x].Left + 310;
                        timefora[i][x].Left = timefora[i - 1][x].Left + 310;
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
                        if (timecasa[i][x].Left > 750 || timefora[i][x].Left > 750)
                        {
                            if (top == true)
                            {
                                I = i;
                                top = false;
                            }
                            timecasa[i][x].Left = 42;
                            timefora[i][x].Left = 130;
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
                        golsforaR[i][x].Left = timefora[i][x].Left + 80; ;

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

                //partidatop[i] = partida[i].Top;

                panel1.Controls.AddRange(timefora[i]);
                panel1.Controls.AddRange(timecasa[i]);
                panel1.Controls.AddRange(golscasaR[i]);
                panel1.Controls.AddRange(golsforaR[i]);

                panel1.Controls.Add(partida[i]);


            }
            if (barProgress.Value == 100)
            {
                MessageBox.Show("Left: " + barProgress.Left + " Right " + barProgress.Right);
                labelProgress.Text = "Finalizado!";
                labelProgress.Left = barProgress.Left + 82;
            }
        }
    }
}

//    public void Impar(){


//            qtimes = qtimes + 1; // Adiciono a quantidade de times ímpar, +1, se tornando um número Par.

//                List<string> timescopy = new List<string>(times); // Faz uma cópia da lista Time.
//                for (int i = 0; i <= (qtimes - 1); i++) // qtimes/2 pois, em 4 Times digitados, iria aparecer o resultado 2x. Alemanha x Holanda / Holanda x Alemanha.
//                {
//                    check = qtimes - 1; // Se caso qtimes for até o final, em uma tábela de 8 Times, que o correto deveria ser 4 Jogos, ele vai vai ser 5 Rodadas ( Repetindo a primeira ). Então quando ele chegar -1, vai quebrar o for impedindo que conte novamente.
//                    if (i == check) // Faz a verificação se já chegou.
//                    {
//                        break; // Quebra o For.
//                    }
//                    for (int x = 0; x <= (qtimes - 1) / 2; x++)
//                    {
//                        int casa = (i + x) % (qtimes - 1); // Faz a escolha do time.
//                        int fora = (qtimes - 1 - x + i) % (qtimes - 1); // Faz a escolha do time.
//                        if (x == 0) // Se caso x == 0, não vai ter oque embaralhar teoricamente, então fazemos que seja o total -1.
//                        {
//                            fora = qtimes - 1; // Vai receber total - 1.
//                        }

//                        // Como na escolha, todos tem jogar contra todos 1x, alguém vai ter que jogar contra o time Fantasma. 

//                        if (timescopy[fora] == ghost || times[casa] == ghost) // Verificar qual vai jogar contra o time Fantasma.
//                        {
//                            if (timescopy[fora] == ghost) // Verificar se a cópia do time jogou contra o Fantasma. Sendo que este, vai ser o descanço da rodada.
//                            {
//                                listaPartidas.Items.Add("Descanço: " + times[casa]); // Mostra qual time vai ficar descanço.
//                                listaPartidas.Items.Add(""); // Da um espaço para não ficar embolado.
//                            }
//                            if (times[casa] == ghost) // Verificar se o time jogou contra o Fantasma. Sendo que este, vai ser o descanço da rodada.
//                            {
//                                listaPartidas.Items.Add("Descanço: " + timescopy[casa]);
//                                listaPartidas.Items.Add(""); // Da um espaço para não ficar embolado.
//                            }
//                        }
//                        else // Se caso ninguém jogar contra Fantasma, executa este código.
//                        {
//                            listaPartidas.Items.Add(times[casa] + "  -x-  " + timescopy[fora]); // Adiciona os times na ListView.
//                        }
//                    }
//                    listaPartidas.Items.Add(""); // Um espaço parã não ficar embolado.
//                    returno = i + 1; // Necessário para o retorno continuar da última contagem e não zerar.
//                }
//                listaPartidas.Items.Add("///// RETURNO /////"); // Mostrar somente o Retorno.
//                listaPartidas.Items.Add(""); // Adiciona um espaço na ListView.
//                for (int i = 0; i <= (qtimes - 1); i++) // qtimes/2 pois, em 4 Times digitados, iria aparecer o resultado 2x. Alemanha x Holanda / Holanda x Alemanha.
//                {
//                    check = qtimes - 1; // Se caso qtimes for até o final, em uma tábela de 8 Times, que o correto deveria ser 4 Jogos, ele vai vai ser 5 Rodadas ( Repetindo a primeira ). Então quando ele chegar -1, vai quebrar o for impedindo que conte novamente.
//                    if (i == check) // Faz a verificação se já chegou.
//                    {
//                        break; // Quebra o código.
//                    }
//                    listaPartidas.Items.Add(returno + 1 + "º Rodada: "); // Mostra a rodada.
//                    returno++;
//                    for (int x = 0; x <= (qtimes - 1) / 2; x++)
//                    {

//                        int casa = (i + x) % (qtimes - 1); // Faz a escolha do time ( Não é aleatório, sempre será a mesma ). 
//                        int fora = (qtimes - 1 - x + i) % (qtimes - 1); // Faz a escolha do time ( Não é aleatório, sempre será a mesma ).
//                        if (x == 0) // Se caso x == 0, não vai ter oque embaralhar teoricamente, então fazemos que seja o total -1.
//                        {
//                            fora = qtimes - 1;
//                        }
//                        if (timescopy[fora] == ghost || times[casa] == ghost) // Verifica qual time vai jogar contra Fantasma.
//                        {
//                            if (timescopy[fora] == ghost) // Se a cópia do time jogar contra Fantasma, ela vai ser o descanço da partida.
//                            {
//                                listaPartidas.Items.Add("Descanço: " + times[casa]); // Mostra o descanço.
//                            }
//                            if (times[casa] == ghost) // Se o time jogar contra Fantasma, ela vai ser o descanço da partida.
//                            {
//                                listaPartidas.Items.Add("Descanço: " + timescopy[casa]); // Mostrar o descanço.
//                                listaPartidas.Items.Add(""); // Somente para não ficar em bolado
//                            }
//                        }
//                        else // Se ca~so não houver nenhum contra o time Fantasma.
//                        {
//                            listaPartidas.Items.Add(timescopy[fora] + "  -x-  " + times[casa]); // Adiciona os times na ListView.
//                        }
//                    }
//                    listaPartidas.Items.Add(""); // Adiciona um espaço para não ficar embolado.
//                }
//            // Caso o usuário cancele.
//            else
//            {
//                times.Clear(); // Limpa os times caso cancele.
//                cancel = false; // Cancel vira false.
//            }
//    }
//}
