using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSFDigital.Controls
{
    public class Acao
    {
        #region Atributos
        private string _referencia;
        private string _responsavel;
        private string _tempoRestanteSLAContingencia;
        private string _tempoRestanteSLAFinal;
        private string _atuante;
        private string _descricao_FluxoAtual;
        private string _descricao_FluxoAlternativo;
        private string _descricao;
        private DateTime? _data_Acao;
        private string _codigoFluxo;
        #endregion

        #region Métodos Get / Set
        public string Referencia
        {
            get { return _referencia; }
            set { _referencia = value; }
        }
        public string Responsavel
        {
            get { return _responsavel; }
            set { _responsavel = value; }
        }
        public string TempoRestanteSLAContingencia
        {
            get { return _tempoRestanteSLAContingencia; }
            set { _tempoRestanteSLAContingencia = value; }
        }
        public string TempoRestanteSLAFinal
        {
            get { return _tempoRestanteSLAFinal; }
            set { _tempoRestanteSLAFinal = value; }
        }
        public string Atuante
        {
            get { return _atuante; }
            set { _atuante = value; }
        }
        public string Descricao_FluxoAtual
        {
            get { return _descricao_FluxoAtual; }
            set { _descricao_FluxoAtual = value; }
        }
        public string Descricao_FluxoAlternativo
        {
            get { return _descricao_FluxoAlternativo; }
            set { _descricao_FluxoAlternativo = value; }
        }
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        public DateTime? Data_Acao
        {
            get { return _data_Acao; }
            set { _data_Acao = value; }
        }
        public string CodigoFluxo
        {
            get { return _codigoFluxo; }
            set { _codigoFluxo = value; }
        }
        #endregion propriedades

        public Acao(string referencia, string responsavel, string tempoRestanteSLAContingencia, string tempoRestanteSLAFinal, string atuante, DateTime? data, string descricao, string descricaoFluxoAtual, string descricaoFluxoAlternativo, string codigoFluxo)
        {
            this.Referencia = referencia;
            this.Responsavel = responsavel;
            this.TempoRestanteSLAContingencia = tempoRestanteSLAContingencia;
            this.TempoRestanteSLAFinal = tempoRestanteSLAFinal;
            this.Atuante = atuante;
            this.Data_Acao = data;
            this.Descricao = descricao;
            this.Descricao_FluxoAtual = descricaoFluxoAtual;
            this.Descricao_FluxoAlternativo = descricaoFluxoAlternativo;
            this.CodigoFluxo = codigoFluxo;
        }

        public static List<Acao> RetornaAcoes()
        {
            List<Acao> lista = new List<Acao>();
            List<Chamado> listaChamadosAtivos = Chamado.MontaChamados(null, null, "", TipoChaves.Ativas, null, null);
            Atividade.CarregarLogAtividadesAlternativo(listaChamadosAtivos);
            List<Chave> Chaves = Chave.Chaves;

            foreach (Chamado chamado in listaChamadosAtivos)
            {
                foreach (Atividade atividade in chamado.Log_Atividades_Alternativo)
                {
                    string descricao = Util.RemoveAcentos(atividade.Descricao.Trim().ToUpper());

                    if (atividade.Descricao.Contains("!%!"))
                    {
                        foreach (Chave chave in Chaves)
                        {
                            if (chave.ValorAntigo != "")
                            {
                                if (descricao.Contains(chave.ValorAntigo) || descricao.Contains(chave.Valor))
                                {
                                    if (atividade.Tipo_Atividade == Atividade.TipoAtividade.Inicio)
                                        lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, "Chamado aberto.", chave.Traducao, chave.Valor));
                                    else if (atividade.Tipo_Atividade == Atividade.TipoAtividade.Transfer)
                                        lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, "Responsabilidade alterada.", chave.Traducao, chave.Valor));
                                    else if (atividade.Tipo_Atividade == Atividade.TipoAtividade.Outros)
                                        lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, "Informação.", chave.Traducao, chave.Valor));
                                    else if (atividade.Tipo_Atividade == Atividade.TipoAtividade.AtualizarStatus)
                                    {
                                        Status status = Status.ListaStatus().Find(c => c.StatusOcorrencia == atividade.Status_Chamado);

                                        if (status != null)
                                            lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, status.Descricao, chave.Traducao, chave.Valor));
                                        else
                                            lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, "", chave.Traducao, chave.Valor));
                                    }
                                    else
                                        lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, atividade.Descricao_Fluxo, chave.Traducao, chave.Valor));
                                }
                            }
                            else
                            {
                                if (descricao.Contains(chave.Valor))
                                {
                                    if (atividade.Tipo_Atividade == Atividade.TipoAtividade.Inicio)
                                        lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, "Chamado aberto.", chave.Traducao, chave.Valor));
                                    else if (atividade.Tipo_Atividade == Atividade.TipoAtividade.Transfer)
                                        lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, "Responsabilidade alterada.", chave.Traducao, chave.Valor));
                                    else if (atividade.Tipo_Atividade == Atividade.TipoAtividade.Outros)
                                        lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, "Informação.", chave.Traducao, chave.Valor));
                                    else if (atividade.Tipo_Atividade == Atividade.TipoAtividade.AtualizarStatus)
                                    {
                                        Status status = Status.ListaStatus().Find(c => c.StatusOcorrencia == atividade.Status_Chamado);

                                        if (status != null)
                                            lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, status.Descricao, chave.Traducao, chave.Valor));
                                        else
                                            lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, "", chave.Traducao, chave.Valor));
                                    }
                                    else
                                        lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, atividade.Descricao_Fluxo, chave.Traducao, chave.Valor));
                                }
                            }
                        }
                    }
                    else
                    {
                        if(atividade.Tipo_Atividade == Atividade.TipoAtividade.Inicio)
                            lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, "Chamado aberto.", "", "---"));
                        else if (atividade.Tipo_Atividade == Atividade.TipoAtividade.Transfer)
                            lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, "Responsabilidade alterada.", "", "---"));
                        else if (atividade.Tipo_Atividade == Atividade.TipoAtividade.Outros)
                            lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, "Informação.", "", "---"));
                        else if (atividade.Tipo_Atividade == Atividade.TipoAtividade.AtualizarStatus)
                        {
                            Status status = Status.ListaStatus().Find(c => c.StatusOcorrencia == atividade.Status_Chamado);

                            if (status != null)
                                lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, status.Descricao, "", "---"));
                            else
                                lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, "", "", "---"));
                        }
                        else
                            lista.Add(new Acao(chamado.Referencia, chamado.Responsavel, chamado.TempoSLAContingenciaRestanteFormatado, chamado.TempoSLARestanteFormatado, atividade.Atuante, atividade.Data_Atividade.Value, atividade.Descricao, atividade.Descricao_Fluxo, "", "---"));
                    }
                }
            }

            return lista;
        }
    }
}
