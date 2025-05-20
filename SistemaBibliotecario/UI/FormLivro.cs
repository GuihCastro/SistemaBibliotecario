using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaBibliotecario.BLL;
using SistemaBibliotecario.Models;

namespace SistemaBibliotecario.UI
{
    /// <summary>
    /// Formulário para gerenciamento de livros no sistema bibliotecário.
    /// Permite ao usuário realizar operações CRUD de livros.
    /// </summary>
    public partial class FormLivro : Form
    {
        /// <summary>
        /// Construtor da classe.
        /// Inicializa os componentes do formulário e define o estado da janela.
        /// </summary>
        public FormLivro()
        {
            InitializeComponent();
            CarregarLivros();

            this.WindowState = FormWindowState.Maximized;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        /// <summary>
        /// Evento de clique do botão "Inserir".
        /// Recebe os dados do livro e chama os métodos para validar e inserir no Banco de Dados.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a inserção</exception>"
        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                Livro livro = new Livro()
                {
                    Codigo = int.Parse(txtCodigo.Text),
                    Titulo = txtTitulo.Text,
                    Autor = txtAutor.Text,
                    Categoria = txtCategoria.Text,
                    Editora = txtEditora.Text,
                    Disponivel = true
                };

                LivroBLL.Inserir(livro);
                MessageBox.Show("Livro inserido com sucesso!");
                LimparCampos();
                dgvLivros.DataSource = LivroBLL.Listar();
                AtualizarTotais();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inserir: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de clique do botão "Buscar".
        /// Verifica se o código do livro foi informado e chama o método para buscar no Banco de Dados.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a busca</exception>"
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("Informe o código do livro para buscar");
                    return;
                }

                Livro livro = LivroBLL.BuscarPorCodigo(int.Parse(txtCodigo.Text));
                if (livro == null)
                {
                    MessageBox.Show("Livro não encontrado");
                    return;
                }

                PreencherCampos(livro);
                List<Livro> livros = new List<Livro>() { livro };
                dgvLivros.DataSource = livros;
                MessageBox.Show("Livro encontrado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de clique do botão "Atualizar".
        /// Recebe os dados do livro e chama os métodos para validar e atualizar no Banco de Dados.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a atualização</exception>"
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Livro livro = new Livro
                {
                    Codigo = int.Parse(txtCodigo.Text),
                    Titulo = txtTitulo.Text,
                    Autor = txtAutor.Text,
                    Categoria = txtCategoria.Text,
                    Editora = txtEditora.Text,
                    Disponivel = chkDisponivel.Checked
                };
                LivroBLL.Atualizar(livro);
                Livro livroAtualizado = LivroBLL.BuscarPorCodigo(livro.Codigo);
                PreencherCampos(livroAtualizado);
                List<Livro> livros = new List<Livro>() { livroAtualizado };
                dgvLivros.DataSource = livros;
                MessageBox.Show("Livro atualizado com sucesso!");
                AtualizarTotais();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de clique do botão "Excluir".
        /// Verifica se o código do livro foi informado e chama o método para excluir no Banco de Dados.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a exclusão</exception>""
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("Informe o código do livro para excluir");
                    return;
                }

                LivroBLL.Excluir(int.Parse(txtCodigo.Text));
                MessageBox.Show("Livro excluído com sucesso!");
                LimparCampos();
                List<Livro> livros = LivroBLL.Listar();
                dgvLivros.DataSource = livros;
                AtualizarTotais();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de clique do botão "Listar".
        /// Chama o método para listar todos os livros cadastrados no sistema.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a listagem</exception>""
        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Livro> livros = LivroBLL.Listar();
                dgvLivros.DataSource = livros;
                MessageBox.Show("Livros listados com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de clique do botão "Listar Disponíveis".
        /// Chama o método para listar todos os livros disponíveis no sistema.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a listagem</exception>"""
        private void btnListarDisponiveis_Click(object sender, EventArgs e)
        {
            try
            {
                List<Livro> livrosDisponiveis = LivroBLL.ListarDisponiveis();
                dgvLivros.DataSource = livrosDisponiveis;
                MessageBox.Show("Livros disponíveis listados com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Preenche os campos do formulário com os dados do livro.
        /// </summary>
        /// <param name="livro">Objeto do tipo Livro com os dados para serem preenchidos</param>
        private void PreencherCampos(Livro livro)
        {
            txtCodigo.Text = livro.Codigo.ToString();
            txtTitulo.Text = livro.Titulo;
            txtAutor.Text = livro.Autor;
            txtCategoria.Text = livro.Categoria;
            txtEditora.Text = livro.Editora;
            chkDisponivel.Checked = livro.Disponivel;
        }

        /// <summary>
        /// Limpa os campos do formulário.
        /// </summary>
        private void LimparCampos()
        {
            txtCodigo.Clear();
            txtTitulo.Clear();
            txtAutor.Clear();
            txtCategoria.Clear();
            txtEditora.Clear();
            chkDisponivel.Checked = false;
        }

        /// <summary>
        /// Carrega os livros cadastrados no sistema e exibe na DataGridView.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante o carregamento</exception>""
        private void CarregarLivros()
        {
            try
            {
                List<Livro> livros = LivroBLL.Listar();
                dgvLivros.DataSource = livros;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar livros: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Atualiza os totais de livros e livros disponíveis exibidos no formulário.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a atualização</exception>"
        private void AtualizarTotais()
        {
            try
            {
                int totalLivros = LivroBLL.Listar().Count;
                int totalDisponiveis = LivroBLL.ListarDisponiveis().Count;
                lblTotal.Text = $"Total de Livros: {totalLivros}";
                lblDisponiveis.Text = $"Livros Disponíveis: {totalDisponiveis}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar totais: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
