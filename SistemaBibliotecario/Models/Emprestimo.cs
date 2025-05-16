using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBibliotecario.Models
{
    public class Emprestimo
    {
        // Propriedades do empréstimo com validações por Data Annotations
        [Required(ErrorMessage = "É obrigatório informar o código do empréstimo!")]
        [Range(1, int.MaxValue, ErrorMessage = "O código do empréstimo deve ser um número positivo!")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o RA do aluno!")]
        [Range(1, int.MaxValue, ErrorMessage = "O RA do aluno deve ser um número positivo!")]
        public int RAAluno { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o código do livro!")]
        [Range(1, int.MaxValue, ErrorMessage = "O código do livro deve ser um número positivo!")]
        public int CodigoLivro { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a data de retirada!")]
        [DataType(DataType.Date, ErrorMessage = "A data de retirada deve ser uma data válida!")]
        public DateTime DataRetirada { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a data de entrega!")]
        [DataType(DataType.Date, ErrorMessage = "A data de entrega deve ser uma data válida!")]
        public DateTime DataEntrega { get; set; }

        public bool Devolvido { get; set; } = false;

        // Construtor padrão
        public Emprestimo() 
        {
            this.DataRetirada = DateTime.Now;
            this.DataEntrega = DateTime.Now.AddDays(7); // Define a data de entrega como 7 dias após a retirada
        }

        // Construtor com parâmetros para leitura do banco de dados
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
