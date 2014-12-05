using System;
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
                SqlCommand cmd = new SqlCommand("INSERT INTO Tabela(Time) VALUES(@Time);", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@Time", time);
                cmd.ExecuteNonQuery();
                connection.Close();
                //MessageBox.Show("Time inserido com Sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void consultarTime(String time)
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
            }
            catch (SqlException sqlE)
            {
                MessageBox.Show("Falha ao checar se o Time '" + time + "' existe no Banco! \n\n" + sqlE, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("Falha ao checar se o Time '" + time + "' existe no Banco! \n\n" + e, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
