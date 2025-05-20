using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaBibliotecario.DAL;
using SistemaBibliotecario.Models;

namespace SistemaBibliotecario.BLL
{
    /// <summary>
    /// Classe que aplica as Regras de Negócios (Business Logic Layer) para manipulação de empréstimos.
    /// </summary>
    public class EmprestimoBLL
    {
        private static readonly EmprestimoDAL _emprestimoDAL = new EmprestimoDAL();
        private static readonly LivroDAL _livroDAL = new LivroDAL();
        private static readonly AlunoDAL _alunoDAL = new AlunoDAL();

        /// <summary>
        /// Método responsável por registrar um novo empréstimo.
        /// </summary>
        /// <param name="emprestimo">Objeto do tipo Emprestimo a ser registrado</param>
        /// <exception cref="ArgumentNullException">Lançada quando o emprestimo é nulo</exception>
        /// <exception cref="Exception">Lançada quando o aluno não é encontrado ou o livro não está disponível</exception>
        public static void RegistrarEmprestimo(Emprestimo emprestimo)
        {
            if (emprestimo == null)
            {
                throw new ArgumentNullException(nameof(emprestimo), "O empréstimo não pode ser nulo.");
            }

            if (_alunoDAL.BuscarPorRA(emprestimo.RAAluno) == null)
            {
                throw new Exception("Aluno não encontrado.");
            }

            Livro livro = _livroDAL.BuscarPorCodigo(emprestimo.CodigoLivro);
            if (livro == null || !livro.Disponivel)
            {
                throw new Exception("Livro indisponível para empréstimo.");
            }

            emprestimo.DataEntrega = emprestimo.DataRetirada.AddDays(7);

            livro.Disponivel = false;

            _emprestimoDAL.RegistrarEmprestimo(emprestimo);
        }

        /// <summary>
        /// Método responsável por registrar a devolução de um livro.
        /// </summary>
        /// <param name="codigoEmprestimo">Código do empréstimo a ser devolvido</param>
        /// <exception cref="Exception">Lançada quando o empréstimo não é encontrado ou já foi devolvido</exception>
        public static void RegistrarDevolucao(int codigoEmprestimo)
        {
            Emprestimo emprestimo = _emprestimoDAL.BuscarPorCodigo(codigoEmprestimo);
            if (emprestimo == null) throw new Exception("Empréstimo não encontrado.");
            if (emprestimo.Devolvido) throw new Exception("Empréstimo já devolvido.");

            _emprestimoDAL.RegistrarDevolucao(codigoEmprestimo);
        }

        /// <summary>
        /// Método responsável por verificar se há empréstimos atrasados.
        /// </summary>
        /// <returns>Lista de Objetos Emprestimo com Data de Entrega vencida e ainda não devolvidos</returns>
        public static List<Emprestimo> VerificarAtrasos()
        {
            List<Emprestimo> emprestimosAtrasados = _emprestimoDAL.ListarAtivos().FindAll(e => e.DataEntrega < DateTime.Now && !e.Devolvido);
            try
            {
                if (emprestimosAtrasados.Count == 0)
                {
                    throw new Exception("Não há empréstimos atrasados.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao busca empréstimos atrasados: " + e.Message);
            }
            return emprestimosAtrasados;
        }

        /// <summary>
        /// Método responsável por excluir um empréstimo.
        /// </summary>
        /// <param name="codigo">Código do empréstimo a ser excluído</param>
        /// <exception cref="Exception">Lançada quando o empréstimo não é encontrado</exception>
        public static void Excluir(int codigo)
        {
            Emprestimo emprestimo = _emprestimoDAL.BuscarPorCodigo(codigo);
            if (emprestimo == null)
            {
                throw new Exception("Empréstimo não encontrado.");
            }
            Livro livro = _livroDAL.BuscarPorCodigo(emprestimo.CodigoLivro);
            if (livro != null && !livro.Disponivel)
            {
                LivroBLL.AtualizarDisponibilidade(livro.Codigo, true);
            }
            _emprestimoDAL.Excluir(codigo);
        }

        // Métodos de consulta delegados para a DAL
        /// <summary>
        /// Método responsável por buscar um empréstimo pelo código.
        /// </summary>
        /// <param name="codigo">Código do empréstimo a ser buscado</param>
        /// <returns>Objeto Emprestimo encontrado ou null se não existir</returns>
        public static Emprestimo BuscarPorCodigo(int codigo) => _emprestimoDAL.BuscarPorCodigo(codigo);

        /// <summary>
        /// Método responsável por listar todos os empréstimos ativos.
        /// </summary>
        /// <returns>Lista com empréstimos com Devolvido=false</returns>
        public static List<Emprestimo> ListarAtivos() => _emprestimoDAL.ListarAtivos();

        /// <summary>
        /// Método responsável por listar todos os empréstimos devolvidos.
        /// </summary>
        /// <returns>Lista com empréstimos com Devolvido=true</returns>
        public static List<Emprestimo> ListarDevolvidos() => _emprestimoDAL.ListarDevolvidos();

        /// <summary>
        /// Método responsável por listar todos os empréstimos de um aluno específico.
        /// </summary>
        /// <param name="ra">RA do aluno a ser buscado</param>
        /// <returns>Lista com todos os empréstimos com o campo RAAluno igual ao ra informado</returns>
        public static List<Emprestimo> ListarPorAluno(int ra) => _emprestimoDAL.ListarPorAluno(ra);

        /// <summary>
        /// Método responsável por listar todos os empréstimos de um livro específico.
        /// </summary>
        /// <param name="codigoLivro">Código do livro a ser buscado</param>
        /// <returns>Lista com todos os empréstimos com o campo CodigoLivro igual ao codigoLivro informado</returns>
        public static List<Emprestimo> ListarPorLivro(int codigoLivro) => _emprestimoDAL.ListarPorLivro(codigoLivro);
    }
}
