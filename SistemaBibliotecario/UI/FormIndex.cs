using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaBibliotecario.UI
{
    /// <summary>
    /// Classe principal do sistema bibliotecário.
    /// Gerencia a navegação entre os formulários de Alunos, Livros e Empréstimos.
    /// </summary>
    public partial class FormIndex : Form
    {
        /// <summary>
        /// Construtor da classe.
        /// Inicializa os componentes do formulário e define o título da janela.
        /// </summary>
        public FormIndex()
        {
            InitializeComponent();
            this.Text = "Sistema Bibliotecário";
        }

        /// <summary>
        /// Evento de clique do item de menu "Alunos".
        /// Abre o formulário de gerenciamento de alunos.
        /// </summary>
        private void btnAlunos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormAluno());
        }

        /// <summary>
        /// Evento de clique do item de menu "Livros".
        /// Abre o formulário de gerenciamento de livros.
        /// </summary>
        private void btnLivros_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormLivro());
        }


        /// <summary>
        /// Evento de clique do item de menu "Empréstimos".
        /// Abre o formulário de gerenciamento de empréstimos.
        /// </summary>
        private void btnEmprestimos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormEmprestimo());
        }

        /// <summary>
        /// Evento de clique do botão "Sair".
        /// Fecha o aplicativo.
        /// </summary>
        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Método para abrir um novo formulário.
        /// Fecha todos os formulários abertos antes de abrir o novo formulário.
        /// </summary>
        /// <param name="form">Formulário a ser aberto</param>
        private void AbrirFormulario(Form form)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }

            // Abre o novo formulário
            form.MdiParent = this;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Show();
        }
    }
}
