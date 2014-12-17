﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerador
{
    class SQL
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Zack\documents\visual studio 2013\Projects\Gerador\Gerador\Tabela.mdf;Integrated Security=True");

        public void insertTime(String time)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Tabela(Time,Gols,Pontos,SaldoDeGols,Jogos,Vitórias,Derrotas,Empates) VALUES(@Time,0,0,0,0,0,0,0);", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@Time", time);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException SqlE)
            {
                MessageBox.Show("Falha ao Adicionar Time! \n\n" + SqlE, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("Falha ao Adicionar Time! \n\n" + e, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool consultarTime(String time)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT ID FROM Tabela WHERE Time = @time;", connection);
                cmd.Parameters.AddWithValue("@time", time);
                connection.Open();
                SqlDataReader resultado = cmd.ExecuteReader();
                if (!resultado.HasRows)
                {
                    connection.Close();
                    insertTime(time);
                }
                connection.Close();
                return true;
            }
            catch (SqlException sqlE)
            {
                MessageBox.Show("Falha ao checar se o Time '" + time + "' existe no Banco! \n\n" + sqlE, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Falha ao checar se o Time '" + time + "' existe no Banco! \n\n" + e, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void insertValores(int gol, String time, int vitoria, int derrota, int empate)
        {
            if (consultarTime(time) == true)
            {
                SqlCommand cmd = new SqlCommand("UPDATE Tabela SET Gols = Gols+@gols, Vitórias = Vitórias+@vit, Derrotas = Derrotas+@derr, Jogos = Jogos+1, Empates = Empates+@emp WHERE Time = @time;", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@gols", gol);
                cmd.Parameters.AddWithValue("@vit", vitoria);
                cmd.Parameters.AddWithValue("@derr", derrota);
                cmd.Parameters.AddWithValue("@emp", empate);
                cmd.Parameters.AddWithValue("@time", time);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
