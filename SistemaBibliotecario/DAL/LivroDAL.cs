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
    /// Classe de comunicação com o Banco de Dados (Data Access Layer) para a entidade Livro.
    /// Implementa as operações CRUD (Create, Read, Update, Delete).
    /// </summary>
    public class LivroDAL
    {
        // String de conexão com o banco de dados
        private static string _connectionString = ConfigurationManager.ConnectionStrings["BibliotecaConnection"].ConnectionString;

        /// <summary>
        /// Construtor padrão da classe.
        /// </summary>
        public LivroDAL() { }

        /// <summary>
        /// Método para inserir um novo livro no banco de dados.
        /// </summary>
        /// <param name="livro">Objeto do tipo Livro com os dados a serem inseridos</param>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public void Inserir(Livro livro)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand
                    (
                        "INSERT INTO Livros (Codigo, Titulo, Autor, Categoria, Editora, Disponivel) " +
                        "VALUES (@Codigo, @Titulo, @Autor, @Categoria, @Editora, @Disponivel)",
                        connection
                    );
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = livro.Codigo;
                    cmd.Parameters.Add("@Titulo", SqlDbType.NVarChar, 100).Value = livro.Titulo;
                    cmd.Parameters.Add("@Autor", SqlDbType.NVarChar, 100).Value = livro.Autor;
                    cmd.Parameters.Add("@Categoria", SqlDbType.NVarChar, 50).Value = livro.Categoria;
                    cmd.Parameters.Add("@Editora", SqlDbType.NVarChar, 50).Value = livro.Editora;
                    cmd.Parameters.Add("@Disponivel", SqlDbType.Bit).Value = livro.Disponivel;
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao inserir livro: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Método para buscar um livro pelo código no banco de dados.
        /// </summary>
        /// <param name="codigo">Código do livro a ser buscado</param>
        /// <returns>Objeto Livro encontrado ou null se não existir</returns>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public Livro BuscarPorCodigo(int codigo)
        {
            Livro livro = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand
                    (
                        "SELECT * FROM Livros " +
                        "WHERE Codigo = @Codigo",
                        connection
                    );
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigo;
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            livro = new Livro
                            {
                                Codigo = (int)reader["Codigo"],
                                Titulo = reader["Titulo"]?.ToString() ?? string.Empty, // Trata nulidade
                                Autor = reader["Autor"]?.ToString() ?? string.Empty,
                                Categoria = reader["Categoria"]?.ToString() ?? string.Empty,
                                Editora = reader["Editora"]?.ToString() ?? string.Empty,
                                Disponivel = (bool)reader["Disponivel"]
                            };
                        }
                    }

                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar livro: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
            return livro;
        }

        /// <summary>
        /// Método para atualizar os dados de um livro no banco de dados.
        /// </summary>
        /// <param name="livro">Objeto Livro com os dados atualizados</param>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public void Atualizar(Livro livro)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand
                    (
                        "UPDATE Livros " +
                        "SET Titulo = @Titulo, Autor = @Autor, Categoria = @Categoria, Editora = @Editora, Disponivel = @Disponivel " +
                        "WHERE Codigo = @Codigo",
                        connection
                    );
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = livro.Codigo;
                    cmd.Parameters.Add("@Titulo", SqlDbType.NVarChar, 100).Value = livro.Titulo;
                    cmd.Parameters.Add("@Autor", SqlDbType.NVarChar, 100).Value = livro.Autor;
                    cmd.Parameters.Add("@Categoria", SqlDbType.NVarChar, 50).Value = livro.Categoria;
                    cmd.Parameters.Add("@Editora", SqlDbType.NVarChar, 50).Value = livro.Editora;
                    cmd.Parameters.Add("@Disponivel", SqlDbType.Bit).Value = livro.Disponivel;
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao atualizar livro: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Método para excluir um livro do banco de dados pelo código.
        /// </summary>
        /// <param name="codigo">Código do livro a ser excluído</param>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public void Excluir(int codigo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand
                    (
                        "DELETE FROM Livros " +
                        "WHERE Codigo = @Codigo",
                        connection
                    );
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigo;
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao deletar livro: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Método para listar todos os livros do banco de dados.
        /// </summary>
        /// <returns>Lista de Objetos do tipo Livro contendo todos os livros registrados no Banco de Dados</returns>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public List<Livro> Listar()
        {
            List<Livro> livros = new List<Livro>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Livros", connection);
                    cmd.CommandType = System.Data.CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Livro livro = new Livro
                            {
                                Codigo = (int)reader["Codigo"],
                                Titulo = reader["Titulo"]?.ToString() ?? string.Empty,
                                Autor = reader["Autor"]?.ToString() ?? string.Empty,
                                Categoria = reader["Categoria"]?.ToString() ?? string.Empty,
                                Editora = reader["Editora"]?.ToString() ?? string.Empty,
                                Disponivel = (bool)reader["Disponivel"]
                            };
                            livros.Add(livro);
                        }
                    }

                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao listar livros: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
            return livros;
        }

        /// <summary>
        /// Método para atualizar a disponibilidade de um livro no banco de dados.
        /// </summary>
        /// <param name="codigoLivro">Código do livro a ser atualizado</param>
        /// <param name="estaDisponivel">Status de Disponibilidade a ser atualizado (true ou false)</param>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public void AtualizarDisponibilidade(int codigoLivro, bool estaDisponivel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    if (BuscarPorCodigo(codigoLivro) == null)
                    {
                        throw new Exception("Livro não encontrado!");
                    }

                    SqlCommand cmd = new SqlCommand
                    (
                        "UPDATE Livros " +
                        "SET Disponivel = @Disponivel " +
                        "WHERE Codigo = @Codigo",
                        connection
                    );
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigoLivro;
                    cmd.Parameters.Add("@Disponivel", SqlDbType.Bit).Value = estaDisponivel;
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao atualizar disponibilidade do livro: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro: " + ex.Message);
                }
            }
        }
    }
}
