using System.Windows.Forms;
using SistemaBibliotecario.BLL;

namespace SistemaBibliotecario.UI
{
    partial class FormEmprestimo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Label lblCodigo;
        private TextBox txtCodigo;
        private Label lblRAAluno;
        private TextBox txtRAAluno;
        private Label lblCodigoLivro;
        private TextBox txtCodigoLivro;
        private Label lblDataRetirada;
        private DateTimePicker dtpDataRetirada;
        private Label lblDataEntrega;
        private DateTimePicker dtpDataEntrega;
        private Label lblDevolvido;
        private CheckBox chkDevolvido;
        private Button btnRegistrarEmprestimo;
        private Button btnRegistrarDevolucao;
        private Button btnBuscar;
        private Button btnListarPorAluno;
        private Button btnListarAtivos;
        private Button btnListarAtrasados;
        private DataGridView dgvEmprestimos;
        private Label lblTotal;
        private Label lblAtivos;
        private Label lblAtrasados;

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

            // Inicializando Componentes
            this.lblCodigo = new Label();
            this.txtCodigo = new TextBox();
            this.lblRAAluno = new Label();
            this.txtRAAluno = new TextBox();
            this.lblCodigoLivro = new Label();
            this.txtCodigoLivro = new TextBox();
            this.lblDataRetirada = new Label();
            this.dtpDataRetirada = new DateTimePicker();
            this.lblDataEntrega = new Label();
            this.dtpDataEntrega = new DateTimePicker();
            this.lblDevolvido = new Label();
            this.chkDevolvido = new CheckBox();
            this.btnRegistrarEmprestimo = new Button();
            this.btnRegistrarDevolucao = new Button();
            this.btnBuscar = new Button();
            this.btnListarPorAluno = new Button();
            this.btnListarAtivos = new Button();
            this.btnListarAtrasados = new Button();
            this.dgvEmprestimos = new DataGridView();
            this.lblTotal = new Label();
            this.lblAtivos = new Label();
            this.lblAtrasados = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmprestimos)).BeginInit();
            this.SuspendLayout();

            // lblCodigo
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(12, 15);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(26, 13);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código:";

            // txtCodigo
            this.txtCodigo.Location = new System.Drawing.Point(90, 12);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(100, 20);
            this.txtCodigo.TabIndex = 1;

            // lblRAAluno
            this.lblRAAluno.AutoSize = true;
            this.lblRAAluno.Location = new System.Drawing.Point(12, 41);
            this.lblRAAluno.Name = "lblRAAluno";
            this.lblRAAluno.Size = new System.Drawing.Size(26, 13);
            this.lblRAAluno.TabIndex = 2;
            this.lblRAAluno.Text = "RA Aluno:";

            // txtRAAluno
            this.txtRAAluno.Location = new System.Drawing.Point(90, 38);
            this.txtRAAluno.Name = "txtRAAluno";
            this.txtRAAluno.Size = new System.Drawing.Size(100, 20);
            this.txtRAAluno.TabIndex = 3;

            // lblCodigoLivro
            this.lblCodigoLivro.AutoSize = true;
            this.lblCodigoLivro.Location = new System.Drawing.Point(12, 67);
            this.lblCodigoLivro.Name = "lblCodigoLivro";
            this.lblCodigoLivro.Size = new System.Drawing.Size(26, 13);
            this.lblCodigoLivro.TabIndex = 4;
            this.lblCodigoLivro.Text = "Código Livro:";

            // txtCodigoLivro
            this.txtCodigoLivro.Location = new System.Drawing.Point(90, 64);
            this.txtCodigoLivro.Name = "txtCodigoLivro";
            this.txtCodigoLivro.Size = new System.Drawing.Size(100, 20);
            this.txtCodigoLivro.TabIndex = 5;

            // lblDataRetirada
            this.lblDataRetirada.AutoSize = true;
            this.lblDataRetirada.Location = new System.Drawing.Point(12, 93);
            this.lblDataRetirada.Name = "lblDataRetirada";
            this.lblDataRetirada.Size = new System.Drawing.Size(26, 13);
            this.lblDataRetirada.TabIndex = 6;
            this.lblDataRetirada.Text = "Data Retirada:";

            // dtpDataRetirada
            this.dtpDataRetirada.Format = DateTimePickerFormat.Short;
            this.dtpDataRetirada.Location = new System.Drawing.Point(90, 90);
            this.dtpDataRetirada.Name = "dtpDataRetirada";
            this.dtpDataRetirada.Size = new System.Drawing.Size(200, 20);
            this.dtpDataRetirada.TabIndex = 7;

            // lblDataEntrega
            this.lblDataEntrega.AutoSize = true;
            this.lblDataEntrega.Location = new System.Drawing.Point(12, 119);
            this.lblDataEntrega.Name = "lblDataEntrega";
            this.lblDataEntrega.Size = new System.Drawing.Size(26, 13);
            this.lblDataEntrega.TabIndex = 8;
            this.lblDataEntrega.Text = "Data Entrega:";

            // dtpDataEntrega
            this.dtpDataEntrega.Format = DateTimePickerFormat.Short;
            this.dtpDataEntrega.Location = new System.Drawing.Point(90, 116);
            this.dtpDataEntrega.Name = "dtpDataEntrega";
            this.dtpDataEntrega.Size = new System.Drawing.Size(200, 20);
            this.dtpDataEntrega.TabIndex = 9;

            // lblDevolvido
            this.lblDevolvido.AutoSize = true;
            this.lblDevolvido.Location = new System.Drawing.Point(12, 145);
            this.lblDevolvido.Name = "lblDevolvido";
            this.lblDevolvido.Size = new System.Drawing.Size(26, 13);
            this.lblDevolvido.TabIndex = 10;
            this.lblDevolvido.Text = "Devolvido:";

            // chkDevolvido
            this.chkDevolvido.AutoSize = true;
            this.chkDevolvido.Location = new System.Drawing.Point(90, 144);
            this.chkDevolvido.Name = "chkDevolvido";
            this.chkDevolvido.Size = new System.Drawing.Size(15, 14);
            this.chkDevolvido.TabIndex = 11;
            this.chkDevolvido.UseVisualStyleBackColor = true;

            // btnRegistrarEmprestimo
            this.btnRegistrarEmprestimo.Location = new System.Drawing.Point(12, 164);
            this.btnRegistrarEmprestimo.Name = "btnRegistrarEmprestimo";
            this.btnRegistrarEmprestimo.Size = new System.Drawing.Size(150, 23);
            this.btnRegistrarEmprestimo.TabIndex = 12;
            this.btnRegistrarEmprestimo.Text = "Registrar Empréstimo";
            this.btnRegistrarEmprestimo.UseVisualStyleBackColor = true;
            this.btnRegistrarEmprestimo.Click += new System.EventHandler(this.btnRegistrarEmprestimo_Click);

            // btnRegistrarDevolucao
            this.btnRegistrarDevolucao.Location = new System.Drawing.Point(168, 164);
            this.btnRegistrarDevolucao.Name = "btnRegistrarDevolucao";
            this.btnRegistrarDevolucao.Size = new System.Drawing.Size(150, 23);
            this.btnRegistrarDevolucao.TabIndex = 13;
            this.btnRegistrarDevolucao.Text = "Registrar Devolução";
            this.btnRegistrarDevolucao.UseVisualStyleBackColor = true;
            this.btnRegistrarDevolucao.Click += new System.EventHandler(this.btnRegistrarDevolucao_Click);

            // btnBuscar
            this.btnBuscar.Location = new System.Drawing.Point(324, 164);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(100, 23);
            this.btnBuscar.TabIndex = 14;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);

            // btnListarPorAluno
            this.btnListarPorAluno.Location = new System.Drawing.Point(430, 164);
            this.btnListarPorAluno.Name = "btnListarPorAluno";
            this.btnListarPorAluno.Size = new System.Drawing.Size(100, 23);
            this.btnListarPorAluno.TabIndex = 15;
            this.btnListarPorAluno.Text = "Listar por Aluno";
            this.btnListarPorAluno.UseVisualStyleBackColor = true;
            this.btnListarPorAluno.Click += new System.EventHandler(this.btnListarPorAluno_Click);

            // btnListarAtivos
            this.btnListarAtivos.Location = new System.Drawing.Point(536, 164);
            this.btnListarAtivos.Name = "btnListarAtivos";
            this.btnListarAtivos.Size = new System.Drawing.Size(100, 23);
            this.btnListarAtivos.TabIndex = 16;
            this.btnListarAtivos.Text = "Listar Ativos";
            this.btnListarAtivos.UseVisualStyleBackColor = true;
            this.btnListarAtivos.Click += new System.EventHandler(this.btnListarAtivos_Click);

            // btnListarAtrasados
            this.btnListarAtrasados.Location = new System.Drawing.Point(642, 164);
            this.btnListarAtrasados.Name = "btnListarAtrasados";
            this.btnListarAtrasados.Size = new System.Drawing.Size(100, 23);
            this.btnListarAtrasados.TabIndex = 17;
            this.btnListarAtrasados.Text = "Listar Atrasados";
            this.btnListarAtrasados.UseVisualStyleBackColor = true;
            this.btnListarAtrasados.Click += new System.EventHandler(this.btnListarAtrasados_Click);

            // dgvEmprestimos
            this.dgvEmprestimos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmprestimos.Location = new System.Drawing.Point(12, 193);
            this.dgvEmprestimos.Name = "dgvEmprestimos";
            this.dgvEmprestimos.RowTemplate.Height = 25;
            this.dgvEmprestimos.Size = new System.Drawing.Size(776, 245);
            this.dgvEmprestimos.TabIndex = 18;

            // lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(12, 441);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(26, 13);
            this.lblTotal.TabIndex = 19;
            this.lblTotal.Text = "Total: ";
            this.lblTotal.Text += EmprestimoBLL.ListarAtivos().Count + EmprestimoBLL.ListarDevolvidos().Count;

            // lblAtivos
            this.lblAtivos.AutoSize = true;
            this.lblAtivos.Location = new System.Drawing.Point(112, 441);
            this.lblAtivos.Name = "lblAtivos";
            this.lblAtivos.Size = new System.Drawing.Size(26, 13);
            this.lblAtivos.TabIndex = 20;
            this.lblAtivos.Text = "Ativos: ";
            this.lblAtivos.Text += EmprestimoBLL.ListarAtivos().Count.ToString();

            // lblAtrasados
            this.lblAtrasados.AutoSize = true;
            this.lblAtrasados.Location = new System.Drawing.Point(212, 441);
            this.lblAtrasados.Name = "lblAtrasados";
            this.lblAtrasados.Size = new System.Drawing.Size(26, 13);
            this.lblAtrasados.TabIndex = 21;
            this.lblAtrasados.Text = "Atrasados: ";
            this.lblAtrasados.Text += EmprestimoBLL.VerificarAtrasos().Count.ToString();

            // FormEmprestimo
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblAtrasados);
            this.Controls.Add(this.lblAtivos);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dgvEmprestimos);
            this.Controls.Add(this.btnListarAtrasados);
            this.Controls.Add(this.btnListarAtivos);
            this.Controls.Add(this.btnListarPorAluno);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnRegistrarDevolucao);
            this.Controls.Add(this.btnRegistrarEmprestimo);
            this.Controls.Add(this.chkDevolvido);
            this.Controls.Add(this.lblDevolvido);
            this.Controls.Add(this.dtpDataEntrega);
            this.Controls.Add(this.lblDataEntrega);
            this.Controls.Add(this.dtpDataRetirada);
            this.Controls.Add(this.lblDataRetirada);
            this.Controls.Add(this.txtCodigoLivro);
            this.Controls.Add(this.lblCodigoLivro);
            this.Controls.Add(this.txtRAAluno);
            this.Controls.Add(this.lblRAAluno);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.lblCodigo);
            this.Name = "FormEmprestimo";
            this.Text = "Formumário de Emprestimos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmprestimos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}