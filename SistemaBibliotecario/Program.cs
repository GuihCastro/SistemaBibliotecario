using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaBibliotecario.UI;

namespace SistemaBibliotecario
{
    /// <summary>
    /// Classe principal do sistema bibliotecário.
    /// </summary>
    internal static class Program
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["BibliotecaConnection"].ConnectionString;

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// Abre o formulário inicial do sistema.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Conexão com o banco de dados estabelecida com sucesso.");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormIndex());
        }
    }
}
