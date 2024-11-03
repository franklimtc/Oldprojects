using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSFDigital.Controls
{
    public class DetalhamentoChamado
    {
        #region Atributos
        private Chamado _chamadoReferencia;
        private Atividade _atividadeInicial;
        private string _acaoInicial;
        private Atividade _atividadeFinal;
        private string _acaoFinal;
        private TimeSpan? _tmpConsiderado;
        private TimeSpan? _tmpDesconsiderado;
        #endregion

        #region Métodos Get / Set
        public Chamado ChamadoReferencia
        {
            get { return _chamadoReferencia; }
            set { _chamadoReferencia = value; }
        }
        public Atividade AtividadeInicial
        {
            get { return _atividadeInicial; }
            set { _atividadeInicial = value; }
        }
        public string AcaoInicial
        {
            get { return _acaoInicial; }
            set { _acaoInicial = value; }
        }
        public Atividade AtividadeFinal
        {
            get { return _atividadeFinal; }
            set { _atividadeFinal = value; }
        }
        public string AcaoFinal
        {
            get { return _acaoFinal; }
            set { _acaoFinal = value; }
        }
        public TimeSpan? TempoConsiderado
        {
            get { return _tmpConsiderado.Value; }
            set { _tmpConsiderado = value; }
        }
        public TimeSpan? TempoDesconsiderado
        {
            get { return _tmpDesconsiderado.Value; }
            set { _tmpDesconsiderado = value; }
        }
        public string TempoConsideradoFormatado
        {
            get { return Util.FormatarTimeSpan(_tmpConsiderado.Value); }
        }
        public string TempoDesconsideradoFormatado
        {
            get { return Util.FormatarTimeSpan(_tmpDesconsiderado.Value); }
        }

        public string ChamadoNumero
        {
            get { return _chamadoReferencia.Referencia; }
        }
        public string ChamadoTipo
        {
            get { return _chamadoReferencia.Tipo; }
        }
        public string Serie
        {
            get { return _chamadoReferencia.MaquinaRelacionada.Serie; }
        }
        public string Uf
        {
            get { return _chamadoReferencia.MaquinaRelacionada.Local.Estado; }
        }
        public string Cidade
        {
            get { return _chamadoReferencia.MaquinaRelacionada.Local.Cidade; }
        }
        public string Resumo
        {
            get { return _chamadoReferencia.Resumo; }
        }
        public string Responsavel
        {
            get { return _chamadoReferencia.Responsavel; }
        }
        public string Categoria
        {
            get { return _chamadoReferencia.Categoria; }
        }
        public string Status
        {
            get { return _chamadoReferencia.StatusChamado.Descricao; }
        }
        public DateTime? ChamadoDataAbertura
        {
            get { return _chamadoReferencia.Data_Abertura; }
        }
        public DateTime? ChamadoDataFechamento
        {
            get { return _chamadoReferencia.Data_Fechamento; }
        }
        public string ChamadoSLALimite
        {
            get { return _chamadoReferencia.SlaDefinida.LimiteFormatado; }
        }
        public string ChamadoSLATempo
        {
            get { return _chamadoReferencia.TempoSLAFormatado; }
        }
        public string ChamadoSLATempoInvalido
        {
            get { return _chamadoReferencia.TempoDescontosFormatado; }
        }
        public string ChamadoSLATempoTotal
        {
            get { return _chamadoReferencia.TempoTotalFormatado; }
        }
        public DateTime? AtividadeDataInicial
        {
            get { return _atividadeInicial.Data_Atividade; }
        }
        public DateTime? AtividadeDataFinal
        {
            get { return _atividadeFinal.Data_Atividade; }
        }
        #endregion

        public override string ToString()
        {
            return String.Format("Chamado [{0}] ; Ação Incial: {1} -  Ação Final: {2} ; Considerado: {3} - Desconsiderado: {4}",
                _chamadoReferencia.Referencia.ToString(),
                _atividadeInicial.ToString(),
                _atividadeFinal.ToString(),
                TempoConsideradoFormatado,
                TempoDesconsideradoFormatado);
        }

        #region Monta detalhamento para o relatório analítico
        public static List<DetalhamentoChamado> MontaDetalhamentoChamados(List<Chamado> listaChamado)
        {
            List<DetalhamentoChamado> listaDetalhamento = new List<DetalhamentoChamado>();

            foreach (Chamado chamado in listaChamado)
            {
                Atividade acaoAnterior = null;

                string cidade = "";
                string estado = "";

                if (chamado.Atendente != null)
                {
                    if (chamado.Atendente.LocalContato != null)
                    {
                        if (chamado.Atendente.LocalContato.Cidade != null)
                            cidade = chamado.Atendente.LocalContato.Cidade.Trim().ToUpper();
                        if (chamado.Atendente.LocalContato.Estado != null)
                            cidade = chamado.Atendente.LocalContato.Estado.Trim().ToUpper();
                    }
                }

                foreach (Atividade atividade in chamado.Log_Atividades)
                {
                    DetalhamentoChamado detalhe = new DetalhamentoChamado();

                    if (acaoAnterior != null)
                    {
                        detalhe.ChamadoReferencia = chamado;
                        detalhe.AtividadeInicial = acaoAnterior;

                        if (atividade.Descricao == null)
                            atividade.Descricao = "";
                        if (acaoAnterior.Descricao == null)
                            acaoAnterior.Descricao = "";

                        detalhe.AcaoInicial = detalhe.AtividadeInicial.Atuante.Trim() + "\n\n\n" +
                            detalhe.AtividadeInicial.Descricao.Trim() + "\n\n\n" + detalhe.AtividadeInicial.Descricao_Fluxo.Trim();

                        detalhe.AtividadeFinal = atividade;

                        detalhe.AcaoFinal = detalhe.AtividadeFinal.Atuante.Trim() + "\n\n\n" +
                            detalhe.AtividadeFinal.Descricao.Trim() + "\n\n\n" + detalhe.AtividadeFinal.Descricao_Fluxo.Trim();

                        detalhe.TempoConsiderado = SLA.CalcularR1(acaoAnterior.Data_Atividade.Value, atividade.Data_Atividade.Value, cidade, estado);

                        if (atividade.Status_Chamado == TipoStatusChamado.PendenteUsuario || atividade.Status_Chamado == TipoStatusChamado.Fechado)
                            detalhe.TempoDesconsiderado = SLA.CalcularR1(acaoAnterior.Data_Atividade.Value, atividade.Data_Atividade.Value, cidade, estado);
                        else
                            detalhe.TempoDesconsiderado = TimeSpan.Zero;

                        listaDetalhamento.Add(detalhe);

                        acaoAnterior = atividade;
                    }
                    else
                    {
                        acaoAnterior = atividade;
                    }
                }
            }
            return listaDetalhamento;
        }
        #endregion
    }
}