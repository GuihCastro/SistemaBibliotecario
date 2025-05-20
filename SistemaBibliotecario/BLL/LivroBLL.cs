using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SistemaBibliotecario.DAL;
using SistemaBibliotecario.Models;

namespace SistemaBibliotecario.BLL
{
    /// <summary>
    /// Classe que aplica as Regras de Negócios (Business Logic Layer) para manipulação de livros.
    /// </summary>
    public class LivroBLL
    {
        private static readonly LivroDAL _livroDAL = new LivroDAL();

        /// <summary>
        /// Método responsável por validar os dados de um novo livro antes de inserí-lo no sistema.
        /// </summary>
        /// <param name="livro">Objeto do tipo Livro a ser inserido</param>
        /// <exception cref="Exception">Lançada quando há erro de validação</exception>
        public static void Inserir(Livro livro)
        {
            ValidarLivro(livro); 
            ValidarCodigo(livro.Codigo);
            _livroDAL.Inserir(livro);
        }

        /// <summary>
        /// Método responsável por atualizar os dados de um livro já cadastrado no sistema.
        /// </summary>
        /// <param name="livro">Objeto Livro com os dados atualizados</param>
        /// <exception cref="Exception">Lançada quando o livro não é encontrado ou há erro de validação</exception>
        public static void Atualizar(Livro livro)
        {
            ValidarLivro(livro);
            if (_livroDAL.BuscarPorCodigo(livro.Codigo) == null)
            {
                throw new Exception("Livro não encontrado para atualização!");
            }
            _livroDAL.Atualizar(livro);
        }

        /// <summary>
        /// Método responsável por validar se um livro pode ser excluído do sistema.
        /// </summary>
        /// <param name="codigo">Código do livro a ser excluído</param>
        /// <exception cref="Exception">Lançada quando o livro não é encontrado ou está presente em empréstimos ativos</exception>
        public static void Excluir(int codigo)
        {
            if (_livroDAL.BuscarPorCodigo(codigo) == null)
            {
                throw new Exception("Livro não encontrado para exclusão!");
            }

            List<Emprestimo> emprestimosDoLivro = EmprestimoBLL.ListarPorLivro(codigo);
            if (emprestimosDoLivro.Any(e => !e.Devolvido))
            {
                throw new Exception("Não é possível excluir um livro com empréstimos ativos!");
            }

            foreach (var emprestimo in emprestimosDoLivro)
            {
                EmprestimoBLL.Excluir(emprestimo.Codigo);
            }

            _livroDAL.Excluir(codigo);
        }

        /// <summary>
        /// Método responsável por atualizar a disponibilidade de um livro no sistema.
        /// </summary>
        /// <param name="codigoLivro">Código do livro a ser atualizado</param>
        /// <param name="estaDisponivel">Novo status</param>
        /// <exception cref="Exception">Lançada quando o livro não é encontrado</exception>
        public static void AtualizarDisponibilidade(int codigoLivro, bool estaDisponivel)
        {
            if (_livroDAL.BuscarPorCodigo(codigoLivro) == null)
            {
                throw new Exception("Livro não encontrado para atualização de disponibilidade!");
            }
            _livroDAL.AtualizarDisponibilidade(codigoLivro, estaDisponivel);
        }

        // Métodos auxiliares de validação
        /// <summary>
        /// Método responsável por validar os campos de um livro.
        /// </summary>
        /// <param name="livro">Objeto Livro a ser validade</param>
        /// <exception cref="Exception">Lançada quando algum campo não atende aos requisitos</exception>
        private static void ValidarLivro(Livro livro)
        {
            if (livro.Codigo <= 0)
            {
                throw new Exception("O código do livro deve ser positivo e diferente de zero!");
            }

            if (string.IsNullOrWhiteSpace(livro.Titulo))
            {
                throw new Exception("É obrigatório informar o título do livro!");
            }

            if (livro.Titulo.Length <=2 || livro.Titulo.Length > 100)
            {
                throw new Exception("O título deve ter entre 2 e 100 caracteres!");
            }

            if (string.IsNullOrWhiteSpace(livro.Autor))
            {
                throw new Exception("É obrigatório informar o autor do livro!");
            }

            if (livro.Autor.Length <= 2 || livro.Autor.Length > 100)
            {
                throw new Exception("O autor deve ter entre 2 e 100 caracteres!");
            }

            if (!string.IsNullOrEmpty(livro.Categoria) && !Regex.IsMatch(livro.Categoria, @"^[a-zA-Zá-úÁ-Ú\s]+$"))
            {
                throw new Exception("A categoria deve conter apenas letras e espaços!");
            }

            if (!string.IsNullOrEmpty(livro.Editora) && livro.Editora.Length < 2 || livro.Editora.Length > 50)
            {
                throw new Exception("A editora deve ter pelo entre 2 e 50 caracteres!");
            }
        }

        /// <summary>
        /// Método responsável por validar se já existe um livro cadastrado com o mesmo código.
        /// </summary>
        /// <param name="codigo">Código a ser validado</param>
        /// <exception cref="Exception">Lançada quando o código já existe no Banco de Dados</exception>
        private static void ValidarCodigo(int codigo)
        {
            if (_livroDAL.BuscarPorCodigo(codigo) != null)
            {
                throw new Exception("Já existe um livro cadastrado com esse código!");
            }
        }

        // Métodos de consulta - delegado para a DAL
        /// <summary>
        /// Método responsável por buscar um livro pelo código.
        /// </summary>
        /// <param name="codigo">Código a ser buscado</param>
        /// <returns>Objeto Livro encontrado ou null se não existir</returns>
        public static Livro BuscarPorCodigo(int codigo) => _livroDAL.BuscarPorCodigo(codigo);

        /// <summary>
        /// Método responsável por listar todos os livros cadastrados no sistema.
        /// </summary>
        /// <returns>Lista com livros presentes no Banco de Dados</returns>
        public static List<Livro> Listar() => _livroDAL.Listar();

        /// <summary>
        /// Método responsável por listar todos os livros disponíveis no sistema.
        /// </summary>
        /// <returns>Lista de livros com Disponivel=true</returns>
        public static List<Livro> ListarDisponiveis() => _livroDAL.Listar().FindAll(l => l.Disponivel);
    }
}
