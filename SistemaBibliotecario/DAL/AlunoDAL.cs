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
    public class AlunoDAL
    {
        // String de conexão com o banco de dados
        private static string _connectionString = ConfigurationManager.ConnectionStrings["BibliotecaConnection"].ConnectionString;

        // Construtor padrão
        public AlunoDAL() { }

        // Métodos CRUD para o banco de dados
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
            }
        }

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
                                Nome = reader["Nome"]?.ToString() ?? string.Empty, // Trata nulidade
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
            }
            return aluno;
        }

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
            }
        }

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
            }
        }

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
            }
            return alunos;
        }
    }
}
