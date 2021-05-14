using GestaoComercial.Formularios.Indicadores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telecon.Genericos.Controles.Classes;


using System.Globalization;
using Telecon.Genericos.Classes.Outros;

namespace GestaoComercial.Formularios.PackVirtual
{
    public partial class FrmRelatorioPack : Form
    {
        public FrmRelatorioPack()
        {
            InitializeComponent();
        }

        private void FrmRelatorioPack_Load(object sender, EventArgs e)
        {
            navDatas.Select();
        }


        private void btnProcessar_Click(object sender, EventArgs e)
        {

            dgvLojasCadastro.ExecutarFormatacaoPadrao(2);
            dgvLojasCadastro.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvLojasCadastro.FormatarColuna(0, "", 25);
            dgvLojasCadastro.FormatarColuna(1, "Lojas", 90);            

            dgvLojasCadastro.Columns.RemoveAt(0);
            dgvLojasCadastro.Columns.Insert(0, dgvLojasCadastro.RetornarColunaCheckBox());

            dgvLojasCadastro.DefinirTipoColuna(0, ItemComboBusca.TipoDados.CheckBox);
            dgvLojasCadastro.DefinirTipoColuna(1, ItemComboBusca.TipoDados.Livre);

            for (int i = 0; i < 5; i++)
            {
                var linha = dgvLojasCadastro.Rows.Add();
                dgvLojasCadastro.MarcarCelula(linha, 0);
                dgvLojasCadastro[linha, 1] = "Loja " + i.ToString();
            }

            //-----------------------------------------------------------------------------

            lblProdQtdVendida.Text = "267";
            lblProdValorUnit.Text = "R$ 66,85";
            lblFaturamento.Text = "R$ 17.848,94";
            lblProdCustoGerencial.Text = "R$ 13.789,05";
            lblProdCMV.Text = "R$ 13.348,94";
            lblProdMargemRS.Text = "R$ 13.647,37";
            lblProdMargemPerc.Text = "20,43";
            lblProdMarkup.Text = "29,44";

            //-----------------------------------------------------------------------------


            dgvRel.ExecutarFormatacaoPadrao(13);
            dgvRel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            var col = 0;

            var direita = DataGridViewContentAlignment.MiddleRight;
            var esquerda = DataGridViewContentAlignment.MiddleLeft;

            dgvRel.FormatarColuna(col++," ", 20, esquerda);
            dgvRel.FormatarColuna(col++,"Cód.", 35, direita);
            dgvRel.FormatarColuna(col++, "Pack", 45, esquerda);
            dgvRel.FormatarColuna(col++, "Cod. Barras", 95, direita);
            dgvRel.FormatarColuna(col++, "Produto", 245, esquerda);
            dgvRel.FormatarColuna(col++, "Qtd.Vendida", 70, direita);
            dgvRel.FormatarColuna(col++, "Vlr.Unit.Médio", 80, direita);
            dgvRel.FormatarColuna(col++, "Total", 60, direita);
            dgvRel.FormatarColuna(col++, "C.Ger.Méd", 70, direita);
            dgvRel.FormatarColuna(col++, "CMV", 50, direita);
            dgvRel.FormatarColuna(col++, "MargemR$", 70, direita);
            dgvRel.FormatarColuna(col++, "Margem%", 60, direita);
            dgvRel.FormatarColuna(col++, "Markup%", 60, direita);

            col = 0;


            for (int aux = 0; aux < 4; aux++)
            {
                
                var linha = dgvRel.Rows.Add();
                var iCol = 0;

                dgvRel[linha, iCol++]= "-";
                dgvRel[linha, iCol++] = "9999";
                dgvRel[linha, iCol++] = "A PARTIR DE 6 UNIDADES GANHE 10% DE DESCONTO";
                dgvRel[linha, iCol++] = "";
                dgvRel[linha, iCol++] = "";
                dgvRel[linha, iCol++] = "99999";
                dgvRel[linha, iCol++] = "999,99";
                dgvRel[linha, iCol++] = "9999,99";
                dgvRel[linha, iCol++] = "999,99";
                dgvRel[linha, iCol++] = "9999,99";
                dgvRel[linha, iCol++] = "9999,99";
                dgvRel[linha, iCol++] = "99,99";
                dgvRel[linha, iCol++] = "99,99";

                dgvRel.JuntarColunas(linha, 2, 3);

                dgvRel.Rows[linha].DefaultCellStyle.BackColor = Color.LightGray;

                for (int i = 0; i < 5; i++)
                {
                    if (aux == 2) { i = 5; }
                    
                    linha = dgvRel.Rows.Add();
                    iCol = 0;

                    dgvRel[linha, iCol++] = "";
                    dgvRel[linha, iCol++] = "";
                    dgvRel[linha, iCol++] = "";
                    dgvRel[linha, iCol++] = "7891149201006";
                    dgvRel[linha, iCol++] = "CERVEJA SKOL LATAO  473ML UND";
                    dgvRel[linha, iCol++] = "50";
                    dgvRel[linha, iCol++] = "3,27";
                    dgvRel[linha, iCol++] = "163,38";
                    dgvRel[linha, iCol++] = "2,49";
                    dgvRel[linha, iCol++] = "124,50";
                    dgvRel[linha, iCol++] = "38,88";
                    dgvRel[linha, iCol++] = "23,80";
                    dgvRel[linha, iCol++] = "31,33";

                }
            }

            navDatas.Focus();
            dgvRel.ClearSelection();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvRel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grbAnalIndividual_Enter(object sender, EventArgs e)
        {

        }
    }
}
