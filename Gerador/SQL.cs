﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Gerador
{
    class SQL
    {        
        SQLiteConnection connection = new SQLiteConnection("Data Source=Tabela.sqlite;Version=3;Pooling=True;Max Pool Size=100;");

        public void createTable()
        {
            try
            {
                connection.Open();
                SQLiteCommand cmd = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Tabela(ID INTEGER PRIMARY KEY,Time VARCHAR(100),Gols INT,Pontos INT,SaldoDeGols INT,Jogos INT,Vitorias INT,Derrotas INT,Empates INT);", connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("Falha ao criar Tabela!\n\n" + e, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void resetarTabela()
        {
            try
            {
                connection.Open();
                SQLiteCommand cmd = new SQLiteCommand("TRUNCATE TABLE Tabela", connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Tabela resetada com Sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Falha ao deletar Tabela!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public void dataGridViewUpdate(DataGridView Tabela)
        {
            try
            {
                connection.Open();
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM Tabela", connection);
                DataSet DS = new DataSet();
                dataAdapter.Fill(DS);
                DataTable DT = new DataTable();
                DT = DS.Tables[0];
                Tabela.DataSource = DT;
                connection.Close();
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("Falha ao carregar dados da Tabela\n\n" + e, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void deletarTime(int ID)
        {
            try
            {
                SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Tabela WHERE ID = @id;", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Deletado com Sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao Deletar \n\n" + ex, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void insertTime(String time)
        {
            try
            {                
                connection.Open();
                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Tabela(Time,Gols,Pontos,SaldoDeGols,Jogos,Vitorias,Derrotas,Empates) VALUES(@Time,0,0,0,0,0,0,0);", connection);
                cmd.Parameters.AddWithValue("@Time", time);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException SqlE)
            {
                connection.Close();
                MessageBox.Show("Falha ao Adicionar Time! \n\n" + SqlE, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show("Falha ao Adicionar Time! \n\n" + e, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool consultarTime(String time)
        {
            try
            {
                connection.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT ID FROM Tabela WHERE Time = @time;", connection);
                cmd.Parameters.AddWithValue("@time", time);
                SQLiteDataReader resultado = cmd.ExecuteReader();
                if (!resultado.HasRows)
                {
                    connection.Close();
                    insertTime(time);
                    return true;
                }
                connection.Close();
                return true;
            }
            catch (SqlException sqlE)
            {
                connection.Close();
                MessageBox.Show("Falha ao checar se o Time '" + time + "' existe no Banco! \n\n" + sqlE, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show("Falha ao checar se o Time '" + time + "' existe no Banco! \n\n" + e, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void updateValores(int gol, String time, int vitoria, int derrota, int empate)
        {
            if (consultarTime(time) == true)
            {
                connection.Open();
                SQLiteCommand cmd = new SQLiteCommand("UPDATE Tabela SET Gols = Gols+@gols, Vitorias = Vitorias+@vit, Derrotas = Derrotas+@derr, Jogos = Jogos+1, Empates = Empates+@emp WHERE Time = @time;", connection);
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
