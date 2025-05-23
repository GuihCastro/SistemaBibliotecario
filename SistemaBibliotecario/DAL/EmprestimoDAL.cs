﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaBibliotecario.Models;

namespace SistemaBibliotecario.DAL
{
    /// <summary>
    /// Classe de comunicação com o Banco de Dados (Data Access Layer) para a entidade Empréstimo.
    /// Implementa as operações CRUD (Create, Read, Update, Delete).
    /// </summary>
    public class EmprestimoDAL
    {
        // String de conexão com o Banco de Dados
        private static string _connectionString = ConfigurationManager.ConnectionStrings["BibliotecaConnection"].ConnectionString;

        /// <summary>
        /// Construtor padrão da classe.
        /// </summary>
        public EmprestimoDAL() { }

        /// <summary>
        /// Método para registrar um novo empréstimo.
        /// </summary>
        /// <param name="emprestimo">Objeto do tipo Emprestimo a ser registrado</param>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public void RegistrarEmprestimo(Emprestimo emprestimo)
        {
            // Método para registrar um novo empréstimo
            using (SqlConnection connection = new SqlConnection(_connectionString))
            //using (var transaction = connection.BeginTransaction())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {

                        // 1. Verificar se o aluno existe
                        AlunoDAL alunoDAL = new AlunoDAL();
                        Aluno aluno = alunoDAL.BuscarPorRA(emprestimo.RAAluno);
                        if (aluno == null)
                        {
                            throw new Exception("Aluno não encontrado!");
                        }

                        // 2. Verificar disponibilidade do livro
                        LivroDAL livroDAL = new LivroDAL();
                        Livro livro = livroDAL.BuscarPorCodigo(emprestimo.CodigoLivro);
                        if (livro == null || !livro.Disponivel)
                        {
                            throw new Exception("Livro indisponível para empréstimo!");
                        }

                        // 3. Se o aluno existir e o livro estiver disponível, registrar o empréstimo
                        SqlCommand cmd = new SqlCommand
                        (
                            "INSERT INTO Emprestimos (RAAluno, CodigoLivro, DataRetirada, DataEntrega, Devolvido) " +
                            "VALUES (@RAAluno, @CodigoLivro, @DataRetirada, @DataEntrega, @Devolvido); " +
                            "SELECT SCOPE_IDENTITY();",
                            connection,
                            transaction
                        //transaction
                        );
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = emprestimo.Codigo;
                        cmd.Parameters.Add("@RAAluno", SqlDbType.Int).Value = emprestimo.RAAluno;
                        cmd.Parameters.Add("@CodigoLivro", SqlDbType.Int).Value = emprestimo.CodigoLivro;
                        cmd.Parameters.Add("@DataRetirada", SqlDbType.DateTime).Value = emprestimo.DataRetirada;
                        cmd.Parameters.Add("@DataEntrega", SqlDbType.DateTime).Value = emprestimo.DataEntrega;
                        cmd.Parameters.Add("@Devolvido", SqlDbType.Bit).Value = emprestimo.Devolvido;
                        cmd.ExecuteNonQuery();

                        //emprestimo.Codigo = Convert.ToInt32(cmd.ExecuteScalar());

                        // 4. Atualizar a disponibilidade do livro
                        livroDAL.AtualizarDisponibilidade(emprestimo.CodigoLivro, false);

                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao registrar empréstimo: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao registrar empréstimo: " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Método para registrar a devolução de um livro.
        /// </summary>
        /// <param name="codigoEmprestimo">Código do empréstimo a ser devolvido</param>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public void RegistrarDevolucao(int codigoEmprestimo)
        {
            Emprestimo emprestimo = new Emprestimo();
            //using (SqlTransaction transaction = connection.BeginTransaction())
            try
            {
                // 1. Localizar empréstimo
                emprestimo = BuscarPorCodigo(codigoEmprestimo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao localizar empréstimo: " + ex.Message);
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction  = connection.BeginTransaction())
                {
                    try
                    {

                        // 2. Atualizar status do empréstimo
                        SqlCommand cmd = new SqlCommand
                        (
                            "UPDATE Emprestimos " +
                            "SET Devolvido = 1, DataEntrega = @DataEntrega " +
                            "WHERE Codigo = @Codigo",
                            connection,
                            transaction
                        //transaction
                        );
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigoEmprestimo;
                        cmd.Parameters.Add("@DataEntrega", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.ExecuteNonQuery();

                        // 3. Atualizar disponibilidade do livro
                        LivroDAL livroDAL = new LivroDAL();
                        livroDAL.AtualizarDisponibilidade(emprestimo.CodigoLivro, true);

                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao registrar devolução: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao registrar devolução: " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Método para buscar um empréstimo pelo código.
        /// </summary>
        /// <param name="codigo">Código do empréstimo a ser buscado</param>
        /// <returns>Objeto do tipo Empréstimo localizado ou null se não existir</returns>
        /// <exception cref="Exception"></exception>
        public Emprestimo BuscarPorCodigo(int codigo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand
                    (
                        "SELECT * FROM Emprestimos " +
                        "WHERE Codigo = @Codigo",
                        connection
                    );
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigo;

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            return new Emprestimo
                            (
                                (int)reader["Codigo"],
                                (int)reader["RAAluno"],
                                (int)reader["CodigoLivro"],
                                (DateTime)reader["DataRetirada"],
                                (DateTime)reader["DataEntrega"],
                                (bool)reader["Devolvido"]
                            );
                        }
                    }
                    return null;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar empréstimo: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar empréstimo: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Método para excluir um empréstimo pelo código.
        /// </summary>
        /// <param name="codigo">Código do empréstimo a ser excluído</param>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public void Excluir(int codigo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand
                        (
                            "DELETE FROM Emprestimos " +
                            "WHERE Codigo = @Codigo",
                            connection,
                            transaction
                        );
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigo;
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Empréstimo excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao excluir empréstimo: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao excluir empréstimo: " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Método para listar todos os empréstimos ativos (não devolvidos).
        /// </summary>
        /// <returns>Lista de Objetos Emprestimo com Devolvido=false</returns>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public List<Emprestimo> ListarAtivos()
        {
            List<Emprestimo> emprestimos = new List<Emprestimo>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand
                    (
                        "SELECT * FROM Emprestimos " +
                        "WHERE Devolvido = 0",
                        connection
                    );
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            emprestimos.Add(new Emprestimo
                            (
                                (int)reader["Codigo"],
                                (int)reader["RAAluno"],
                                (int)reader["CodigoLivro"],
                                (DateTime)reader["DataRetirada"],
                                (DateTime)reader["DataEntrega"],
                                (bool)reader["Devolvido"]
                            ));
                        }
                    }
                    return emprestimos;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao listar empréstimos: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao listar empréstimos: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Método para listar todos os empréstimos devolvidos.
        /// </summary>
        /// <returns>Lista de Objetos Emprestimo com Devolvido=true</returns>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public List<Emprestimo> ListarDevolvidos()
        {
            List<Emprestimo> emprestimos = new List<Emprestimo>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand
                    (
                        "SELECT * FROM Emprestimos " +
                        "WHERE Devolvido = 1",
                        connection
                    );
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            emprestimos.Add(new Emprestimo
                            (
                                (int)reader["Codigo"],
                                (int)reader["RAAluno"],
                                (int)reader["CodigoLivro"],
                                (DateTime)reader["DataRetirada"],
                                (DateTime)reader["DataEntrega"],
                                (bool)reader["Devolvido"]
                            ));
                        }
                    }
                    return emprestimos;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao listar empréstimos: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao listar empréstimos: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Método para listar todos os empréstimos de um aluno específico.
        /// </summary>
        /// <param name="raAluno">RA do aluno a ser buscado</param>
        /// <returns>Lista de Objetos emprestimo com o valor do campo RAAluno igual ao raAluno informado</returns>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public List<Emprestimo> ListarPorAluno(int raAluno)
        {
            List<Emprestimo> emprestimos = new List<Emprestimo>();

            using (SqlConnection connection = new SqlConnection( _connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand
                    (
                        "SELECT * FROM Emprestimos " +
                        "WHERE RAAluno = @RAAluno",
                        connection
                    );
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@RAAluno", SqlDbType.Int).Value = raAluno;

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            emprestimos.Add(new Emprestimo
                            (
                                (int)reader["Codigo"],
                                (int)reader["RAAluno"],
                                (int)reader["CodigoLivro"],
                                (DateTime)reader["DataRetirada"],
                                (DateTime)reader["DataEntrega"],
                                (bool)reader["Devolvido"]
                            ));
                        }
                    }
                    return emprestimos;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao listar empréstimos: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao listar empréstimos: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Método para listar todos os empréstimos de um livro específico.
        /// </summary>
        /// <param name="codigoLivro">Código do livro a ser buscado</param>
        /// <returns>Lista com os Objetos Emprestimo com o valor do campo CodigoLivro igual ao codigoLivro informado</returns>
        /// <exception cref="SqlException">Lançada quando ocorre um erro no SQL Server</exception>
        /// <exception cref="Exception">Lançada quando ocorre um erro genérico durante a operação</exception>
        public List<Emprestimo> ListarPorLivro(int codigoLivro)
        {
            List<Emprestimo> emprestimos = new List<Emprestimo>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand
                    (
                        "SELECT * FROM Emprestimos " +
                        "WHERE CodigoLivro = @CodigoLivro",
                        connection
                    );
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@CodigoLivro", SqlDbType.Int).Value = codigoLivro;

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            emprestimos.Add(new Emprestimo
                            (
                                (int)reader["Codigo"],
                                (int)reader["RAAluno"],
                                (int)reader["CodigoLivro"],
                                (DateTime)reader["DataRetirada"],
                                (DateTime)reader["DataEntrega"],
                                (bool)reader["Devolvido"]
                            ));
                        }
                    }
                    return emprestimos;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao listar empréstimos: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao listar empréstimos: " + ex.Message);
                }
            }
        }
    }
}
