using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaBibliotecario.DAL
{
    internal class ConexaoBD
    {
        // Obter a connection string do App.config
        private static string _connectionString = ConfigurationManager.ConnectionStrings["BibliotecaConnection"].ConnectionString;

        // Testando a conexão com o Banco
        public static void TestarConexao()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show
                    (
                        "Conexão com o Banco de Dados estabelecida com sucesso!",
                        "Sucesso!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (SqlException ex)
                {
                    MessageBox.Show
                    (
                        $"Erro ao conectar ao Banco de Dados: {ex.Message}",
                        "Falha na conexão",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }
    }
}
