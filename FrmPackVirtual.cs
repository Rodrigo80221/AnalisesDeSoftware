using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GestaoComercial.Classes;
using GestaoComercial.Formularios.Produtos;
using PresentationControls;
using SistemaR.Biblioteca.Utilitarios;
using Telecon.Genericos.Classes.BancoDeDados;
using Telecon.Genericos.Classes.Outros;
using Telecon.Genericos.Classes.TiposDados;
using Telecon.Genericos.Classes.Windows;
using Telecon.Genericos.Controles.Classes;
using Telecon.Genericos.Formularios;
using Telecon.GestaoComercial.Biblioteca.Estoque;
using Telecon.GestaoComercial.Biblioteca.Outros;
using Telecon.GestaoComercial.Biblioteca.Pessoas;
using Pack = Telecon.GestaoComercial.Biblioteca.PackVirtual;
using Telecon.Genericos.Controles;
using Telecon.GestaoComercial.Biblioteca.Gerenciador;
using Telecon.GestaoComercial.Biblioteca.Vendas;


namespace GestaoComercial.Formularios.PackVirtual
{
    public partial class frmPackVirtual : Form
    {
        public string Origem;

        private bool _passouFormLoad;
        private bool _atualizar;
        private bool _alteradoPackVirtual;
        private bool _alteradoPackVirtualGrupo1;
        private bool _alteradoPackVirtualGrupo2;
        private bool _alteradoPackVirtualDiferenciado;
        private bool _alteradoPackVirtualLojas;
        private bool _alteradoPackVirtualGruposClientes;
        private bool _alteradoPackVirtualFormaPGTO;
        private bool _permitirTrocaTab;
        private bool _encarteHabilitado;
        private bool _dataBloqueada;
        private const bool VALOR = true;
        private string CodigoEncarteAlterado;
        private Thread _threadAtualizarLinhaGrupos;
        private bool _moduloKw = false;

        private Telecon.GestaoComercial.Biblioteca.PackVirtual.ModeloPack _modelos = new Pack.ModeloPack();

        public frmPackVirtual()
        {
            InitializeComponent();
        }

        private enum ColunasLoja
        {
            Selecao = 0,
            Loja,
            Codigo
        }
        private enum ColunasFormas
        {
            Codigo = 0,
            Selecao,
            Forma
        }

        private enum ColunasPack
        {
            Selecao = 0,
            Codigo,
            Descricao,
            ModeloPack,
            DtInicial,
            DtFinal,
            GrupoCliente,
            QuantidadeLimite,
            CodScannTech
        }

        private enum ColunasDgvGrupo
        {
            Codigo = 0,
            Descricao,
            Valor,
            ValorAtacado,
            Excluir
        }
        private enum ColunasDgvGrupoAtacado
        {
            Codigo = 0,
            Descricao,
            Valor,
            ValorAtacado,
            Excluir
        }

        private enum ColunasDgvDiferenciado
        {
            Codigo = 0,
            Descricao,
            Valor,
            Preco2,
            Excluir
        }

        private enum ColunasDgvGruposClientes
        {
            Selecao = 0,
            Nome
        }

        private void btnEncerrar_Click(object sender, EventArgs e)
        {


            var listaPackVirtual = RetornarPackVirtualSelecionadoDataGridView(dgvPackFiltro);

            Pack.PackVirtual pack = null;

            var lista = new List<Pack.PackVirtual>();

            var temPackScanntech = listaPackVirtual.Exists(x => x.CodScannTech != 0);

            if (temPackScanntech)
            {
                Msg.Informar("Não é possível encerrar um pack virtual recebido do Clube de Promoções!");
                return;
            }

            if (listaPackVirtual.Count.Equals(0))
            {
                Telecon.Genericos.Classes.Outros.Msg.Informar("Selecione pelo menos um pack virutal.");
                return;
            }

            if (Telecon.Genericos.Classes.Outros.Msg.Perguntar("Confirma o encerramento do(s) pack(s) selecionado(s)?\n") == DialogResult.No)
                return;

            IBanco banco = null;

            var dataAtual = DateTime.Today;

            var sql = new StringBuilder();

            var packs = new StringBuilder();

            var packErros = new StringBuilder();

            var dataAtualServidor = ObterDataHoraServidor();

            try
            {

                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

                sql = new StringBuilder();

                foreach (var item in listaPackVirtual)
                {
                    pack = Pack.PackVirtual.ConsultarChave(banco, item.Codigo);
                    lista.Add(pack);
                }

                foreach (var item in lista)
                {
                    if (item.DtFinal < dataAtualServidor)
                    {
                        packErros.AppendLine("Pack nº :" + item.Codigo + " já encontra-se encerrado!");
                    }
                    else
                    {
                        sql.AppendLine("Update PackVirtual SET DtFinal = " + banco.ObterData(dataAtual));
                        sql.AppendLine("Where Codigo = " + item.Codigo);
                        packs.AppendLine("Pack nº : " + item.Codigo + " foi encerrado com sucesso!");
                    }

                }


                if (sql.Length > 0)
                {
                    banco.ExecutarComando(sql.ToString());

                    sql = new StringBuilder();


                    foreach (var item in listaPackVirtual)
                    {
                        var packVirtual = Pack.PackVirtual.ConsultarChave(banco, item.Codigo);

                        if (packVirtual.DtInicial > dataAtualServidor)
                        {
                            sql.AppendLine("Update PackVirtual SET DtInicial = " + banco.ObterDataHora(dataAtual));
                            sql.AppendLine("Where Codigo = " + item.Codigo);
                        }

                    }
                }

                if (sql.Length > 0)
                {
                    banco.ExecutarComando(sql.ToString());
                }

                if (!string.IsNullOrEmpty(packErros.ToString()))
                {
                    Msg.Criticar(packErros.ToString());
                }

                if (!string.IsNullOrEmpty(packs.ToString()))
                {
                    Msg.Informar(packs.ToString());
                }

                DesmarcarColunaCheckBoxDataGridViewPacks(dgvPackFiltro);

                //LimparCampos();

                CarregarPacks(0);
            }
            catch (Exception ex)
            {
                Msg.Informar(ex.Message);
            }
            finally
            {
                if (banco != null) banco.FecharConexao();
            }
        }

        private void frmPackVirtual_Load(object sender, EventArgs e)
        {
            IBanco banco = null;

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

                var mapaCalor = new MapaCalor
                {
                    CodModuloCustomerSuccess = 6,
                    CodLoja = VariaveisGlobais.CodLoja,
                    CodOperador = VariaveisGlobais.CodOperador,
                    DataHora = DateTime.Now
                };
                MapaCalor.Inserir(banco, mapaCalor);

                if (Modulo.ConsultarChave(banco, 711).Permissao == Modulo.EnumPermissao.Habilitado)
                    _moduloKw = true;

                var lista = Loja.ConsultarTodas(banco);

                if (lista.Count() == 1)
                {
                    ExibirGridLojas(!VALOR);
                    FormatarDgvFormarPGTO();
                    lblMarcaLojas.Visible = false;

                }
                else
                {
                    ExibirGridLojas(VALOR);

                    FormatarDgvLojasFiltro();
                    FormatarDgvLojasCadastro();

                    CarregarDgvLojasFiltro(lista);
                    CarregarDgvLojasCadastro(lista);


                    LimparSelecaoDataGridView(dgvLojasFiltro);

                    LimparSelecaoDataGridView(dgvLojasCadastro);

                    MarcarColunaCheckBoxDataGridViewDgvLojas(dgvLojasCadastro);
                    MarcarColunaCheckBoxDataGridViewDgvLojas(dgvLojasFiltro);
                    lblMarcaLojas.Visible = true;
                }

                FormatarDgvFormarPGTO();
                CarregarDgvFormasPGTO();

                FormatarCoresLabels();

                FormatarDgvPackFiltro();

                FormatarDgvGrupo1();

                FormatarDgvGrupo2();

                FormatarDgvDiferenciado();

                FormatarDgvGruposClientes();

                CarregarDgvGrupoClientes();

                DefinirCodigoPack();

                FormatarDatePicker(dtpHoraInicio);
                FormatarDatePicker(dtpHoraFim);

                CarregarPermissaoEncarte();

                CarregarComboModeloPack(cboModeloPackPesquisa, true);

                CarregarComboModeloPack(cboModeloPack, false);

                CarregarFormas(banco);


                CarregarPacks(0);
                tabControl1.TabPages.Remove(tbCadastro);

                LimparCampos(true, _moduloKw);
                btnNovo.Enabled = new UsuarioAcesso().PermiteIncluir(169, VariaveisGlobais.CodLoja);
            }

            finally
            {
                if (banco != null) banco.FecharConexao();
                _passouFormLoad = true;
            }
        }

        private void FormatarCoresLabels()
        {
            chkEncerrados.ForeColor = Cores.VermelhoFonte;
            chkProximos.ForeColor = Cores.AmareloFonte;
            chkEmAndamento.ForeColor = Cores.VerdeFonte;
            btnEncerrar.ForeColor = Cores.VermelhoFonte;
            btnProrrogar.ForeColor = Cores.VerdeFonte;
        }

        private void buscaPackVirtualGrupo1_SelecionouItem(string item)
        {
            txtCodProdGrupo1.Text = item;
            txtCodProdGrupo1_KeyDown(null, new KeyEventArgs(Keys.Enter));
            btnAddGrupo1_Click(null, new KeyEventArgs(Keys.Enter));
        }

        private void buscaPackVirtualGrupo2_SelecionouItem(string item)
        {
            txtCodProdGrupo2.Text = item;
            txtCodProdGrupo2_KeyDown(null, new KeyEventArgs(Keys.Enter));
            btnAddGrupo2_Click(null, new KeyEventArgs(Keys.Enter));
        }

        private void buscaPackVirtualDiferenciado_SelecionouItem(string item)
        {
            txtCodProdutoDiferenciado.Text = item;
            txtCodProdutoDiferenciado_KeyDown(null, new KeyEventArgs(Keys.Enter));
            txtValorDiferenciado.Text = buscaValorPreco2(item, "0", DefinirLojaValor(RetornarLojasSelecionadasDataGridView(dgvLojasCadastro)));
        }

        private void buscaPackVirtualPesquisa_SelecionouItem(string item)
        {
            txtCodProdPesquisa.Text = item;
            txtCodProdPesquisa_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }
        private void FormatarDgvFormarPGTO()
        {
            dgvFormasPGTO.ExecutarFormatacaoPadrao(3);

            dgvFormasPGTO.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvFormasPGTO.FormatarColuna((int)ColunasFormas.Codigo, " ", 0);
            dgvFormasPGTO.FormatarColuna((int)ColunasFormas.Selecao, " ", 20);
            dgvFormasPGTO.FormatarColuna((int)ColunasFormas.Forma, "Formas de Pagamento", 150);

            dgvFormasPGTO.Columns.RemoveAt((int)ColunasFormas.Selecao);
            dgvFormasPGTO.Columns.Insert((int)ColunasFormas.Selecao, dgvFormasPGTO.RetornarColunaCheckBox());
            dgvFormasPGTO.DefinirTipoColuna((int)ColunasFormas.Selecao, ItemComboBusca.TipoDados.CheckBox);
        }

        private void FormatarDgvLojasCadastro()
        {
            dgvLojasCadastro.ExecutarFormatacaoPadrao(3);

            dgvLojasCadastro.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvLojasCadastro.FormatarColuna((int)ColunasLoja.Selecao, " ", 20);
            dgvLojasCadastro.FormatarColuna((int)ColunasLoja.Loja, "Loja", 150);
            dgvLojasCadastro.FormatarColuna((int)ColunasLoja.Codigo, "Codigo", 0);

            dgvLojasCadastro.Columns.RemoveAt((int)ColunasLoja.Selecao);
            dgvLojasCadastro.Columns.Insert((int)ColunasLoja.Selecao, dgvLojasCadastro.RetornarColunaCheckBox());
            dgvLojasCadastro.DefinirTipoColuna((int)ColunasLoja.Selecao, ItemComboBusca.TipoDados.CheckBox);
        }

        private void FormatarDgvLojasFiltro()
        {
            dgvLojasFiltro.ExecutarFormatacaoPadrao(3);

            dgvLojasFiltro.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvLojasFiltro.FormatarColuna((int)ColunasLoja.Selecao, " ", 20);
            dgvLojasFiltro.FormatarColuna((int)ColunasLoja.Loja, "Loja", 150);
            dgvLojasFiltro.FormatarColuna((int)ColunasLoja.Codigo, "Codigo", 0);

            dgvLojasFiltro.Columns.RemoveAt((int)ColunasLoja.Selecao);
            dgvLojasFiltro.Columns.Insert((int)ColunasLoja.Selecao, dgvLojasFiltro.RetornarColunaCheckBox());
            dgvLojasFiltro.DefinirTipoColuna((int)ColunasLoja.Selecao, ItemComboBusca.TipoDados.CheckBox);
        }


        private void FormatarDgvPackFiltro()
        {
            dgvPackFiltro.ExecutarFormatacaoPadrao(1);

            dgvPackFiltro.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvPackFiltro.FormatarColuna((int)ColunasPack.Selecao, " ", 20);

            dgvPackFiltro.Columns.RemoveAt((int)ColunasLoja.Selecao);
            dgvPackFiltro.Columns.Insert((int)ColunasLoja.Selecao, dgvPackFiltro.RetornarColunaCheckBox());
            dgvPackFiltro.DefinirTipoColuna((int)ColunasLoja.Selecao, ItemComboBusca.TipoDados.CheckBox);
        }
        private void CarregarDgvFormasPGTO()
        {
            IBanco banco = null;

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();



                List<FormaPgto> lista = FormaPgto.ConsultarTodos(banco);
                dgvFormasPGTO.Rows.Cast<DataGridViewRow>().ToList().ForEach(x => dgvFormasPGTO.MarcarCelula(x.Index, (int)ColunasFormas.Selecao));
                foreach (var item in lista)
                {
                    dgvFormasPGTO.Rows.Add();

                    dgvFormasPGTO.DesmarcarCelula(dgvFormasPGTO.UltimaLinha.Index, (int)ColunasFormas.Selecao);
                    dgvFormasPGTO[(int)ColunasFormas.Forma] = item.Nome;
                    dgvFormasPGTO[(int)ColunasFormas.Codigo] = item.Codigo.ToString();

                    dgvFormasPGTO.UltimaLinha.Tag = item.Codigo;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private void CarregarDgvLojasCadastro(List<Loja> lojas)
        {
            foreach (var item in lojas)
            {
                dgvLojasCadastro.Rows.Add();

                dgvLojasCadastro.DesmarcarCelula(dgvLojasCadastro.UltimaLinha.Index, (int)ColunasLoja.Selecao);
                dgvLojasCadastro[(int)ColunasLoja.Loja] = item.Nome;
                dgvLojasCadastro[(int)ColunasLoja.Codigo] = item.Codigo.ToString();

                dgvLojasCadastro.UltimaLinha.Tag = item.Codigo;

                if (!new UsuarioAcesso().PermiteAlterar(169, item.Codigo))
                {
                    dgvLojasCadastro.UltimaLinha.Visible = false;

                }
            }
        }

        private void CarregarDgvLojasFiltro(List<Loja> lojas)
        {
            foreach (var item in lojas)
            {
                if (new UsuarioAcesso().Habilitado(169, item.Codigo))
                {
                    dgvLojasFiltro.Rows.Add();

                    dgvLojasFiltro.DesmarcarCelula(dgvLojasFiltro.UltimaLinha.Index, (int)ColunasLoja.Selecao);
                    dgvLojasFiltro[(int)ColunasLoja.Loja] = item.Nome;

                    dgvLojasFiltro.UltimaLinha.Tag = item.Codigo;
                }
            }
        }
        private void FormatarDgvGruposClientes()
        {
            dgvGruposClientes.ExecutarFormatacaoPadrao(2);

            dgvGruposClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvGruposClientes.FormatarColuna((int)ColunasDgvGruposClientes.Selecao, " ", 20);
            dgvGruposClientes.FormatarColuna((int)ColunasDgvGruposClientes.Nome, "Grupo de Clientes", 170);

            dgvGruposClientes.Columns.RemoveAt((int)ColunasDgvGruposClientes.Selecao);
            dgvGruposClientes.Columns.Insert((int)ColunasDgvGruposClientes.Selecao, dgvGruposClientes.RetornarColunaCheckBox());
            dgvGruposClientes.DefinirTipoColuna((int)ColunasDgvGruposClientes.Selecao, ItemComboBusca.TipoDados.CheckBox);
        }

        private void ExibirGridLojas(Boolean valor)
        {
            label1.Visible = valor;
            label2.Visible = valor;
            dgvLojasCadastro.Visible = valor;
            dgvLojasFiltro.Visible = valor;
        }

        /// <summary>
        /// Método que marca todas as colunas do tipo checkbox de DataGridView Loja passado por parâmetro.
        /// </summary>
        /// <param name="dgv">(DataGridViewPersonalizado). Data Grid View que será utilizado para a seleção do checkbox.</param>
        private static void MarcarColunaCheckBoxDataGridViewDgvLojas(DataGridViewPersonalizado dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.MarcarCelula(i, (int)ColunasLoja.Selecao);
            }
        }

        /// <summary>
        /// Método que marca todas as colunas do tipo checkbox das Lojas do DataGridView de Cadastro, conforme a lista de lojas informada por parâmetro.
        /// </summary>
        /// <param name="lojas">(List (Loja)). Lista de lotas em que será realizada seleção do checkbox</param>
        private void MarcarColunaCheckBoxDataGridViewDgvLojasCadastro(List<Pack.PackVirtualLojas> lojas)
        {
            DesmarcarColunaCheckBoxDataGridViewLojas(dgvLojasCadastro);

            for (var i = 0; i < dgvLojasCadastro.Rows.Count; i++)
            {
                foreach (var loja in lojas)
                {
                    if ((int)dgvLojasCadastro.Rows[i].Tag == loja.CodLoja)
                    {
                        dgvLojasCadastro.MarcarCelula(i, (int)ColunasLoja.Selecao);
                    }
                }
            }
        }



        private List<Loja> RetornarLojasSelecionadasDataGridView(DataGridViewPersonalizado dgv)
        {
            var listaLojas = new List<Loja>();

            for (int linha = 0; linha < dgv.Rows.Count; linha++)
            {
                if (dgv.TestarCelulaMarcada(linha, (int)ColunasLoja.Selecao))
                {
                    var loja = new Loja { Codigo = (int)dgvLojasFiltro.Rows[linha].Tag };

                    listaLojas.Add(loja);
                }
            }

            return listaLojas;
        }

        private string DefinirLojaValor(List<Loja> listaLojas)
        {
            if (listaLojas.Count > 1 || listaLojas.Count == 0)
            {
                return "0";
            }
            return listaLojas[0].Codigo.ToString();
        }

        private List<Pack.PackVirtual> RetornarPackVirtualSelecionadoDataGridView(DataGridViewPersonalizado dgv)
        {
            var listaPackVirtual = new List<Pack.PackVirtual>();

            for (int linha = 0; linha < dgv.Rows.Count; linha++)
            {
                if (dgv.TestarCelulaMarcada(linha, (int)ColunasPack.Selecao))
                {
                    var packVirtual = new Pack.PackVirtual();

                    packVirtual.Codigo = int.Parse(dgvPackFiltro[linha, (int)ColunasPack.Codigo]);
                    packVirtual.CodScannTech = int.Parse(dgvPackFiltro[linha, (int)ColunasPack.CodScannTech]);

                    listaPackVirtual.Add(packVirtual);
                }
            }

            return listaPackVirtual;
        }

        private void FormatarDgvGrupo1()
        {
            dgvGrupo1.ExecutarFormatacaoPadrao(5);

            dgvGrupo1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvGrupo1.FormatarColuna((int)ColunasDgvGrupo.Codigo, "Código", 60);
            dgvGrupo1.FormatarColuna((int)ColunasDgvGrupo.Descricao, "Descrição", 210);
            dgvGrupo1.FormatarColuna((int)ColunasDgvGrupo.Valor, "Valor", 60, DataGridViewContentAlignment.MiddleRight);
            dgvGrupo1.FormatarColuna((int)ColunasDgvGrupo.ValorAtacado, "Valor Atacad.", 80, DataGridViewContentAlignment.MiddleRight);
            dgvGrupo1.FormatarColuna((int)ColunasDgvGrupo.Excluir, " ", 20);

            dgvGrupo1.Columns.RemoveAt((int)ColunasDgvGrupo.Excluir);
            dgvGrupo1.Columns.Insert((int)ColunasDgvGrupo.Excluir, dgvLojasFiltro.RetornarColunaImagem());
            dgvGrupo1.DefinirTipoColuna((int)ColunasDgvGrupo.Excluir, ItemComboBusca.TipoDados.NaoEditavel);
        }

        private void FormatarDgvGrupo1_Atacado()
        {





            //  dgvGrupo1.Columns.Insert((int)ColunasDgvGrupoAtacado.ValorAtacado, coluna);

            //dgvGrupo1.FormatarColuna((int)ColunasDgvGrupoAtacado.ValorAtacado, "Valor Atacad.", 80, DataGridViewContentAlignment.MiddleRight);
            // Use the Text property for the button text for all cells rather
            // than using each cell's value as the text for its own button.


            // Add the button column to the control.





            //dgvGrupo1.ExecutarFormatacaoPadrao(5);

            //dgvGrupo1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //dgvGrupo1.FormatarColuna((int)ColunasDgvGrupoAtacado.Codigo, "Código", 60);
            //dgvGrupo1.FormatarColuna((int)ColunasDgvGrupoAtacado.Descricao, "Descrição", 190);
            //dgvGrupo1.FormatarColuna((int)ColunasDgvGrupoAtacado.Valor, "Valor", 80, DataGridViewContentAlignment.MiddleRight);
            dgvGrupo1.Columns[(int)ColunasDgvGrupoAtacado.ValorAtacado].Visible = true;

            dgvGrupo1.FormatarColuna((int)ColunasDgvGrupoAtacado.ValorAtacado, "Valor Atacad.", 80, DataGridViewContentAlignment.MiddleRight);


            //dgvGrupo1.FormatarColuna((int)ColunasDgvGrupoAtacado.Excluir, " ", 20);

            //dgvGrupo1.Columns.RemoveAt((int)ColunasDgvGrupoAtacado.Excluir);
            //dgvGrupo1.Columns.Insert((int)ColunasDgvGrupoAtacado.Excluir, dgvLojasFiltro.RetornarColunaImagem());
            //dgvGrupo1.DefinirTipoColuna((int)ColunasDgvGrupoAtacado.Excluir, ItemComboBusca.TipoDados.NaoEditavel);
        }

        private void CarregarDgvGrupo1Atacado(List<Produto> produtoAssociados)
        {

            string _valorProduto = "0";
            bool _mandarMensagem = false;
            foreach (var item in produtoAssociados)
            {
                if (!VerificarProdutoExistenteDgvGrupo1(item.Codigo.ToString()))
                {
                    dgvGrupo1.Rows.Add();
                    if (_valorProduto != item.ValorVenda.ToString("0.00") && _valorProduto != "0")
                    {
                        _mandarMensagem = true;
                    }
                    else
                    {
                        _valorProduto = item.ValorVenda.ToString("0.00");
                    }
                    dgvGrupo1[(int)ColunasDgvGrupoAtacado.Codigo] = item.Codigo.ToString();
                    dgvGrupo1[(int)ColunasDgvGrupoAtacado.Descricao] = item.DescricaoReduzida;
                    dgvGrupo1[(int)ColunasDgvGrupoAtacado.ValorAtacado] = (item.ValorVenda - ((item.ValorVenda * Convert.ToDecimal(txtValorRegra.Text)) / 100)).ToString("0.00");
                    dgvGrupo1[(int)ColunasDgvGrupoAtacado.Valor] = item.ValorVenda.ToString("0.00");

                    _valorProduto = item.ValorVenda.ToString("0.00");

                    dgvGrupo1.DefinirImagem(dgvGrupo1.UltimaLinha.Index, (int)ColunasDgvGrupoAtacado.Excluir,
                                            Properties.Resources.excluir);
                }
            }
            if (_mandarMensagem)
            {
                if (!_atualizar)
                {
                    Msg.Informar("Alguns produtos da lista estão com valores diferentes dos demais produtos!");
                    lblMensagemPrecoDiferente.Visible = false;
                }

            }
        }
        private void CarregarDgvGrupo1(List<Produto> produtoAssociados)
        {

            string _valorProduto = "0";
            bool _mandarMensagem = false;
            foreach (var item in produtoAssociados)
            {
                if (!VerificarProdutoExistenteDgvGrupo1(item.Codigo.ToString()))
                {
                    dgvGrupo1.Rows.Add();
                    if (_valorProduto != item.ValorVenda.ToString("0.00") && _valorProduto != "0")
                    {
                        _mandarMensagem = true;
                    }
                    else
                    {
                        _valorProduto = item.ValorVenda.ToString("0.00");
                    }
                    if (cboModeloPack.Text.Equals("Pague x porcento a menos a partir de x unidades (atacado)"))
                    {
                        decimal valTemp = item.VALOR_NO_PDV - ((item.VALOR_NO_PDV * Convert.ToDecimal(txtValorRegra.Text)) / 100);
                        dgvGrupo1[(int)ColunasDgvGrupoAtacado.Codigo] = item.Codigo.ToString();
                        dgvGrupo1[(int)ColunasDgvGrupoAtacado.Descricao] = item.DescricaoReduzida;
                        if (txtUltimaCasa.Text.Equals(""))
                            txtUltimaCasa.Text = "-1";
                        dgvGrupo1[(int)ColunasDgvGrupoAtacado.ValorAtacado] = arredondaValorAtacado(valTemp, Convert.ToDecimal(txtUltimaCasa.Text)).ToString("0.00");
                        dgvGrupo1[(int)ColunasDgvGrupoAtacado.Valor] = item.VALOR_NO_PDV.ToString("0.00");
                        if (txtUltimaCasa.Text == "-1")
                            txtUltimaCasa.Text = "0";

                    }
                    else
                    {
                        dgvGrupo1[(int)ColunasDgvGrupo.Codigo] = item.Codigo.ToString();
                        dgvGrupo1[(int)ColunasDgvGrupo.Descricao] = item.DescricaoReduzida;
                        dgvGrupo1[(int)ColunasDgvGrupo.Valor] = item.ValorVenda.ToString("0.00");
                    }
                    _valorProduto = item.ValorVenda.ToString("0.00");

                    if (cboModeloPack.Text.Equals("Pague x porcento a menos a partir de x unidades (atacado)"))
                        dgvGrupo1.DefinirImagem(dgvGrupo1.UltimaLinha.Index, (int)ColunasDgvGrupoAtacado.Excluir,
                                            Properties.Resources.excluir);
                    else
                        dgvGrupo1.DefinirImagem(dgvGrupo1.UltimaLinha.Index, (int)ColunasDgvGrupo.Excluir,
                                            Properties.Resources.excluir);
                }
            }
            if (_mandarMensagem)
            {
                if (!_atualizar)
                {
                    Msg.Informar("Alguns produtos da lista estão com valores diferentes dos demais produtos!");
                    lblMensagemPrecoDiferente.Visible = false;
                }
                else
                {
                    if (!cboModeloPack.Text.Equals("Valor Diferenciado (Preço 2)"))
                        lblMensagemPrecoDiferente.Visible = true;
                }
            }

            HabilitarGridLojas();
        }

        public bool VerificarProdutoExistenteDgvGrupo1(string codigoProduto)
        {
            bool retorno = false;

            if (dgvGrupo1.Rows.Count == 0)
            {
                return false;
            }

            for (var linha = 0; linha < dgvGrupo1.Rows.Count; linha++)
            {
                if (dgvGrupo1[linha, 0] == codigoProduto)
                {
                    retorno = true;
                }
            }

            return retorno;
        }

        public bool VerificarProdutoExistenteDgvDiferenciado(string codigoProduto)
        {
            bool retorno = false;

            if (dgvDiferenciado.Rows.Count == 0)
            {
                return false;
            }

            for (var linha = 0; linha < dgvDiferenciado.Rows.Count; linha++)
            {
                if (dgvDiferenciado[linha, 0] == codigoProduto)
                {
                    retorno = true;
                }
            }

            return retorno;
        }


        private void FormatarDgvGrupo2()
        {
            dgvGrupo2.ExecutarFormatacaoPadrao(5);

            dgvGrupo2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvGrupo2.FormatarColuna((int)ColunasDgvGrupo.Codigo, "Código", 60);
            dgvGrupo2.FormatarColuna((int)ColunasDgvGrupo.Descricao, "Descrição", 270);
            dgvGrupo2.FormatarColuna((int)ColunasDgvGrupo.Valor, "Valor", 80, DataGridViewContentAlignment.MiddleRight);
            dgvGrupo2.FormatarColuna((int)ColunasDgvGrupo.ValorAtacado, "Valor", 0, DataGridViewContentAlignment.MiddleRight);
            dgvGrupo2.FormatarColuna((int)ColunasDgvGrupo.Excluir, " ", 20);

            dgvGrupo2.Columns.RemoveAt((int)ColunasDgvGrupo.Excluir);
            dgvGrupo2.Columns.Insert((int)ColunasDgvGrupo.Excluir, dgvLojasFiltro.RetornarColunaImagem());
            dgvGrupo2.DefinirTipoColuna((int)ColunasDgvGrupo.Excluir, ItemComboBusca.TipoDados.NaoEditavel);
        }

        private void FormatarDgvDiferenciado()
        {
            dgvDiferenciado.ExecutarFormatacaoPadrao(5);

            dgvDiferenciado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvDiferenciado.FormatarColuna((int)ColunasDgvDiferenciado.Codigo, "Código", 60);
            dgvDiferenciado.FormatarColuna((int)ColunasDgvDiferenciado.Descricao, "Descrição", 295);
            dgvDiferenciado.FormatarColuna((int)ColunasDgvDiferenciado.Valor, "Valor", 80, DataGridViewContentAlignment.MiddleRight);
            dgvDiferenciado.FormatarColuna((int)ColunasDgvDiferenciado.Preco2, "Valor 2", 80, DataGridViewContentAlignment.MiddleRight);
            dgvDiferenciado.FormatarColuna((int)ColunasDgvDiferenciado.Excluir, " ", 20);

            dgvDiferenciado.Columns.RemoveAt((int)ColunasDgvDiferenciado.Excluir);
            dgvDiferenciado.Columns.Insert((int)ColunasDgvDiferenciado.Excluir, dgvLojasFiltro.RetornarColunaImagem());
            dgvDiferenciado.DefinirTipoColuna((int)ColunasDgvDiferenciado.Excluir, ItemComboBusca.TipoDados.NaoEditavel);
        }

        private void CarregarDgvGrupo2(List<Produto> produtoAssociados)
        {
            foreach (var item in produtoAssociados)
            {
                if (!VerificarProdutoExistenteDgvGrupo2(item.Codigo.ToString()))
                {
                    dgvGrupo2.Rows.Add();

                    dgvGrupo2[(int)ColunasDgvGrupo.Codigo] = item.Codigo.ToString();
                    dgvGrupo2[(int)ColunasDgvGrupo.Descricao] = item.DescricaoReduzida;
                    dgvGrupo2[(int)ColunasDgvGrupo.Valor] = item.ValorVenda.ToString("0.00");
                    dgvGrupo2.DefinirImagem(dgvGrupo2.UltimaLinha.Index, (int)ColunasDgvGrupo.Excluir,
                                            Properties.Resources.excluir);
                }
            }

            HabilitarGridLojas();
        }

        private void CarregarDgvDiferenciado(List<Produto> produtoAssociados, double? valorAssociados)
        {
            int linhaExcluir = 0;
            bool excluir = false;

            foreach (var item in produtoAssociados)
            {

                if (VerificarProdutoExistenteDgvDiferenciado(item.Codigo.ToString()))
                {
                    for (var linha = 0; linha < dgvDiferenciado.Rows.Count; linha++)
                    {
                        if (dgvDiferenciado[linha, (int)ColunasDgvDiferenciado.Codigo].Equals(item.Codigo.ToString()))
                        {
                            excluir = true;
                            linhaExcluir = linha;
                        }
                    }
                }

                if (excluir)
                {
                    ExcluirLinhaDataGridView(linhaExcluir, dgvDiferenciado, false);
                }

                dgvDiferenciado.Rows.Add();

                dgvDiferenciado[(int)ColunasDgvDiferenciado.Codigo] = item.Codigo.ToString();
                dgvDiferenciado[(int)ColunasDgvDiferenciado.Descricao] = item.DescricaoReduzida;
                dgvDiferenciado[(int)ColunasDgvDiferenciado.Valor] = buscaValorAtualLoja(item.Codigo.ToString(), DefinirLojaValor(RetornarLojasSelecionadasDataGridView(dgvLojasCadastro)));
                if (valorAssociados == null)
                {
                    if (txtValorDiferenciado.Text != "0,00")
                        dgvDiferenciado[(int)ColunasDgvDiferenciado.Preco2] = txtValorDiferenciado.Text;
                    else
                        dgvDiferenciado[(int)ColunasDgvDiferenciado.Preco2] = buscaValorPreco2(item.Codigo.ToString(), txtCodPackCadastro.Text, DefinirLojaValor(RetornarLojasSelecionadasDataGridView(dgvLojasCadastro)));
                }
                else
                {
                    string valorProdutosAssociados = Convert.ToString(valorAssociados);
                    dgvDiferenciado[(int)ColunasDgvDiferenciado.Preco2] = Texto.FormatarMoeda(valorProdutosAssociados, 2);
                }

                dgvDiferenciado.DefinirImagem(dgvDiferenciado.UltimaLinha.Index, (int)ColunasDgvDiferenciado.Excluir,
                                        Properties.Resources.excluir);

            }
        }



        private void atualizarDgvDiferenciado()
        {
            for (int i = 0; i < dgvDiferenciado.Rows.Count; i++)
            {
                if (dgvDiferenciado[(int)ColunasDgvDiferenciado.Valor] != buscaValorAtualLoja(dgvDiferenciado[(int)ColunasDgvDiferenciado.Codigo], DefinirLojaValor(RetornarLojasSelecionadasDataGridView(dgvLojasCadastro))))
                {
                    dgvDiferenciado[(int)ColunasDgvDiferenciado.Valor] = buscaValorAtualLoja(dgvDiferenciado[(int)ColunasDgvDiferenciado.Codigo], DefinirLojaValor(RetornarLojasSelecionadasDataGridView(dgvLojasCadastro)));
                }
            }

        }

        private void CarregarDgvPackFiltro(List<Pack.PackVirtual> packVirtual)
        {
            if (dgvPackFiltro.Rows.Count > 0)
                LimparDataGridView(dgvPackFiltro);

            FormatarDgvPackFiltro();

            foreach (var item in packVirtual)
            {
                cboModeloPack.SelectedIndex = item.ModeloPack;

                dgvPackFiltro.Rows.Add();

                dgvPackFiltro[(int)ColunasPack.Codigo] = item.Codigo.ToString();
                dgvPackFiltro[(int)ColunasPack.Descricao] = item.Descricao;
                dgvPackFiltro[(int)ColunasPack.ModeloPack] = cboModeloPack.SelectedItem.ToString();
                dgvPackFiltro[(int)ColunasPack.DtInicial] = item.DtInicial.ToString();
                dgvPackFiltro[(int)ColunasPack.DtFinal] = item.DtFinal.ToString();
                dgvPackFiltro[(int)ColunasPack.QuantidadeLimite] = item.QuantidadeLimite.ToString();
                dgvPackFiltro[(int)ColunasPack.CodScannTech] = item.CodScannTech.ToString();
            }
        }

        public bool VerificarProdutoExistenteDgvGrupo2(string codigoProduto)
        {
            bool retorno = false;

            if (dgvGrupo2.Rows.Count == 0)
            {
                retorno = false;
            }

            else
            {
                for (var linha = 0; linha < dgvGrupo2.Rows.Count; linha++)
                {
                    if (dgvGrupo2[linha, 0] == codigoProduto)
                    {
                        retorno = true;
                    }
                }
            }

            return retorno;
        }
        private void CarregarFormas(IBanco banco)
        {
            var formas = FormaPgto.ConsultarTodos(banco);

            cboFormasPgto.Adicionar("Todas", -1);
            foreach (var item in formas)
                cboFormasPgto.Adicionar(item.Nome, item.Codigo);

            cboFormasPgto.SelectedIndex = 0;
        }

        private void CarregarComboModeloPack(ComboBox cboPack, bool usaTodos)
        {

            IBanco banco = null;
            cboPack.Items.Clear();

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

                if (usaTodos)
                    cboPack.Items.Add("Todos");
                else
                    cboPack.Items.Add("Selecione um modelo");

                cboPack.Items.Add("Leve X Pague Y");
                cboPack.Items.Add("Pague 1 centavo ou mais em outro produto");
                cboPack.Items.Add("Pague menos a partir de x unidades (atacado)");
                cboPack.Items.Add("Pague menos por unidade");
                cboPack.Items.Add("Pague 1 centavo ou mais no próximo produto");
                cboPack.Items.Add("Leve x e Receba desconto percentual");
                cboPack.Items.Add("Leve x e Receba desconto por unidade");

                if (Modulo.ConsultarChave(banco, 711).Permissao == Modulo.EnumPermissao.Habilitado)
                    cboPack.Items.Add("Valor Diferenciado (Preço 2)");

                cboPack.Items.Add("Pague x porcento a menos a partir de x unidades (atacado)");
                cboPack.Items.Add("Leve R$ X e Receba desconto por unidade no produto x");
                cboPack.Items.Add("Leve R$ X e Receba desconto percentual no produto x");
                cboPack.Items.Add("Pague x porcento a menos por unidade");

            }
            catch (Exception ex)
            {
                Msg.Informar(ex.Message);
            }
            finally
            {
                if (banco != null) banco.FecharConexao();
            }
            if (Modulo.ConsultarChave(banco, 711).Permissao == Modulo.EnumPermissao.Habilitado)
                cboPack.SelectedIndex = 8;
            else
                cboPack.SelectedIndex = 0;
        }

