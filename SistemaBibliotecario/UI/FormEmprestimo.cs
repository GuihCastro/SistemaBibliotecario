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
    public partial class FormEmprestimo : Form
    {
        public FormEmprestimo()
        {
            InitializeComponent();
            CarregarEmprestimosAtivos();
        }

        private void btnRegistrarEmprestimo_Click(object sender, EventArgs e)
        {
            try
            {
                Emprestimo emprestimo = new Emprestimo
                {
                    //Codigo = int.Parse(txtCodigo.Text),
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

        private void btnRegistrarDevolucao_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEmprestimos.CurrentRow == null && string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    // Verifica se o código do empréstimo foi informado
                    MessageBox.Show("Informe o código do empréstimo para registrar a devolução");
                    return;
                }

                // Pegando o código do empréstimo da linha selecionada ou do campo de texto
                int codigoEmprestimo = dgvEmprestimos.CurrentRow != null ? (int)dgvEmprestimos.CurrentRow.Cells["Codigo"].Value : int.Parse(txtCodigo.Text);

                // Registrando a devolução
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    // Verifica se o código do empréstimo foi informado
                    MessageBox.Show("Informe o código do empréstimo para buscar");
                    return;
                }

                // Localiza o empréstimo pelo código
                Emprestimo emprestimo = EmprestimoBLL.BuscarPorCodigo(int.Parse(txtCodigo.Text));
                if (emprestimo == null)
                {
                    // Verifica se o empréstimo foi encontrado
                    MessageBox.Show("Empréstimo não encontrado");
                    return;
                }

                // Preenche os campos com os dados do empréstimo encontrado
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

        private void btnListarPorAluno_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRAAluno.Text))
                {
                    // Verifica se o RA do aluno foi informado
                    MessageBox.Show("Informe o RA do aluno para listar os empréstimos");
                    return;
                }

                // Lista os empréstimos do aluno pelo RA
                List<Emprestimo> emprestimos = EmprestimoBLL.ListarPorAluno(int.Parse(txtRAAluno.Text));
                if (emprestimos.Count == 0)
                {
                    // Verifica se o aluno possui empréstimos
                    MessageBox.Show("Nenhum empréstimo encontrado para este aluno");
                    return;
                }

                // Atualiza a tabela de visualização com os empréstimos encontrados
                dgvEmprestimos.DataSource = emprestimos;
                MessageBox.Show($"Total de empréstimos encontrados: {emprestimos.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar empréstimos do aluno: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnListarAtivos_Click(object sender, EventArgs e)
        {
            try
            {
                List<Emprestimo> emprestimosAtivos = EmprestimoBLL.ListarAtivos();
                if (emprestimosAtivos.Count == 0)
                {
                    // Verifica se há empréstimos ativos
                    MessageBox.Show("Nenhum empréstimo ativo encontrado");
                    return;
                }

                // Atualiza a tabela de visualização com os empréstimos ativos encontrados
                dgvEmprestimos.DataSource = emprestimosAtivos;
                MessageBox.Show($"Total de empréstimos ativos encontrados: {emprestimosAtivos.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar empréstimos ativos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnListarAtrasados_Click(object sender, EventArgs e)
        {
            try
            {
                List<Emprestimo> emprestimosAtrasados = EmprestimoBLL.VerificarAtrasos();
                if (emprestimosAtrasados.Count == 0)
                {
                    // Verifica se há empréstimos atrasados
                    MessageBox.Show("Nenhum empréstimo atrasado encontrado");
                    return;
                }

                // Atualiza a tabela de visualização com os empréstimos atrasados encontrados
                dgvEmprestimos.DataSource = emprestimosAtrasados;
                MessageBox.Show($"Total de empréstimos atrasados encontrados: {emprestimosAtrasados.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar empréstimos atrasados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Carrega os empréstimos ativos na tabela de visualização
        private void CarregarEmprestimosAtivos() => dgvEmprestimos.DataSource = EmprestimoBLL.ListarAtivos();

        // Atualiza os totais de empréstimos ativos e atrasados
        private void AtualizarTotais()
        {
            lblTotal.Text = $"Total: {EmprestimoBLL.ListarAtivos().Count + EmprestimoBLL.ListarDevolvidos().Count}";
            lblAtivos.Text = $"Ativos: {EmprestimoBLL.ListarAtivos().Count}";
            lblAtrasados.Text = $"Atrasados: {EmprestimoBLL.VerificarAtrasos().Count}";
        }

        private void PreencherCampos(Emprestimo emprestimo)
        {
            txtCodigo.Text = emprestimo.Codigo.ToString();
            txtRAAluno.Text = emprestimo.RAAluno.ToString();
            txtCodigoLivro.Text = emprestimo.CodigoLivro.ToString();
            dtpDataRetirada.Value = emprestimo.DataRetirada;
            dtpDataEntrega.Value = emprestimo.DataEntrega;
        }

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
