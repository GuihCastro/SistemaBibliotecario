﻿using System;
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
    /// Formulário para gerenciamento de alunos no sistema bibliotecário.
    /// Permite ao usuário realizar operações CRUD de alunos.
    /// </summary>
    public partial class FormAluno: Form
    {
        /// <summary>
        /// Construtor da classe.
        /// Inicializa os componentes do formulário e define o estado da janela.
        /// </summary>
        public FormAluno()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        /// <summary>
        /// Evento de clique do botão "Inserir".
        /// Recebe os dados do aluno e chama os métodos para validar e inserir no Banco de Dados.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a inserção</exception>
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
                MessageBox.Show("Aluno inserido com sucesso!");
                LimparCampos();
                dgvAlunos.DataSource = AlunoBLL.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inserir: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de clique do botão "Buscar".
        /// Verifica se o RA foi informado e chama o método para buscar o aluno no Banco de Dados.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a busca</exception>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRA.Text))
                {
                    MessageBox.Show("Informe o RA para buscar");
                    return;
                }

                Aluno aluno = AlunoBLL.BuscarPorRA(int.Parse(txtRA.Text));
                if (aluno == null)
                {
                    MessageBox.Show("Aluno não encontrado");
                    return;
                }

                PreencherCampos(aluno);
                List<Aluno> alunos = new List<Aluno>() { aluno };
                dgvAlunos.DataSource = alunos;
                MessageBox.Show("Aluno encontrado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de clique do botão "Atualizar".
        /// Recebe os dados do aluno e chama os métodos para validar e atualizar no Banco de Dados.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a atualização</exception>"
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
                MessageBox.Show("Aluno atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de clique do botão "Excluir".
        /// Verifica se o RA foi informado e chama o método para excluir o aluno do Banco de Dados.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a exclusão</exception>"
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRA.Text))
                {
                    MessageBox.Show("Informe o RA para excluir");
                    return;
                }

                AlunoBLL.Excluir(int.Parse(txtRA.Text));
                MessageBox.Show("Aluno excluído com sucesso!");
                LimparCampos();
                List<Aluno> alunos = AlunoBLL.Listar();
                dgvAlunos.DataSource = alunos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de clique do botão "Listar".
        /// Chama o método para listar todos os alunos cadastrados no Banco de Dados.
        /// </summary>
        /// <exception cref="Exception">Lançada quando ocorre um erro durante a listagem</exception>"
        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Aluno> alunos = AlunoBLL.Listar();
                dgvAlunos.DataSource = alunos;
                MessageBox.Show($"Total de alunos: {alunos.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Preenche os campos do formulário com os dados de um aluno.
        /// </summary>
        /// <param name="aluno">Objeto do tipo Aluno com os dados a serem preenchidos</param>
        private void PreencherCampos(Aluno aluno)
        {
            txtRA.Text = aluno.RA.ToString();
            txtNome.Text = aluno.Nome;
            txtEmail.Text = aluno.Email;
            txtTelefone.Text = aluno.Telefone;
            dtpDataNascimento.Value = aluno.DataNascimento;
        }

        /// <summary>
        /// Limpa os campos do formulário.
        /// </summary>
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
