using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBibliotecario.Models
{
    // Classe Model para representar um aluno
    public class Aluno
    {
        // Propriedades do aluno com validações por Data Annotations
        [Key]
        [Required(ErrorMessage = "É obrigatório informar o RA!")]
        [Range(1, int.MaxValue, ErrorMessage = "RA deve ser um número positivo!")]
        public int RA { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o nome!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigatório informar um endereço de e-mail!")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "É obrigatório informar um telefone!")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "O telefone deve ter entre 10 e 15 caracteres (com DDD)!")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a data de nascimento!")]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2025", ErrorMessage = "A data de nascimento deve ser entre 01/01/1900 e 01/01/2025!")]
        public DateTime DataNascimento { get; set; }

        public Aluno() { } // Construtor padrão

        // Construtor com parâmetros para leitura do banco de dados
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
