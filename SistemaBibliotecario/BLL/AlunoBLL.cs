using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaBibliotecario.DAL;
using SistemaBibliotecario.Models;

namespace SistemaBibliotecario.BLL
{
    public class AlunoBLL
    {
        private static readonly AlunoDAL _alunoDAL = new AlunoDAL();

        // Método para validar o cadastro de aluno antes de inserir no banco
        public static void Inserir(Aluno aluno)
        {
            ValidarAluno(aluno); // Valida os dados do aluno
            ValidarRA(aluno.RA); // Verifica se o RA já existe
            _alunoDAL.Inserir(aluno); // Chamada ao método de inserção da DAL
        }

        // Validar os dados antes de mandar para a DAL atualizar
        public static void Atualizar(Aluno aluno)
        {
            ValidarAluno(aluno);
            if (_alunoDAL.BuscarPorRA(aluno.RA) == null)
            {
                throw new Exception("Aluno não encontrado para atualização!");
            }
            _alunoDAL.Atualizar(aluno); // Chamada ao método de atualização da DAL
        }

        public static void Excluir(int ra)
        {
            if (_alunoDAL.BuscarPorRA(ra) == null)
            {
                throw new Exception("Aluno não encontrado para exclusão!");
            }
            _alunoDAL.Excluir(ra); // Chamada ao método de exclusão da DAL
        }

        // Métodos auxiliares de validação
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

        private static void ValidarRA(int ra)
        {
            if (_alunoDAL.BuscarPorRA(ra) != null)
            {
                throw new Exception("Já existe um aluno cadastrado com esse RA!");
            }
        }

        // Métodos de consulta (sem validação) - delegam para a DAL
        public static Aluno BuscarPorRA(int ra) => _alunoDAL.BuscarPorRA(ra);
        public static List<Aluno> Listar() => _alunoDAL.Listar();
    }
}
