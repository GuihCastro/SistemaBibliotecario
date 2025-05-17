using SistemaBibliotecario.BLL;

namespace SistemaBibliotecario.UI
{
    partial class FormLivro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label lblAutor;
        private System.Windows.Forms.TextBox txtAutor;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.TextBox txtCategoria;
        private System.Windows.Forms.Label lblEditora;
        private System.Windows.Forms.TextBox txtEditora;
        private System.Windows.Forms.CheckBox chkDisponivel;
        private System.Windows.Forms.Button btnInserir;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnListar;
        private System.Windows.Forms.Button btnListarDisponiveis;
        private System.Windows.Forms.DataGridView dgvLivros;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblDisponiveis;

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
            this.ClientSize = new System.Drawing.Size(850, 500);

            // Controles
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.lblAutor = new System.Windows.Forms.Label();
            this.txtAutor = new System.Windows.Forms.TextBox();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.txtCategoria = new System.Windows.Forms.TextBox();
            this.lblEditora = new System.Windows.Forms.Label();
            this.txtEditora = new System.Windows.Forms.TextBox();
            this.chkDisponivel = new System.Windows.Forms.CheckBox();
            this.btnInserir = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnListar = new System.Windows.Forms.Button();
            this.btnListarDisponiveis = new System.Windows.Forms.Button();
            this.dgvLivros = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblDisponiveis = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLivros)).BeginInit();
            this.SuspendLayout();

            // lblCodigo
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(20, 20);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(40, 13);
            this.lblCodigo.Text = "Código:";
            this.lblCodigo.TabIndex = 0;

            // txtCodigo
            this.txtCodigo.Location = new System.Drawing.Point(80, 20);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(100, 20);
            this.txtCodigo.TabIndex = 1;


            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 60);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(36, 13);
            this.lblTitulo.Text = "Título:";
            this.lblTitulo.TabIndex = 2;

            // txtTitulo
            this.txtTitulo.Location = new System.Drawing.Point(80, 60);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(200, 20);
            this.txtTitulo.TabIndex = 3;

            // lblAutor
            this.lblAutor.AutoSize = true;
            this.lblAutor.Location = new System.Drawing.Point(20, 100);
            this.lblAutor.Name = "lblAutor";
            this.lblAutor.Size = new System.Drawing.Size(34, 13);
            this.lblAutor.Text = "Autor:";
            this.lblAutor.TabIndex = 4;

            // txtAutor
            this.txtAutor.Location = new System.Drawing.Point(80, 100);
            this.txtAutor.Name = "txtAutor";
            this.txtAutor.Size = new System.Drawing.Size(200, 20);
            this.txtAutor.TabIndex = 5;

            // lblCategoria
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new System.Drawing.Point(20, 140);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(54, 13);
            this.lblCategoria.Text = "Categoria:";
            this.lblCategoria.TabIndex = 6;

            // txtCategoria
            this.txtCategoria.Location = new System.Drawing.Point(80, 140);
            this.txtCategoria.Name = "txtCategoria";
            this.txtCategoria.Size = new System.Drawing.Size(200, 20);
            this.txtCategoria.TabIndex = 7;

            // lblEditora
            this.lblEditora.AutoSize = true;
            this.lblEditora.Location = new System.Drawing.Point(20, 180);
            this.lblEditora.Name = "lblEditora";
            this.lblEditora.Size = new System.Drawing.Size(45, 13);
            this.lblEditora.Text = "Editora:";
            this.lblEditora.TabIndex = 8;

            // txtEditora
            this.txtEditora.Location = new System.Drawing.Point(80, 180);
            this.txtEditora.Name = "txtEditora";
            this.txtEditora.Size = new System.Drawing.Size(200, 20);
            this.txtEditora.TabIndex = 9;

            // chkDisponivel
            this.chkDisponivel.AutoSize = true;
            this.chkDisponivel.Location = new System.Drawing.Point(80, 220);
            this.chkDisponivel.Name = "chkDisponivel";
            this.chkDisponivel.Size = new System.Drawing.Size(78, 17);
            this.chkDisponivel.Text = "Disponível";
            this.chkDisponivel.UseVisualStyleBackColor = true;
            this.chkDisponivel.TabIndex = 10;
            //this.chkDisponivel.Enabled = false;

            // btnInserir
            this.btnInserir.Location = new System.Drawing.Point(20, 260);
            this.btnInserir.Name = "btnInserir";
            this.btnInserir.Size = new System.Drawing.Size(75, 23);
            this.btnInserir.Text = "Inserir";
            this.btnInserir.UseVisualStyleBackColor = true;
            this.btnInserir.TabIndex = 11;
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);

            // btnBuscar
            this.btnBuscar.Location = new System.Drawing.Point(100, 260);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.TabIndex = 12;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);

            // btnAtualizar
            this.btnAtualizar.Location = new System.Drawing.Point(180, 260);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(75, 23);
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.TabIndex = 13;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);

            // btnExcluir
            this.btnExcluir.Location = new System.Drawing.Point(260, 260);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(75, 23);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.TabIndex = 14;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);

            // btnListar
            this.btnListar.Location = new System.Drawing.Point(340, 260);
            this.btnListar.Name = "btnListar";
            this.btnListar.Size = new System.Drawing.Size(75, 23);
            this.btnListar.Text = "Listar";
            this.btnListar.UseVisualStyleBackColor = true;
            this.btnListar.TabIndex = 15;
            this.btnListar.Click += new System.EventHandler(this.btnListar_Click);

            // btnListarDisponiveis
            this.btnListarDisponiveis.Location = new System.Drawing.Point(420, 260);
            this.btnListarDisponiveis.Name = "btnListarDisponiveis";
            this.btnListarDisponiveis.Size = new System.Drawing.Size(120, 23);
            this.btnListarDisponiveis.Text = "Listar Disponíveis";
            this.btnListarDisponiveis.UseVisualStyleBackColor = true;
            this.btnListarDisponiveis.TabIndex = 16;
            this.btnListarDisponiveis.Click += new System.EventHandler(this.btnListarDisponiveis_Click);

            // dgvLivros
            this.dgvLivros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLivros.Location = new System.Drawing.Point(20, 300);
            this.dgvLivros.Name = "dgvLivros";
            this.dgvLivros.RowTemplate.Height = 25;
            this.dgvLivros.Size = new System.Drawing.Size(800, 150);
            this.dgvLivros.TabIndex = 17;

            // lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(20, 470);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(35, 13);
            this.lblTotal.Text = $"Total de livros: {LivroBLL.Listar().Count}";
            this.lblTotal.TabIndex = 18;
            //this.lblTotal.Visible = false;

            // lblDisponiveis
            this.lblDisponiveis.AutoSize = true;
            this.lblDisponiveis.Location = new System.Drawing.Point(120, 470);
            this.lblDisponiveis.Name = "lblDisponíveis";
            this.lblDisponiveis.Size = new System.Drawing.Size(0, 13);
            this.lblDisponiveis.Text = $"Livros disponíveis: {LivroBLL.ListarDisponiveis().Count}";
            this.lblDisponiveis.TabIndex = 19;


            // FormLivro
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 500);
            this.Controls.Add(this.lblDisponiveis);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dgvLivros);
            this.Controls.Add(this.btnListarDisponiveis);
            this.Controls.Add(this.btnListar);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnInserir);
            this.Controls.Add(this.chkDisponivel);
            this.Controls.Add(this.txtEditora);
            this.Controls.Add(this.lblEditora);
            this.Controls.Add(this.txtCategoria);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.txtAutor);
            this.Controls.Add(this.lblAutor);
            this.Controls.Add(this.txtTitulo);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.lblCodigo);
            this.Name = "FormLivro";
            this.Text = "Formulário de Livros";
            //this.Load += new System.EventHandler(this.FormLivro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLivros)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}