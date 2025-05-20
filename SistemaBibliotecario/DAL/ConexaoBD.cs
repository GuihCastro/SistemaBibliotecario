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
    /// <summary>
    /// Classe responsável por gerenciar a conexão com o banco de dados.
    /// </summary>
    internal class ConexaoBD
    {
        // Obter a connection string do App.config
        private static string _connectionString = ConfigurationManager.ConnectionStrings["BibliotecaConnection"].ConnectionString;

        /// <summary>
        /// Método para testar a conexão com o banco de dados.
        /// </summary>
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
