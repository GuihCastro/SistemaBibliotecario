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
    public partial class FormTesteAluno : Form
    {
        public FormTesteAluno()
        {
            InitializeComponent();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                Aluno aluno = new Aluno
                {
                    RA = int.Parse(txtRA.Text),
                    Nome = txtNome.Text,
                    Email = txtEmail.Text,
                    Telefone = txtTelefone.Text,
                    DataNascimento = dtpDataNascimento.Value
                };

                AlunoBLL.Inserir(aluno);
                MessageBox.Show("Aluno inserido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCampos();
            }
            catch (FormatException)
            {
                MessageBox.Show("Formato inválido. Verifique os dados informados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inserir aluno: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRA.Text))
                {
                    MessageBox.Show("Informe o RA do aluno para buscar!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Aluno aluno = AlunoBLL.BuscarPorRA(int.Parse(txtRA.Text));
                if (aluno == null)
                {
                    MessageBox.Show("Aluno não encontrado!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PreencherCampos(aluno);
                List<Aluno> alunos = new List<Aluno>() { aluno };
                dgvAlunos.DataSource = alunos;
                MessageBox.Show("Aluno encontrado!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Formato inválido. Verifique os dados informados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar aluno: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Aluno aluno = new Aluno
                {
                    RA = int.Parse(txtRA.Text),
                    Nome = txtNome.Text,
                    Email = txtEmail.Text,
                    Telefone = txtTelefone.Text,
                    DataNascimento = dtpDataNascimento.Value
                };
                AlunoBLL.Atualizar(aluno);
                Aluno alunoAtualizado = AlunoBLL.BuscarPorRA(aluno.RA);
                PreencherCampos(alunoAtualizado);
                List<Aluno> alunos = new List<Aluno>() { alunoAtualizado };
                dgvAlunos.DataSource = alunos;
                MessageBox.Show("Aluno atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Formato inválido. Verifique os dados informados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar aluno: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRA.Text))
                {
                    MessageBox.Show("Informe o RA do aluno para excluir!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AlunoBLL.Excluir(int.Parse(txtRA.Text));
                MessageBox.Show("Aluno excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCampos();
            }
            catch (FormatException)
            {
                MessageBox.Show("Formato inválido. Verifique os dados informados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir aluno: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Aluno> alunos = AlunoBLL.Listar();
                dgvAlunos.DataSource = alunos;
                MessageBox.Show($"Lista de alunos carregada com sucesso!\nTotal de alunos: {alunos.Count}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar alunos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PreencherCampos(Aluno aluno)
        {
            txtRA.Text = aluno.RA.ToString();
            txtNome.Text = aluno.Nome;
            txtEmail.Text = aluno.Email;
            txtTelefone.Text = aluno.Telefone;
            dtpDataNascimento.Value = aluno.DataNascimento;
        } 

        private void LimparCampos()
        {
            txtRA.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            dtpDataNascimento.Value = DateTime.Now;
        }
    }
}
