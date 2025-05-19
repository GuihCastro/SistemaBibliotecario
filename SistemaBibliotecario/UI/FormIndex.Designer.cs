using System.Windows.Forms;

namespace SistemaBibliotecario.UI
{
    partial class FormIndex
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Button btnAlunos;
        private Button btnLivros;
        private Button btnEmprestimos;
        private Button btnSair;
        private MenuStrip menuPrincipal;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "FormIndex";

            this.menuPrincipal = new MenuStrip();
            menuPrincipal.Dock = DockStyle.Top;

            // Cadastros
            ToolStripMenuItem cadastrosMenu = new ToolStripMenuItem("Cadastros");

            // Subitens
            ToolStripMenuItem alunosMenu = new ToolStripMenuItem("Alunos");
            alunosMenu.Click += btnAlunos_Click;

            ToolStripMenuItem livrosMenu = new ToolStripMenuItem("Livros");
            livrosMenu.Click += btnLivros_Click;

            ToolStripMenuItem emprestimosMenu = new ToolStripMenuItem("Empréstimos");
            emprestimosMenu.Click += btnEmprestimos_Click;


            // Adicionando subitens ao menu
            cadastrosMenu.DropDownItems.AddRange(new ToolStripItem[] { alunosMenu, livrosMenu, emprestimosMenu });

            // Sair
            ToolStripMenuItem sairMenu = new ToolStripMenuItem("Sair");
            sairMenu.Click += btnSair_Click;

            // Adicionando os menus ao menu principal
            menuPrincipal.Items.AddRange(new ToolStripItem[] { cadastrosMenu, sairMenu });

            // Configurando o formulário
            this.IsMdiContainer = true;
            this.Controls.Add(this.menuPrincipal);
            //this.Size = new System.Drawing.Size(1000, 700);
        }

        #endregion
    }
}