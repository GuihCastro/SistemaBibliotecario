using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBibliotecario.Models
{
    /// <summary>
    /// Classe que representa um livro no sistema bibliotecário.
    /// </summary>
    public class Livro
    {
        /// <summary>
        /// Código do livro - Chave Primária.
        /// </summary>
        [Key]
        [Required(ErrorMessage = "É obrigatório informar o código do livro!")]
        [Range(1, int.MaxValue, ErrorMessage = "O código do livro deve ser um número positivo!")]
        public int Codigo { get; set; }

        /// <summary>
        /// Título do livro - Campo obrigatório.
        /// </summary>
        [Required(ErrorMessage = "É obrigatório informar o título do livro!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 100 caracteres!")]
        public string Titulo { get; set; }

        /// <summary>
        /// Autor do livro - Campo obrigatório.
        /// </summary>
        [Required(ErrorMessage = "É obrigatório informar o autor do livro!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O autor deve ter entre 2 e 100 caracteres!")]
        public string Autor { get; set; }

        /// <summary>
        /// Categoria do livro - Campo opcional.
        /// </summary>
        [StringLength(50, MinimumLength = 2, ErrorMessage = "A categoria deve ter entre 2 e 50 caracteres!")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "A categoria deve conter apenas letras e espaços!")]
        public string Categoria { get; set; }

        /// <summary>
        /// Editora do livro - Campo opcional.
        /// </summary>
        [StringLength(50, MinimumLength = 2, ErrorMessage = "A editora deve ter entre 2 e 50 caracteres!")]
        public string Editora { get; set; }

        /// <summary>
        /// Disponibilidade do livro - Padrão é verdadeiro (disponível).
        /// </summary>
        public bool Disponivel { get; set; } = true;

        /// <summary>
        /// Construtor padrão da classe.
        /// </summary>
        public Livro() { }

        /// <summary>
        /// Construtor completo com parâmetros.
        /// </summary>
        /// <param name="codigo">Código do livro</param>
        /// <param name="titulo">Título do livro</param>
        /// <param name="autor">Nome do autor</param>
        /// <param name="categoria">Categoria (opcional)</param>
        /// <param name="editora">Editora (opcional)</param>
        /// <param name="disponivel">Disponibilidade do livro</param>
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
