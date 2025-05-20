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
    /// Formulário para gerenciamento de empréstimos no sistema bibliotecário.
    /// Permite ao usuário realizar operações de empréstimo e devolução de livros.
    /// </summary>
    public partial class FormEmprestimo : Form
    {
        /// <summary>
        /// Construtor da classe.
        /// Inicializa os componentes do formulário e define o estado da janela.
        /// </summary>
        public FormEmprestimo()
        {
            InitializeComponent();
            CarregarEmprestimosAtivos();

            this.WindowState = FormWindowState.Maximized;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        /// <summary>
        /// Evento de clique do botão "Registrar Empréstimo".
        /// Recebe os dados do empréstimo e chama os métodos para validar e registrar no Banco de Dados.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante o registro</exception>"
        private void btnRegistrarEmprestimo_Click(object sender, EventArgs e)
        {
            try
            {
                Emprestimo emprestimo = new Emprestimo
                {
                    RAAluno = int.Parse(txtRAAluno.Text),
                    CodigoLivro = int.Parse(txtCodigoLivro.Text),
                    DataRetirada = dtpDataRetirada.Value,
                    DataEntrega = dtpDataEntrega.Value,
                    Devolvido = chkDevolvido.Checked
                };

                EmprestimoBLL.RegistrarEmprestimo(emprestimo);
                MessageBox.Show("Empréstimo registrado com sucesso! Código: " + emprestimo.Codigo);
                LimparCampos();
                CarregarEmprestimosAtivos();
                AtualizarTotais();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar empréstimo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de clique do botão "Registrar Devolução".
        /// Verifica se o código do empréstimo foi informado e chama o método para registrar a devolução.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante o registro da devolução</exception>"
        private void btnRegistrarDevolucao_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEmprestimos.CurrentRow == null && string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("Informe o código do empréstimo para registrar a devolução");
                    return;
                }

                int codigoEmprestimo = dgvEmprestimos.CurrentRow != null ? (int)dgvEmprestimos.CurrentRow.Cells["Codigo"].Value : int.Parse(txtCodigo.Text);

                EmprestimoBLL.RegistrarDevolucao(codigoEmprestimo);
                MessageBox.Show("Devolução registrada com sucesso!");
                CarregarEmprestimosAtivos();
                AtualizarTotais();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar devolução: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de clique do botão "Buscar Empréstimo".
        /// Verifica se o código do empréstimo foi informado e chama o método para buscar o empréstimo no Banco de Dados.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a busca</exception>""
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("Informe o código do empréstimo para buscar");
                    return;
                }

                Emprestimo emprestimo = EmprestimoBLL.BuscarPorCodigo(int.Parse(txtCodigo.Text));
                if (emprestimo == null)
                {
                    MessageBox.Show("Empréstimo não encontrado");
                    return;
                }

                PreencherCampos(emprestimo);
                List<Emprestimo> emprestimos = new List<Emprestimo>() { emprestimo };
                dgvEmprestimos.DataSource = emprestimos;
                MessageBox.Show("Empréstimo encontrado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar empréstimo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de clique do botão "Listar Empréstimos por Aluno".
        /// Verifica se o RA do aluno foi informado e chama o método para listar os empréstimos do aluno.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a listagem</exception>""
        private void btnListarPorAluno_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRAAluno.Text))
                {
                    MessageBox.Show("Informe o RA do aluno para listar os empréstimos");
                    return;
                }

                List<Emprestimo> emprestimos = EmprestimoBLL.ListarPorAluno(int.Parse(txtRAAluno.Text));
                if (emprestimos.Count == 0)
                {
                    MessageBox.Show("Nenhum empréstimo encontrado para este aluno");
                    return;
                }

                dgvEmprestimos.DataSource = emprestimos;
                MessageBox.Show($"Total de empréstimos encontrados: {emprestimos.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar empréstimos do aluno: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de clique do botão "Listar Empréstimos Ativos".
        /// Verifica se há empréstimos ativos e chama o método para listar os empréstimos ativos.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a listagem</exception>"""
        private void btnListarAtivos_Click(object sender, EventArgs e)
        {
            try
            {
                List<Emprestimo> emprestimosAtivos = EmprestimoBLL.ListarAtivos();
                if (emprestimosAtivos.Count == 0)
                {
                    MessageBox.Show("Nenhum empréstimo ativo encontrado");
                    return;
                }

                dgvEmprestimos.DataSource = emprestimosAtivos;
                MessageBox.Show($"Total de empréstimos ativos encontrados: {emprestimosAtivos.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar empréstimos ativos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de clique do botão "Listar Empréstimos Atrasados".
        /// Chama o método para listar os empréstimos atrasados.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a listagem</exception>""""
        private void btnListarAtrasados_Click(object sender, EventArgs e)
        {
            try
            {
                List<Emprestimo> emprestimosAtrasados = EmprestimoBLL.VerificarAtrasos();
                if (emprestimosAtrasados.Count == 0)
                {
                    MessageBox.Show("Nenhum empréstimo atrasado encontrado");
                    return;
                }

                dgvEmprestimos.DataSource = emprestimosAtrasados;
                MessageBox.Show($"Total de empréstimos atrasados encontrados: {emprestimosAtrasados.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar empréstimos atrasados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Carrega os empréstimos ativos no DataGridView.
        /// </summary>
        private void CarregarEmprestimosAtivos() => dgvEmprestimos.DataSource = EmprestimoBLL.ListarAtivos();

        /// <summary>
        /// Atualiza os totais de empréstimos, ativos e atrasados.
        /// </summary>
        private void AtualizarTotais()
        {
            lblTotal.Text = $"Total: {EmprestimoBLL.ListarAtivos().Count + EmprestimoBLL.ListarDevolvidos().Count}";
            lblAtivos.Text = $"Ativos: {EmprestimoBLL.ListarAtivos().Count}";
            lblAtrasados.Text = $"Atrasados: {EmprestimoBLL.VerificarAtrasos().Count}";
        }

        /// <summary>
        /// Preenche os campos do formulário com os dados do empréstimo selecionado.
        /// </summary>
        /// <param name="emprestimo">Objeto do tipo Emprestimo com os dados a serem preenchidos</param>
        private void PreencherCampos(Emprestimo emprestimo)
        {
            txtCodigo.Text = emprestimo.Codigo.ToString();
            txtRAAluno.Text = emprestimo.RAAluno.ToString();
            txtCodigoLivro.Text = emprestimo.CodigoLivro.ToString();
            dtpDataRetirada.Value = emprestimo.DataRetirada;
            dtpDataEntrega.Value = emprestimo.DataEntrega;
        }

        /// <summary>
        /// Limpa os campos do formulário.
        /// </summary>
        private void LimparCampos()
        {
            txtCodigo.Clear();
            txtRAAluno.Clear();
            txtCodigoLivro.Clear();
            dtpDataRetirada.Value = DateTime.Now;
            dtpDataEntrega.Value = DateTime.Now.AddDays(7);
        }
    }
}
