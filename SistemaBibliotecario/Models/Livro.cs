using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBibliotecario.Models
{
    // Classe Model para representar um livro
    public class Livro
    {
        // Propriedades do livro com validações por Data Annotations
        [Key]
        [Required(ErrorMessage = "É obrigatório informar o código do livro!")]
        [Range(1, int.MaxValue, ErrorMessage = "O código do livro deve ser um número positivo!")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o título do livro!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 100 caracteres!")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o autor do livro!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O autor deve ter entre 2 e 100 caracteres!")]
        public string Autor { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "A categoria deve ter entre 2 e 50 caracteres!")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "A categoria deve conter apenas letras e espaços!")]
        public string Categoria { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "A editora deve ter entre 2 e 50 caracteres!")]
        public string Editora { get; set; }

        public bool Disponivel { get; set; } = true;

        // Construtor padrão
        public Livro() { }

        // Construtor com parâmetros para leitura do banco de dados
        public Livro(int codigo, string titulo, string autor, string categoria, string editora, bool disponivel)
        {
            this.Codigo = codigo;
            this.Titulo = titulo;
            this.Autor = autor;
            this.Categoria = categoria;
            this.Editora = editora;
            this.Disponivel = disponivel;
        }
    }
}
