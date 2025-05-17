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
    public class LivroDAL
    {
        // String de conexão com o banco de dados
        private static string _connectionString = ConfigurationManager.ConnectionStrings["BibliotecaConnection"].ConnectionString;

        // Construtor padrão
        public LivroDAL() { }

        // Métodos CRUD para o banco de dados
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
            }
        }

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
            }
            return livro;
        }

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
            }
        }

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
            }
        }

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
            }
            return livros;
        }

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
