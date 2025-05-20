using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaBibliotecario.DAL;
using SistemaBibliotecario.Models;

namespace SistemaBibliotecario.BLL
{
    /// <summary>
    /// Classe que aplica as Regras de Negócios (Business Logic Layer) para manipulação de alunos.
    /// </summary>
    public class AlunoBLL
    {
        private static readonly AlunoDAL _alunoDAL = new AlunoDAL();

        /// <summary>
        /// Método responsável por validar os dados de um novo aluno antes de inserí-lo no sistema.
        /// </summary>
        /// <param name="aluno">Objeto do tipo Aluno contendo os dados a serem validados e inseridos</param>
        /// <exception cref="Exception">Lançada quando há erro de validação</exception>
        public static void Inserir(Aluno aluno)
        {
            ValidarAluno(aluno); 
            ValidarRA(aluno.RA);
            _alunoDAL.Inserir(aluno);
        }

        /// <summary>
        /// Método responsável por atualizar os dados de um aluno já cadastrado no sistema.
        /// </summary>
        /// <param name="aluno">Objeto do tipo Aluno com os dados atualizados</param>
        /// <exception cref="Exception">Lançada quando o aluno não existe ou quando há erro de validação</exception>
        public static void Atualizar(Aluno aluno)
        {
            ValidarAluno(aluno);
            if (_alunoDAL.BuscarPorRA(aluno.RA) == null)
            {
                throw new Exception("Aluno não encontrado para atualização!");
            }
            _alunoDAL.Atualizar(aluno);
        }

        /// <summary>
        /// Método responsável por validar se um aluno pode ser excluido do sistema.
        /// </summary>
        /// <param name="ra">RA do aluno a ser excluído</param>
        /// <exception cref="Exception">Lançada quando o aluno não existe ou possui empréstimos ativos</exception>
        public static void Excluir(int ra)
        {
            if (_alunoDAL.BuscarPorRA(ra) == null)
            {
                throw new Exception("Aluno não encontrado para exclusão!");
            }

            List<Emprestimo> emprestimosDoAluno = EmprestimoBLL.ListarPorAluno(ra);
            if (emprestimosDoAluno.Any(e => !e.Devolvido))
            {
                throw new Exception("Não é possível excluir o aluno, pois ele possui empréstimos ativos!");
            }

            foreach (var emprestimo in emprestimosDoAluno)
            {
                EmprestimoBLL.Excluir(emprestimo.Codigo);
            }

            _alunoDAL.Excluir(ra); 
        }

        // Métodos auxiliares de validação
        /// <summary>
        /// Método responsável por validar todos os campos de um aluno.
        /// </summary>
        /// <param name="aluno">Objeto Aluno a ser validado</param>
        /// <exception cref="Exception">Lançada quando algum campo não atende aos requisitos</exception>
        private static void ValidarAluno(Aluno aluno)
        {
            if (aluno.RA <= 0)
            {
                throw new Exception("O número de RA deve ser positivo e diferente de zero!");
            }

            if (string.IsNullOrWhiteSpace(aluno.Nome))
            {
                throw new Exception("É obrigatório informar um nome para o aluno!");
            }

            if (aluno.Nome.Length <= 2 || aluno.Nome.Length > 100)
            {
                throw new Exception("O nome deve ter entre 2 e 100 caracteres!");
            }

            if (string.IsNullOrWhiteSpace(aluno.Email))
            {
                throw new Exception("É obrigatório informar um endereço de e-mail!");
            }

            if (!aluno.Email.Contains("@") || !aluno.Email.Contains("."))
            {
                throw new Exception("O e-mail informado não é válido!");
            }

            if (aluno.Telefone.Length < 10 || aluno.Telefone.Length > 15)
            {
                throw new Exception("O telefone deve ter entre 10 e 15 caracteres (com DDD)!");
            }

            if (aluno.DataNascimento > DateTime.Now.AddYears(-10)) // Exemplo de validação de idade mínima
            {
                throw new Exception("O aluno deve ter pelo menos 10 anos!");
            }
        }

        /// <summary>
        /// Método responsável por validar se o RA do aluno já existe no sistema.
        /// </summary>
        /// <param name="ra">RA a ser validado</param>
        /// <exception cref="Exception">Lançada quando o RA já existe no Banco de Dados</exception>
        private static void ValidarRA(int ra)
        {
            if (_alunoDAL.BuscarPorRA(ra) != null)
            {
                throw new Exception("Já existe um aluno cadastrado com esse RA!");
            }
        }

        // Métodos de consulta (sem validação) - delegam para a DAL
        /// <summary>
        /// Método responsável por buscar um aluno pelo RA.
        /// </summary>
        /// <param name="ra">RA do aluno a ser buscado</param>
        /// <returns>Objeto Aluno encontrado ou null se não existir</returns>
        public static Aluno BuscarPorRA(int ra) => _alunoDAL.BuscarPorRA(ra);

        /// <summary>
        /// Método responsável por listar todos os alunos cadastrados no sistema.
        /// </summary>
        /// <returns>Lista com os objetos Aluno encontrados</returns>
        public static List<Aluno> Listar() => _alunoDAL.Listar();
    }
}
