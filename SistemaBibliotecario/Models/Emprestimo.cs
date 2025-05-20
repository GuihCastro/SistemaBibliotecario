using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBibliotecario.Models
{
    /// <summary>
    /// Classe que representa um empréstimo de livro no sistema bibliotecário.
    /// </summary>
    public class Emprestimo
    {
        /// <summary>
        /// Código do empréstimo - Chave Primária, autoincrementado pelo Banco.
        /// </summary>
        [Key]
        [Required(ErrorMessage = "É obrigatório informar o código do empréstimo!")]
        [Range(1, int.MaxValue, ErrorMessage = "O código do empréstimo deve ser um número positivo!")]
        public int Codigo { get; set; }

        /// <summary>
        /// Registro Acadêmico (RA) do aluno - Chave Estrangeira (obrigatório).
        /// </summary>
        [Required(ErrorMessage = "É obrigatório informar o RA do aluno!")]
        [Range(1, int.MaxValue, ErrorMessage = "O RA do aluno deve ser um número positivo!")]
        public int RAAluno { get; set; }

        /// <summary>
        /// Código do livro - Chave Estrangeira (obrigatório).
        /// </summary>
        [Required(ErrorMessage = "É obrigatório informar o código do livro!")]
        [Range(1, int.MaxValue, ErrorMessage = "O código do livro deve ser um número positivo!")]
        public int CodigoLivro { get; set; }

        /// <summary>
        /// Data de retirada do livro - Campo obrigatório, implementado automaticamente pelo construtor padrão.
        /// </summary>
        [Required(ErrorMessage = "É obrigatório informar a data de retirada!")]
        [DataType(DataType.Date, ErrorMessage = "A data de retirada deve ser uma data válida!")]
        public DateTime DataRetirada { get; set; }

        /// <summary>
        /// Data de entrega do livro - Campo obrigatório, implementado automaticamente pelo construtor padrão.
        /// </summary>
        [Required(ErrorMessage = "É obrigatório informar a data de entrega!")]
        [DataType(DataType.Date, ErrorMessage = "A data de entrega deve ser uma data válida!")]
        public DateTime DataEntrega { get; set; }

        /// <summary>
        /// Indica se o livro foi devolvido - Padrão é falso (não devolvido).
        /// </summary>
        public bool Devolvido { get; set; } = false;

        /// <summary>
        /// Construtor padrão da classe.
        /// </summary>
        public Emprestimo() 
        {
            this.DataRetirada = DateTime.Now;
            this.DataEntrega = DateTime.Now.AddDays(7); // Define a data de entrega como 7 dias após a retirada
        }

        /// <summary>
        /// Construtor completo com parâmetros.
        /// </summary>
        /// <param name="codigo">Código do emprestimo (autoincrementado)</param>
        /// <param name="raAluno">RA do aluno</param>
        /// <param name="codigoLivro">Código do Livro</param>
        /// <param name="dataRetirada">Data de retirada</param>
        /// <param name="dataEntrega">Data de entrega</param>
        /// <param name="devolvido">Status da devolução</param>
        public Emprestimo(int codigo, int raAluno, int codigoLivro, DateTime dataRetirada, DateTime dataEntrega, bool devolvido)
        {
            this.Codigo = codigo;
            this.RAAluno = raAluno;
            this.CodigoLivro = codigoLivro;
            this.DataRetirada = dataRetirada;
            this.DataEntrega = dataEntrega;
            this.Devolvido = devolvido;
        }
    }
}
