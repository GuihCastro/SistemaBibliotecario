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
    public class EmprestimoBLL
    {
        // Camada de Regra de Negócio para validações do Empréstimo

        private static readonly EmprestimoDAL _emprestimoDAL = new EmprestimoDAL();
        private static readonly LivroDAL _livroDAL = new LivroDAL();
        private static readonly AlunoDAL _alunoDAL = new AlunoDAL();

        public static void RegistrarEmprestimo(Emprestimo emprestimo)
        {
            // Validação básica de nulidade
            if (emprestimo == null)
            {
                throw new ArgumentNullException(nameof(emprestimo), "O empréstimo não pode ser nulo.");
            }

            // Verifica se o aluno existe
            if (_alunoDAL.BuscarPorRA(emprestimo.RAAluno) == null)
            {
                throw new Exception("Aluno não encontrado.");
            }

            // Verifica disponibilidade do livro
            Livro livro = _livroDAL.BuscarPorCodigo(emprestimo.CodigoLivro);
            if (livro == null || !livro.Disponivel)
            {
                throw new Exception("Livro indisponível para empréstimo.");
            }

            // Data de entrega padrão (7 dias após a retirada)
            emprestimo.DataEntrega = emprestimo.DataRetirada.AddDays(7);

            // Atualiza a disponibilidade do livro
            livro.Disponivel = false;

            // Registra o empréstimo
            _emprestimoDAL.RegistrarEmprestimo(emprestimo);
        }

        public static void RegistrarDevolucao(int codigoEmprestimo)
        {
            // Localiza o empréstimo
            Emprestimo emprestimo = _emprestimoDAL.BuscarPorCodigo(codigoEmprestimo);
            if (emprestimo == null) throw new Exception("Empréstimo não encontrado.");
            if (emprestimo.Devolvido) throw new Exception("Empréstimo já devolvido.");

            // Registra a devolução
            _emprestimoDAL.RegistrarDevolucao(codigoEmprestimo);
        }
        public static List<Emprestimo> VerificarAtrasos()
        {
            // Verifica se há empréstimos atrasados
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

        // Métodos de consulta delegados para a DAL
        public static Emprestimo BuscarPorCodigo(int codigo) => _emprestimoDAL.BuscarPorCodigo(codigo);
        public static List<Emprestimo> ListarAtivos() => _emprestimoDAL.ListarAtivos();
        public static List<Emprestimo> ListarDevolvidos() => _emprestimoDAL.ListarDevolvidos();
        public static List<Emprestimo> ListarPorAluno(int ra) => _emprestimoDAL.ListarPorAluno(ra);
    }
}
