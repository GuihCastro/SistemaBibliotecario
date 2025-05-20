using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaBibliotecario.Models;

namespace SistemaBibliotecario.DAL
{
    /// <summary>
    /// Classe de comunicação com o Banco de Dados (Data Access Layer) para a entidade Aluno.
    /// Implementa as operações CRUD (Create, Read, Update, Delete).
    /// </summary>
    public class AlunoDAL
    {
        // String de conexão com o banco de dados
        private static string _connectionString = ConfigurationManager.ConnectionStrings["BibliotecaConnection"].ConnectionString;

        /// <summary>
        /// Construtor padrão da classe.
        /// </summary>
        public AlunoDAL() { }

        /// <summary>
        /// Método para inserir um novo aluno no banco de dados.
        /// </summary>
        /// <param name="aluno">Objeto do tipo Aluno com os dados a serem inseridos</param>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public void Inserir(Aluno aluno)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand
                    (
                        "INSERT INTO Alunos (RA, Nome, Email, Telefone, DataNascimento) " +
                        "VALUES (@RA, @Nome, @Email, @Telefone, @DataNascimento)",
                        connection
                    );
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@RA", SqlDbType.Int).Value = aluno.RA;
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = aluno.Nome;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = aluno.Email;
                    cmd.Parameters.Add("@Telefone", SqlDbType.NVarChar, 20).Value = aluno.Telefone;
                    cmd.Parameters.Add("@DataNascimento", SqlDbType.Date).Value = aluno.DataNascimento;
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao inserir aluno: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Método para buscar um aluno pelo RA (Registro Acadêmico).
        /// </summary>
        /// <param name="ra">RA do aluno a ser buscado</param>
        /// <returns>Objeto do tipo Aluno encontrado</returns>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public Aluno BuscarPorRA(int ra)
        {
            Aluno aluno = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand
                    (
                        "SELECT * FROM Alunos " +
                        "WHERE RA = @RA",
                        connection
                    );
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@RA", SqlDbType.Int).Value = ra;
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            aluno = new Aluno
                            {
                                RA = (int)reader["RA"],
                                Nome = reader["Nome"]?.ToString() ?? string.Empty,
                                Email = reader["Email"]?.ToString() ?? string.Empty,
                                Telefone = reader["Telefone"]?.ToString() ?? string.Empty,
                                DataNascimento = (DateTime)reader["DataNascimento"]
                            };
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar aluno: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
            return aluno;
        }

        /// <summary>
        /// Método para atualizar os dados de um aluno no banco de dados.
        /// </summary>
        /// <param name="aluno">Objeto Aluno com os dados atualizados</param>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public void Atualizar(Aluno aluno)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand
                    (
                        "UPDATE Alunos " +
                        "SET Nome = @Nome, Email = @Email, Telefone = @Telefone, DataNascimento = @DataNascimento " +
                        "WHERE RA = @RA",
                        connection
                    );
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@RA", SqlDbType.Int).Value = aluno.RA;
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = aluno.Nome;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = aluno.Email;
                    cmd.Parameters.Add("@Telefone", SqlDbType.NVarChar, 20).Value = aluno.Telefone;
                    cmd.Parameters.Add("@DataNascimento", SqlDbType.Date).Value = aluno.DataNascimento;
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao atualizar aluno: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Método para excluir um aluno do banco de dados pelo RA.
        /// </summary>
        /// <param name="ra">RA do aluno a ser excluído</param>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public void Excluir(int ra)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand
                    (
                        "DELETE FROM Alunos " +
                        "WHERE RA = @RA",
                        connection
                    );
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@RA", SqlDbType.Int).Value = ra;
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao deletar aluno: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Método para listar todos os alunos cadastrados no banco de dados.
        /// </summary>
        /// <returns>Lista de Objetos Aluno existentes no Banco de Dados</returns>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public List<Aluno> Listar()
        {
            List<Aluno> alunos = new List<Aluno>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Alunos", connection);
                    cmd.CommandType = System.Data.CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Aluno aluno = new Aluno
                            {
                                RA = (int)reader["RA"],
                                Nome = reader["Nome"]?.ToString() ?? string.Empty,
                                Email = reader["Email"]?.ToString() ?? string.Empty,
                                Telefone = reader["Telefone"]?.ToString() ?? string.Empty,
                                DataNascimento = (DateTime)reader["DataNascimento"]
                            };
                            alunos.Add(aluno);
                        }
                    }
    
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao listar alunos: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
            return alunos;
        }
    }
}