        private void CarregarDgvGrupoClientes()
        {
            IBanco banco = null;
            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

                var lista = GrupoCliente.ConsultarTodos(banco);
                var indice = dgvGruposClientes.Rows.Add();
                dgvGruposClientes.DesmarcarCelula(indice, (int)ColunasDgvGruposClientes.Selecao);
                dgvGruposClientes[(int)ColunasDgvGruposClientes.Nome] = Pack.PackVirtualGrupoCliente.NOME_EXIBICAO_NAO_IDENTIFICADO;
                dgvGruposClientes.Rows[indice].Tag = 0;
                dgvGruposClientes.Rows[indice].Cells[(int)ColunasDgvGruposClientes.Nome].Style.Font = new Font(dgvGruposClientes.DefaultCellStyle.Font, FontStyle.Italic);
                foreach (var item in lista)
                {
                    indice = dgvGruposClientes.Rows.Add();
                    dgvGruposClientes.DesmarcarCelula(indice, (int)ColunasDgvGruposClientes.Selecao);
                    dgvGruposClientes[(int)ColunasDgvGruposClientes.Nome] = item.NOME;
                    dgvGruposClientes.UltimaLinha.Tag = item.CODIGO;
                }

            }
            finally

            {
                if (banco != null) banco.FecharConexao();
            }
        }

        private void cboModeloPack_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (dgvDiferenciado.Rows.Count != 0)
            {
                ReiniciarVariaveisControle();
            }

            if (_atualizar)
            {
                _alteradoPackVirtual = true;
            }

            DefinirPosicaoPadraoLayout();

            gbxLimitePack.Visible = false;

            const bool VALOR = false;

            if (!_atualizar)
            {
                txtValorRegra.Text = "";
            }

            lblProdutoGratis.Visible = false;
            gbAjustarQuebra.Visible = false;

            btnImprimirEtiqueta.Visible = false;


            LimparDataGridView(dgvGrupo2);

            //dgvGrupo1.FormatarColuna((int)ColunasDgvGrupoAtacado.ValorAtacado, "Valor Atacad.", 0, DataGridViewContentAlignment.MiddleRight);
            dgvGrupo1.Columns[(int)ColunasDgvGrupoAtacado.ValorAtacado].Visible = false;

            //FormatarDgvGrupo1();
            switch (cboModeloPack.Text)
            {
                case "Leve X Pague Y":

                    if (!string.IsNullOrEmpty(CodigoEncarteAlterado))
                    {
                        txtCodEncarte.Text = CodigoEncarteAlterado;
                        txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    }

                    ExibirObjetosPreco2(false);

                    txtQtdRegra.Text = "0";

                    chkLimitarQntPack.Checked = false; 

                    gbxLimitePack.Visible = true;

                    ExibirObjetosGruposClientes(true);

                    ExibirObjetosEncarte(true);

                    ExibirGroupBoxValores(!VALOR);

                    ExibirObjetosRegras(!VALOR);

                    DefinirValoresLabels("Compre ", "unidade(s) deste(s) produto(s) ", "Pague apenas ", "  unidade(s)");

                    ExibirPanelGrupo2(VALOR);

                    PosicionarLayoutLabel(lblParte2, lblParte2.Location.X, txtQtdRegra.Location.Y,
                                        txtQtdRegra.Text.Length, -5, 15);

                    PosicionarLayoutLabel(lblParte4, lblParte4.Location.X, txtValorRegra.Location.Y,
                                          lblParte3.Text.Length, 25, lblParte4.Location.Y);

                    PosicionarLayoutTextBox(txtValorRegra, txtValorRegra.Location.X, lblParte3.Location.Y,
                                            lblParte3.Text.Length, 28, txtValorRegra.Location.Y);

                    Application.DoEvents();

                    break;

                case "Pague 1 centavo ou mais em outro produto":

                    if (!string.IsNullOrEmpty(CodigoEncarteAlterado))
                    {
                        txtCodEncarte.Text = CodigoEncarteAlterado;
                        txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    }


                    ExibirObjetosPreco2(false);

                    ExibirObjetosGruposClientes(true);

                    ExibirObjetosEncarte(true);

                    ExibirGroupBoxValores(!VALOR);

                    ExibirObjetosRegras(!VALOR);

                    DefinirValoresLabels("Compre ", "unidade(s) deste(s) produto(s) ", "Pague apenas R$",
                                         "  nestes outros produtos");

                    ExibirPanelGrupo2(!VALOR);

                    PosicionarLayoutLabel(lblParte2, lblParte2.Location.X, txtQtdRegra.Location.Y,
                               txtQtdRegra.Text.Length, -5, 15);

                    PosicionarLayoutTextBox(txtValorRegra, txtValorRegra.Location.X, lblParte3.Location.Y,
                                            lblParte3.Text.Length, 45, txtValorRegra.Location.Y);

                    PosicionarLayoutLabel(lblProdutoGratis, lblParte3.Location.X, lblParte2.Location.Y,
                                          lblParte2.Text.Length,
                                          -45, 37);

                    PosicionarLayoutLabel(lblParte4, 591, txtValorRegra.Location.Y, lblParte3.Text.Length, 41,
                                          lblParte4.Location.Y);

                    txtValorRegra.Text = "0,01";

                    lblProdutoGratis.Visible = true;

                    Application.DoEvents();

                    break;

                case "Pague menos a partir de x unidades (atacado)":

                    if (!string.IsNullOrEmpty(CodigoEncarteAlterado))
                    {
                        txtCodEncarte.Text = CodigoEncarteAlterado;
                        txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    }

                    btnImprimirEtiqueta.Visible = true;
                    ExibirObjetosPreco2(false);

                    ExibirObjetosGruposClientes(true);

                    ExibirObjetosEncarte(true);

                    ExibirGroupBoxValores(!VALOR);

                    ExibirObjetosRegras(!VALOR);

                    DefinirValoresLabels("Compre a partir de ", "unidade(s) deste(s) produto(s) ", "Pague apenas R$",
                                         " por unidade");

                    ExibirPanelGrupo2(VALOR);
                    
                    PosicionarLayoutLabel(lblParte1, 58, 0, 0, 0, 15);

                    PosicionarLayoutLabel(lblParte2, lblParte2.Location.X, txtQtdRegra.Location.Y,
                                          txtQtdRegra.Text.Length, 10, 15);

                    PosicionarLayoutLabel(lblParte3, lblParte3.Location.X, lblParte2.Location.Y, lblParte2.Text.Length,
                                          -20, lblParte3.Location.Y);

                    PosicionarLayoutTextBox(txtQtdRegra, txtQtdRegra.Location.X, lblParte1.Location.Y,
                                            lblParte1.Text.Length, -16, txtQtdRegra.Location.Y);

                    PosicionarLayoutTextBox(txtValorRegra, txtValorRegra.Location.X, lblParte3.Location.Y,
                                            lblParte3.Text.Length, 70, txtValorRegra.Location.Y);

                    PosicionarLayoutLabel(lblParte4, lblParte4.Location.X, txtValorRegra.Location.Y,
                                          txtValorRegra.Text.Length, 85, lblParte4.Location.Y);

                    Application.DoEvents();

                    break;
                case "Pague x porcento a menos a partir de x unidades (atacado)":

                    if (!string.IsNullOrEmpty(CodigoEncarteAlterado))
                    {
                        txtCodEncarte.Text = CodigoEncarteAlterado;
                        txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    }
                    dgvGrupo1.Tag = "Atacado";
                    btnImprimirEtiqueta.Visible = true;


                    ExibirObjetosPreco2(false);

                    ExibirObjetosGruposClientes(true);

                    ExibirObjetosEncarte(true);

                    ExibirGroupBoxValores(!VALOR);

                    ExibirObjetosRegras(!VALOR);

                    FormatarDgvGrupo1_Atacado();


                    DefinirValoresLabels("Compre a partir de ", "unidade(s) deste(s) produto(s) ", "Receba",
                                         " % de desconto nestes outros produtos.");

                    ExibirPanelGrupo2(VALOR);

                    PosicionarLayoutLabel(lblParte1, 58, 0, 0, 0, 15);

                    PosicionarLayoutLabel(lblParte2, lblParte2.Location.X, txtQtdRegra.Location.Y,
                                          txtQtdRegra.Text.Length, 10, 15);

                    PosicionarLayoutLabel(lblParte3, lblParte3.Location.X, lblParte2.Location.Y, lblParte2.Text.Length,
                                         20, lblParte3.Location.Y);

                    PosicionarLayoutTextBox(txtQtdRegra, txtQtdRegra.Location.X, lblParte1.Location.Y,
                                            lblParte1.Text.Length, -16, txtQtdRegra.Location.Y);

                    PosicionarLayoutTextBox(txtValorRegra, txtValorRegra.Location.X, lblParte3.Location.Y,
                                            lblParte3.Text.Length, 70, txtValorRegra.Location.Y);

                    PosicionarLayoutLabel(lblParte4, lblParte4.Location.X, txtValorRegra.Location.Y,
                                          txtValorRegra.Text.Length, 85, lblParte4.Location.Y);
                    gbAjustarQuebra.Visible = true;

                    Application.DoEvents();

                    break;

                case "Pague menos por unidade":

                    if (!string.IsNullOrEmpty(CodigoEncarteAlterado))
                    {
                        txtCodEncarte.Text = CodigoEncarteAlterado;
                        txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    }


                    ExibirObjetosPreco2(false);

                    ExibirObjetosGruposClientes(true);

                    ExibirObjetosEncarte(true);

                    txtQtdRegra.Text = "0";

                    chkLimitarQntPack.Checked = false;

                    gbxLimitePack.Visible = true;

                    ExibirGroupBoxValores(!VALOR);

                    ExibirObjetosRegras(!VALOR);

                    DefinirValoresLabels("Compre ", "unidade(s) deste(s) produto(s) ", "Pague apenas R$",
                                         " por unidade(s)");

                    ExibirPanelGrupo2(VALOR);

                    PosicionarLayoutLabel(lblParte2, lblParte2.Location.X, txtQtdRegra.Location.Y,
                               txtQtdRegra.Text.Length, -5, 15);

                    PosicionarLayoutTextBox(txtValorRegra, txtValorRegra.Location.X, lblParte3.Location.Y,
                                            lblParte3.Text.Length, 43, txtValorRegra.Location.Y);

                    PosicionarLayoutLabel(lblParte4, lblParte4.Location.X, txtValorRegra.Location.Y,
                                          lblParte3.Text.Length, 40, lblParte4.Location.Y);

                    Application.DoEvents();

                    break;

                case "Pague x porcento a menos por unidade":

                    if (!string.IsNullOrEmpty(CodigoEncarteAlterado))
                    {
                        txtCodEncarte.Text = CodigoEncarteAlterado;
                        txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    }


                    ExibirObjetosPreco2(false);

                    ExibirObjetosGruposClientes(true);

                    ExibirObjetosEncarte(true);

                    txtQtdRegra.Text = "0";

                    chkLimitarQntPack.Checked = false;

                    gbxLimitePack.Visible = true;

                    ExibirGroupBoxValores(!VALOR);

                    ExibirObjetosRegras(!VALOR);

                    DefinirValoresLabels("Compre ", "unidade(s) deste(s) produto(s) ", "Receba ",
                                         "% de desconto nestes produtos");
                   
                    ExibirPanelGrupo2(VALOR);

                    PosicionarLayoutLabel(lblParte2, lblParte2.Location.X, txtQtdRegra.Location.Y,
                               txtQtdRegra.Text.Length, -5, 15);

                    PosicionarLayoutTextBox(txtValorRegra, txtValorRegra.Location.X, lblParte3.Location.Y,
                                            lblParte3.Text.Length, 43, txtValorRegra.Location.Y);

                    PosicionarLayoutLabel(lblParte4, lblParte4.Location.X, txtValorRegra.Location.Y,
                                          lblParte3.Text.Length, 40, lblParte4.Location.Y);

                    Application.DoEvents();

                    break;

                case "Pague 1 centavo ou mais no próximo produto":

                    if (!string.IsNullOrEmpty(CodigoEncarteAlterado))
                    {
                        txtCodEncarte.Text = CodigoEncarteAlterado;
                        txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    }


                    ExibirObjetosPreco2(false);

                    ExibirObjetosGruposClientes(true);

                    ExibirObjetosEncarte(true);

                    ExibirGroupBoxValores(!VALOR);

                    ExibirObjetosRegras(!VALOR);

                    DefinirValoresLabels("A cada ", "unidade(s) deste(s) produto(s) ", "Pague R$",
                                         " na próxima unidade do(s) mesmo(s) produto(s)");

                    ExibirPanelGrupo2(VALOR);

                    PosicionarLayoutLabel(lblParte1, 38, 0, 0, 0, 15);

                    PosicionarLayoutLabel(lblParte2, lblParte2.Location.X, txtQtdRegra.Location.Y,
                                          txtQtdRegra.Text.Length, -85, 15);

                    PosicionarLayoutLabel(lblParte3, lblParte3.Location.X, lblParte2.Location.Y, lblParte2.Text.Length,
                                          -140, lblParte3.Location.Y);

                    PosicionarLayoutLabel(lblProdutoGratis, lblParte3.Location.X, lblParte2.Location.Y,
                                          lblParte2.Text.Length,
                                          -45, 37);

                    PosicionarLayoutLabel(lblParte4, lblParte4.Location.X, txtValorRegra.Location.Y,
                                          txtValorRegra.Text.Length, -80, lblParte4.Location.Y);

                    PosicionarLayoutTextBox(txtQtdRegra, 100, 0, 0, 0, txtQtdRegra.Location.Y);

                    PosicionarLayoutTextBox(txtValorRegra, txtValorRegra.Location.X, lblParte3.Location.Y,
                                            lblParte3.Text.Length, -95, txtValorRegra.Location.Y);

                    txtValorRegra.Text = "0,01";

                    lblProdutoGratis.Visible = true;

                    Application.DoEvents();

                    break;

                case "Leve x e Receba desconto percentual":

                    if (!string.IsNullOrEmpty(CodigoEncarteAlterado))
                    {
                        txtCodEncarte.Text = CodigoEncarteAlterado;
                        txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    }

                    ExibirObjetosPreco2(false);

                    ExibirObjetosGruposClientes(true);

                    ExibirObjetosEncarte(true);

                    ExibirGroupBoxValores(!VALOR);

                    ExibirObjetosRegras(!VALOR);

                    DefinirValoresLabels("Compre ", " unidade(s) deste(s) produto(s)", "Receba ",
                                         "% de desconto nestes outros produtos");

                    ExibirPanelGrupo2(!VALOR);

                    PosicionarLayoutLabel(lblParte2, lblParte2.Location.X, txtQtdRegra.Location.Y,
                               txtQtdRegra.Text.Length, -5, 15);

                    PosicionarLayoutTextBox(txtValorRegra, txtValorRegra.Location.X, lblParte3.Location.Y,
                                            lblParte3.Text.Length, -12, txtValorRegra.Location.Y);

                    PosicionarLayoutLabel(lblParte4, lblParte4.Location.X, txtValorRegra.Location.Y,
                                          lblParte3.Text.Length, -10, lblParte4.Location.Y);

                    Application.DoEvents();

                    break;

                case "Leve x e Receba desconto por unidade":

                    if (!string.IsNullOrEmpty(CodigoEncarteAlterado))
                    {
                        txtCodEncarte.Text = CodigoEncarteAlterado;
                        txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    }


                    ExibirObjetosPreco2(false);

                    ExibirObjetosGruposClientes(true);

                    ExibirObjetosEncarte(true);

                    ExibirGroupBoxValores(!VALOR);

                    ExibirObjetosRegras(!VALOR);

                    DefinirValoresLabels("Compre ", " unidade(s) deste(s) produto(s)", "Receba R$ ",
                                         " de desconto por unidade nestes outros produtos");

                    ExibirPanelGrupo2(!VALOR);

                    PosicionarLayoutLabel(lblParte1, 38, 0, 0, 0, 15);

                    PosicionarLayoutLabel(lblParte2, lblParte2.Location.X, txtQtdRegra.Location.Y,
                                          txtQtdRegra.Text.Length, -90, 15);

                    PosicionarLayoutLabel(lblParte3, lblParte3.Location.X, lblParte2.Location.Y, lblParte2.Text.Length,
                                          -138, lblParte3.Location.Y);

                    PosicionarLayoutLabel(lblParte4, lblParte4.Location.X, txtValorRegra.Location.Y,
                                          lblParte3.Text.Length, -80, lblParte4.Location.Y);

                    PosicionarLayoutTextBox(txtQtdRegra, txtQtdRegra.Location.X, lblParte1.Location.Y,
                                            lblParte1.Text.Length, -100, txtValorRegra.Location.Y);

                    PosicionarLayoutTextBox(txtValorRegra, txtValorRegra.Location.X, lblParte3.Location.Y,
                                            lblParte3.Text.Length, -85, txtValorRegra.Location.Y);

                    Application.DoEvents();

                    break;

                case "Valor Diferenciado (Preço 2)":
                    if (dgvGrupo1.Rows.Count != 0 || dgvGrupo2.Rows.Count != 0)
                    {
                        ReiniciarVariaveisControle();
                    }
                    CodigoEncarteAlterado = txtCodEncarte.Text;
                    txtCodEncarte.Text = "1";
                    txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    groupBoxValores.Visible = false;
                    ExibirObjetosPreco2(true);
                    ExibirObjetosEncarte(false);
                    ExibirObjetosGruposClientes(false);
                    break;

                case "Leve R$ X e Receba desconto percentual no produto x":

                    if (!string.IsNullOrEmpty(CodigoEncarteAlterado))
                    {
                        txtCodEncarte.Text = CodigoEncarteAlterado;
                        txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    }

                    ExibirObjetosPreco2(false);

                    ExibirObjetosGruposClientes(true);

                    ExibirObjetosEncarte(true);

                    ExibirGroupBoxValores(!VALOR);

                    ExibirObjetosRegras(!VALOR);

                    DefinirValoresLabels("Compre R$ ", " em mercadorias (fora deste pack) ", "Receba ",
                                         "% de desconto nestes produtos");

                    ExibirPanelGrupo2(VALOR);

                    PosicionarLayoutLabel(lblParte1, 65, 0, 0, 0, 15);

                    PosicionarLayoutLabel(lblParte2, lblParte2.Location.X, txtQtdRegra.Location.Y,
                                          txtQtdRegra.Text.Length, -35, 15);

                    PosicionarLayoutLabel(lblParte3, lblParte3.Location.X, lblParte2.Location.Y, lblParte2.Text.Length,
                                          -30, lblParte3.Location.Y);

                    PosicionarLayoutTextBox(txtQtdRegra, txtQtdRegra.Location.X, lblParte1.Location.Y,
                                            lblParte1.Text.Length, -50, txtQtdRegra.Location.Y);

                    PosicionarLayoutTextBox(txtValorRegra, txtValorRegra.Location.X, lblParte3.Location.Y,
                                            lblParte3.Text.Length, 10, txtValorRegra.Location.Y);

                    PosicionarLayoutLabel(lblParte4, lblParte4.Location.X, txtValorRegra.Location.Y,
                                          txtValorRegra.Text.Length, 20, lblParte4.Location.Y);

                    Application.DoEvents();

                    break;

                case "Leve R$ X e Receba desconto por unidade no produto x":

                    if (!string.IsNullOrEmpty(CodigoEncarteAlterado))
                    {
                        txtCodEncarte.Text = CodigoEncarteAlterado;
                        txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    }


                    ExibirObjetosPreco2(false);

                    ExibirObjetosGruposClientes(true);

                    ExibirObjetosEncarte(true);

                    ExibirGroupBoxValores(!VALOR);

                    ExibirObjetosRegras(!VALOR);

                    DefinirValoresLabels("Compre R$ ", " em mercadorias (fora deste pack) ", "Receba R$ ",
                                         " de desconto por unidade nestes produtos");

                    ExibirPanelGrupo2(VALOR);

                    PosicionarLayoutLabel(lblParte1, 65, 0, 0, 0, 15);

                    PosicionarLayoutLabel(lblParte2, lblParte2.Location.X, txtQtdRegra.Location.Y,
                                          txtQtdRegra.Text.Length, -35, 15);

                    PosicionarLayoutLabel(lblParte3, lblParte3.Location.X, lblParte2.Location.Y, lblParte2.Text.Length,
                                          -30, lblParte3.Location.Y);

                    PosicionarLayoutTextBox(txtQtdRegra, txtQtdRegra.Location.X, lblParte1.Location.Y,
                                            lblParte1.Text.Length, -50, txtQtdRegra.Location.Y);

                    PosicionarLayoutTextBox(txtValorRegra, txtValorRegra.Location.X, lblParte3.Location.Y,
                                            lblParte3.Text.Length, 30, txtValorRegra.Location.Y);

                    PosicionarLayoutLabel(lblParte4, lblParte4.Location.X, txtValorRegra.Location.Y,
                                          txtValorRegra.Text.Length, 40, lblParte4.Location.Y);

                    Application.DoEvents();

                    break;


                default:
                    if (!string.IsNullOrEmpty(CodigoEncarteAlterado))
                    {
                        txtCodEncarte.Text = CodigoEncarteAlterado;
                        txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    }

                    ExibirObjetosRegras(VALOR);
                    ExibirPanelGrupo2(VALOR);
                    ExibirGroupBoxValores(VALOR);
                    ExibirObjetosPreco2(false);
                    ExibirObjetosEncarte(true);
                    ExibirObjetosGruposClientes(true);
                    break;
            }

            if (cboModeloPack.Text.Equals("Leve X Pague Y"))
            {
                txtValorRegra.Formato = CaixaTexto.TipoFormato.Inteiro;
            }
            else
            {
                txtValorRegra.Formato = CaixaTexto.TipoFormato.Moeda;
            }

            if (cboModeloPack.Text.Equals("Leve R$ X e Receba desconto por unidade no produto x")
                || cboModeloPack.Text.Equals("Leve R$ X e Receba desconto percentual no produto x"))
            {
                txtQtdRegra.Formato = CaixaTexto.TipoFormato.Moeda;
            }
            else
            {
                txtQtdRegra.Formato = CaixaTexto.TipoFormato.Livre;
                txtQtdRegra.Decimais = 0;
                if (!Texto.TestarNumerico(txtQtdRegra.Text))
                    txtQtdRegra.Text = "0";

                txtQtdRegra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            }


        }


        /// <summary>
        /// Método que posiciona o TextBox na Posição X (Location.X) na tela.
        /// </summary>
        /// <param name="txt">(TextBox) TextBox a ser posicionado</param>
        /// <param name="posicaoObjetoX">(int) Posição X atual do Label informado por parâmetro</param>
        /// <param name="posicaoObjetoAnteriorY">(int) Posição Y do objeto anterior na tela, ex: lblExemplo.Location.Y</param>
        /// <param name="tamanhoObjetoAnterior">(int) Tamanho do objeto anterior ao objeto a ser posicionado.</param>
        /// <param name="valorIncrementar">(int) Valor a ser incrementado para a nova posição</param>
        /// <param name="posicaoObjetoY">(int) Posição Y atual do TextBox informado por parâmetro</param>
        private void PosicionarLayoutTextBox(TextBox txt, int posicaoObjetoX, int posicaoObjetoAnteriorY,
                                             int tamanhoObjetoAnterior, int valorIncrementar, int posicaoObjetoY)
        {
            txt.Location = new Point(
                posicaoObjetoX + posicaoObjetoAnteriorY + tamanhoObjetoAnterior + valorIncrementar, posicaoObjetoY);
        }

        /// <summary>
        /// Método que posiciona o Label na Posição X (Location.X) na tela.
        /// </summary>
        /// <param name="l">(Label) Label a ser posicionado</param>
        /// <param name="posicaoObjetoX">(int) Posição X atual do Label informado por parâmetro</param>
        /// <param name="posicaoObjetoAnteriorY">(int) Posição Y do objeto anterior na tela, ex: lblExemplo.Location.Y</param>
        /// <param name="tamanhoObjetoAnterior">(int) Tamanho do objeto anterior ao objeto a ser posicionado.</param>
        /// <param name="valorIncrementar">(int) Valor a ser incrementado para a nova posição</param>
        /// <param name="posicaoObjetoY">(int) Posição Y atual do Label informado por parâmetro</param>
        private void PosicionarLayoutLabel(Label l, int posicaoObjetoX, int posicaoObjetoAnteriorY,
                                           int tamanhoObjetoAnterior, int valorIncrementar, int posicaoObjetoY)
        {
            l.Location = new Point(posicaoObjetoX + posicaoObjetoAnteriorY + tamanhoObjetoAnterior + valorIncrementar,
                                   posicaoObjetoY);
        }


        protected void DefinirPosicaoPadraoLayout()
        {
            //DefinirValoresLabels("Compre ", " unidade(s) deste(s) produto(s)", "Pague ", " neste(s) outros produto(s)");

            const int VALOR_ORIGINAL_LBLPARTE1_POSICAO_X = 114;
            const int VALOR_ORIGINAL_LBLPARTE1_POSICAO_Y = 15;

            const int VALOR_ORIGINAL_LBLPARTE2_POSICAO_X = 230;
            const int VALOR_ORIGINAL_LBLPARTE2_POSICAO_Y = 15;

            const int VALOR_ORIGINAL_LBLPARTE3_POSICAO_X = 480;
            const int VALOR_ORIGINAL_LBLPARTE3_POSICAO_Y = 15;

            const int VALOR_ORIGINAL_LBLPARTE4_POSICAO_X = 591;
            const int VALOR_ORIGINAL_LBLPARTE4_POSICAO_Y = 15;

            const int VALOR_ORIGINAL_TXT_QTD_REGRA_POSICAO_X = 182;
            const int VALOR_ORIGINAL_TXT_QTD_REGRA_POSICAO_Y = 13;

            const int VALOR_ORIGINAL_TXT_VALOR_REGRA_POSICAO_X = 539;
            const int VALOR_ORIGINAL_TXT_VALOR_REGRA_POSICAO_Y = 13;

            lblParte1.Location = new Point(VALOR_ORIGINAL_LBLPARTE1_POSICAO_X, VALOR_ORIGINAL_LBLPARTE1_POSICAO_Y);
            lblParte2.Location = new Point(VALOR_ORIGINAL_LBLPARTE2_POSICAO_X, VALOR_ORIGINAL_LBLPARTE2_POSICAO_Y);
            lblParte3.Location = new Point(VALOR_ORIGINAL_LBLPARTE3_POSICAO_X, VALOR_ORIGINAL_LBLPARTE3_POSICAO_Y);
            lblParte4.Location = new Point(VALOR_ORIGINAL_LBLPARTE4_POSICAO_X, VALOR_ORIGINAL_LBLPARTE4_POSICAO_Y);

            txtQtdRegra.Location = new Point(VALOR_ORIGINAL_TXT_QTD_REGRA_POSICAO_X,
                                             VALOR_ORIGINAL_TXT_QTD_REGRA_POSICAO_Y);
            txtValorRegra.Location = new Point(VALOR_ORIGINAL_TXT_VALOR_REGRA_POSICAO_X,
                                               VALOR_ORIGINAL_TXT_VALOR_REGRA_POSICAO_Y);

            lblParte1.Size = new Size(62, 16);
            lblParte2.Size = new Size(218, 16);
            lblParte3.Size = new Size(53, 16);
            lblParte4.Size = new Size(186, 16);
        }

        private void ExibirObjetosPreco2(bool valor)
        {
            groupBoxPrecoDiferenciado.Visible = valor;
            groupBoxPrecoDiferenciado.Location = groupBoxValores.Location;
            groupBoxPrecoDiferenciado.Size = groupBoxValores.Size;

            if (cboModeloPack.Text.Equals("Valor Diferenciado (Preço 2)"))
            {
                groupBoxValores.Visible = !valor;
            }
        }

        private void ExibirObjetosEncarte(bool valor)
        {
            if (!_encarteHabilitado)
                valor = false;
            btnBuscaEncarte.Visible = valor;
            txtCodEncarte.Visible = valor;
            lblCodEncarte.Visible = valor;
            txtEncarte.Visible = valor;
            lblEncarte.Visible = valor;
            ptbAjudaEncarte.Visible = valor;
        }

        private void ExibirObjetosRegras(bool valor)
        {
            lblParte1.Visible = valor;
            lblParte2.Visible = valor;
            lblParte3.Visible = valor;
            lblParte4.Visible = valor;

            txtQtdRegra.Visible = valor;
            txtValorRegra.Visible = valor;
        }

        private void ExibirPanelGrupo2(bool valor)
        {
            pnlGrupo2.Visible = valor;
        }
        

        private void ExibirGroupBoxValores(bool valor)
        {
            groupBoxValores.Visible = valor;
        }

        /// <summary>
        /// Método que define os valores nos Labels.
        ///  Parâmetros:
        /// 1° (string) Texto a ser exibido no lblParte1
        /// 2º (string) Texto a ser exibido no lblParte2
        /// 3º (string) Texto a ser exibido no lblParte3
        /// 4° (string) Texto a ser exibido no lblParte4
        /// </summary>
        private void DefinirValoresLabels(string textoLabel1, string textoLabel2, string textoLabel3, string textoLabel4)
        {
            lblParte1.Text = textoLabel1;
            lblParte2.Text = textoLabel2;
            lblParte3.Text = textoLabel3;
            lblParte4.Text = textoLabel4;

            DefinirTamanhoLabels(lblParte1.Text.Length, lblParte2.Text.Length, lblParte3.Text.Length,
                                 lblParte4.Text.Length);
        }


        /// <summary>
        /// Método que define os valores dos tamanhos nos Labels.
        ///  Parâmetros:
        /// 1° (int) Tamanho a ser definido no lblParte1
        /// 2° (int) Tamanho a ser definido no lblParte2
        /// 3° (int) Tamanho a ser definido no lblParte3
        /// 4° (int) Tamanho a ser definido no lblParte4
        /// </summary>
        private void DefinirTamanhoLabels(int tamanhoLabel1, int tamanhoLabel2, int tamanhoLabel3, int tamanhoLabel4)
        {
            lblParte1.Width = tamanhoLabel1;
            lblParte2.Width = tamanhoLabel2;
            lblParte3.Width = tamanhoLabel3;
            lblParte4.Width = tamanhoLabel4;
        }

        private void txtValorRegra_Leave(object sender, EventArgs e)
        {
            if (cboModeloPack.Text == "Leve x e Receba desconto percentual"
                || cboModeloPack.Text == "Leve R$ X e Receba desconto percentual no produto x")
            {
                if (Double.Parse(txtValorRegra.Text) > 100)
                {
                    txtValorRegra.Text = "100";
                }
            }

            double numero = 0;
            if (!double.TryParse(txtValorRegra.Text, out numero))
            {
                Msg.Criticar("Valor deve ser numérico.");
                txtValorRegra.Focus();
                return;
            }


        }

        private void txtValorRegra_Enter(object sender, EventArgs e)
        {
            //if (cboModeloPack.Text == "Leve x e Receba desconto percentual")
            //{
            //    txtValorRegra.MaxLength = 4;
            //}
            //else
            //    txtValorRegra.MaxLength = 5;
        }

        private void cboModeloPack_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        protected void btnProcurarGrupo1_Click(object sender, EventArgs e)
        {
            if (!ExisteLojasSelecionadas())
                return;

            SetarFocusTextBox(txtCodProdGrupo1);

            var buscaProdutoPackVirtual = new FrmBuscaProdutoPackVirtual();

            buscaProdutoPackVirtual.SelecionouItem += buscaPackVirtualGrupo1_SelecionouItem;

            buscaProdutoPackVirtual.ShowDialog();
        }

        private void frmPackVirtual_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (ActiveControl != txtCodEncarte)
                        SendKeys.SendWait("{TAB}");
                    break;
                case Keys.Escape:

                    if (tabControl1.SelectedTab.Equals(tabPesquisa))
                    {
                        if (txtCodigo.Enabled)
                            Close();
                    }
                    else
                    {
                        btnCancelar_Click(null, null);
                    }
                    break;
            }
        }


        private Pack.PackVirtualLojas RetornarPackVirtualLojaAnteriorExistente(string codigoPack, double codigoProduto,
                                                                               DateTime dataInicial, DateTime dataFinal,
                                                                               bool prorrogar)
        {
            IBanco banco = null;

            Pack.PackVirtualLojas packVirtualLojas = null;

            var dataInicio = DateTime.Parse(string.Format("{0:dd/MM/yyyy HH:mm}", dataInicial));

            var dataFim = DateTime.Parse(string.Format("{0:dd/MM/yyyy HH:mm}", dataFinal));

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();
                var lojas = "";

                if (dgvLojasCadastro.Visible || dgvPackFiltro.Visible)
                {

                    if (prorrogar)
                    {
                        var listaPackVirtualLojas = Pack.PackVirtualLojas.ObterLojasCodigoPack(banco,
                                                                                               double.Parse(codigoPack));

                        lojas = listaPackVirtualLojas.Aggregate(lojas,
                                                                (current, item) => current + (", " + item.CodLoja));
                    }
                    else
                    {
                        var listaLojas = RetornarLojasSelecionadasDataGridView(dgvLojasCadastro);

                        lojas = listaLojas.Aggregate(lojas, (current, item) => current + (", " + item.Codigo));

                        if (listaLojas.Count.Equals(0))

                            throw new Exception("Selecione uma loja!");
                    }
                    lojas = lojas.Remove(0, 2);


                }
                else
                {
                    lojas = "1";
                }
                packVirtualLojas = Pack.PackVirtualLojas.ConsultarPackVirtualLojaAnteriorExistente(banco, banco.ObterDuplo(codigoProduto), banco.ObterDataHora(dataInicio), banco.ObterDataHora(dataFim), banco.ObterDataHora(banco.ObterDataServidor()), lojas, codigoPack);
            }
            catch (Exception ex)
            {
                dgvLojasCadastro.Focus();
            }

            finally
            {
                if (banco != null) banco.FecharConexao();
            }

            return packVirtualLojas;
        }

        private Pack.PackVirtualLojas RetornarPackVirtualDiferenciadoLojaAnteriorExistente(string codigoPack, double codigoProduto,
                                                                       DateTime dataInicial, DateTime dataFinal,
                                                                       bool prorrogar)
        {
            IBanco banco = null;

            Pack.PackVirtualLojas packVirtualLojas = null;

            var dataInicio = DateTime.Parse(string.Format("{0:dd/MM/yyyy HH:mm}", dataInicial));

            var dataFim = DateTime.Parse(string.Format("{0:dd/MM/yyyy HH:mm}", dataFinal));

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();
                var lojas = "";

                if (dgvLojasCadastro.Visible || dgvPackFiltro.Visible)
                {

                    if (prorrogar)
                    {
                        var listaPackVirtualLojas = Pack.PackVirtualLojas.ObterLojasCodigoPack(banco,
                                                                                               double.Parse(codigoPack));

                        lojas = listaPackVirtualLojas.Aggregate(lojas,
                                                                (current, item) => current + (", " + item.CodLoja));
                    }
                    else
                    {
                        var listaLojas = RetornarLojasSelecionadasDataGridView(dgvLojasCadastro);

                        lojas = listaLojas.Aggregate(lojas, (current, item) => current + (", " + item.Codigo));

                        if (listaLojas.Count.Equals(0))

                            throw new Exception("Selecione uma loja!");
                    }
                    lojas = lojas.Remove(0, 2);


                }
                else
                {
                    lojas = "1";
                }
                packVirtualLojas = Pack.PackVirtualLojas.ConsultarPackVirtualDiferenciadoLojaAnteriorExistente(banco, banco.ObterDuplo(codigoProduto), banco.ObterDataHora(dataInicio), banco.ObterDataHora(dataFim), banco.ObterDataHora(banco.ObterDataServidor()), lojas, codigoPack);
            }
            catch (Exception ex)
            {
                dgvLojasCadastro.Focus();
            }

            finally
            {
                if (banco != null) banco.FecharConexao();
            }

            return packVirtualLojas;
        }


        private void txtCodProdGrupo1_KeyDown(object sender, KeyEventArgs e)
        {
            int numero;
            List<Produto> produtoAssociado = null;
            IBanco banco = null;

            if (e.KeyCode == Keys.F4)
                btnProcurarGrupo1_Click(null, null);

            else if (e.KeyCode != Keys.Enter)
                return;

            if (!ExisteLojasSelecionadas())
                return;

            else if (!string.IsNullOrEmpty(txtCodProdGrupo1.Text) &&
                     txtCodProdGrupo1.Text != "0" && (Int32.TryParse(txtCodProdGrupo1.Text, out numero) || Texto.TestarNumerico(txtCodProdGrupo1.Text)))
            {
                var produto = ConsultarProduto(txtCodProdGrupo1.Text);

                if (produto.Count > 0)
                {
                    if (!ValidarProdutosAtivosMix(produto[0].Codigo))
                        return;

                    var packVirtualLojas = RetornarPackVirtualLojaAnteriorExistente(txtCodPackCadastro.Text,
                                                                                    produto[0].Codigo,
                                                                                    dtpHoraInicio.Value,
                                                                                    dtpHoraFim.Value, false);

                    if (packVirtualLojas != null)
                    {
                        if (packVirtualLojas.CodPack != 0)
                        {
                            Msg.Criticar("O produto código " + produto[0].Codigo +
                                         " não pode ser inserido, pois já encontra-se vinculado ao Pack código " +
                                         packVirtualLojas.CodPack + ".");
                            txtDesProdGrupo1.Text = "";
                            SetarFocusTextBox(txtCodProdGrupo1);
                            return;
                        }
                    }

                    if (produto[0].CD_ASSOCIADO != null)
                    {
                        produtoAssociado = RetornarProdutoAssociado(produto[0].CD_ASSOCIADO);

                        if (produtoAssociado.Count > 0)
                        {
                            var reposta = Msg.PerguntarPadraoNao("Este produto possui produtos associados, deseja inserí-los também?");

                            if (reposta == DialogResult.Yes)
                            {
                                foreach (var item in produtoAssociado)
                                {
                                    packVirtualLojas = RetornarPackVirtualLojaAnteriorExistente(
                                        txtCodPackCadastro.Text, item.Codigo, dtpHoraInicio.Value, dtpHoraFim.Value,
                                        false);

                                    if (packVirtualLojas == null) continue;
                                    if (packVirtualLojas.CodPack != 0)
                                    {
                                        Msg.Criticar("O produto código " + item.Codigo +
                                                     " não pode ser inserido, pois já encontra-se vinculado ao Pack código " +
                                                     packVirtualLojas.CodPack + ".");
                                        txtDesProdGrupo1.Text = "";
                                        SetarFocusTextBox(txtCodProdGrupo1);
                                        return;
                                    }
                                }



                                CarregarDgvGrupo1(produtoAssociado);

                                LimparCamposDgvGrupo1();

                                LimparSelecaoDataGridView(dgvGrupo1);

                                SetarFocusTextBox(txtCodProdGrupo1);
                            }
                            else
                            {
                                DefinirCamposGrupo(1, produto);

                                btnAddGrupo1_Click(null, new KeyEventArgs(Keys.Enter));
                            }

                        }
                    }
                    else
                    {
                        DefinirCamposGrupo(1, produto);
                    }
                }
                else
                {
                    Msg.Informar("Não foram encontrados registros ou o produto está inativo!");
                    txtDesProdGrupo1.Text = "";

                    SetarFocusTextBox(txtCodProdGrupo1);
                }
            }
        }


        /// <summary>
        /// Método que seta valores no TextBox dos Grupos (1 ou 2)
        /// </summary>
        /// <param name="TipoGrupo">Tipo do Grupo a ser setado a informação. Valores válidos 1 (Grupo 1) - 2 (Grupo 2) </param>
        /// <param name="produto">Lista com o produto a ser definido nos campos.</param>
        private void DefinirCamposGrupo(int TipoGrupo, List<Produto> produto)
        {
            if (produto.Count.Equals(0)) return;

            if (TipoGrupo == 1)
            {
                txtCodProdGrupo1.Text = produto[0].Codigo.ToString();
                txtDesProdGrupo1.Text = produto[0].DescricaoReduzida;

                btnAddGrupo1.Focus();
            }
            else if (TipoGrupo == 2)
            {
                txtCodProdGrupo2.Text = produto[0].Codigo.ToString();
                txtDescProdGrupo2.Text = produto[0].DescricaoReduzida;

                btnAddGrupo2.Focus();
            }
            else
            {
                txtCodProdutoDiferenciado.Text = produto[0].Codigo.ToString();
                txtDescProdDiferenciado.Text = produto[0].DescricaoReduzida;
                txtValorAtual.Text = buscaValorAtualLoja(produto[0].Codigo.ToString(), DefinirLojaValor(RetornarLojasSelecionadasDataGridView(dgvLojasCadastro)));
                txtValorDiferenciado.Text = buscaValorPreco2(produto[0].Codigo.ToString(), "0", DefinirLojaValor(RetornarLojasSelecionadasDataGridView(dgvLojasCadastro)));

                btnAddDiferenciado.Focus();
            }
        }

        private static List<Produto> RetornarProdutoAssociado(double? codigoProduto)
        {
            IBanco banco = null;

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

                return Produto.ConsultarAssociados(banco, (double)codigoProduto);

            }
            finally
            {
                if (banco != null) banco.FecharConexao();
            }
        }

        public Pack.PackVirtualSimplificado RetornarPackVirtualSimplificado(double codigo, string descricao,
                                                                            double modelo, DateTime dataInicial,
                                                                            DateTime dataFinal, string grupoCliente, int quantidadeLimite, int codScannTech)
        {
            var modeloPack = "";

            switch ((int)modelo)
            {
                case 01:
                    modeloPack = "Leve X Pague Y";
                    break;

                case 02:
                    modeloPack = "Pague 1 centavo ou mais em outro produto";
                    break;

                case 03:
                    modeloPack = "Pague menos a partir de x unidades (atacado)";
                    break;

                case 04:
                    modeloPack = "Pague menos por unidade";
                    break;

                case 05:
                    modeloPack = "Pague 1 centavo ou mais no próximo produto";
                    break;

                case 06:
                    modeloPack = "Leve x e Receba desconto percentual";
                    break;

                case 07:
                    modeloPack = "Leve x e Receba desconto por unidade";
                    break;

                case 08:
                    modeloPack = "Valor Diferenciado (Preço 2)";
                    break;

                case 09:
                    modeloPack = "Pague x porcento a menos a partir de x unidades (atacado)";
                    break;

                case 10:
                    modeloPack = "Leve R$ X e Receba desconto por unidade no produto x";
                    break;

                case 11:
                    modeloPack = "Leve R$ X e Receba desconto percentual no produto x";
                    break;

                case 12:
                    modeloPack = "Pague x porcento a menos por unidade";
                    break;

                default:
                    modeloPack = "Selecione um modelo";
                    break;
            }

            var packVirtualSimplificado = new Pack.PackVirtualSimplificado(codigo, descricao, modeloPack, dataInicial,
                                                                           dataFinal, grupoCliente, quantidadeLimite, codScannTech);

            return packVirtualSimplificado;
        }

        private static List<Produto> ConsultarProduto(string codigoProduto)
        {
            IBanco banco = null;
            var lista = new List<Produto>();

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();
                lista = Produto.ConsultarProdutoPack(banco, codigoProduto);
            }
            finally
            {
                if (banco != null) banco.FecharConexao();
            }

            return lista;
        }


        private static void SetarFocusTextBox(TextBox textBox)
        {
            textBox.SelectAll();
            textBox.Focus();
        }

        private static void SetarFocusDateTimePicker(DateTimePicker dtp)
        {
            dtp.Select();
            dtp.Focus();
        }

        /// <summary>
        /// Método que define o foco para o ComBox informado e seleciona a primeira posição deste.
        /// </summary>
        /// <param name="comboBox">(ComboBox) Combo em que será definido o foco.</param>
        private void SetarFocusComboBox(ComboBox comboBox)
        {
            comboBox.Focus();
            comboBox.SelectedIndex = 0;

            comboBox.SelectionStart = 0;
            comboBox.SelectionLength = cboModeloPack.Text.Length;
        }

        private void LimparCamposDgvGrupo1()
        {
            txtCodProdGrupo1.Text = "0";
            txtDesProdGrupo1.Text = "";
        }


        private void LimparCamposDgvGrupo2()
        {
            txtCodProdGrupo2.Text = "0";
            txtDescProdGrupo2.Text = "";
        }

        private void LimparCamposDgvDiferenciado()
        {
            txtCodProdutoDiferenciado.Text = "0";
            txtDescProdDiferenciado.Text = "";
            txtValorDiferenciado.Text = "0,00";
            txtValorAtual.Text = "0,00";
        }

        private void btnAddGrupo1_Click(object sender, EventArgs e)
        {
            IBanco banco = null;

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

                var listaProdutos = new List<Produto>();
                var produto = new Produto();
                bool valorDiferente = false;
                bool retornoResposta = false;
                double numero;

                if (Double.TryParse(txtCodProdGrupo1.Text, out numero))
                {
                    if (txtCodProdGrupo1.Text != "0" && !string.IsNullOrEmpty(txtDesProdGrupo1.Text))
                    {
                        produto = Produto.ConsultarPorCodigo(banco, double.Parse(txtCodProdGrupo1.Text));
                        if (produto == null) return;

                        if (dgvGrupo1.Rows.Count > 0)
                        {
                            for (var i = 0; i < dgvGrupo1.Rows.Count; i++)
                            {
                                if (produto.ValorVenda != decimal.Parse(dgvGrupo1[i, (int)ColunasDgvGrupo.Valor]))
                                    valorDiferente = true;
                            }
                        }

                        if (valorDiferente)
                        {
                            var resposta =
                                Msg.PerguntarPadraoNao(
                                    "O produto informado tem valor diferente dos demais produtos da lista. \n\nDeseja Adicionar?");
                            if (resposta == DialogResult.Yes)
                                retornoResposta = true;
                        }


                        if (valorDiferente && retornoResposta || !valorDiferente)
                        {
                            listaProdutos.Add(produto);

                            CarregarDgvGrupo1(listaProdutos);

                            LimparCamposDgvGrupo1();

                            LimparSelecaoDataGridView(dgvGrupo1);

                            SetarFocusTextBox(txtCodProdGrupo1);
                        }

                    }
                }
            }
            catch (Exception erro)
            {
                Msg.Criticar(erro.Message);
            }
            finally
            {
                if (banco != null) banco.FecharConexao();
            }
        }

        private void btnProcurarGrupo2_Click(object sender, EventArgs e)
        {
            if (!ExisteLojasSelecionadas())
                return;

            SetarFocusTextBox(txtCodProdGrupo2);

            var buscaProdutoPackVirtual = new FrmBuscaProdutoPackVirtual();

            buscaProdutoPackVirtual.SelecionouItem += buscaPackVirtualGrupo2_SelecionouItem;

            buscaProdutoPackVirtual.ShowDialog();
        }

        private void btnAddDiferenciado_Click(object sender, EventArgs e)
        {
            IBanco banco = null;

            if (txtDescProdDiferenciado.Text == "")
            {
                Msg.Criticar("Favor escolher um produto para inserir!");
                SetarFocusTextBox(txtCodProdutoDiferenciado);
                return;
            }
            else if (txtValorDiferenciado.Text == "0,00" || txtValorDiferenciado.Text == txtValorAtual.Text)
            {
                Msg.Criticar("\"Valor 2\" deve ser maior que zero e diferente de \"Valor\"!");
                SetarFocusTextBox(txtValorDiferenciado);
                return;
            }

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

                var listaProdutos = new List<Produto>();
                var produto = new Produto();
                var listaProdutosPack = new List<Pack.PackVirtualPreco2>();
                bool mostraMsg = true;
                int numero;

                if (Int32.TryParse(txtCodProdutoDiferenciado.Text, out numero))
                {
                    if (txtCodProdutoDiferenciado.Text != "0" && !string.IsNullOrEmpty(txtDescProdDiferenciado.Text))
                    {
                        produto = Produto.ConsultarPorCodigo(banco, double.Parse(txtCodProdutoDiferenciado.Text));
                        if (produto == null) return;
                        if (buscaValorAtualLoja(produto.Codigo.ToString(), DefinirLojaValor(RetornarLojasSelecionadasDataGridView(dgvLojasCadastro))) == txtValorDiferenciado.Text)
                        {
                            Msg.Criticar("Selecione o produto novamente!");
                            return;
                        }

                        listaProdutos.Add(produto);

                        CarregarDgvDiferenciado(listaProdutos, null);

                        LimparCamposDgvDiferenciado();

                        LimparSelecaoDataGridView(dgvDiferenciado);

                        SetarFocusTextBox(txtCodProdutoDiferenciado);

                    }
                }
            }
            catch (Exception erro)
            {
                Msg.Criticar(erro.Message);
            }
            finally
            {
                if (banco != null) banco.FecharConexao();
            }
        }

        private void txtCodProdGrupo2_KeyDown(object sender, KeyEventArgs e)
        {
            int numero = 0;

            if (e.KeyCode == Keys.F4)
                btnProcurarGrupo2_Click(null, null);

            else if (e.KeyCode != Keys.Enter)
                return;

            if (!ExisteLojasSelecionadas())
                return;

            else if (!string.IsNullOrEmpty(txtCodProdGrupo2.Text) &&
                     txtCodProdGrupo2.Text != "0" && (Int32.TryParse(txtCodProdGrupo2.Text, out numero) || Texto.TestarNumerico(txtCodProdGrupo2.Text)))
            {
                var produto = ConsultarProduto(txtCodProdGrupo2.Text);

                if (produto.Count > 0)
                {
                    if (!ValidarProdutosAtivosMix(produto[0].Codigo))
                        return;

                    var packVirtualLojas = RetornarPackVirtualLojaAnteriorExistente(txtCodPackCadastro.Text,
                                                                                    produto[0].Codigo,
                                                                                    dtpHoraInicio.Value,
                                                                                    dtpHoraFim.Value, false);

                    if (packVirtualLojas != null)
                    {
                        if (packVirtualLojas.CodPack != 0)
                        {
                            Msg.Criticar("O produto código " + produto[0].Codigo +
                                         " não pode ser inserido, pois já encontra-se vinculado ao Pack código " +
                                         packVirtualLojas.CodPack + ".");
                            SetarFocusTextBox(txtCodProdGrupo2);
                            txtDescProdGrupo2.Text = "";
                            return;
                        }
                    }
                    if (produto[0].CD_ASSOCIADO != null)
                    {

                        var produtoAssociado = RetornarProdutoAssociado(produto[0].CD_ASSOCIADO);

                        if (produtoAssociado.Count > 0)
                        {
                            var reposta =
                                Msg.PerguntarPadraoNao(
                                    "Este produto possui produtos associados, deseja inserí-los também?");

                            if (reposta == DialogResult.Yes)
                            {
                                foreach (var item in produtoAssociado)
                                {
                                    packVirtualLojas = RetornarPackVirtualLojaAnteriorExistente(
                                        txtCodPackCadastro.Text, item.Codigo, dtpHoraInicio.Value, dtpHoraFim.Value,
                                        false);

                                    if (packVirtualLojas == null) continue;
                                    if (packVirtualLojas.CodPack != 0)
                                    {
                                        Msg.Criticar("O produto código " + item.Codigo +
                                                     " não pode ser inserido, pois já encontra-se vinculado ao Pack código " +
                                                     packVirtualLojas.CodPack + ".");
                                        SetarFocusTextBox(txtCodProdGrupo2);
                                        txtDescProdGrupo2.Text = "";
                                        return;
                                    }
                                }

                                CarregarDgvGrupo2(produtoAssociado);

                                LimparCamposDgvGrupo2();

                                LimparSelecaoDataGridView(dgvGrupo2);

                                SetarFocusTextBox(txtCodProdGrupo2);
                            }
                            else
                            {
                                DefinirCamposGrupo(2, produto);

                                btnAddGrupo2_Click(null, new KeyEventArgs(Keys.Enter));
                            }
                        }
                    }
                    else
                    {
                        DefinirCamposGrupo(2, produto);
                    }
                }
                else
                {
                    Msg.Informar("Não foram encontrados registros ou o produto está inativo!");
                    txtDescProdGrupo2.Text = "";

                    SetarFocusTextBox(txtCodProdGrupo2);
                }
            }
        }

        private void txtCodProdutoDiferenciado_KeyDown(object sender, KeyEventArgs e)
        {
            int numero = 0;

            if (e.KeyCode == Keys.F4)
                btnProcurarDiferenciado_Click(null, null);

            else if (e.KeyCode != Keys.Enter)
                return;

            else if (!string.IsNullOrEmpty(txtCodProdutoDiferenciado.Text) &&
                     txtCodProdutoDiferenciado.Text != "0" && (Int32.TryParse(txtCodProdutoDiferenciado.Text, out numero) || Texto.TestarNumerico(txtCodProdutoDiferenciado.Text)))
            {
                var produto = ConsultarProduto(txtCodProdutoDiferenciado.Text);

                if (produto.Count > 0)
                {
                    var packVirtualLojas = RetornarPackVirtualDiferenciadoLojaAnteriorExistente(txtCodPackCadastro.Text,
                                                                                    produto[0].Codigo,
                                                                                    dtpHoraInicio.Value,
                                                                                    dtpHoraFim.Value, false);


                    if (packVirtualLojas != null)
                    {
                        if (packVirtualLojas.CodPack != 0)
                        {
                            Msg.Criticar("O produto código " + produto[0].Codigo +
                                         " não pode ser inserido, pois já encontra-se vinculado ao Pack código " +
                                         packVirtualLojas.CodPack + ".\nVerifique se não há carga de Preço2 pendente pela tela de Preços Alterados.");
                            txtCodProdutoDiferenciado.Text = "";
                            SetarFocusTextBox(txtCodProdutoDiferenciado);
                            return;
                        }
                    }


                    if (produto[0].CD_ASSOCIADO != null)
                    {

                        var produtoAssociado = RetornarProdutoAssociado(produto[0].CD_ASSOCIADO);

                        if (produtoAssociado.Count > 0)
                        {
                            var reposta =
                                Msg.PerguntarPadraoNao(
                                    "Este produto possui produtos associados, deseja inserí-los também?");

                            if (reposta == DialogResult.Yes)
                            {
                                foreach (var item in produtoAssociado)
                                {
                                    packVirtualLojas = RetornarPackVirtualLojaAnteriorExistente(
                                        txtCodPackCadastro.Text, item.Codigo, dtpHoraInicio.Value, dtpHoraFim.Value,
                                        false);

                                    if (packVirtualLojas == null) continue;
                                    if (packVirtualLojas.CodPack != 0)
                                    {
                                        Msg.Criticar("O produto código " + item.Codigo +
                                                     " não pode ser inserido, pois já encontra-se vinculado ao Pack código " +
                                                     packVirtualLojas.CodPack + ".");
                                        SetarFocusTextBox(txtCodProdutoDiferenciado);
                                        txtCodProdutoDiferenciado.Text = "";
                                        return;
                                    }

                                }

                                var inputBox = new FrmInputBox();
                                inputBox.Icon = Icon;
                                var valor = inputBox.Show("Entre com o valor do Preço2:", "Valor 2", "0.00", CaixaTexto.TipoFormato.Moeda).Trim();

                                if (Convert.ToDouble(valor) == 0 || buscaValorAtualLoja(txtCodProdutoDiferenciado.Text, DefinirLojaValor(RetornarLojasSelecionadasDataGridView(dgvLojasCadastro))) == valor)
                                {
                                    Msg.Informar("O \"Valor 2\" não pode ser igual ao \"Valor\" e nem zero.");

                                    txtValorDiferenciado.Text = "0.00";
                                    DefinirCamposGrupo(3, produto);
                                    SetarFocusTextBox(txtValorDiferenciado);
                                    return;
                                }

                                CarregarDgvDiferenciado(produtoAssociado, Convert.ToDouble(valor));

                                LimparCamposDgvDiferenciado();

                                LimparSelecaoDataGridView(dgvDiferenciado);

                                SetarFocusTextBox(txtCodProdutoDiferenciado);
                            }
                            else
                            {
                                DefinirCamposGrupo(3, produto);
                                SetarFocusTextBox(txtValorDiferenciado);
                            }
                        }
                    }
                    else
                    {
                        DefinirCamposGrupo(3, produto);
                        SetarFocusTextBox(txtValorDiferenciado);
                    }
                }
                else
                {
                    Msg.Informar("Não foram encontrados registros ou o produto está inativo!");
                    LimparCamposDgvDiferenciado();

                    SetarFocusTextBox(txtCodProdutoDiferenciado);
                }
            }
        }

        private void btnAddGrupo2_Click(object sender, EventArgs e)
        {
            IBanco banco = null;

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

                var listaProdutos = new List<Produto>();
                var produto = new Produto();
                int numero;

                if (Int32.TryParse(txtCodProdGrupo2.Text, out numero))
                {
                    if (txtCodProdGrupo2.Text != "0" && !string.IsNullOrEmpty(txtDescProdGrupo2.Text))
                    {
                        produto = Produto.ConsultarPorCodigo(banco, double.Parse(txtCodProdGrupo2.Text));
                        if (produto == null) return;

                        listaProdutos.Add(produto);

                        CarregarDgvGrupo2(listaProdutos);

                        LimparCamposDgvGrupo2();

                        LimparSelecaoDataGridView(dgvGrupo1);

                        SetarFocusTextBox(txtCodProdGrupo2);
                    }
                }
            }
            catch (Exception erro)
            {
                Msg.Criticar(erro.Message);
            }
            finally
            {
                if (banco != null) banco.FecharConexao();
            }
        }


        private void HabilitarEdicao(bool valor)
        {
            dtpHoraInicio.Enabled = !_dataBloqueada && valor;
            dtpHoraFim.Enabled = !_dataBloqueada && valor;
            btnSalvar.Enabled = valor;
            btnReplicar.Enabled = valor;
            dgvGrupo1.Enabled = valor;
            dgvGrupo2.Enabled = valor;
            txtDescPack.Enabled = valor;
            dgvLojasCadastro.Enabled = valor;
            cboModeloPack.Enabled = valor;
            ptbQuestao.Enabled = valor;
            txtQtdRegra.Enabled = valor;
            btnAddGrupo1.Enabled = valor;
            btnAddGrupo2.Enabled = valor;
            txtValorRegra.Enabled = valor;
            txtCodProdGrupo1.Enabled = valor;
            btnProcurarGrupo1.Enabled = valor;
            txtCodProdGrupo2.Enabled = valor;
            btnProcurarGrupo2.Enabled = valor;
            dgvGruposClientes.Enabled = valor;
            btnBuscaEncarte.Enabled = valor;
            txtCodEncarte.Enabled = valor;
            ptbAjudaEncarte.Enabled = valor;
            dgvFormasPGTO.Enabled = valor;
            gbxLimitePack.Enabled = valor;
            gbAjustarQuebra.Enabled = valor;
            ptbFormaPagamento.Enabled = valor;
            lblMarcaFormas.Enabled = valor;
            lblMarcaGrupos.Enabled = valor;
            lblMarcaLojas.Enabled = valor;
        }

        private void dgvGrupo1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (cboModeloPack.Text.Equals("Pague x porcento a menos a partir de x unidades (atacado)"))
            {
                switch (e.ColumnIndex)
                {
                    case (int)ColunasDgvGrupoAtacado.Excluir:
                        ExcluirLinhaDataGridView(e.RowIndex, dgvGrupo1, true);
                        break;
                }
            }
            else
            {
                switch (e.ColumnIndex)
                {
                    case (int)ColunasDgvGrupo.Excluir:
                        ExcluirLinhaDataGridView(e.RowIndex, dgvGrupo1, true);
                        break;
                }
            }
        }

        private void dgvGrupo2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case (int)ColunasDgvGrupo.Excluir:
                    ExcluirLinhaDataGridView(e.RowIndex, dgvGrupo2, true);
                    break;
            }
        }


        private void ExcluirLinhaDataGridView(int linha, DataGridView dgv, bool msg)
        {
            if (linha > -1)
            {
                if (msg)
                {
                    if (Msg.Perguntar("Confirma a exclusão do produto " + dgv[1, linha].Value + "?") == DialogResult.No)
                    {
                        LimparSelecaoDataGridView(dgv);
                        return;
                    }
                    if (dgv.CurrentRow != null) dgv.Rows.Remove(dgv.CurrentRow);
                }
                else
                {
                    dgv.Rows.Remove(dgv.Rows[linha]);
                }
            }

            LimparSelecaoDataGridView(dgv);

            HabilitarGridLojas();
        }


        private void ExcluirLinhaDataGridViewCadastro(string nomeDataGrid, int linha, int coluna)
        {
            if (linha != -1 && coluna != -1)
            {
                if (nomeDataGrid.Equals("dgvGrupo1"))
                {
                    txtCodProdGrupo1.Text = dgvGrupo1[linha, (int)ColunasDgvGrupo.Codigo];
                    txtDesProdGrupo1.Text = dgvGrupo1[linha, (int)ColunasDgvGrupo.Descricao];
                    SetarFocusTextBox(txtCodProdGrupo1);

                    dgvGrupo1.Rows.RemoveAt(linha);
                }
                else
                {
                    txtCodProdGrupo2.Text = dgvGrupo2[linha, (int)ColunasDgvGrupo.Codigo];
                    txtDescProdGrupo2.Text = dgvGrupo2[linha, (int)ColunasDgvGrupo.Descricao];
                    SetarFocusTextBox(txtCodProdGrupo2);

                    dgvGrupo2.Rows.RemoveAt(linha);
                }
            }
        }

        /// <summary>
        /// Método que limpa as células selecionadas no DataGridView. Caso não haja células selecionadas, define a cor do DatagridView como padrão.
        /// </summary>
        /// <param name="dgv">(DataGridView) DataGridView que será utilizado para limpara a seleção.</param>
        private void LimparSelecaoDataGridView(DataGridView dgv)
        {
            if (!dgv.SelectedCells.Count.Equals(0))
                dgv.ClearSelection();
            else
            {
                if (dgv.Name.Equals("dgvLojasFiltro") || dgv.Name.Equals("dgvLojasCadastro"))
                {
                    dgv.DefaultCellStyle.SelectionBackColor = dgv.DefaultCellStyle.BackColor;
                    dgv.DefaultCellStyle.SelectionForeColor = dgv.DefaultCellStyle.ForeColor;
                }
            }
        }

        private void txtCodProdPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e != null)
            {
                int numero = 0;

                IBanco banco = null;

                //LimparDataGridView(dgvPackFiltro);

                if (e.KeyCode == Keys.F4)
                    btnProdPesquisa_Click(null, null);

                else if (e.KeyCode != Keys.Enter)
                    return;

                else if ((!string.IsNullOrEmpty(txtCodProdPesquisa.Text) && txtCodProdPesquisa.Text != "0" &&
                          Int32.TryParse(txtCodProdPesquisa.Text, out numero)))
                {

                    var produto = ConsultarProduto(txtCodProdPesquisa.Text);

                    if (produto.Count > 0)
                    {
                        //SetarFocusTextBox(btnAplicarFiltros);
                        btnAplicarFiltros.Focus();
                        txtDescProduto.Text = produto[0].DescricaoReduzida;
                    }
                    else
                    {
                        Msg.Informar("Não foram encontrados registros!");
                        txtDesProdGrupo1.Text = "";
                        SetarFocusTextBox(txtCodProdPesquisa);
                    }
                }
            }
        }

        private void btnProdPesquisa_Click(object sender, EventArgs e)
        {
            SetarFocusTextBox(txtCodProdPesquisa);

            var buscaProdutoPackVirtual = new FrmBuscaProdutoPackVirtual();

            buscaProdutoPackVirtual.SelecionouItem += buscaPackVirtualPesquisa_SelecionouItem;

            buscaProdutoPackVirtual.ShowDialog();
        }

        private void btnReplicar_Click(object sender, EventArgs e)
        {
            if (dgvGrupo1.RowCount > 0)
            {
                var lista = new List<Produto>();

                for (var linha = 0; linha < dgvGrupo1.Rows.Count; linha++)
                {
                    var produto = new Produto();

                    produto.Codigo = double.Parse(dgvGrupo1[linha, (int)ColunasDgvGrupo.Codigo]);
                    produto.DescricaoReduzida = dgvGrupo1[linha, (int)ColunasDgvGrupo.Descricao];
                    produto.ValorVenda = decimal.Parse(dgvGrupo1[linha, (int)ColunasDgvGrupo.Valor]);

                    lista.Add(produto);
                }

                CarregarDgvGrupo2(lista);

                LimparSelecaoDataGridView(dgvGrupo2);
            }
            else
            {
                return;
            }
        }

        private void dgvGrupo1_Leave(object sender, EventArgs e)
        {
            LimparSelecaoDataGridView(dgvGrupo1);
        }

        private void dgvGrupo2_Leave(object sender, EventArgs e)
        {
            LimparSelecaoDataGridView(dgvGrupo2);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            _atualizar = false;
            lblPackEncerrado.Visible = !VALOR;
            HabilitarEdicao(VALOR);
            tabControl1.TabPages.Add(tbCadastro);
            tabControl1.SelectedTab = tbCadastro;
            IBanco banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();
            int codEncarte = 0;
            try
            {
                var encartes = Encarte.ConsultarEncartesAtivos(banco);
                if (encartes.Any())
                    codEncarte = encartes.Min(x => x.Codigo);
            }
            catch (Exception erro)
            {
                Msg.Criticar(erro.Message);
            }
            finally
            {
                if (banco != null) banco.FecharConexao();
            }
            string textoComboGrupos = "";
            dgvGruposClientes.Rows.Cast<DataGridViewRow>().ToList().ForEach(x =>
            {
                dgvGruposClientes.MarcarCelula(x.Index, (int)ColunasDgvGruposClientes.Selecao);
            });
            dgvFormasPGTO.Rows.Cast<DataGridViewRow>().ToList().ForEach(x => dgvFormasPGTO.MarcarCelula(x.Index, (int)ColunasFormas.Selecao));
            LimparCampos(false, Modulo.ConsultarChave(banco, 711).Permissao == Modulo.EnumPermissao.Habilitado);
            txtCodEncarte.Text = codEncarte != 0 && _encarteHabilitado ? codEncarte.ToString() : "1";
            txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
            SetarFocusTextBox(txtDescPack);
        }

        private void DesmarcarColunaCheckBoxDataGridViewLojas(DataGridViewPersonalizado dgv)
        {
            if (dgv.Rows.Count > 0)
            {
                for (var linha = 0; linha < dgv.Rows.Count; linha++)
                {
                    dgv.DesmarcarCelula(linha, (int)ColunasLoja.Selecao);
                }
            }
        }

        private void DesmarcarColunaCheckBoxDataGridViewPacks(DataGridViewPersonalizado dgv)
        {
            if (dgv.Rows.Count > 0)
            {
                for (var linha = 0; linha < dgv.Rows.Count; linha++)
                {
                    dgv.DesmarcarCelula(linha, (int)ColunasPack.Selecao);
                }
            }
        }

        private void LimparDataGridView(DataGridViewPersonalizado dgv)
        {
            dgv.Rows.Clear();
        }

        private void DefinirCodigoPack()
        {
            IBanco banco = null;

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

                txtCodPackCadastro.Text = Pack.PackVirtual.ObterProximoCodigo(banco).ToString();
            }
            finally
            {
                if (banco != null) banco.FecharConexao();
            }
        }

        private void LimparCampos(bool limpaTelaPesquisa, bool moduloKw)
        {

            if (limpaTelaPesquisa == true)
            {
                SelecionarCheckBox(chkEmAndamento, true);
                SelecionarCheckBox(chkProximos, true);
                SelecionarCheckBox(chkEncerrados, false);

                txtCodigo.Text = "0";
                txtCodProdPesquisa.Text = "";
                txtDescProduto.Text = "";

                if (dgvLojasFiltro.Visible)
                {
                    MarcarColunaCheckBoxDataGridViewDgvLojas(dgvLojasFiltro);
                }
            }

            txtQtdRegra.Text = "";

            txtValorRegra.Text = "0,00";

            txtDescPack.Text = "";
            lblTotalRegistros.Text = "";

            LimparCamposDgvGrupo1();

            LimparCamposDgvGrupo2();


            dtpHoraInicio.Value = DateTime.Now;

            dtpHoraFim.Value = DateTime.Now.AddDays(3);

            SugerirDataFinalDatePicker(dtpHoraFim);



            if (dgvLojasCadastro.Visible)
            {
                DesmarcarColunaCheckBoxDataGridViewLojas(dgvLojasCadastro);


            }

            LimparSelecaoDataGridView(dgvLojasFiltro);

            LimparSelecaoDataGridView(dgvLojasCadastro);

            LimparSelecaoDataGridView(dgvPackFiltro);

            LimparSelecaoDataGridView(dgvDiferenciado);

            LimparDataGridView(dgvGrupo1);

            LimparDataGridView(dgvGrupo2);

            LimparDataGridView(dgvDiferenciado);

            LimparCamposDgvDiferenciado();

            cboModeloPack.SelectedIndex = moduloKw ? 8 : 0;

            DefinirCodigoPack();

            ReiniciarVariaveisControle();

            //if (moduloKw) cbockGrupoClientes.Enabled = false;

        }

        private void LimparCamposCadastro()
        {
            txtDescPack.Text = "";

            txtQtdRegra.Text = "";

            txtValorRegra.Text = "0,00";

            LimparCamposDgvGrupo1();

            LimparCamposDgvGrupo2();

            dtpHoraInicio.Value = DateTime.Now;

            dtpHoraFim.Value = DateTime.Now.AddDays(3);

            SugerirDataFinalDatePicker(dtpHoraFim);

            if (dgvLojasCadastro.Visible)
            {
                MarcarColunaCheckBoxDataGridViewDgvLojas(dgvLojasCadastro);
            }

            LimparSelecaoDataGridView(dgvLojasCadastro);

            LimparDataGridView(dgvGrupo1);

            LimparDataGridView(dgvGrupo2);

            cboModeloPack.SelectedIndex = 0;

            DefinirCodigoPack();

            ReiniciarVariaveisControle();

            SetarFocusTextBox(txtDescPack);
        }

        private static DateTime ObterDataHoraServidor()
        {
            DateTime dataHoraServidor;

            IBanco banco = null;

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

                dataHoraServidor = Utilitarios.ObterDataServidor(banco);
            }
            finally
            {
                if (banco != null) banco.FecharConexao();
            }

            return dataHoraServidor;
        }

        private void TestarCampos()
        {
            if (string.IsNullOrEmpty(txtDescPack.Text))
            {
                throw new Exception("Informe uma descrição para o Pack!");
            }

            if (cboModeloPack.Text.Equals("Selecione um modelo"))
            {
                throw new Exception("Selecione um modelo para o Pack Virtual!");
            }

            if (dtpHoraFim.Value < dtpHoraInicio.Value)
            {
                throw new Exception("A data final do Pack Virtual não pode ser menor que a data inicial!");
            }

            if (!cboModeloPack.Text.Equals("Valor Diferenciado (Preço 2)"))
            {
                if (string.IsNullOrEmpty(txtQtdRegra.Text) || txtQtdRegra.Text.Equals("0"))
                {
                    throw new Exception("O valor para a quantidade de produto no Pack está inválido ou nulo.");
                }
            }

            if (cboModeloPack.Text.Equals("Pague 1 centavo ou mais em outro produto") ||
                cboModeloPack.Text.Equals("Pague 1 centavo ou mais no próximo produto"))
            {
                if (string.IsNullOrEmpty(txtValorRegra.Text))
                {
                    throw new Exception("O valor de produto para o Pack está inválido ou nulo.");
                }
            }
            else
            {
                if (!cboModeloPack.Text.Equals("Valor Diferenciado (Preço 2)"))
                {
                    if (string.IsNullOrEmpty(txtValorRegra.Text) || txtValorRegra.Text.Equals("0") ||
                        txtValorRegra.Text.Equals("0,00"))
                    {
                        throw new Exception("O valor de produto para o Pack está inválido ou nulo.");
                    }
                }
            }

            if (!cboModeloPack.Text.Equals("Valor Diferenciado (Preço 2)"))
            {
                if (dgvGrupo1.Rows.Count == 0)
                {
                    throw new Exception("Por favor insira produtos na primeira lista do o Pack Virtual!");
                }
            }
            if (cboModeloPack.Text.Equals("Leve X Pague Y") &&
                Convert.ToDouble(txtQtdRegra.Text) <= Convert.ToDouble(txtValorRegra.Text))
            {
                throw new Exception(
                    "Você não pode criar um Pack Virtual onde a quantidade paga seja maior ou igual a quantidade comprada!");
            }
            if ((cboModeloPack.Text.Equals("Pague 1 centavo ou mais em outro produto") ||
                 cboModeloPack.Text.Equals("Leve x e Receba desconto por unidade") ||
                 cboModeloPack.Text.Equals("Leve x e Receba desconto percentual")) && dgvGrupo2.Rows.Count == 0)
            {
                throw new Exception("Por favor insira produtos na segunda lista do o Pack Virtual!");
            }

            if (dtpHoraFim.Value < ObterDataHoraServidor())
            {
                Msg.Criticar(
                    "O pack virtual não será enviado aos pdvs porque a promoção não está em vigência.\nSe necessário, altere o pack virtual corrigindo a data final.");
            }

            if ((cboModeloPack.Text.Equals("Pague menos a partir de x unidades (atacado)") ||
                 cboModeloPack.Text.Equals("Pague menos por unidade") ||
                 cboModeloPack.Text.Equals("Leve R$ X e Receba desconto por unidade no produto x")) && !ValidaMenorValor())
            {
                throw new Exception("O valor a ser pago não pode ser maior que o menor valor dos produtos da lista!");
            }


            if ((cboModeloPack.Text.Equals("Leve R$ X e Receba desconto percentual no produto x") ||
                cboModeloPack.Text.Equals("Leve R$ X e Receba desconto por unidade no produto x")) && Convert.ToDouble(txtQtdRegra.Text) == 0)
            {
                throw new Exception("O valor a ser comprado deve ser maior que 0!");
            }


            IBanco banco = null;

            banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();
            if (Encarte.ConsultarChave(banco, Convert.ToInt32(txtCodEncarte.Text)) == null)
                throw new Exception("O encarte código " + txtCodEncarte.Text + " não existe!");
            if (banco != null) banco.FecharConexao();
        }

        private bool ValidaMenorValor()
        {
            double menorValor = double.Parse(dgvGrupo1[0, (int)ColunasDgvGrupo.Valor]);

            for (var i = 0; i < dgvGrupo1.Rows.Count; i++)
            {
                if (menorValor > double.Parse(dgvGrupo1[i, (int)ColunasDgvGrupo.Valor]))
                {
                    menorValor = double.Parse(dgvGrupo1[i, (int)ColunasDgvGrupo.Valor]);
                }
            }

            if (double.Parse(txtValorRegra.Text) > menorValor)
                return false;
            else
                return true;
        }

        private bool ValidaMaiorValor()
        {
            double maiorValor = double.Parse(dgvGrupo1[0, (int)ColunasDgvGrupo.Valor]);

            for (var i = 0; i < dgvGrupo1.Rows.Count; i++)
            {
                if (maiorValor < double.Parse(dgvGrupo1[i, (int)ColunasDgvGrupo.Valor]))
                {
                    maiorValor = double.Parse(dgvGrupo1[i, (int)ColunasDgvGrupo.Valor]);
                }
            }

            if (double.Parse(txtQtdRegra.Text) < maiorValor)
                return false;
            else
                return true;
        }

        private void TestarCamposPesquisa()
        {
            if ((txtCodProdPesquisa.Text.Equals("0") || string.IsNullOrEmpty(txtCodProdPesquisa.Text)) &&
                !string.IsNullOrEmpty(txtDescProduto.Text))
            {
                throw new Exception("Selecione um produto!");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (dgvGrupo1.Rows.Count > 0 || dgvDiferenciado.Rows.Count > 0)
            {
                var resposta =
                        Msg.PerguntarPadraoNao("Deseja cancelar esta operação?");
                if (resposta == DialogResult.No)
                    return;
            }
            _permitirTrocaTab = true;
            tabControl1.SelectedTab = tabPesquisa;
            _permitirTrocaTab = false;

            _atualizar = false;

            lblMensagemPrecoDiferente.Visible = false;
            SetarFocusTextBox(txtCodigo);

            CarregarPacks(0);
        }

        private string retornaTipoArredondamento()
        {
            if (optNaoAplicarArredondamento.Checked)
                return "";
            else if (optAjustarBaixo.Checked)
                return "AB";
            else if (optAjustarCima.Checked)
                return "AC";
            else if (optAjustarBaixoCima.Checked)
                return "BC";
            return "";
        }

        private decimal arredondaBC(decimal valor, decimal? ultimaCasa)
        {


            if (ultimaCasa == null || ultimaCasa == -1 || ultimaCasa == 0)
            {
                return Convert.ToDecimal(Math.Round(valor, 1).ToString("0.00"));
            }
            else
            {
                ultimaCasa = ultimaCasa / 100;
                return Convert.ToDecimal(Math.Round(valor, 1) + ultimaCasa);
            }


        }
        private decimal arredondaAB(decimal valor, decimal? ultimaCasa)
        {


            if (ultimaCasa == null || ultimaCasa == -1)
            {
                return Convert.ToDecimal(Math.Round(valor - Convert.ToDecimal(0.049), 1).ToString("0.00"));
            }
            else
            {
                ultimaCasa = ultimaCasa / 100;
                return
                    Convert.ToDecimal(
                        Math.Round(valor - (Convert.ToDecimal(0.049) + Convert.ToDecimal(ultimaCasa)), 1) + ultimaCasa);
            }

        }
        private decimal arredondaAC(decimal valor, decimal? ultimaCasa)
        {


            if (ultimaCasa == null || ultimaCasa == -1)
            {
                return Convert.ToDecimal(Math.Round(valor + Convert.ToDecimal(0.049), 1).ToString("0.00"));
            }
            else
            {
                ultimaCasa = ultimaCasa / 100;
                return
                    Convert.ToDecimal(
                    ((Math.Round((valor + Convert.ToDecimal(0.049) - Convert.ToDecimal(ultimaCasa)), 1)) +
                     ultimaCasa));
            }

        }
        public static string Right(string value, int size)
        {
            // if length is greater than "size" resets "size"(se comprimento é maior que "tamanho" redefine"tamanho")
            size = (value.Length < size ? value.Length : size);
            string newValue = value.Substring(value.Length - size);
            return newValue;
        }



        private decimal arredondaValorAtacado(decimal valor, decimal ultimaCasa)
        {
            valor = Convert.ToDecimal(valor.ToString("0.00"));

            if (retornaTipoArredondamento().Equals("BC"))
            {
                if (!txtUltimaCasa.Text.Equals(""))
                {
                    if (Convert.ToDouble(Right(valor.ToString("0.00"), 1)) > Convert.ToDouble(4))
                    {
                        return arredondaAC(valor, ultimaCasa);
                    }
                    else
                    {
                        return arredondaAB(valor, ultimaCasa);
                    }
                }
                else
                {
                    return arredondaBC(valor, ultimaCasa);
                }
            }
            else if (retornaTipoArredondamento().Equals("AC"))
                return arredondaAC(valor, ultimaCasa);
            else if (retornaTipoArredondamento().Equals("AB"))
                return arredondaAB(valor, ultimaCasa);
            else if (retornaTipoArredondamento().Equals(""))
                return valor;




            return 0;
        }


        private Pack.PackVirtual RetornarPackVirtual()
        {
            Pack.PackVirtual packVirtual = null;
            packVirtual = new Pack.PackVirtual();
            packVirtual.Codigo = double.Parse(txtCodPackCadastro.Text);
            packVirtual.Descricao = txtDescPack.Text;
            if (txtUltimaCasa.Text.Equals(""))
                txtUltimaCasa.Text = "0";
            if (txtUltimaCasa.Text.Trim().Equals(""))
                packVirtual.AjusteUltimaCasaDecimal = null;
            else
                packVirtual.AjusteUltimaCasaDecimal = Convert.ToDecimal(txtUltimaCasa.Text);

            packVirtual.TipoAjusteValor = retornaTipoArredondamento();
            packVirtual.DtFinal = dtpHoraFim.Value;
            packVirtual.DtInicial = dtpHoraInicio.Value;

            if (cboModeloPack.SelectedIndex >= 8 && !_moduloKw)
            {
                packVirtual.ModeloPack = cboModeloPack.SelectedIndex + 1; //Devido ao pack do Tischler visível apenas para eles
            }
            else
            {
                packVirtual.ModeloPack = cboModeloPack.SelectedIndex;
            }

            if (chkLimitarQntPack.Checked)
            {
                packVirtual.QuantidadeLimite = Convert.ToInt32(txtNroLimitePack.Text);
                _alteradoPackVirtual = true;
            }
            else
            {
                packVirtual.QuantidadeLimite = 0;
                _alteradoPackVirtual = true;
            }


            packVirtual.ValidoClienteNaoIdent = (int)dgvGruposClientes.Rows.Cast<DataGridViewRow>().ToList().Find(x => x.Cells[(int)ColunasDgvGruposClientes.Nome].Value.ToString().Equals(Pack.PackVirtualGrupoCliente.NOME_EXIBICAO_NAO_IDENTIFICADO)).Cells[(int)ColunasDgvGruposClientes.Selecao].Value == -1;
            if (cboModeloPack.Text.Equals("Valor Diferenciado (Preço 2)"))
            {
                packVirtual.EnviouPreco2 = false;
                packVirtual.CodEncarte = 1;
                //packVirtual.CodGrupoCliente = null;
            }
            else
            {
                packVirtual.QtdRegra = decimal.Parse(txtQtdRegra.Text);
                packVirtual.VlrRegra = decimal.Parse(txtValorRegra.Text);
                if (txtCodEncarte.Text == "0")
                    packVirtual.CodEncarte = 1;
                else
                    packVirtual.CodEncarte = Convert.ToInt32(txtCodEncarte.Text);
            }
            //if (!cboModeloPack.Text.Equals("Valor Diferenciado (Preço 2)"))
            //packVirtual.CodGrupoCliente = cboGrupoClientes.ObterValor() == -1 ? (int?)null : Convert.ToInt32(cboGrupoClientes.ObterValor());

            return packVirtual;
        }

        private static void FormatarDatePicker(DateTimePicker dtp)
        {
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "dd/MM/yyyy HH:mm";
        }

        private static void SugerirDataFinalDatePicker(DateTimePicker dtp)
        {
            dtp.Value = !string.IsNullOrEmpty(dtp.Value.ToString())
                            ? DateTime.Parse(dtp.Value.ToString("d") + " " + "23:59")
                            : DateTime.Parse(DateTime.Today.ToString("d") + " " + "23:59");
        }

        private static DateTime SugerirDataFinal(DateTime data)
        {
            var dataFinal = "";


            dataFinal = data.ToString("dd/MM/yyyy") + " 23:59";

            return DateTime.Parse(dataFinal);
        }

        /// <summary>
        /// Método que seleciona o TextBox definido por parâmetro
        /// </summary>
        /// <param name="check"> (checkBox) CheckBox a ser selecionado ou Desmarcado.</param>
        /// <param name="valor"> (bool) Valor a ser definido no checkBox (Marcado = true, Desmarcado = False)</param>
        private static void SelecionarCheckBox(CheckBox check, bool valor)
        {
            check.Checked = valor;
        }

        private void DefinirFocoComponenteErroCadastro(Exception ex)
        {
            if (ex.Message.Contains("descrição"))
            {
                SetarFocusTextBox(txtDescPack);
            }
            else if (ex.Message.Contains("quantidade"))
            {
                SetarFocusTextBox(txtQtdRegra);
            }
            else if (ex.Message.Contains("modelo"))
            {
                SetarFocusComboBox(cboModeloPack);
            }
            else if (ex.Message.Contains("primeira lista"))
            {
                SetarFocusTextBox(txtCodProdGrupo1);
            }
            else if (ex.Message.Contains("segunda lista"))
            {
                SetarFocusTextBox(txtCodProdGrupo2);
            }
            else if (ex.Message.Contains("A data inicial do Pack Virtual"))
            {
                SetarFocusDateTimePicker(dtpHoraInicio);
            }
            else if (ex.Message.Contains("A data final do Pack Virtual"))
            {
                SetarFocusDateTimePicker(dtpHoraFim);
            }
            else if (ex.Message.Contains("O valor de produto"))
            {
                SetarFocusTextBox(txtValorRegra);
            }
            else if (ex.Message.Contains("loja"))
            {
                dgvLojasCadastro.Focus();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            bool sucesso;
            double numero = 0;

            if (VerificarProdutoPackVirtualExistentePassado())
                return;

            if (cboModeloPack.Text.Equals("Leve x e Receba desconto percentual") &&
                decimal.Parse(txtValorRegra.Text) == 100)
            {
                var resposta =
                    Msg.PerguntarPadraoNao("Os produtos sairão de graça. \n\nDeseja confirmar?");
                if (resposta == DialogResult.No)
                    return;
            }

            if (cboModeloPack.Text.Equals("Leve x e Receba desconto por unidade") &&
                decimal.Parse(txtValorRegra.Text) == 0 || txtValorRegra.Text == "0,01")
            {
                var resposta =
                    Msg.PerguntarPadraoNao("Os produtos sairão de graça. Deseja confirmar?");
                if (resposta == DialogResult.No)
                    return;
            }


            if (cboModeloPack.Text.Equals("Pague x porcento a menos a partir de x unidades (atacado)"))
            {
                if (double.TryParse(txtValorRegra.Text, out numero))
                {
                    if (Convert.ToDouble(txtValorRegra.Text) < 1 || Convert.ToDouble(txtValorRegra.Text) > 99)
                    {
                        Msg.Criticar("Percentual de desconto deve ser entre 1% e 99%");
                        txtValorRegra.Focus();
                        return;
                    }
                }
            
                if (double.TryParse(txtUltimaCasa.Text, out numero))
                {
                    if (Convert.ToDouble(txtUltimaCasa.Text) < 0 || Convert.ToDouble(txtUltimaCasa.Text) > 9)
                    {
                        Msg.Criticar("Última casa decimal para arredondamento deve ser entre 1 e 9");
                        txtUltimaCasa.Focus();
                        return;
                    }
                }
            }

            if (cboModeloPack.Text.Equals("Pague x porcento a menos por unidade"))
            {
                if (double.TryParse(txtValorRegra.Text, out numero))
                {
                    if (Convert.ToDouble(txtValorRegra.Text) < 1 || Convert.ToDouble(txtValorRegra.Text) > 99)
                    {
                        Msg.Criticar("Percentual de desconto deve ser entre 1% e 99%");
                        txtValorRegra.Focus();
                        return;
                    }
                }
            }



            if (!dgvGruposClientes.Rows.Cast<DataGridViewRow>().ToList().Any(x => (int)x.Cells[(int)ColunasDgvGruposClientes.Selecao].Value == -1) && !cboModeloPack.Text.Equals("Valor Diferenciado (Preço 2)"))
            {
                Msg.Informar("Atribua pelo menos um grupo de clientes antes de salvar!");
                return;
            }
            if (!txtUltimaCasa.Text.Equals(""))
            {
                if (!double.TryParse(txtUltimaCasa.Text, out numero) && txtUltimaCasa.Visible && txtUltimaCasa.Enabled)
                {
                    Msg.Informar("A ultima casa decimal deve ser numérica!");
                    txtUltimaCasa.Focus();
                    return;
                }
            }

            if (!_atualizar)
            {
                sucesso = SalvarPack();

            }
            else
            {
                sucesso = AtualizarPack();
            }

            if (sucesso)
            {
                _permitirTrocaTab = true;
                tabControl1.SelectedTab = tabPesquisa;
                _permitirTrocaTab = false;

                _atualizar = false;

                tabControl1.TabPages.Remove(tbCadastro);
                SetarFocusTextBox(txtCodigo);

                CarregarPacks(0);
            }
        }

        private bool AtualizarPack()
        {
            IBanco banco = null;

            var comandoSQL = new StringBuilder();







            if (!_alteradoPackVirtual && !_alteradoPackVirtualGrupo1 && !_alteradoPackVirtualGrupo2 &&
                !_alteradoPackVirtualDiferenciado && !_alteradoPackVirtualLojas && !_alteradoPackVirtualGruposClientes && !_alteradoPackVirtualFormaPGTO)
            {
                return false;
            }

            try
            {
                TestarCampos();

                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

                var packVirtual = RetornarPackVirtual();

                if (_alteradoPackVirtual)
                {
                    Pack.PackVirtual.Alterar(banco, packVirtual);
                }

                if (_alteradoPackVirtualLojas)
                {
                    if (dgvLojasCadastro.Visible)
                    {
                        comandoSQL.AppendLine("Delete From PackVirtualLojas Where CodPack = " + packVirtual.Codigo.ToString());

                        banco.ExecutarComando(comandoSQL.ToString());

                        var lojas = RetornarLojasSelecionadasDataGridView(dgvLojasCadastro);

                        if (!lojas.Count.Equals(0))
                        {

                            foreach (var loja in lojas)
                            {
                                var packVirtualLojas = new Pack.PackVirtualLojas { CodLoja = loja.Codigo, CodPack = packVirtual.Codigo };
                                Pack.PackVirtualLojas.Inserir(banco, packVirtualLojas);
                            }
                        }
                        else
                        {
                            throw new Exception("Selecione uma loja!");
                        }
                    }
                }

                if (_alteradoPackVirtualGrupo1)
                {
                    Pack.PackVirtual.deletarTabelasPack(banco, "PackVirtualGrupo1", packVirtual.Codigo);
                    Pack.PackVirtual.deletarTabelasPack(banco, "PackVirtualPreco2", packVirtual.Codigo);

                    if (dgvGrupo1.Visible)
                    {
                        for (var i = 0; i < dgvGrupo1.Rows.Count; i++)
                        {
                            var packVirtualGrupo1 = new Pack.PackVirtualGrupo1
                            {
                                CodPack = packVirtual.Codigo,
                                CodProduto =
                                                                double.Parse(dgvGrupo1[i, (int)ColunasDgvGrupo.Codigo])
                            };

                            Pack.PackVirtualGrupo1.Inserir(banco, packVirtualGrupo1);
                        }
                    }
                }

                if (_alteradoPackVirtualGrupo2)
                {
                    var packVirtualGrupo2Teste = Pack.PackVirtualGrupo2.ConsultarChave(banco, packVirtual.Codigo);

                    Pack.PackVirtual.deletarTabelasPack(banco, "PackVirtualPreco2", packVirtual.Codigo);

                    if (packVirtualGrupo2Teste != null)
                    {
                        Pack.PackVirtual.deletarTabelasPack(banco, "PackVirtualGrupo2", packVirtual.Codigo);
                    }

                    if (dgvGrupo2.Visible)
                    {
                        for (var i = 0; i < dgvGrupo2.Rows.Count; i++)
                        {
                            var packVirtualGrupo2 = new Pack.PackVirtualGrupo2
                            {
                                CodPack = packVirtual.Codigo,
                                CodProduto =
                                                                double.Parse(dgvGrupo2[i, (int)ColunasDgvGrupo.Codigo])
                            };

                            Pack.PackVirtualGrupo2.Inserir(banco, packVirtualGrupo2);
                        }
                    }
                }

                if (_alteradoPackVirtualDiferenciado)
                {
                    var packVirtualDiferenciadoTeste = Pack.PackVirtualPreco2.ConsultarChave(banco, packVirtual.Codigo);

                    Pack.PackVirtual.deletarTabelasPack(banco, "PackVirtualGrupo1", packVirtual.Codigo);
                    Pack.PackVirtual.deletarTabelasPack(banco, "PackVirtualGrupo2", packVirtual.Codigo);
                    Pack.PackVirtual.deletarTabelasPack(banco, "PackVirtualPreco2", packVirtual.Codigo);
                    Pack.PackVirtual.ForcarEnvioPreco2(banco, packVirtual.Codigo);

                    if (dgvDiferenciado.Visible)
                    {
                        for (var i = 0; i < dgvDiferenciado.Rows.Count; i++)
                        {
                            var packVirtualPreco2 = new Pack.PackVirtualPreco2
                            {
                                CodPack = packVirtual.Codigo,
                                CodProduto = double.Parse(dgvDiferenciado[i, (int)ColunasDgvDiferenciado.Codigo]),
                                VlrPreco2 = Decimal.Parse(dgvDiferenciado[i, (int)ColunasDgvDiferenciado.Preco2])
                            };

                            Pack.PackVirtualPreco2.Inserir(banco, packVirtualPreco2);

                        }
                    }
                }
                if (_alteradoPackVirtualGruposClientes)
                {
                    var grupos = RetornarGruposClientes(banco, packVirtual.Codigo);
                    Pack.PackVirtualGrupoCliente.ExcluirCodPack(banco, packVirtual.Codigo);
                    grupos.ForEach(x => Pack.PackVirtualGrupoCliente.Inserir(banco, x));
                }

                //ADICIONA FORMA DE PAGAMENTO NO PACK VIRTUAL- O PACK VIRTUAL SÓ IRA EXECUTAR PARA ESTA FORMA DE PAGAMENTO

                if (dgvFormasPGTO.Visible)
                {
                    int contadorFormasMarcadas = 0;
                    if (dgvFormasPGTO.Visible)
                    {


                        for (var i = 0; i < dgvFormasPGTO.Rows.Count; i++)
                        {
                            if (dgvFormasPGTO.TestarCelulaMarcada(i, (int)ColunasFormas.Selecao))
                            {
                                contadorFormasMarcadas++;
                            }
                        }

                    }
                    Pack.PackVirtualFormasPgto.ExcluirFormasPack(banco, packVirtual.Codigo);
                    if (dgvFormasPGTO.Visible && contadorFormasMarcadas > 0 && contadorFormasMarcadas != dgvFormasPGTO.Rows.Count)
                    {


                        for (var i = 0; i < dgvFormasPGTO.Rows.Count; i++)
                        {
                            if (dgvFormasPGTO.TestarCelulaMarcada(i, (int)ColunasFormas.Selecao))
                            {
                                var packVirtualFormas = new Pack.PackVirtualFormasPgto()
                                {
                                    CodPack = packVirtual.Codigo,
                                    CodFormaPgto = int.Parse(dgvFormasPGTO[i, (int)ColunasFormas.Codigo])
                                };
                                Pack.PackVirtualFormasPgto.Inserir(banco, packVirtualFormas);
                            }
                        }
                    }
                }


                Msg.Informar("Pack Virtual alterado com sucesso!");


                btnNovo_Click(null, null);

                _atualizar = false;
                return true;
            }
            catch (Exception ex)
            {
                Msg.Criticar(ex.Message);

                DefinirFocoComponenteErroCadastro(ex);
                return false;
            }

            finally
            {
                if (banco != null) banco.FecharConexao();

            }
        }

        private bool SalvarPack()
        {
            IBanco banco = null;

            try
            {
                TestarCampos();

                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

                var packVirtual = RetornarPackVirtual();

                Pack.PackVirtualLojas packVirtualLojas = null;




                if (dgvLojasCadastro.Visible)
                {
                    var lojas = RetornarLojasSelecionadasDataGridView(dgvLojasCadastro);

                    if (!lojas.Count.Equals(0))
                    {
                        Pack.PackVirtual.Inserir(banco, packVirtual);

                        foreach (var loja in lojas)
                        {
                            packVirtualLojas = new Pack.PackVirtualLojas { CodLoja = loja.Codigo, CodPack = packVirtual.Codigo };

                            Pack.PackVirtualLojas.Inserir(banco, packVirtualLojas);
                        }
                        var grupos = RetornarGruposClientes(banco, packVirtual.Codigo);
                        Pack.PackVirtualGrupoCliente.ExcluirCodPack(banco, packVirtual.Codigo);
                        grupos.ForEach(x => Pack.PackVirtualGrupoCliente.Inserir(banco, x));
                    }
                    else
                    {
                        throw new Exception("Selecione uma loja!");
                    }
                }
                else
                {
                    const int COD_LOJA = 1;

                    Pack.PackVirtual.Inserir(banco, packVirtual);

                    packVirtualLojas = new Pack.PackVirtualLojas { CodLoja = COD_LOJA, CodPack = packVirtual.Codigo };


                    Pack.PackVirtualLojas.Inserir(banco, packVirtualLojas);

                    var grupos = RetornarGruposClientes(banco, packVirtual.Codigo);
                    Pack.PackVirtualGrupoCliente.ExcluirCodPack(banco, packVirtual.Codigo);
                    grupos.ForEach(x => Pack.PackVirtualGrupoCliente.Inserir(banco, x));
                }

                if (dgvGrupo1.Visible)
                {
                    for (var i = 0; i < dgvGrupo1.Rows.Count; i++)
                    {
                        var packVirtualGrupo1 = new Pack.PackVirtualGrupo1
                        {
                            CodPack = packVirtual.Codigo,
                            CodProduto =
                                                            double.Parse(dgvGrupo1[i, (int)ColunasDgvGrupo.Codigo])
                        };

                        Pack.PackVirtualGrupo1.Inserir(banco, packVirtualGrupo1);
                    }
                }
                if (dgvGrupo2.Visible)
                {
                    for (int i = 0; i < dgvGrupo2.Rows.Count; i++)
                    {
                        var packVirtualGrupo2 = new Pack.PackVirtualGrupo2
                        {
                            CodPack = packVirtual.Codigo,
                            CodProduto =
                                                            double.Parse(dgvGrupo2[i, (int)ColunasDgvGrupo.Codigo])
                        };

                        Pack.PackVirtualGrupo2.Inserir(banco, packVirtualGrupo2);
                    }
                }

                if (dgvDiferenciado.Visible)
                {
                    for (int i = 0; i < dgvDiferenciado.Rows.Count; i++)
                    {
                        var packVirtualDiferenciado = new Pack.PackVirtualPreco2
                        {
                            CodPack = packVirtual.Codigo,
                            CodProduto =
                                                            double.Parse(dgvDiferenciado[i, (int)ColunasDgvDiferenciado.Codigo]),
                            VlrPreco2 = Decimal.Parse(dgvDiferenciado[i, (int)ColunasDgvDiferenciado.Preco2])
                        };

                        Pack.PackVirtualPreco2.Inserir(banco, packVirtualDiferenciado);
                    }
                }
                //ADICIONA FORMA DE PAGAMENTO NO PACK VIRTUAL- O PACK VIRTUAL SÓ IRA EXECUTAR PARA ESTA FORMA DE PAGAMENTO
                if (dgvFormasPGTO.Visible)
                {
                    int contadorFormasMarcadas = 0;
                    if (dgvFormasPGTO.Visible)
                    {


                        for (var i = 0; i < dgvFormasPGTO.Rows.Count; i++)
                        {
                            if (dgvFormasPGTO.TestarCelulaMarcada(i, (int)ColunasFormas.Selecao))
                            {
                                contadorFormasMarcadas++;
                            }
                        }

                    }
                    Pack.PackVirtualFormasPgto.ExcluirFormasPack(banco, packVirtual.Codigo);

                    if (dgvFormasPGTO.Visible && contadorFormasMarcadas > 0 && contadorFormasMarcadas != dgvFormasPGTO.Rows.Count)
                    {


                        for (var i = 0; i < dgvFormasPGTO.Rows.Count; i++)
                        {
                            if (dgvFormasPGTO.TestarCelulaMarcada(i, (int)ColunasFormas.Selecao))
                            {
                                var packVirtualFormas = new Pack.PackVirtualFormasPgto()
                                {
                                    CodPack = packVirtual.Codigo,
                                    CodFormaPgto = int.Parse(dgvFormasPGTO[i, (int)ColunasFormas.Codigo])
                                };
                                Pack.PackVirtualFormasPgto.Inserir(banco, packVirtualFormas);
                            }
                        }
                    }
                }


                Msg.Informar("Pack Virtual cadastrado com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Msg.Criticar(ex.Message);

                DefinirFocoComponenteErroCadastro(ex);
                return false;
            }

            finally
            {
                if (banco != null) banco.FecharConexao();

            }
        }

        private void txtCodProdPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtDescProduto.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CarregarPacks(0);
        }


        private void CarregarPacks(int quantidade)
        {
            if (_threadAtualizarLinhaGrupos != null && _threadAtualizarLinhaGrupos.IsAlive)
                _threadAtualizarLinhaGrupos.Abort();

            chkEmAndamento.Invoke(new Action(() =>
                                                 {

                                                     IBanco banco = null;

                                                     dgvPackFiltro.Visible = false;

                                                     var quantidadeRegistros = 0;
                                                     var sql = new StringBuilder();
                                                     var complemetoSQL = new StringBuilder();
                                                     var lojas = "";
                                                     var dataServidor = ObterDataHoraServidor();
                                                     var listaPackVirtualSimplificado =
                                                         new List<Pack.PackVirtualSimplificado>();

                                                     if (!chkEmAndamento.Checked && !chkEncerrados.Checked &&
                                                         !chkProximos.Checked)
                                                     {
                                                         //LimparDataGridView(dgvPackFiltro);

                                                         dgvPackFiltro.DataSource = null;

                                                         lblTotalRegistros.Text = quantidadeRegistros +
                                                                                  " Registro(s) listado(s)";

                                                         return;
                                                     }

                                                     try
                                                     {
                                                         Cursor = Cursors.WaitCursor;

                                                         TestarCamposPesquisa();

                                                         if (dgvLojasFiltro.Visible)
                                                         {
                                                             var listaLojas =
                                                                 RetornarLojasSelecionadasDataGridView(dgvLojasFiltro);

                                                             if (listaLojas.Count.Equals(0))
                                                             {
                                                                 throw new Exception("Selecione uma loja!");
                                                             }

                                                             lojas = listaLojas.Aggregate(lojas,
                                                                                          (current, item) =>
                                                                                          current + (", " + item.Codigo));

                                                             lojas = lojas.Remove(0, 2);
                                                         }


                                                         banco =
                                                             Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.
                                                                 ObterConexao();

                                                         if (quantidade > 0)
                                                         {
                                                             var quantidadeLabel = Texto.ObterApenasNumeros(lblTotalRegistros.Text);

                                                             var quantidadeAuxiliar = quantidade +
                                                                 int.Parse(string.IsNullOrEmpty(quantidadeLabel) ? "0" : quantidadeLabel);

                                                             sql.AppendLine("SELECT TOP " + quantidadeAuxiliar +
                                                                            " PV.Codigo, PV.Descricao , PV.ModeloPack, CASE PV.CodEncarte WHEN 1 THEN PV.DtInicial ELSE E.DataHoraInicial END as DtInicial, CASE PV.CodEncarte WHEN 1 THEN PV.DtFinal ELSE E.DataHoraFinal END as DtFinal, PV.QuantidadeLimite, PV.CodScannTech From PackVirtual PV");
                                                         }
                                                         else
                                                         {
                                                             sql.AppendLine(
                                                                 "SELECT DISTINCT PV.Codigo,  PV.Descricao, PV.ModeloPack, CASE PV.CodEncarte WHEN 1 THEN PV.DtInicial ELSE E.DataHoraInicial END as DtInicial, CASE PV.CodEncarte WHEN 1 THEN PV.DtFinal ELSE E.DataHoraFinal END as DtFinal, PV.QuantidadeLimite, PV.CodScannTech From PackVirtual PV");
                                                         }

                                                         if (!string.IsNullOrEmpty(txtCodigo.Text) &&
                                                             !txtCodigo.Text.Equals("0"))
                                                         {
                                                             complemetoSQL.AppendLine("And PV.Codigo = " +
                                                                                      Texto.ObterApenasNumeros(
                                                                                          txtCodigo.Text));
                                                         }

                                                         if (!string.IsNullOrEmpty(txtCodProdPesquisa.Text) &&
                                                             !txtCodProdPesquisa.Text.Equals("0"))
                                                         {
                                                             sql.AppendLine(
                                                                 " INNER JOIN PackVirtualGrupo1 PackVirtual1 ON (PV.Codigo = PackVirtual1.CodPack)");
                                                             sql.AppendLine(
                                                                 " LEFT OUTER JOIN PackVirtualGrupo2 PackVirtual2 ON (PV.Codigo = PackVirtual2.CodPack)");

                                                             complemetoSQL.AppendLine(
                                                                 " And (PackVirtual1.CodProduto = " +
                                                                 Texto.ObterApenasNumeros(txtCodProdPesquisa.Text));
                                                             complemetoSQL.AppendLine(" Or PackVirtual2.CodProduto = " +
                                                                                      Texto.ObterApenasNumeros(
                                                                                          txtCodProdPesquisa.Text) + ")");
                                                         }

                                                         sql.AppendLine(
                                                             "INNER JOIN ENCARTES E ON PV.CodEncarte = E.Codigo");


                                                         if (chkEmAndamento.Checked)
                                                         {
                                                             complemetoSQL.AppendLine(" And (PV.DtInicial <= " +
                                                                                      banco.ObterDataHora(dataServidor) +
                                                                                      "And PV.DtFinal > " +
                                                                                      banco.ObterDataHora(dataServidor));
                                                         }

                                                         if (chkProximos.Checked)
                                                         {
                                                             if (chkEmAndamento.Checked)
                                                                 complemetoSQL.AppendLine(" Or PV.DtInicial > " +
                                                                                          banco.ObterDataHora(
                                                                                              dataServidor) +
                                                                                          Texto.QuebraLinha);
                                                             else
                                                                 complemetoSQL.AppendLine(" And (PV.DtInicial > " +
                                                                                          banco.ObterDataHora(
                                                                                              dataServidor) +
                                                                                          Texto.QuebraLinha);
                                                         }

                                                         if (chkEncerrados.Checked)
                                                         {
                                                             if (chkEmAndamento.Checked || chkProximos.Checked)
                                                                 complemetoSQL.AppendLine(" Or PV.DtFinal < " +
                                                                                          banco.ObterDataHora(
                                                                                              dataServidor));
                                                             else
                                                                 complemetoSQL.AppendLine(" And (PV.DtFinal < " +
                                                                                          banco.ObterDataHora(
                                                                                              dataServidor));
                                                         }

                                                         complemetoSQL.Append(")");

                                                         if (!string.IsNullOrEmpty(lojas))
                                                         {
                                                             complemetoSQL.AppendLine(" And PVL.CodLoja in (" + lojas +
                                                                                      ")");

                                                             sql.AppendLine(
                                                                 " INNER JOIN PackVirtualLojas PVL ON (PV.Codigo = PVL.CodPack)");

                                                         }

                                                         if (cboFormasPgto.SelectedIndex != 0)
                                                         {


                                                             sql.AppendLine(
                                                                 " INNER JOIN PackVirtualFormasPgto PVFM ON (PV.Codigo = PVFM.CodPack)");


                                                             complemetoSQL.AppendLine(" And PVFM.CodFormaPgto in (" +
                                                                                      cboFormasPgto.SelectedIndex +
                                                                                      ")");
                                                         }


                                                         if (cboModeloPackPesquisa.SelectedIndex >= 8 && !_moduloKw)
                                                             complemetoSQL.AppendLine(" And PV.ModeloPack = " + (cboModeloPackPesquisa.SelectedIndex + 1));
                                                         else if (cboModeloPackPesquisa.SelectedIndex != 0)
                                                             complemetoSQL.AppendLine(" And PV.ModeloPack = " + cboModeloPackPesquisa.SelectedIndex);

                                                         sql.AppendLine(" Where 1 = 1");

                                                         sql.AppendLine(complemetoSQL.ToString());

                                                         if (quantidade > 0)
                                                         {
                                                             sql.AppendLine(
                                                                 "GROUP BY PV.Codigo, PV.Descricao, PV.ModeloPack, PV.DtInicial, PV.DtFinal,PV.CodEncarte ,E.DataHoraInicial,E.DataHoraFinal");
                                                         }

                                                         sql.AppendLine("ORDER BY PV.Codigo Desc");

                                                         var dr = banco.Consultar(sql.ToString());

                                                         while (dr.Read())
                                                         {

                                                             var packVirtualSimplificado =
                                                                 RetornarPackVirtualSimplificado(
                                                                     double.Parse(dr["Codigo"].ToString()),
                                                                     dr["Descricao"].ToString(),
                                                                     int.Parse(dr["ModeloPack"].ToString()),
                                                                     DateTime.Parse(dr["DtInicial"].ToString()),
                                                                     DateTime.Parse(dr["DtFinal"].ToString()),
                                                                     "", int.Parse(dr["QuantidadeLimite"].ToString()), int.Parse(dr["CodScannTech"].ToString())
                                                                     );

                                                             listaPackVirtualSimplificado.Add(packVirtualSimplificado);
                                                         }

                                                         dgvPackFiltro.DataSource = null;

                                                         if (!listaPackVirtualSimplificado.Count.Equals(0))
                                                         {
                                                             dgvPackFiltro.Visible = true;

                                                             dgvPackFiltro.DataSource = listaPackVirtualSimplificado;

                                                             FormatarDataGridViewPackFiltro();

                                                             _threadAtualizarLinhaGrupos = new Thread(AtualizarLinhaGrupos);
                                                             _threadAtualizarLinhaGrupos.Start();
                                                             dgvPackFiltro.Rows[0].Selected = true;

                                                             //CarregarDgvPackFiltro(listaPackVirtual);
                                                         }

                                                         quantidadeRegistros = dgvPackFiltro.Rows.Count;

                                                         lblTotalRegistros.Text = quantidadeRegistros +
                                                                                  " Registro(s) listado(s)";

                                                         btnProrrogar.Enabled = new UsuarioAcesso().PermiteAlterar(169, VariaveisGlobais.CodLoja);
                                                         btnEncerrar.Enabled = new UsuarioAcesso().PermiteAlterar(169, VariaveisGlobais.CodLoja);
                                                         Cursor = Cursors.Default;
                                                         SetarFocusTextBox(txtCodigo);
                                                         LimparSelecaoDataGridView(dgvPackFiltro);
                                                     }
                                                     catch (Exception ex)
                                                     {
                                                         Msg.Criticar(ex.Message);

                                                         if (ex.Message.Contains("produto"))
                                                         {
                                                             SetarFocusTextBox(txtCodProdPesquisa);
                                                         }
                                                         if (ex.Message.Contains("loja"))
                                                         {
                                                             dgvLojasFiltro.Focus();
                                                         }
                                                     }
                                                     finally
                                                     {
                                                         if (banco != null) banco.FecharConexao();

                                                         Cursor = Cursors.Default;
                                                     }
                                                 }));
        }

        private void AtualizarLinhaGrupos()
        {
            IBanco banco = null;
            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

                for (var i = 0; i < dgvPackFiltro.Rows.Count; i++)
                {
                    var grupos = Pack.PackVirtualGrupoCliente.ConsultaNomeGruposClientesPack(
                        banco
                        , double.Parse(dgvPackFiltro[i, (int)ColunasPack.Codigo])
                        );

                    dgvPackFiltro[i, (int)ColunasPack.GrupoCliente] = grupos;
                }
            }
            catch (ThreadAbortException e)
            {
                Console.WriteLine("A thread de AtualizarLinhaGrupos foi abortada.");
            }
            catch (Exception e)
            {
                Msg.Criticar(e.Message);
            }
            finally
            {
                if (banco != null)
                    banco.FecharConexao();
            }

        }

        private void FormatarDataGridViewPackFiltro()
        {
            DefinirFormatacaoPadraoDataGridView(dgvPackFiltro);

            dgvPackFiltro.Columns[(int)ColunasPack.Codigo].HeaderText = "Código";
            dgvPackFiltro.Columns[(int)ColunasPack.Descricao].HeaderText = "Descrição";
            dgvPackFiltro.Columns[(int)ColunasPack.ModeloPack].HeaderText = "Modelo";
            dgvPackFiltro.Columns[(int)ColunasPack.DtInicial].HeaderText = "Data Inicial";
            dgvPackFiltro.Columns[(int)ColunasPack.DtFinal].HeaderText = "Data Final";
            dgvPackFiltro.Columns[(int)ColunasPack.QuantidadeLimite].HeaderText = "Limite por Venda";
            dgvPackFiltro.Columns[(int)ColunasPack.GrupoCliente].HeaderText = "Grupo de Clientes";

            dgvPackFiltro.DefinirTipoColuna((int)ColunasPack.Codigo, ItemComboBusca.TipoDados.NaoEditavel);
            dgvPackFiltro.DefinirTipoColuna((int)ColunasPack.Descricao, ItemComboBusca.TipoDados.NaoEditavel);
            dgvPackFiltro.DefinirTipoColuna((int)ColunasPack.ModeloPack, ItemComboBusca.TipoDados.NaoEditavel);
            dgvPackFiltro.DefinirTipoColuna((int)ColunasPack.DtInicial, ItemComboBusca.TipoDados.NaoEditavel);
            dgvPackFiltro.DefinirTipoColuna((int)ColunasPack.DtFinal, ItemComboBusca.TipoDados.NaoEditavel);
            dgvPackFiltro.DefinirTipoColuna((int)ColunasPack.QuantidadeLimite, ItemComboBusca.TipoDados.NaoEditavel);
            dgvPackFiltro.DefinirTipoColuna((int)ColunasPack.GrupoCliente, ItemComboBusca.TipoDados.NaoEditavel);

            dgvPackFiltro.Columns[(int)ColunasPack.Selecao].Width = 20;
            dgvPackFiltro.Columns[(int)ColunasPack.Codigo].Width = 60;
            dgvPackFiltro.Columns[(int)ColunasPack.Descricao].Width = 258;
            dgvPackFiltro.Columns[(int)ColunasPack.ModeloPack].Width = 230;
            dgvPackFiltro.Columns[(int)ColunasPack.DtInicial].Width = 130;
            dgvPackFiltro.Columns[(int)ColunasPack.DtFinal].Width = 130;
            dgvPackFiltro.Columns[(int)ColunasPack.QuantidadeLimite].Width = 140;
            dgvPackFiltro.Columns[(int)ColunasPack.GrupoCliente].Width = 140;
            dgvPackFiltro.FormatarColuna((int)ColunasPack.CodScannTech, "Cód. ScannTech", 0, ItemComboBusca.TipoDados.NaoEditavel);
        }

        private void DefinirFormatacaoPadraoDataGridView(DataGridView dgv)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            //Application.DoEvents();

            //if (tabControl1.SelectedTab.Equals(tbCadastro))
            //{
            //    dgvPackFiltro.Visible = false;
            //    cboModeloPack.SelectedIndex = 0;
            //    LimparCamposCadastro();
            //    _atualizar = false;
            //}

            //else
            //{
            //    SetarFocusTextBox(txtCodigo);
            //    CarregarPacks(0);
            //}
        }

        private void chkProximos_CheckedChanged(object sender, EventArgs e)
        {
            CarregarPacks(0);
        }

        private void lblProximosRegistros_Click(object sender, EventArgs e)
        {
            CarregarPacks(500);
            lblProximosRegistros.ForeColor = Color.DarkRed;
        }

        private void btnProrrogar_Click(object sender, EventArgs e)
        {
            var listaPackVirtual = RetornarPackVirtualSelecionadoDataGridView(dgvPackFiltro);
            List<Pack.PackVirtualGrupo1> listaPackVirtualGrupo1 = new List<Pack.PackVirtualGrupo1>();
            List<Pack.PackVirtualGrupo2> listaPackVirtualGrupo2 = new List<Pack.PackVirtualGrupo2>();

            var temPackScanntech = listaPackVirtual.Exists(x => x.CodScannTech != 0);
            if (temPackScanntech)
            {
                Msg.Informar("Não é possível prorrogar um pack virtual recebido do Clube de Promoções!");
                return;
            }

            if (!listaPackVirtual.Count.Equals(0))
            {
                if (listaPackVirtual.Count > 1)
                {
                    Msg.Criticar("Para prorrogar um Pack Virtual, selecione um de cada vez!");
                    return;
                }
                var form = new FrmInputBox();
                var valor =
                    form.Show("Por mais quantos dias esta promoção será prorrogada? \na (partir da data de hoje)",
                              " Atenção", "0", CaixaTexto.TipoFormato.Inteiro);

                if (valor.Length > 3)
                {
                    Msg.Criticar("Valor muito extenso!\nPor favor, informe uma quantidade menor.");
                    valor = "0";
                    btnProrrogar_Click(null, null);
                }

                var codigoPack = "";

                var sql = new StringBuilder();

                var packsErro = new StringBuilder();
                var packs = new StringBuilder();

                if (!valor.Equals("0"))
                {
                    var quantidadeDias = 0.0;
                    if (double.TryParse(valor, out quantidadeDias))
                    {
                        var banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();
                        quantidadeDias = double.Parse(valor);

                        var dataAtual = DateTime.Now;

                        codigoPack = listaPackVirtual.Aggregate(codigoPack,
                                                                (current, item) => current + (", " + item.Codigo));

                        codigoPack = codigoPack.Remove(0, 2);

                        sql.AppendLine("Select Codigo, DtFinal from PackVirtual Where Codigo in (" + codigoPack + ")");

                        var dr = banco.Consultar(sql.ToString());

                        listaPackVirtual.Clear();

                        while (dr.Read())
                        {
                            var packVirtual = Pack.PackVirtual.ConsultarChave(banco, int.Parse(dr["Codigo"].ToString()));

                            if (packVirtual.CodEncarte != 1)
                            {
                                Msg.Criticar("Não é possível prorrogar esse Pack Virtual, pois ele está vinculado à um encarte!");
                                return;
                            }

                            if (DateTime.Parse(dr["DtFinal"].ToString()) < DateTime.Now)
                            {
                                packVirtual.DtFinal = DateTime.Now;

                            }
                            else
                            {
                                packVirtual.DtFinal = DateTime.Parse(dr["DtFinal"].ToString());
                            }
                            packVirtual.DtFinal = packVirtual.DtFinal.AddDays(quantidadeDias);
                            listaPackVirtual.Add(packVirtual);
                        }

                        dr.Close();

                        sql = new StringBuilder();

                        foreach (var item in listaPackVirtual)
                        {

                            listaPackVirtualGrupo1 =
                                Pack.PackVirtualGrupo1.ConsultarPackVirtualGrupo1PorCodigoPack(banco, item.Codigo);

                            Pack.PackVirtualLojas packVirtualExistente = null;

                            bool removido = false;

                            foreach (var packVirtualGrupo1 in listaPackVirtualGrupo1)
                            {
                                packVirtualExistente =
                                    RetornarPackVirtualLojaAnteriorExistente(packVirtualGrupo1.CodPack.ToString(),
                                                                             packVirtualGrupo1.CodProduto, dataAtual,
                                                                             item.DtFinal, true);
                                if (packVirtualExistente != null)
                                {
                                    if (packVirtualExistente.CodPack != 0)
                                    {
                                        packsErro.AppendLine("Não foi possível prorrogar o pack código " + item.Codigo +
                                                         " pois o produto código: " + packVirtualGrupo1.CodProduto +
                                                         " já está vinculado ao pack código " + packVirtualExistente.CodPack +
                                                         ".\nA data inicial do pack código " + packVirtualExistente.CodPack +
                                                         " é igual à data final prorrogada do pack código " + item.Codigo +
                                                         ".");

                                        if (listaPackVirtual.Count == 1)
                                        {
                                            Msg.Criticar(packsErro.ToString());
                                            return;
                                        }

                                        removido = true;

                                        break;
                                    }
                                }
                            }

                            if (!removido)
                            {
                                listaPackVirtualGrupo2 =
                                    Pack.PackVirtualGrupo2.ConsultarPackVirtualGrupo2PorCodigoPack(banco, item.Codigo);

                                if (listaPackVirtualGrupo2.Count > 0)
                                {
                                    foreach (var packVirtualGrupo2 in listaPackVirtualGrupo2)
                                    {
                                        packVirtualExistente =
                                            RetornarPackVirtualLojaAnteriorExistente(
                                                packVirtualGrupo2.CodPack.ToString(),
                                                packVirtualGrupo2.CodProduto, dataAtual,
                                                item.DtFinal, true);
                                        if (packVirtualExistente != null)
                                        {
                                            if (packVirtualExistente.CodPack != 0)
                                            {
                                                packsErro.AppendLine("Não foi possível prorrogar o pack código " + item.Codigo +
                                                                 " pois o produto código: " + packVirtualGrupo2.CodProduto +
                                                                 " já está vinculado ao pack código " +
                                                                 packVirtualExistente.CodPack +
                                                                 ".\nA data inicial do pack código " +
                                                                 packVirtualExistente.CodPack +
                                                                 " é igual à data final prorrogada do pack código " +
                                                                 item.Codigo + ".");

                                                if (listaPackVirtual.Count == 1)
                                                {
                                                    Msg.Criticar(packsErro.ToString());
                                                    return;
                                                }

                                                removido = true;

                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            if (!removido)
                            {
                                sql.AppendLine("Update PackVirtual set DtInicial = " + banco.ObterDataHora(dataAtual) +
                                               ", DtFinal = " + banco.ObterDataHora(item.DtFinal) +
                                               " Where Codigo = " +
                                               item.Codigo);
                                Pack.PackVirtual.GerarInsertComandosPDV(banco, item, listaPackVirtualGrupo1, listaPackVirtualGrupo2);
                                packs.AppendLine("Pack Virtual nº: " + item.Codigo + " foi prorrogado com sucesso!");
                                banco.ExecutarComando(sql.ToString());
                            }
                        }



                        if (!string.IsNullOrEmpty(packsErro.ToString()))
                        {
                            Msg.Criticar(packsErro.ToString());
                        }

                        if (!string.IsNullOrEmpty(packs.ToString()))
                        {
                            Msg.Informar(packs.ToString());
                        }

                        //Msg.Informar("Pack Virtual prorrogado com sucesso!");

                        LimparSelecaoDataGridView(dgvPackFiltro);

                        DesmarcarColunaCheckBoxDataGridViewPacks(dgvPackFiltro);

                        CarregarPacks(0);
                    }
                    else
                    {
                        Msg.Criticar("Selecione um Pack!");
                    }
                }
            }
            else
            {
                Msg.Criticar("Selecione um Pack!");
            }
        }

        private void ptbQuestao_Click(object sender, EventArgs e)
        {
            switch (cboModeloPack.Text)
            {
                case "Selecione um modelo":
                    Msg.Informar("Primeiro selecione um modelo de Pack, após clique aqui e veja um exemplo do uso!");
                    break;

                case "Leve X Pague Y":
                    Msg.Informar(
                        "Exemplo de promoção:\nCompre 12 unidades do refrigerante XXX e pague apenas 10 unidades.");
                    break;

                case "Pague 1 centavo ou mais em outro produto":
                    Msg.Informar(
                        "Exemplo de promoção:\nCompre 2 unidades da Pizza XXX e pague 1 centavo ou mais em um bombom da marca YYY.\n" +
                        "Observação: O valor de 1 centavo pode ser trocado por outro valor.");
                    break;

                case "Pague menos a partir de x unidades (atacado)":
                    Msg.Informar(
                        "Exemplo de promoção:\nCompre mais de 100 latas de Refri Lata da marca XXX e pague 1 real em cada unidade.");
                    break;
                case "Pague x porcento a menos a partir de x unidades (atacado)":
                    Msg.Informar(
                        "Exemplo de promoção:\nCompre mais de 100 unidades de Refri Lata da marca XXX e receba 10% de desconto em cada unidade.");
                    break;

                case "Pague menos por unidade":
                    Msg.Informar(
                        "Exemplo de promoção:\nCompre 12 caixas de leite da marca XXX e pague R$ 1,00 cada caixa.\n" +
                        "Observação: A 13ª caixa de leite custará o preço original.");
                    break;

                case "Pague 1 centavo ou mais no próximo produto":
                    Msg.Informar(
                        "Exemplo de promoção:\nCompre 5 biscoitos da marca XXX e pague 1 centavo ou mais no próximo.");
                    break;

                case "Leve x e Receba desconto percentual":
                    Msg.Informar(
                        "Exemplo de promoção:\nCompre 2 unidades da Pizza XXX e ganhe 10% de desconto na compra de um refrigerante da marca HHH.\n" +
                        "\n" +
                        "Exemplo para dar desconto no mesmo produto.\n" +
                        "Exemplo de promoção: Compre o produto XXX e receba 10% de desconto\n" +
                        "Passos: \n" +
                        "1)Insira 0,1 na quantidade e informe o desconto percentual." +
                        "\n2)Insira o mesmo produto nas duas grades.");
                    break;

                case "Leve x e Receba desconto por unidade":
                    Msg.Informar(
                        "Exemplo de promoção:\nCompre 5 unidades da Pizza XXX e ganhe R$ 0,10 de desconto em cada.\n" +
                        "Observação: A 6ª unidade  não entra na promoção, apenas quando fechar 10 unidades.");
                    break;

                case "Valor Diferenciado (Preço 2)":
                    Msg.Informar(
                        "Opção para enviar um preço diferenciado de produto para a KW.\n" +
                        "Após o cadastro será necessário o envio do mercador ou imediata.");
                    break;

                case "Leve R$ X e Receba desconto por unidade no produto x":
                    Msg.Informar(
                        "Exemplo de promoção:\nCompre R$ 50,00 em produtos de fora deste pack e ganhe R$ 2,00 de desconto no produto x.\n");
                    break;

                case "Leve R$ X e Receba desconto percentual no produto x":
                    Msg.Informar(
                        "Exemplo de promoção:\nCompre R$ 50,00 em produtos de fora deste pack e ganhe 10 % de desconto no produto x.\n");
                    break;
            }
        }

        private void SelecionarDvgCadastroLojas(IBanco banco, double codPack)
        {
            if (banco == null)
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();
            }

            var lojas = Pack.PackVirtualLojas.ObterLojasCodigoPack(banco, codPack);

            MarcarColunaCheckBoxDataGridViewDgvLojasCadastro(lojas);
        }


        public void carregaPack(double codPack)
        {
            if (codPack > 0)
            {
                if (!new UsuarioAcesso().PermiteAlterar(169, VariaveisGlobais.CodLoja))
                    return;

                _atualizar = true;

                LimparDataGridView(dgvGrupo1);
                LimparDataGridView(dgvGrupo2);
                LimparDataGridView(dgvDiferenciado);
                dgvPackFiltro.Visible = false;

                var banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();
                tabControl1.TabPages.Add(tbCadastro);
                tabControl1.SelectedTab = tbCadastro;

                var codigoPack = codPack;

                var packVirtual = Pack.PackVirtual.ConsultarChave(banco, codigoPack);

                var listaProduto = new List<Produto>();
                if (packVirtual.ModeloPack >= 8 && _moduloKw)
                    cboModeloPack.SelectedIndex = packVirtual.ModeloPack + 1;
                else
                    cboModeloPack.SelectedIndex = packVirtual.ModeloPack;

                var gruposClientes = Pack.PackVirtualGrupoCliente.ConsultarCodPack(banco, codigoPack);
                List<GrupoCliente> grupos = new List<GrupoCliente>();
                gruposClientes.ForEach(x => grupos.Add(GrupoCliente.ConsultarChave(banco, x.CodGrupoCliente)));
                dgvGruposClientes.Rows.Cast<DataGridViewRow>().ToList().ForEach(x =>
                {
                    if (grupos.Exists(y => y.NOME.Equals(x.Cells[(int)ColunasDgvGruposClientes.Nome].Value.ToString())))
                        dgvGruposClientes.MarcarCelula(x.Index, (int)ColunasDgvGruposClientes.Selecao);
                    else
                        dgvGruposClientes.DesmarcarCelula(x.Index, (int)ColunasDgvGruposClientes.Selecao);
                });
                if (packVirtual.ValidoClienteNaoIdent)
                {
                    var linha = dgvGruposClientes.Rows.Cast<DataGridViewRow>()
                        .ToList()
                        .Find(
                            x =>
                                x.Cells[(int)ColunasDgvGruposClientes.Nome].Value.ToString()
                                    .Equals(Pack.PackVirtualGrupoCliente.NOME_EXIBICAO_NAO_IDENTIFICADO));
                    dgvGruposClientes.MarcarCelula(linha.Index, (int)ColunasDgvGruposClientes.Selecao);
                }

                List<Pack.PackVirtualFormasPgto> formasPgto = Pack.PackVirtualFormasPgto.ConsultarFormas(banco, codigoPack);

                bool marcouUm = false;
                dgvFormasPGTO.Rows.Cast<DataGridViewRow>().ToList().ForEach(x =>
                {
                    if (
                        formasPgto.Exists(
                            z => z.CodFormaPgto.ToString().Equals(x.Cells[(int)ColunasFormas.Codigo].Value)))
                    {
                        dgvFormasPGTO.MarcarCelula(x.Index, (int)ColunasFormas.Selecao);
                        marcouUm = true;
                    }
                    else
                        dgvFormasPGTO.DesmarcarCelula(x.Index, (int)ColunasFormas.Selecao);
                });
                if (!marcouUm) dgvFormasPGTO.Rows.Cast<DataGridViewRow>().ToList().ForEach(x => dgvFormasPGTO.MarcarCelula(x.Index, (int)ColunasFormas.Selecao));

                txtValorRegra.Text = packVirtual.VlrRegra.ToString(packVirtual.ModeloPack == 1 ? "" : "0.00");
                txtQtdRegra.Text = packVirtual.QtdRegra.ToString(packVirtual.ModeloPack == 10 || packVirtual.ModeloPack == 11 ? "0.00" : "");
                txtCodPackCadastro.Text = packVirtual.Codigo.ToString();
                txtDescPack.Text = packVirtual.Descricao.ToString();
                dtpHoraInicio.Value = DateTime.Parse(packVirtual.DtInicial.ToString());
                dtpHoraFim.Value = DateTime.Parse(packVirtual.DtFinal.ToString());
                txtCodEncarte.Text = packVirtual.CodEncarte.ToString();

                if (packVirtual.TipoAjusteValor.Equals(""))
                    optNaoAplicarArredondamento.Checked = true;
                else if (packVirtual.TipoAjusteValor.Equals("BC"))
                    optAjustarBaixoCima.Checked = true;
                else if (packVirtual.TipoAjusteValor.Equals("AB"))
                    optAjustarBaixo.Checked = true;
                else if (packVirtual.TipoAjusteValor.Equals("AC"))
                    optAjustarCima.Checked = true;
                if (txtUltimaCasa.Text.Equals(""))
                    txtUltimaCasa.Text = "0";
                txtUltimaCasa.Text = packVirtual.AjusteUltimaCasaDecimal.ToString();


                if (_encarteHabilitado)
                {
                    txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    var encarte = Encarte.ConsultarChave(banco, packVirtual.CodEncarte);
                    if (encarte.Codigo == 1)
                    {
                        dtpHoraInicio.Enabled = true;
                        dtpHoraFim.Enabled = true;
                        _dataBloqueada = false;
                    }
                    else
                    {
                        dtpHoraInicio.Value = encarte.DataHoraInicial;
                        dtpHoraFim.Value = encarte.DataHoraFinal;
                        dtpHoraInicio.Enabled = false;
                        dtpHoraFim.Enabled = false;
                        _dataBloqueada = true;
                    }
                }

                SelecionarDvgCadastroLojas(banco, codigoPack);

                if (dtpHoraFim.Value < banco.ObterDataServidor())
                {
                    lblPackEncerrado.Visible = VALOR;
                    HabilitarEdicao(!VALOR);
                }
                else
                {
                    lblPackEncerrado.Visible = !VALOR;
                    HabilitarEdicao(VALOR);
                }


                var packVirtualGrupo1 = Pack.PackVirtualGrupo1.ConsultarPackVirtualGrupo1PorCodigoPack(banco, codigoPack);

                Produto produto;

                foreach (var item in packVirtualGrupo1)
                {
                    produto = Produto.ConsultarPorCodigo(banco, item.CodProduto);

                    listaProduto.Add(produto);
                }

                CarregarDgvGrupo1(listaProduto);

                listaProduto.Clear();

                var packVirtualGrupo2 = Pack.PackVirtualGrupo2.ConsultarPackVirtualGrupo2PorCodigoPack(banco, codigoPack);
                if (packVirtualGrupo2.Count > 0)
                {
                    foreach (var item in packVirtualGrupo2)
                    {
                        produto = Produto.ConsultarPorCodigo(banco, item.CodProduto);

                        listaProduto.Add(produto);
                    }

                    CarregarDgvGrupo2(listaProduto);
                }

                var packVirtualDiferenciado = Pack.PackVirtualPreco2.ConsultarPackVirtualDiferenciadoPorCodigoPack(banco, codigoPack);
                if (packVirtualDiferenciado.Count > 0)
                {
                    foreach (var item in packVirtualDiferenciado)
                    {
                        produto = Produto.ConsultarPorCodigo(banco, item.CodProduto);

                        listaProduto.Add(produto);
                    }
                    ExibirObjetosGruposClientes(false);
                    CarregarDgvDiferenciado(listaProduto, null);
                    SetarFocusTextBox(txtCodProdutoDiferenciado);
                }
                ReiniciarVariaveisControle();

            }
        }


        private void dgvPackFiltro_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (!new UsuarioAcesso().PermiteAlterar(169, VariaveisGlobais.CodLoja))
                    return;

                _atualizar = true;

                LimparDataGridView(dgvGrupo1);
                LimparDataGridView(dgvGrupo2);
                LimparDataGridView(dgvDiferenciado);
                dgvPackFiltro.Visible = false;

                var banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();
                tabControl1.TabPages.Add(tbCadastro);
                tabControl1.SelectedTab = tbCadastro;

                var codigoPack = double.Parse(dgvPackFiltro[e.RowIndex, (int)ColunasPack.Codigo]);

                var packVirtual = Pack.PackVirtual.ConsultarChave(banco, codigoPack);

                var listaProduto = new List<Produto>();
                if (packVirtual.ModeloPack >= 8 && !_moduloKw)
                    cboModeloPack.SelectedIndex = packVirtual.ModeloPack - 1; //Devido ao pack do Tischler visível apenas para eles
                else
                    cboModeloPack.SelectedIndex = packVirtual.ModeloPack;

                var gruposClientes = Pack.PackVirtualGrupoCliente.ConsultarCodPack(banco, codigoPack);
                List<GrupoCliente> grupos = new List<GrupoCliente>();
                gruposClientes.ForEach(x => grupos.Add(GrupoCliente.ConsultarChave(banco, x.CodGrupoCliente)));
                dgvGruposClientes.Rows.Cast<DataGridViewRow>().ToList().ForEach(x =>
                {
                    if (grupos.Exists(y => y.NOME.Equals(x.Cells[(int)ColunasDgvGruposClientes.Nome].Value.ToString())))
                        dgvGruposClientes.MarcarCelula(x.Index, (int)ColunasDgvGruposClientes.Selecao);
                    else
                        dgvGruposClientes.DesmarcarCelula(x.Index, (int)ColunasDgvGruposClientes.Selecao);
                });
                if (packVirtual.ValidoClienteNaoIdent)
                {
                    var linha = dgvGruposClientes.Rows.Cast<DataGridViewRow>()
                        .ToList()
                        .Find(
                            x =>
                                x.Cells[(int)ColunasDgvGruposClientes.Nome].Value.ToString()
                                    .Equals(Pack.PackVirtualGrupoCliente.NOME_EXIBICAO_NAO_IDENTIFICADO));
                    dgvGruposClientes.MarcarCelula(linha.Index, (int)ColunasDgvGruposClientes.Selecao);
                }

                List<Pack.PackVirtualFormasPgto> formasPgto = Pack.PackVirtualFormasPgto.ConsultarFormas(banco, codigoPack);

                bool marcouUm = false;
                dgvFormasPGTO.Rows.Cast<DataGridViewRow>().ToList().ForEach(x =>
                {
                    if (
                        formasPgto.Exists(
                            z => z.CodFormaPgto.ToString().Equals(x.Cells[(int)ColunasFormas.Codigo].Value)))
                    {
                        dgvFormasPGTO.MarcarCelula(x.Index, (int)ColunasFormas.Selecao);
                        marcouUm = true;
                    }
                    else
                        dgvFormasPGTO.DesmarcarCelula(x.Index, (int)ColunasFormas.Selecao);
                });
                if (!marcouUm) dgvFormasPGTO.Rows.Cast<DataGridViewRow>().ToList().ForEach(x => dgvFormasPGTO.MarcarCelula(x.Index, (int)ColunasFormas.Selecao));

                txtValorRegra.Text = packVirtual.VlrRegra.ToString(packVirtual.ModeloPack == 1 ? "" : "0.00");
                txtQtdRegra.Text = packVirtual.QtdRegra.ToString(packVirtual.ModeloPack == 10 || packVirtual.ModeloPack == 11 ? "0.00" : "");
                txtCodPackCadastro.Text = dgvPackFiltro[e.RowIndex, (int)ColunasPack.Codigo];
                txtDescPack.Text = dgvPackFiltro[e.RowIndex, (int)ColunasPack.Descricao];
                dtpHoraInicio.Value = DateTime.Parse(dgvPackFiltro[e.RowIndex, (int)ColunasPack.DtInicial]);
                dtpHoraFim.Value = DateTime.Parse(dgvPackFiltro[e.RowIndex, (int)ColunasPack.DtFinal]);
                txtCodEncarte.Text = packVirtual.CodEncarte.ToString();

                if (packVirtual.TipoAjusteValor.Equals(""))
                    optNaoAplicarArredondamento.Checked = true;
                else if (packVirtual.TipoAjusteValor.Equals("BC"))
                    optAjustarBaixoCima.Checked = true;
                else if (packVirtual.TipoAjusteValor.Equals("AB"))
                    optAjustarBaixo.Checked = true;
                else if (packVirtual.TipoAjusteValor.Equals("AC"))
                    optAjustarCima.Checked = true;
                if (txtUltimaCasa.Text.Equals(""))
                    txtUltimaCasa.Text = "0";
                txtUltimaCasa.Text = packVirtual.AjusteUltimaCasaDecimal.ToString();

                if (packVirtual.QuantidadeLimite > 0)
                {
                    txtNroLimitePack.Text = packVirtual.QuantidadeLimite.ToString();
                    chkLimitarQntPack.Checked = true;
                }



                if (_encarteHabilitado)
                {
                    txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    var encarte = Encarte.ConsultarChave(banco, packVirtual.CodEncarte);
                    if (encarte.Codigo == 1)
                    {
                        dtpHoraInicio.Enabled = true;
                        dtpHoraFim.Enabled = true;
                        _dataBloqueada = false;
                    }
                    else
                    {
                        dtpHoraInicio.Value = encarte.DataHoraInicial;
                        dtpHoraFim.Value = encarte.DataHoraFinal;
                        dtpHoraInicio.Enabled = false;
                        dtpHoraFim.Enabled = false;
                        _dataBloqueada = true;
                    }
                }

                SelecionarDvgCadastroLojas(banco, codigoPack);

                if (dtpHoraFim.Value < banco.ObterDataServidor() || packVirtual.CodScannTech != 0)
                {

                    HabilitarEdicao(!VALOR);
                    if (packVirtual.CodScannTech != 0)
                    {
                        lblPackEncerrado.Visible = !VALOR;
                    }
                    else
                    {
                        lblPackEncerrado.Visible = VALOR;
                    }
                }
                else
                {
                    lblPackEncerrado.Visible = !VALOR;
                    HabilitarEdicao(VALOR);
                }


                var packVirtualGrupo1 = Pack.PackVirtualGrupo1.ConsultarPackVirtualGrupo1PorCodigoPack(banco, codigoPack);

                Produto produto;

                foreach (var item in packVirtualGrupo1)
                {
                    produto = Produto.ConsultarPorCodigo(banco, item.CodProduto);

                    listaProduto.Add(produto);
                }

                CarregarDgvGrupo1(listaProduto);

                listaProduto.Clear();

                var packVirtualGrupo2 = Pack.PackVirtualGrupo2.ConsultarPackVirtualGrupo2PorCodigoPack(banco, codigoPack);
                if (packVirtualGrupo2.Count > 0)
                {
                    foreach (var item in packVirtualGrupo2)
                    {
                        produto = Produto.ConsultarPorCodigo(banco, item.CodProduto);

                        listaProduto.Add(produto);
                    }

                    CarregarDgvGrupo2(listaProduto);
                }

                var packVirtualDiferenciado = Pack.PackVirtualPreco2.ConsultarPackVirtualDiferenciadoPorCodigoPack(banco, codigoPack);
                if (packVirtualDiferenciado.Count > 0)
                {
                    foreach (var item in packVirtualDiferenciado)
                    {
                        produto = Produto.ConsultarPorCodigo(banco, item.CodProduto);

                        listaProduto.Add(produto);
                    }
                    ExibirObjetosGruposClientes(false);
                    CarregarDgvDiferenciado(listaProduto, null);
                    SetarFocusTextBox(txtCodProdutoDiferenciado);
                }

                ReiniciarVariaveisControle();
            }

            HabilitarGridLojas();
        }

        private void ReiniciarVariaveisControle()
        {
            _alteradoPackVirtual = false;
            _alteradoPackVirtualGrupo1 = false;
            _alteradoPackVirtualGrupo2 = false;
            _alteradoPackVirtualDiferenciado = false;
            _alteradoPackVirtualLojas = false;
        }

        private void txtDescPack_TextChanged(object sender, EventArgs e)
        {
            if (_atualizar)
            {
                _alteradoPackVirtual = true;
            }
        }

        private void dtpHoraInicio_ValueChanged(object sender, EventArgs e)
        {
            if (_atualizar)
            {
                _alteradoPackVirtual = true;
            }
        }

        private void txtQtdRegra_TextChanged(object sender, EventArgs e)
        {
            if (_atualizar)
            {
                _alteradoPackVirtual = true;
            }
        }

        private void txtValorRegra_TextChanged(object sender, EventArgs e)
        {
            if (_atualizar)
            {
                _alteradoPackVirtual = true;


            }
            if (cboModeloPack.Text.Equals("Pague x porcento a menos a partir de x unidades (atacado)"))
                atualizaValoresAtacadoGrid();
        }

        private void dgvGrupo1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_atualizar)
            {
                _alteradoPackVirtualGrupo1 = true;
            }
        }

        private void dgvLojasCadastro_SelectionChanged(object sender, EventArgs e)
        {
            if (_atualizar)
            {
                _alteradoPackVirtualLojas = true;
            }

            if (dgvDiferenciado.Rows.Count > 0)
            {
                atualizarDgvDiferenciado();
            }
        }

        private void dgvGrupo2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_atualizar)
            {
                _alteradoPackVirtualGrupo2 = true;
            }
        }

        private void dtpHoraFim_ValueChanged(object sender, EventArgs e)
        {
            if (_atualizar)
            {
                _alteradoPackVirtual = true;
            }
        }

        private void dgvPackFiltro_Leave(object sender, EventArgs e)
        {
            LimparSelecaoDataGridView(dgvPackFiltro);
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            //LimparDataGridView(dgvPackFiltro);
        }

        private void txtCodProdPesquisa_TextChanged(object sender, EventArgs e)
        {
            if (txtCodProdPesquisa.Text == "")
                txtDescProduto.Text = "";
        }

        private void dgvGrupo1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //ExcluirLinhaDataGridViewCadastro(dgvGrupo1.Name, e.RowIndex, e.ColumnIndex);
        }

        private void dgvGrupo2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //ExcluirLinhaDataGridViewCadastro(dgvGrupo2.Name, e.RowIndex, e.ColumnIndex);
        }

        private bool VerificarProdutoPackVirtualExistentePassado()
        {
            Pack.PackVirtualLojas packVirtualLojaTeste = null;

            var status = "";

            status = !_atualizar ? "inserido" : "atualizado";


            if (_atualizar && !_alteradoPackVirtual && !_alteradoPackVirtualGrupo1 && !_alteradoPackVirtualGrupo2 &&
                !_alteradoPackVirtualLojas && !_alteradoPackVirtualDiferenciado)
            {
                return false;
            }


            if (!_atualizar || _alteradoPackVirtualGrupo1 || _alteradoPackVirtual)
            {
                for (var i = 0; i < dgvGrupo1.Rows.Count; i++)
                {
                    packVirtualLojaTeste =
                        RetornarPackVirtualLojaAnteriorExistente(txtCodPackCadastro.Text,
                                                                 double.Parse(dgvGrupo1[i, (int)ColunasDgvGrupo.Codigo]),
                                                                 dtpHoraInicio.Value, dtpHoraFim.Value, false);

                    if (packVirtualLojaTeste != null)
                    {
                        if (packVirtualLojaTeste.CodPack != 0)
                        {
                            Msg.Criticar("O produto código " + dgvGrupo1[i, (int)ColunasDgvGrupo.Codigo] +
                                         " não pode ser " + status + ", pois já encontra-se vinculado ao Pack código " +
                                         packVirtualLojaTeste.CodPack + ".");
                            SetarFocusTextBox(txtCodProdGrupo1);

                            return true;
                        }
                    }
                }
            }

            if (dgvGrupo2.Visible)
            {
                if (!_atualizar || _alteradoPackVirtualGrupo2 || _alteradoPackVirtual)
                {
                    for (var i = 0; i < dgvGrupo2.Rows.Count; i++)
                    {
                        packVirtualLojaTeste =
                            RetornarPackVirtualLojaAnteriorExistente(txtCodPackCadastro.Text,
                                                                     double.Parse(
                                                                         dgvGrupo2[i, (int)ColunasDgvGrupo.Codigo]),
                                                                     dtpHoraInicio.Value, dtpHoraFim.Value, false);

                        if (packVirtualLojaTeste != null)
                        {
                            if (packVirtualLojaTeste.CodPack != 0)
                            {
                                Msg.Criticar("O produto código " + dgvGrupo2[i, (int)ColunasDgvGrupo.Codigo] +
                                             " não pode ser " + status + ", pois já encontra-se vinculado ao Pack código " +
                                             packVirtualLojaTeste.CodPack + ".");

                                SetarFocusTextBox(txtCodProdGrupo2);
                                return true;
                            }
                        }
                    }
                }
            }


            if (!_atualizar || _alteradoPackVirtualDiferenciado || _alteradoPackVirtual)
            {
                for (var i = 0; i < dgvDiferenciado.Rows.Count; i++)
                {
                    packVirtualLojaTeste =
                        RetornarPackVirtualDiferenciadoLojaAnteriorExistente(txtCodPackCadastro.Text,
                                                                 double.Parse(dgvDiferenciado[i, (int)ColunasDgvGrupo.Codigo]),
                                                                 dtpHoraInicio.Value, dtpHoraFim.Value, false);

                    if (packVirtualLojaTeste != null)
                    {
                        if (packVirtualLojaTeste.CodPack != 0)
                        {
                            Msg.Criticar("O produto código " + dgvDiferenciado[i, (int)ColunasDgvGrupo.Codigo] +
                                         " não pode ser " + status + ", pois já encontra-se vinculado ao Pack código " +
                                         packVirtualLojaTeste.CodPack + ".\nVerifique se não há carga de Preço2 pendente pela tela de Preços Alterados.");
                            SetarFocusTextBox(txtCodProdutoDiferenciado);

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void dgvPackFiltro_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                LimparSelecaoDataGridView(dgvPackFiltro);
            }
        }

        private void lblProximosRegistros_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void lblProximosRegistros_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            lblProximosRegistros.ForeColor = Color.DarkBlue;
        }

        private void dgvGrupo1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (_atualizar)
                _alteradoPackVirtualGrupo1 = true;
        }

        private void dgvGrupo2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (_atualizar)
                _alteradoPackVirtualGrupo2 = true;
        }

        private void txtDescProduto_TextChanged(object sender, EventArgs e)
        {

        }

        private void gbxDadosDoPack_Enter(object sender, EventArgs e)
        {

        }

        private void dgvPackFiltro_SelecionouLinha(DataGridViewPersonalizado dgv, DataGridViewRow linha)
        {

        }

        private void btnLimparFiltros_Click(object sender, EventArgs e)
        {
            LimparCampos(true, false);

            SetarFocusTextBox(txtCodigo);

            CarregarPacks(0);
        }

        private void tbCadastro_Click(object sender, EventArgs e)
        {

        }

        private void tbCadastro_Enter(object sender, EventArgs e)
        {

        }

        private void gbxFiltros_Enter(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tbCadastro);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvDiferenciado.Rows.Count != 0)
            {
                LimparDataGridView(dgvDiferenciado);
                LimparCamposDgvDiferenciado();
                ReiniciarVariaveisControle();
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedTab.TabIndex == 1 && _permitirTrocaTab == false)
                e.Cancel = true;

        }

        private void txtCodProdGrupo1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboGrupoClientes_TextChanged(object sender, EventArgs e)
        {
            _alteradoPackVirtual = true;
        }

        private void ptbQuestao_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void ptbQuestao_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void cboModeloPackPesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPacks(0);
            //Não usar o SetarFocusComboBox, pois muda o índice
            cboModeloPackPesquisa.Focus();
        }

        private void btnProcurarDiferenciado_Click(object sender, EventArgs e)
        {
            SetarFocusTextBox(txtCodProdutoDiferenciado);

            var buscaProdutoPackDiferenciado = new FrmBuscaProdutoPackVirtual();

            buscaProdutoPackDiferenciado.SelecionouItem += buscaPackVirtualDiferenciado_SelecionouItem;

            buscaProdutoPackDiferenciado.ShowDialog();

            SetarFocusTextBox(txtValorDiferenciado);
        }

        private void txtCodProdutoDiferenciado_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvDiferenciado_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case (int)ColunasDgvDiferenciado.Excluir:
                    ExcluirLinhaDataGridView(e.RowIndex, dgvDiferenciado, true);
                    break;
            }
        }

        private void dgvDiferenciado_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtCodProdutoDiferenciado.Text = dgvDiferenciado[e.RowIndex, (int)ColunasDgvDiferenciado.Codigo];
            txtCodProdutoDiferenciado_KeyDown(txtCodProdutoDiferenciado, new KeyEventArgs(Keys.Enter));
            txtValorDiferenciado.Text = dgvDiferenciado[e.RowIndex, (int)ColunasDgvDiferenciado.Preco2];
            if (e.RowIndex > -1) dgvDiferenciado.Rows.RemoveAt(e.RowIndex);
            txtValorDiferenciado.SelectAll();
            txtValorDiferenciado.Focus();
        }

        private void dgvDiferenciado_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_atualizar)
                _alteradoPackVirtualDiferenciado = true;

        }

        private void dgvDiferenciado_Leave(object sender, EventArgs e)
        {
            LimparSelecaoDataGridView(dgvDiferenciado);
        }

        private void dgvDiferenciado_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (_atualizar)
                _alteradoPackVirtualDiferenciado = true;
        }

        private string buscaValorAtualLoja(string codigoProduto, string codLoja)
        {
            // Se a loja = 0, ira retornar valor da loja com nome MATRIZ, se não tiver loja com nome MATRIZ usará valor da loja 1
            string retorno = "0";
            IBanco banco = null;

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();
                retorno = Pack.PackVirtual.buscarValorAtualLoja(banco, codLoja, codigoProduto);
            }
            catch (Exception ex)
            {
                Msg.Informar(ex.Message);
            }
            finally
            {
                if (banco != null) banco.FecharConexao();
            }

            return Texto.FormatarMoeda(retorno, 2);
        }

        private string buscaValorPreco2(string codigoProduto, string codigoPack, string codLoja)
        {
            // Se a loja = 0, ira retornar valor da loja com nome MATRIZ, se não tiver loja com nome MATRIZ usará valor da loja 1
            string retorno = "0";

            IBanco banco = null;

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();
                retorno = Pack.PackVirtual.buscaValorPreco2(banco, codLoja, codigoProduto, codigoPack);
            }
            catch (Exception ex)
            {
                Msg.Informar(ex.Message);
            }
            finally
            {
                if (banco != null) banco.FecharConexao();
            }

            return Texto.FormatarMoeda(retorno, 2);
        }

        private void txtValorDiferenciado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddDiferenciado.Focus();
            }
        }

        private void txtCodEncarte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                var form = new FrmBuscaEncarte();
                form.SelecionouItem += BuscaEncarte_SelecionouItem;
                form.ShowDialog();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                IBanco banco = null;
                try
                {
                    banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();
                    var encarte = txtCodEncarte.Text == "" ? null : Encarte.ConsultarChave(banco, Convert.ToInt32(txtCodEncarte.Text));
                    if (encarte == null)
                    {
                        Msg.Informar("Encarte não encontrado!");
                        txtCodEncarte.Text = "";
                        SetarFocusTextBox(txtCodEncarte);
                        dtpHoraInicio.Enabled = true;
                        dtpHoraFim.Enabled = true;
                    }
                    else
                    {
                        txtEncarte.Text = encarte.Descricao;
                        if (encarte.Codigo == 1)
                        {
                            var packVirtual = Pack.PackVirtual.ConsultarChave(banco, Convert.ToInt32(txtCodPackCadastro.Text));
                            if (packVirtual != null)
                            {
                                dtpHoraInicio.Value = packVirtual.DtInicial;
                                dtpHoraFim.Value = packVirtual.DtFinal;
                            }
                            dtpHoraInicio.Enabled = true;
                            dtpHoraFim.Enabled = true;
                            _dataBloqueada = false;
                        }
                        else
                        {
                            dtpHoraInicio.Value = encarte.DataHoraInicial;
                            dtpHoraFim.Value = encarte.DataHoraFinal;
                            dtpHoraInicio.Enabled = false;
                            dtpHoraFim.Enabled = false;
                            _dataBloqueada = true;
                        }
                    }
                    _alteradoPackVirtual = true;
                }
                catch (Exception erro)
                {
                    Msg.Informar(erro.Message);
                    throw;
                }
                finally
                {
                    banco.FecharConexao();
                }
                if (txtCodEncarte.Text == "0")
                    txtCodEncarte.Text = "1";
            }
        }

        private void BuscaEncarte_SelecionouItem(string codigo)
        {
            txtCodEncarte.Text = codigo;
            txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }

        private void btnBuscaEncarte_Click(object sender, EventArgs e)
        {
            txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.F4));
        }

        private void lblCodEncarte_Click(object sender, EventArgs e)
        {
            MensagemVB6.Enviar("frmCadEncartes SIM");
            var janela =
                Janelas.ObterJanelasAbertas()
                    .Where(x => x.Value == "Cadastro de Encartes");
            while (janela.Any() == false)
            {
                Thread.Sleep(200);
                janela =
                Janelas.ObterJanelasAbertas()
                    .Where(x => x.Value == "Cadastro de Encartes");
            }
            var hwnd = janela.Select(y => y.Key).First();
            while (Janelas.VerificarJanelaAberta(hwnd))
            {
                Thread.Sleep(200);
            }
            var novoEncarte = Registro.LerVB6("Telecon Sistemas", "Gestão Comercial", "frmCadEncartesNovo", txtCodEncarte.Text);
            txtCodEncarte.Text = novoEncarte;
            txtCodEncarte.Focus();
            txtCodEncarte_KeyDown(null, new KeyEventArgs(Keys.Enter));
            Janelas.SetForegroundWindow(Handle);
        }

        private void CarregarPermissaoEncarte()
        {
            IBanco banco = null;
            banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();
            try
            {
                var modulo = Modulo.ConsultarPalavraChave(banco, "frmCadEncartes").FirstOrDefault();
                var operador = OperadorModulo.ConsultarChave(banco, modulo.Codigo, VariaveisGlobais.CodOperador, VariaveisGlobais.CodLoja);
                if ((modulo.Permissao == Modulo.EnumPermissao.Desabilitado || modulo.Permissao == Modulo.EnumPermissao.Oculto) ||
                    operador.Permissao == OperadorModulo.EnumPermissao.Desabilitado)
                {
                    lblCodEncarte.Visible = false;
                    txtCodEncarte.Visible = false;
                    txtEncarte.Visible = false;
                    btnBuscaEncarte.Visible = false;
                    lblEncarte.Visible = false;
                    _encarteHabilitado = false;
                }
                else
                {
                    _encarteHabilitado = true;
                    if (!cboModeloPack.Text.Equals("Valor Diferenciado (Preço 2)"))
                    {
                        lblCodEncarte.Visible = true;
                        txtCodEncarte.Visible = true;
                        txtEncarte.Visible = true;
                        btnBuscaEncarte.Visible = true;
                        lblEncarte.Visible = true;
                    }
                }
            }
            catch (Exception erro)
            {
                Msg.Informar(erro.Message);
                throw;
            }
            finally
            {
                banco.FecharConexao();
            }
        }

        private void ptbAjudaEncarte_Click(object sender, EventArgs e)
        {
            Msg.Informar("Associando o pack virtual a um encarte os produtos da promoção serão exibidos nos relatórios “ABC de Mercadorias” (se utilizar filtro por encarte) e “Relatório de Encartes”.");
        }

        private List<Pack.PackVirtualGrupoCliente> RetornarGruposClientes(IBanco banco, double codPack)
        {

            List<string> listaGrupos = new List<string>();
            dgvGruposClientes.Rows.Cast<DataGridViewRow>().ToList().FindAll(x => (int)x.Cells[(int)ColunasDgvGruposClientes.Selecao].Value == -1).ForEach(x =>
            {
                listaGrupos.Add(x.Cells[(int)ColunasDgvGruposClientes.Nome].Value.ToString());
            });
            listaGrupos.Remove(Pack.PackVirtualGrupoCliente.NOME_EXIBICAO_NAO_IDENTIFICADO);
            List<Pack.PackVirtualGrupoCliente> gruposClientes = new List<Pack.PackVirtualGrupoCliente>();
            var grupos = GrupoCliente.ConsultarTodos(banco);
            grupos.FindAll(x => listaGrupos.Exists(y => y.Trim().Equals(x.NOME.Trim()))).ForEach(z => gruposClientes.Add(new Pack.PackVirtualGrupoCliente(codPack, z.CODIGO)));
            return gruposClientes;
        }



        private void lblMarcaGrupos_Click(object sender, EventArgs e)
        {
            if (dgvGruposClientes.Rows.Cast<DataGridViewRow>().ToList().All(x => (int)x.Cells[(int)ColunasDgvGruposClientes.Selecao].Value == -1))
            {
                dgvGruposClientes.Rows.Cast<DataGridViewRow>().ToList().ForEach(x => dgvGruposClientes.DesmarcarCelula(x.Index, (int)ColunasDgvGruposClientes.Selecao));
                lblMarcaGrupos.Text = "Marcar todos";
            }
            else
            {
                dgvGruposClientes.Rows.Cast<DataGridViewRow>().ToList().ForEach(x => dgvGruposClientes.MarcarCelula(x.Index, (int)ColunasDgvGruposClientes.Selecao));
                lblMarcaGrupos.Text = "Desmarcar todos";
            }
        }

        private void ExibirObjetosGruposClientes(bool exibir)
        {
            dgvGruposClientes.Visible = exibir;
            label3.Visible = exibir;
            lblMarcaGrupos.Visible = exibir;
        }

        private void dgvGruposClientes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)ColunasDgvGruposClientes.Selecao)
            {
                if (_atualizar)
                {
                    _alteradoPackVirtualGruposClientes = true;
                    _alteradoPackVirtual = true;
                }
                if (
                    dgvGruposClientes.Rows.Cast<DataGridViewRow>()
                        .ToList()
                        .All(x => (int)x.Cells[(int)ColunasDgvGruposClientes.Selecao].Value == -1))
                {
                    lblMarcaGrupos.Text = "Desmarcar todos";
                }
                else
                {
                    lblMarcaGrupos.Text = "Marcar todos";
                }
            }
        }

        private void lblMarcaGrupos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lblMarcaFormas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lblMarcaFormas_Click(object sender, EventArgs e)
        {
            if (dgvFormasPGTO.Rows.Cast<DataGridViewRow>().ToList().All(x => (int)x.Cells[(int)ColunasFormas.Selecao].Value == -1))
            {
                dgvFormasPGTO.Rows.Cast<DataGridViewRow>().ToList().ForEach(x => dgvFormasPGTO.DesmarcarCelula(x.Index, (int)ColunasFormas.Selecao));
                lblMarcaFormas.Text = "Marcar todos";
            }
            else
            {
                dgvFormasPGTO.Rows.Cast<DataGridViewRow>().ToList().ForEach(x => dgvFormasPGTO.MarcarCelula(x.Index, (int)ColunasFormas.Selecao));
                lblMarcaFormas.Text = "Desmarcar todos";
            }
        }

        private void lblMarcaLojas_Click(object sender, EventArgs e)
        {
            if (dgvLojasCadastro.Rows.Cast<DataGridViewRow>().ToList().All(x => (int)x.Cells[(int)ColunasLoja.Selecao].Value == -1))
            {
                dgvLojasCadastro.Rows.Cast<DataGridViewRow>().ToList().ForEach(x => dgvLojasCadastro.DesmarcarCelula(x.Index, (int)ColunasLoja.Selecao));
                lblMarcaLojas.Text = "Marcar todos";
            }
            else
            {
                dgvLojasCadastro.Rows.Cast<DataGridViewRow>().ToList().ForEach(x => dgvLojasCadastro.MarcarCelula(x.Index, (int)ColunasLoja.Selecao));
                lblMarcaLojas.Text = "Desmarcar todos";
            }
        }

        private void lblMarcaLojas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void gbAjustarQuebra_Enter(object sender, EventArgs e)
        {

        }


        private void atualizaValoresAtacadoGrid()
        {
            double numero = 0;
            decimal ultima = 0;
            decimal valDesc = 0;
            if (!double.TryParse(txtValorRegra.Text, out numero))
                return;


            if (double.TryParse(txtUltimaCasa.Text, out numero))
                ultima = Convert.ToDecimal(txtUltimaCasa.Text);
            else
                ultima = -1;
            if (dgvGrupo1.Rows.Count > 0)
            {


                dgvGrupo1.Rows.Cast<DataGridViewRow>().ToList().ForEach(x =>
                {
                    if (double.TryParse(dgvGrupo1[(int)ColunasDgvGrupoAtacado.Valor], out numero))
                    {
                        valDesc = ((Convert.ToDecimal(x.Cells[(int)ColunasDgvGrupoAtacado.Valor].Value) *
                                    Convert.ToDecimal(txtValorRegra.Text)) / 100);

                        var celAtacado = x.Cells[(int)ColunasDgvGrupoAtacado.ValorAtacado];
                        decimal ValorCel = Convert.ToDecimal(x.Cells[(int)ColunasDgvGrupoAtacado.Valor].Value);

                        celAtacado.Value = arredondaValorAtacado(ValorCel - Convert.ToDecimal(valDesc), ultima).ToString("0.00");
                    }
                });
            }
        }


        private void optAjustarBaixoCima_CheckedChanged(object sender, EventArgs e)
        {
            if (optAjustarBaixoCima.Checked)
            {
                lblDescricaoAjudaPackAtacado.Text =
                    "Informando um valor para a última casa decimal, você irá forçar, sempre, a utilização desta casa como último dígito para o valor do produto, arredondando para baixo ou para cima dependendo do valor já com o desconto em percentual aplicado.";

                txtUltimaCasa.Enabled = true;
                atualizaValoresAtacadoGrid();

                _alteradoPackVirtual = true;
            }
        }

        private void optNaoAplicarArredondamento_CheckedChanged(object sender, EventArgs e)
        {

            if (optNaoAplicarArredondamento.Checked)
            {
                lblDescricaoAjudaPackAtacado.Text =
                    "Não é aplicada nenhum tipo de regra de arredondamento para as casas decimais do valor já com o percentual de desconto aplicado.";
                txtUltimaCasa.Text = "0";
                txtUltimaCasa.Enabled = false;
                atualizaValoresAtacadoGrid();

                _alteradoPackVirtual = true;
            }
        }

        private void optAjustarBaixo_CheckedChanged(object sender, EventArgs e)
        {
            if (optAjustarBaixo.Checked)
            {
                lblDescricaoAjudaPackAtacado.Text =
                    "Informando um valor para a última casa decimal, voce irá forçar, sempre, a utilização desta casa como último dígito para o valor do produto, arredondando sempre para baixo, após o desconto em percentual já aplicado.";

                txtUltimaCasa.Enabled = true;
                atualizaValoresAtacadoGrid();

                _alteradoPackVirtual = true;
            }

        }

        private void optAjustarCima_CheckedChanged(object sender, EventArgs e)
        {
            if (optAjustarCima.Checked)
            {
                lblDescricaoAjudaPackAtacado.Text =
                    "Informando um valor para a última casa decimal, voce irá forçar, sempre, a utilização desta casa como último dígito para o valor do produto, arredondando sempre para cima, após o desconto em percentual já aplicado.";

                txtUltimaCasa.Enabled = true;
                atualizaValoresAtacadoGrid();

                _alteradoPackVirtual = true;
            }
        }

        private void optNaoAplicarArredondamento_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void groupBoxValores_Enter(object sender, EventArgs e)
        {

        }

        private void txtUltimaCasa_TextChanged(object sender, EventArgs e)
        {
            if (_atualizar)
            {
                _alteradoPackVirtual = true;
                if (!txtUltimaCasa.Text.Equals(""))
                    atualizaValoresAtacadoGrid();
            }

        }

        private void dgvGrupo1_SelecionouLinha(DataGridViewPersonalizado dgv, DataGridViewRow linha)
        {

        }

        private void ptbFormaPagamento_Click(object sender, EventArgs e)
        {
            Msg.Informar("Para formar um Pack Virtual por forma de pagamento, é necessário que seja pago no PDV, o valor total da venda em apenas uma das formas de pagamento selecionadas ao lado.");
        }

        private void dgvGruposClientes_SelecionouLinha(DataGridViewPersonalizado dgv, DataGridViewRow linha)
        {

        }

        private void dgvFormasPGTO_SelecionouLinha(DataGridViewPersonalizado dgv, DataGridViewRow linha)
        {

        }

        private void dgvFormasPGTO_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)ColunasFormas.Selecao)
            {
                if (_atualizar)
                {
                    _alteradoPackVirtualFormaPGTO = true;
                    _alteradoPackVirtual = true;
                }
                if (
                    dgvFormasPGTO.Rows.Cast<DataGridViewRow>()
                        .ToList()
                        .All(x => (int)x.Cells[(int)ColunasFormas.Selecao].Value == -1))
                {
                    lblMarcaFormas.Text = "Desmarcar todos";
                }
                else
                {
                    lblMarcaFormas.Text = "Marcar todos";
                }
            }
        }

        private void dgvLojasCadastro_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)ColunasLoja.Selecao)
            {
                if (_atualizar)
                {
                    _alteradoPackVirtualLojas = true;
                    _alteradoPackVirtual = true;
                }
                if (
                    dgvLojasCadastro.Rows.Cast<DataGridViewRow>()
                        .ToList()
                        .All(x => (int)x.Cells[(int)ColunasLoja.Selecao].Value == -1))
                {
                    lblMarcaLojas.Text = "Desmarcar todos";
                }
                else
                {
                    lblMarcaLojas.Text = "Marcar todos";
                }
            }

            if (_passouFormLoad)
            {
                LimparCamposDgvGrupo1();
                LimparCamposDgvGrupo2();
            }
        }

        private void btnImprimirEtiqueta_Click(object sender, EventArgs e)
        {
            MensagemVB6.Enviar("frmImpressaoPack " + txtCodPackCadastro.Text);
        }

        private void frmPackVirtual_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_threadAtualizarLinhaGrupos != null && _threadAtualizarLinhaGrupos.IsAlive)
                _threadAtualizarLinhaGrupos.Abort();
        }

        private void txtQtdRegra_Leave(object sender, EventArgs e)
        {
            double numero = 0;
            if (!double.TryParse(txtQtdRegra.Text, out numero))
            {
                Msg.Criticar("Quantidade deve ser numérica.");
                txtQtdRegra.Focus();
                return;
            }

            if (cboModeloPack.Text == "Leve x e Receba desconto percentual"
                || cboModeloPack.Text == "Leve R$ X e Receba desconto percentual no produto x")
            {
                if (Convert.ToDecimal(txtQtdRegra.Text) == 0) //Txt já decimal nesses modelos
                {
                    Msg.Criticar("Valor deve ser maior que 0.");
                    txtQtdRegra.Focus();
                    return;
                }
            }
        }

        private void chkLimitarQntPack_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkLimitarQntPack.Checked)
            {
                txtNroLimitePack.Text = "0";
            }
        }

        private bool ExisteLojasSelecionadas()
        {
            if (!dgvLojasCadastro.Visible) //Se o cliente for monoloja, retorna true para não validar.
                return true;

            if (new UsuarioAcesso().StatusModulo(177) != EnumPermissao.Habilitado) //Se não usa o mix de produtos, retorna true para não validar.
                return true;

            for (int linha = 0; linha < dgvLojasCadastro.Rows.Count; linha++)
                if (dgvLojasCadastro.TestarCelulaMarcada(linha, (int)ColunasLoja.Selecao))
                    return true;

            Msg.Criticar("É obrigatório selecionar as lojas para prosseguir!");
            dgvLojasCadastro.Focus();

            return false;
        }

        private void HabilitarGridLojas()
        {
            if (!dgvLojasCadastro.Visible) //Se o cliente for monoloja, retorna para não validar.
                return;

            if (new UsuarioAcesso().StatusModulo(177) != EnumPermissao.Habilitado) //Se não usa o mix de produtos, retorna para não mudar os controles.
                return;

            if (dgvGrupo1.Rows.Count > 0 || dgvGrupo2.Rows.Count > 0)
            {
                lblMarcaLojas.Enabled = false;
                dgvLojasCadastro.Enabled = false;
            }
            else
            {
                lblMarcaLojas.Enabled = true;
                dgvLojasCadastro.Enabled = true;
            }
        }

        private bool ValidarProdutosAtivosMix(double codProduto)
        {
            IBanco banco = null;

            try
            {
                banco = Telecon.GestaoComercial.Biblioteca.Utilitarios.Utilitarios.ObterConexao();

                if (!dgvLojasCadastro.Visible) //Se o cliente for monoloja, retorna true para não validar.
                    return true;

                if (new UsuarioAcesso().StatusModulo(177) != EnumPermissao.Habilitado) //Se não usa o mix de produtos, retorna true para não validar.
                    return true;

                string lojas = "";
                int qtdLojasInativas = 0;
                for (int linha = 0; linha < dgvLojasCadastro.Rows.Count; linha++)
                {
                    if (dgvLojasCadastro.TestarCelulaMarcada(linha, (int)ColunasLoja.Selecao))
                    {
                        int codLoja = Convert.ToInt32(dgvLojasCadastro[linha, (int)ColunasLoja.Codigo].ToString());

                        ProdutoLojas produtoLojas = ProdutoLojas.ConsultarChave(banco, codLoja, codProduto);
                        if (produtoLojas != null)
                        {
                            if (!produtoLojas.Ativo)
                            {
                                lojas += dgvLojasCadastro[linha, (int)ColunasLoja.Loja].ToUpper() + "\n";
                                qtdLojasInativas++;
                            }
                        }
                    }
                }

                if (lojas != "")
                {
                    if (qtdLojasInativas > 1)
                        Msg.Criticar("Este produto está inativo nas lojas abaixo e não poderá ser utilizado neste pack virtual. \n\n" + lojas);
                    else
                        Msg.Criticar("Este produto está inativo na loja " + Texto.Left(lojas, lojas.Length - 1) + " e não poderá ser utilizado neste pack virtual.");

                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Msg.Informar(ex.Message);
                return false;
            }
            finally
            {
                if (banco != null) banco.FecharConexao();
            }
        }
    }
}