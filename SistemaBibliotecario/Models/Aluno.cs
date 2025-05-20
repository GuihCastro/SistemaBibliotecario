using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBibliotecario.Models
{
    /// <summary>
    /// Classe que representa um aluno no sistema bibliotecário.
    /// </summary>
    public class Aluno
    {
        /// <summary>
        /// Registro Acadêmico (RA) do aluno - Chave Primária.
        /// </summary>
        [Key]
        [Required(ErrorMessage = "É obrigatório informar o RA!")]
        [Range(1, int.MaxValue, ErrorMessage = "RA deve ser um número positivo!")]
        public int RA { get; set; }

        /// <summary>
        /// Nome Completo do aluno - Campo obrigatório.
        /// </summary>
        [Required(ErrorMessage = "É obrigatório informar o nome!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres!")]
        public string Nome { get; set; }

        /// <summary>
        /// Endereço de e-mail do aluno - Campo obrigatório.
        /// </summary>
        [Required(ErrorMessage = "É obrigatório informar um endereço de e-mail!")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }

        /// <summary>
        /// Número de telefone do aluno com DDD - Campo obrigatório, entre 10 e 15 caracteres.
        /// </summary>
        [Required(ErrorMessage = "É obrigatório informar um telefone!")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "O telefone deve ter entre 10 e 15 caracteres (com DDD)!")]
        public string Telefone { get; set; }

        /// <summary>
        /// Data de nascimento do aluno - Campo obrigatório, entre 01/01/1900 - 01/01/2025.
        /// </summary>
        [Required(ErrorMessage = "É obrigatório informar a data de nascimento!")]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2025", ErrorMessage = "A data de nascimento deve ser entre 01/01/1900 e 01/01/2025!")]
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// Construtor padrão da classe.
        /// </summary>
        public Aluno() { }

        /// <summary>
        /// Construtor completo com parâmetros.
        /// </summary>
        /// <param name="ra">Registro Acadêmico</param>
        /// <param name="nome">Nome completo</param>
        /// <param name="email">Endereço de e-mail</param>
        /// <param name="telefone">Número de telefone com DDD</param>
        /// <param name="dataNascimento">Data de nascimento</param>
        public Aluno(int ra, string nome, string email, string telefone, DateTime dataNascimento)
        {
            this.RA = ra;
            this.Nome = nome;
            this.Email = email;
            this.Telefone = telefone;
            this.DataNascimento = dataNascimento;
        }
    }
}
