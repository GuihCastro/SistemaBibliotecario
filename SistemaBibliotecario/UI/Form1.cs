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
using SistemaBibliotecario.DAL;
using SistemaBibliotecario.Helpers;
using SistemaBibliotecario.Models;

namespace SistemaBibliotecario
{
    public partial class Form1 : Form
    {
        private AlunoBLL AlunoBLL = new AlunoBLL();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Testa a conexão com o banco de dados ao carregar o formulário
            ConexaoBD.TestarConexao();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Aluno aluno = new Aluno();
            //{
            //    RA = int.Parse(txtRA.Text),
            //    Nome = txtNome.Text,
            //    Email = txtEmail.Text,
            //};

            if (Validador.Validar(aluno, out var erros))
            {
                // Se não houver erros, prossegue com o cadastro
                AlunoBLL.Inserir(aluno);
                MessageBox.Show("Aluno cadastrado com sucesso!");
            }
            else
            {
                MessageBox.Show($"Erro(s) ao cadastrar aluno: {string.Join("\n", erros)}", "Erro(s) de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
