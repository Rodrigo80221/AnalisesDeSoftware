namespace GestaoComercial.Formularios.PedidosVendas
{
    partial class FrmFornecedorVendedorescs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFornecedorVendedorescs));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnProcurar = new Telecon.Genericos.Controles.BotaoProcurar();
            this.txtCodigo = new Telecon.Genericos.Controles.CaixaTexto();
            this.caixaTexto3 = new Telecon.Genericos.Controles.CaixaTexto();
            this.caixaTexto2 = new Telecon.Genericos.Controles.CaixaTexto();
            this.botaoProcurar1 = new Telecon.Genericos.Controles.BotaoProcurar();
            this.caixaTexto1 = new Telecon.Genericos.Controles.CaixaTexto();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAliquotaPdvTerceiros = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAliquota = new Telecon.Genericos.Controles.CaixaTexto();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescricao = new Telecon.Genericos.Controles.CaixaTexto();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewPersonalizado1 = new Telecon.Genericos.Controles.DataGridViewPersonalizado();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cadCadastro = new Telecon.Genericos.Controles.BarraCadastro();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPersonalizado1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(9, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(570, 308);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnProcurar);
            this.tabPage1.Controls.Add(this.txtCodigo);
            this.tabPage1.Controls.Add(this.caixaTexto3);
            this.tabPage1.Controls.Add(this.caixaTexto2);
            this.tabPage1.Controls.Add(this.botaoProcurar1);
            this.tabPage1.Controls.Add(this.caixaTexto1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.lblAliquotaPdvTerceiros);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtAliquota);
            this.tabPage1.Controls.Add(this.lblRegistros);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtDescricao);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(562, 282);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Vendedores";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnProcurar
            // 
            this.btnProcurar.BackColor = System.Drawing.SystemColors.Control;
            this.btnProcurar.Image = ((System.Drawing.Image)(resources.GetObject("btnProcurar.Image")));
            this.btnProcurar.Location = new System.Drawing.Point(124, 31);
            this.btnProcurar.Name = "btnProcurar";
            this.btnProcurar.Size = new System.Drawing.Size(24, 23);
            this.btnProcurar.TabIndex = 24;
            this.btnProcurar.UseVisualStyleBackColor = false;
            // 
            // txtCodigo
            // 
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.Decimais = 2;
            this.txtCodigo.Font = new System.Drawing.Font("ArialTelecon", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Formato = Telecon.Genericos.Controles.CaixaTexto.TipoFormato.Inteiro;
            this.txtCodigo.Location = new System.Drawing.Point(18, 31);
            this.txtCodigo.MaxLength = 4;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PermiteValorNegativo = false;
            this.txtCodigo.Senha = "0";
            this.txtCodigo.SenhaCrypt = "°";
            this.txtCodigo.Size = new System.Drawing.Size(100, 22);
            this.txtCodigo.TabIndex = 23;
            this.txtCodigo.Text = "0";
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // caixaTexto3
            // 
            this.caixaTexto3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.caixaTexto3.Decimais = 0;
            this.caixaTexto3.Font = new System.Drawing.Font("ArialTelecon", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.caixaTexto3.Formato = Telecon.Genericos.Controles.CaixaTexto.TipoFormato.Livre;
            this.caixaTexto3.Location = new System.Drawing.Point(337, 190);
            this.caixaTexto3.MaxLength = 5;
            this.caixaTexto3.Name = "caixaTexto3";
            this.caixaTexto3.PermiteValorNegativo = false;
            this.caixaTexto3.Senha = "";
            this.caixaTexto3.SenhaCrypt = "";
            this.caixaTexto3.Size = new System.Drawing.Size(210, 22);
            this.caixaTexto3.TabIndex = 22;
            // 
            // caixaTexto2
            // 
            this.caixaTexto2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.caixaTexto2.Decimais = 0;
            this.caixaTexto2.Font = new System.Drawing.Font("ArialTelecon", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.caixaTexto2.Formato = Telecon.Genericos.Controles.CaixaTexto.TipoFormato.Livre;
            this.caixaTexto2.Location = new System.Drawing.Point(18, 190);
            this.caixaTexto2.MaxLength = 5;
            this.caixaTexto2.Name = "caixaTexto2";
            this.caixaTexto2.PermiteValorNegativo = false;
            this.caixaTexto2.Senha = "";
            this.caixaTexto2.SenhaCrypt = "";
            this.caixaTexto2.Size = new System.Drawing.Size(313, 22);
            this.caixaTexto2.TabIndex = 21;
            // 
            // botaoProcurar1
            // 
            this.botaoProcurar1.BackColor = System.Drawing.SystemColors.Control;
            this.botaoProcurar1.Image = ((System.Drawing.Image)(resources.GetObject("botaoProcurar1.Image")));
            this.botaoProcurar1.Location = new System.Drawing.Point(124, 85);
            this.botaoProcurar1.Name = "botaoProcurar1";
            this.botaoProcurar1.Size = new System.Drawing.Size(24, 23);
            this.botaoProcurar1.TabIndex = 15;
            this.botaoProcurar1.UseVisualStyleBackColor = false;
            // 
            // caixaTexto1
            // 
            this.caixaTexto1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.caixaTexto1.Decimais = 2;
            this.caixaTexto1.Font = new System.Drawing.Font("ArialTelecon", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.caixaTexto1.Formato = Telecon.Genericos.Controles.CaixaTexto.TipoFormato.Inteiro;
            this.caixaTexto1.Location = new System.Drawing.Point(18, 85);
            this.caixaTexto1.MaxLength = 4;
            this.caixaTexto1.Name = "caixaTexto1";
            this.caixaTexto1.PermiteValorNegativo = false;
            this.caixaTexto1.Senha = "0";
            this.caixaTexto1.SenhaCrypt = "°";
            this.caixaTexto1.Size = new System.Drawing.Size(100, 22);
            this.caixaTexto1.TabIndex = 16;
            this.caixaTexto1.Text = "0";
            this.caixaTexto1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Cód. Fornecedor";
            // 
            // lblAliquotaPdvTerceiros
            // 
            this.lblAliquotaPdvTerceiros.AutoSize = true;
            this.lblAliquotaPdvTerceiros.Location = new System.Drawing.Point(334, 174);
            this.lblAliquotaPdvTerceiros.Name = "lblAliquotaPdvTerceiros";
            this.lblAliquotaPdvTerceiros.Size = new System.Drawing.Size(52, 13);
            this.lblAliquotaPdvTerceiros.TabIndex = 17;
            this.lblAliquotaPdvTerceiros.Text = "Telefone:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "E-mail:";
            // 
            // txtAliquota
            // 
            this.txtAliquota.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAliquota.Decimais = 0;
            this.txtAliquota.Font = new System.Drawing.Font("ArialTelecon", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAliquota.Formato = Telecon.Genericos.Controles.CaixaTexto.TipoFormato.Livre;
            this.txtAliquota.Location = new System.Drawing.Point(18, 137);
            this.txtAliquota.MaxLength = 5;
            this.txtAliquota.Name = "txtAliquota";
            this.txtAliquota.PermiteValorNegativo = false;
            this.txtAliquota.Senha = "";
            this.txtAliquota.SenhaCrypt = "";
            this.txtAliquota.Size = new System.Drawing.Size(529, 22);
            this.txtAliquota.TabIndex = 19;
            // 
            // lblRegistros
            // 
            this.lblRegistros.Location = new System.Drawing.Point(393, 15);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(154, 13);
            this.lblRegistros.TabIndex = 12;
            this.lblRegistros.Text = "Registro 0 de 0";
            this.lblRegistros.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Nome:";
            // 
            // txtDescricao
            // 
            this.txtDescricao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescricao.Decimais = 0;
            this.txtDescricao.Font = new System.Drawing.Font("ArialTelecon", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.Formato = Telecon.Genericos.Controles.CaixaTexto.TipoFormato.Livre;
            this.txtDescricao.Location = new System.Drawing.Point(154, 85);
            this.txtDescricao.MaxLength = 20;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.PermiteValorNegativo = false;
            this.txtDescricao.Senha = "";
            this.txtDescricao.SenhaCrypt = "";
            this.txtDescricao.Size = new System.Drawing.Size(393, 22);
            this.txtDescricao.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Cód. Vendedor";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewPersonalizado1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(562, 282);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Produtos";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewPersonalizado1
            // 
            this.dataGridViewPersonalizado1.AllowUserToAddRows = false;
            this.dataGridViewPersonalizado1.AllowUserToDeleteRows = false;
            this.dataGridViewPersonalizado1.AllowUserToResizeRows = false;
            this.dataGridViewPersonalizado1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPersonalizado1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridViewPersonalizado1.Decimais = 2;
            this.dataGridViewPersonalizado1.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewPersonalizado1.Logotipo = null;
            this.dataGridViewPersonalizado1.MarcarCelulasCliqueCabecalho = true;
            this.dataGridViewPersonalizado1.Name = "dataGridViewPersonalizado1";
            this.dataGridViewPersonalizado1.RowHeadersVisible = false;
            this.dataGridViewPersonalizado1.Size = new System.Drawing.Size(550, 270);
            this.dataGridViewPersonalizado1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Cód. Produto";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Cód. no Fornecedor";
            this.Column2.Name = "Column2";
            this.Column2.Width = 130;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Descrição";
            this.Column3.Name = "Column3";
            this.Column3.Width = 200;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Última Compra";
            this.Column4.Name = "Column4";
            // 
            // cadCadastro
            // 
            this.cadCadastro.BackColor = System.Drawing.SystemColors.Control;
            this.cadCadastro.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cadCadastro.BackgroundImage")));
            this.cadCadastro.CancelaGravacao = false;
            this.cadCadastro.Dados = null;
            this.cadCadastro.Location = new System.Drawing.Point(9, 323);
            this.cadCadastro.Name = "cadCadastro";
            this.cadCadastro.NovoAposGravar = false;
            this.cadCadastro.PermiteAlterar = true;
            this.cadCadastro.PermiteExcluir = true;
            this.cadCadastro.PermiteNovo = true;
            this.cadCadastro.RegistroAtual = 0;
            this.cadCadastro.Size = new System.Drawing.Size(566, 40);
            this.cadCadastro.TabIndex = 5;
            // 
            // FrmFornecedorVendedorescs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(588, 377);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cadCadastro);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFornecedorVendedorescs";
            this.Text = "Cadastro de Vendedores dos Fornecedores";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPersonalizado1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private Telecon.Genericos.Controles.BotaoProcurar btnProcurar;
        private Telecon.Genericos.Controles.CaixaTexto txtCodigo;
        private Telecon.Genericos.Controles.CaixaTexto caixaTexto3;
        private Telecon.Genericos.Controles.CaixaTexto caixaTexto2;
        private Telecon.Genericos.Controles.BotaoProcurar botaoProcurar1;
        private Telecon.Genericos.Controles.CaixaTexto caixaTexto1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAliquotaPdvTerceiros;
        private System.Windows.Forms.Label label3;
        private Telecon.Genericos.Controles.CaixaTexto txtAliquota;
        private System.Windows.Forms.Label lblRegistros;
        private System.Windows.Forms.Label label2;
        private Telecon.Genericos.Controles.CaixaTexto txtDescricao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private Telecon.Genericos.Controles.DataGridViewPersonalizado dataGridViewPersonalizado1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private Telecon.Genericos.Controles.BarraCadastro cadCadastro;
    }
}