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
    public partial class FormLivro : Form
    {
        public FormLivro()
        {
            InitializeComponent();
            CarregarLivros();

            this.WindowState = FormWindowState.Maximized;
            //this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

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

        private void PreencherCampos(Livro livro)
        {
            txtCodigo.Text = livro.Codigo.ToString();
            txtTitulo.Text = livro.Titulo;
            txtAutor.Text = livro.Autor;
            txtCategoria.Text = livro.Categoria;
            txtEditora.Text = livro.Editora;
            chkDisponivel.Checked = livro.Disponivel;
        }

        private void LimparCampos()
        {
            txtCodigo.Clear();
            txtTitulo.Clear();
            txtAutor.Clear();
            txtCategoria.Clear();
            txtEditora.Clear();
            chkDisponivel.Checked = false;
        }

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
