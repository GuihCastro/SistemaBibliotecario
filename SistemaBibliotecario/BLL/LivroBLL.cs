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
    public class LivroBLL
    {
        private static readonly LivroDAL _livroDAL = new LivroDAL();

        public static void Inserir(Livro livro)
        {
            // Valida os dados antes de enviar para a DAL inserir no Banco
            ValidarLivro(livro); 
            ValidarCodigo(livro.Codigo);
            _livroDAL.Inserir(livro); // Chamada ao método de inserção da DAL
        }

        public static void Atualizar(Livro livro)
        {
            ValidarLivro(livro);
            if (_livroDAL.BuscarPorCodigo(livro.Codigo) == null)
            {
                throw new Exception("Livro não encontrado para atualização!");
            }
            _livroDAL.Atualizar(livro);
        }

        public static void Excluir(int codigo)
        {
            if (_livroDAL.BuscarPorCodigo(codigo) == null)
            {
                throw new Exception("Livro não encontrado para exclusão!");
            }

            // Verifica se o livro possui empréstimos ativos
            List<Emprestimo> emprestimosDoLivro = EmprestimoBLL.ListarPorLivro(codigo);
            if (emprestimosDoLivro.Any(e => !e.Devolvido))
            {
                throw new Exception("Não é possível excluir um livro com empréstimos ativos!");
            }

            // Se não houver empréstimos ativos, prosseguir com a exclusão
            // Primeiro, excluir os empréstimos associados
            foreach (var emprestimo in emprestimosDoLivro)
            {
                EmprestimoBLL.Excluir(emprestimo.Codigo);
            }

            // Após excluir os empréstimos, excluir o livro
            _livroDAL.Excluir(codigo);
        }

        public static void AtualizarDisponibilidade(int codigoLivro, bool estaDisponivel)
        {
            if (_livroDAL.BuscarPorCodigo(codigoLivro) == null)
            {
                throw new Exception("Livro não encontrado para atualização de disponibilidade!");
            }
            _livroDAL.AtualizarDisponibilidade(codigoLivro, estaDisponivel);
        }

        // Métodos auxiliares de validação
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

        private static void ValidarCodigo(int codigo)
        {
            if (_livroDAL.BuscarPorCodigo(codigo) != null)
            {
                throw new Exception("Já existe um livro cadastrado com esse código!");
            }
        }

        // Métodos de consulta - delegado para a DAL
        public static Livro BuscarPorCodigo(int codigo) => _livroDAL.BuscarPorCodigo(codigo);
        public static List<Livro> Listar() => _livroDAL.Listar();
        public static List<Livro> ListarDisponiveis() => _livroDAL.Listar().FindAll(l => l.Disponivel); // Listagem com filtro
    }
}
