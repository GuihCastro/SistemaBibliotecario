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
    public partial class FormIndex : Form
    {
        public FormIndex()
        {
            InitializeComponent();
            this.Text = "Sistema Bibliotecário";
            //this.WindowState = FormWindowState.Maximized;
        }

        private void btnAlunos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormAluno());
        }

        private void btnLivros_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormLivro());
        }

        private void btnEmprestimos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormEmprestimo());
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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
