using System;
using System.Collections.Generic;

namespace CSFDigital.Controls
{
    public static class Contabilizacao
    {
        #region Contabiliza os tempos do chamado
        public static void ProcessarTempoAtividade(List<Chamado> listaChamado)
        {
            DateTime dtNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            foreach (Chamado chamado in listaChamado)
            {
                Dictionary<DateTime, DateTime> DicPendUsu = new Dictionary<DateTime, DateTime>();

                DateTime? dtInicial = (DateTime?)null;
                TipoStatusChamado statusAtual = TipoStatusChamado.Null;

                string cidade = "";
                string estado = "";
                bool VerificouContingencia = false;

                if (chamado.MaquinaRelacionada != null)
                {
                    if (chamado.MaquinaRelacionada.Local != null)
                    {
                        if (chamado.MaquinaRelacionada.Local.Cidade != null)
                            cidade = chamado.MaquinaRelacionada.Local.Cidade.Trim().ToUpper();
                        if (chamado.MaquinaRelacionada.Local.Estado != null)
                            estado = chamado.MaquinaRelacionada.Local.Estado.Trim().ToUpper();
                    }
                }

                if (chamado.StatusChamado.StatusOcorrencia != TipoStatusChamado.Fechado)
                {
                    Atividade atividade = new Atividade();
                    atividade.Atuante = "S/A";
                    atividade.Data_Atividade = dtNow;
                    atividade.Descricao = "Nenhuma ação, informação para tratamento interno.";
                    atividade.Descricao_Fluxo = "N/A";
                    atividade.Id = 99999;
                    atividade.PersidReq = chamado.Persid;
                    atividade.Referencia = chamado.Referencia;
                    atividade.Status_Chamado = TipoStatusChamado.Fechado;
                    atividade.TempoDocumetacao = TimeSpan.Zero;
                    atividade.Tipo_Atividade = Atividade.TipoAtividade.Fechado;

                    chamado.Log_Atividades.Add(atividade);
                }

                foreach (Atividade atividade in chamado.Log_Atividades)
                {
                    if (atividade.Descricao.Contains("!%!SOLUÇÃO DE CONTINGÊNCIA!%!") || (atividade.Tipo_Atividade == Atividade.TipoAtividade.Solucao && chamado.ContingenciaSolucao == null))
                    {
                        chamado.ContingenciaSolucao = atividade;

                        if (atividade.Descricao.Contains("!%!SOLUÇÃO DE CONTINGÊNCIA!%!"))
                            chamado.ContingenciaSolucao.Descricao = chamado.UltimaSolucao.Descricao.Substring(0, chamado.UltimaSolucao.Descricao.IndexOf("!%!SOLUÇÃO DE CONTINGÊNCIA!%!"));

                        if (System.Configuration.ConfigurationManager.AppSettings["ContingenciaAtiva"] == "N")
                        {
                            chamado.ContingenciaSolucao = new Atividade();
                            chamado.ContingenciaSolucao.Descricao = "---";
                        }
                    }

                    if (dtInicial.HasValue)
                    {
                        if (!chamado.TempoPorStatus.ContainsKey(statusAtual))
                            chamado.TempoPorStatus.Add(statusAtual, TimeSpan.Zero);

                        chamado.TempoPorStatus[statusAtual] += atividade.Data_Atividade.Value.Subtract(dtInicial.Value);

                        if (chamado.Categoria.Contains("TONNER"))
                        {
                            if (statusAtual == TipoStatusChamado.PendenteUsuario || statusAtual == TipoStatusChamado.Fechado)
                            {
                                chamado.TempoDescontos += SLA.CalcularR3(dtInicial.Value.Date, atividade.Data_Atividade.Value.Date, cidade, estado);

                                //DicPendUsu.Add(dtInicial.Value, atividade.Data_Atividade.Value);
                            }

                            chamado.TempoTotal += SLA.CalcularR3(dtInicial.Value.Date, atividade.Data_Atividade.Value.Date, cidade, estado);
                        }
                        else
                        {
                            if (statusAtual == TipoStatusChamado.PendenteUsuario || statusAtual == TipoStatusChamado.Fechado)
                                chamado.TempoDescontos += SLA.CalcularR1(dtInicial.Value, atividade.Data_Atividade.Value, cidade, estado);

                            chamado.TempoTotal += SLA.CalcularR1(dtInicial.Value, atividade.Data_Atividade.Value, cidade, estado);

                            if (chamado.ContingenciaSolucao != null && VerificouContingencia == false)
                            {
                                chamado.TempoSLAContingencia = chamado.TempoTotal - chamado.TempoDescontos;

                                VerificouContingencia = true;
                            }
                        }

                        dtInicial = atividade.Data_Atividade;
                        statusAtual = atividade.Status_Chamado;
                    }
                    else
                    {
                        dtInicial = atividade.Data_Atividade;
                        statusAtual = atividade.Status_Chamado;
                    }

                    if (atividade.Tipo_Atividade == Atividade.TipoAtividade.Solucao)
                    {
                        chamado.UltimaSolucao = atividade;

                        if (chamado.UltimaSolucao.Descricao.Contains("!%!SOLUÇÃO DEFINITIVA!%!"))
                            chamado.UltimaSolucao.Descricao = chamado.UltimaSolucao.Descricao.Substring(0, chamado.UltimaSolucao.Descricao.IndexOf("!%!SOLUÇÃO DEFINITIVA!%!"));

                        chamado.UltimaSolucao.Descricao = chamado.UltimaSolucao.Descricao.Trim();
                    }
                }

                chamado.TempoSLA = chamado.TempoTotal - chamado.TempoDescontos;

                if (VerificouContingencia == false)
                    chamado.TempoSLAContingencia = chamado.TempoTotal - chamado.TempoDescontos;

                if (chamado.UltimaSolucao == null)
                {
                    chamado.UltimaSolucao = new Atividade();
                    chamado.UltimaSolucao.Descricao = "";
                }

                if (chamado.ContingenciaSolucao == null)
                {
                    chamado.ContingenciaSolucao = new Atividade();
                    chamado.ContingenciaSolucao.Descricao = "";
                }

                if (chamado.SlaDefinida.DeAcordo != "Indefinido")
                {
                    if (chamado.Categoria.Contains("INSTALAÇÃO / CONFIGURAÇÃO"))
                    {
                        chamado.SlaDefinida.DeAcordo = "**SLO";
                        chamado.SlaDefinida.DeAcordoContingencia = "**SLO";
                    }
                    else if (chamado.Categoria.Contains("RECOLHIMENTO DE TONER"))
                    {
                        chamado.SlaDefinida.DeAcordo = "---";
                        chamado.SlaDefinida.DeAcordoContingencia = "---";
                        chamado.SlaDefinida.Limite = TimeSpan.Zero;
                        chamado.SlaDefinida.LimiteContingencia = TimeSpan.Zero;
                    }
                    else
                    {
                        if (chamado.TempoSLA.Subtract(chamado.SlaDefinida.Limite) < TimeSpan.Zero)
                            chamado.SlaDefinida.Excedido = Util.FormatarTimeSpan(TimeSpan.Zero);
                        else
                            chamado.SlaDefinida.Excedido = Util.FormatarTimeSpan(chamado.TempoSLA.Subtract(chamado.SlaDefinida.Limite));

                        if (!chamado.Categoria.Contains("TONNER"))
                        {
                            if (chamado.SlaDefinida.LimiteContingencia > chamado.TempoSLAContingencia)
                                chamado.SlaDefinida.DeAcordoContingencia = "Sim";
                            else
                                chamado.SlaDefinida.DeAcordoContingencia = "Não";
                        }

                        if (chamado.SlaDefinida.Limite > chamado.TempoSLA)
                            chamado.SlaDefinida.DeAcordo = "Sim";
                        else
                        {
                            chamado.SlaDefinida.DeAcordo = "Não";

                            //if (!chamado.Categoria.Contains("TONNER"))
                            //    ProcessarTempoAtividadeRegra2(chamado);
                        }
                    }
                }
            }
        }
        public static void ProcessarTempoAtividadeRegra2(Chamado chamado)
        {
            chamado.TempoPorStatus = new Dictionary<TipoStatusChamado, TimeSpan>();
            chamado.TempoDescontos = TimeSpan.Zero;
            chamado.TempoTotal = TimeSpan.Zero;
            chamado.TempoSLA = TimeSpan.Zero;

            DateTime? dtInicial = (DateTime?)null;
            TipoStatusChamado statusAtual = TipoStatusChamado.Null;

            string cidade = "";
            string estado = "";

            if (chamado.Atendente != null)
            {
                if (chamado.Atendente.LocalContato != null)
                {
                    if (chamado.Atendente.LocalContato.Cidade != null)
                        cidade = chamado.Atendente.LocalContato.Cidade.Trim().ToUpper();
                    if (chamado.Atendente.LocalContato.Estado != null)
                        estado = chamado.Atendente.LocalContato.Estado.Trim().ToUpper();
                }
            }

            foreach (Atividade atividade in chamado.Log_Atividades)
            {
                if (dtInicial.HasValue)
                {
                    if (!chamado.TempoPorStatus.ContainsKey(statusAtual))
                        chamado.TempoPorStatus.Add(statusAtual, TimeSpan.Zero);

                    chamado.TempoPorStatus[statusAtual] += atividade.Data_Atividade.Value.Subtract(dtInicial.Value);

                    if (statusAtual == TipoStatusChamado.PendenteUsuario || statusAtual == TipoStatusChamado.Fechado)
                        chamado.TempoDescontos += SLA.CalcularR2(dtInicial.Value, atividade.Data_Atividade.Value, cidade, estado);

                    chamado.TempoTotal += SLA.CalcularR2(dtInicial.Value, atividade.Data_Atividade.Value, cidade, estado);

                    dtInicial = atividade.Data_Atividade;
                    statusAtual = atividade.Status_Chamado;
                }
                else
                {
                    dtInicial = atividade.Data_Atividade;
                    statusAtual = atividade.Status_Chamado;
                }

                if (atividade.Tipo_Atividade == Atividade.TipoAtividade.Solucao)
                    chamado.UltimaSolucao = atividade;
            }
            chamado.TempoSLA = chamado.TempoTotal - chamado.TempoDescontos;



            if (chamado.UltimaSolucao == null)
                chamado.UltimaSolucao = new Atividade();

            if (chamado.SlaDefinida != null)
            {
                if (chamado.Categoria.Contains("INSTALAÇÃO / CONFIGURAÇÃO"))
                    chamado.SlaDefinida.DeAcordo = "**SLO";
                else
                {
                    if (chamado.SlaDefinida.LimiteContingencia > chamado.TempoSLAContingencia)
                        chamado.SlaDefinida.DeAcordoContingencia = "Sim";
                    else
                        chamado.SlaDefinida.DeAcordoContingencia = "Não";

                    if (chamado.SlaDefinida.Limite > chamado.TempoSLA)
                        chamado.SlaDefinida.DeAcordo = "Sim";
                    else
                        chamado.SlaDefinida.DeAcordo = "Não";
                }
            }
        }
        #endregion

        #region Retorna lista de chamados de acordo ou não com o SLA
        public static List<Chamado> RetornaListaChamadoSLA(List<Chamado> listaChamado, string deAcordo)
        {
            List<Chamado> lista = new List<Chamado>();

            foreach (Chamado chamado in listaChamado)
            {
                if (chamado.SlaDefinida != null)
                {
                    if (chamado.SlaDefinida.DeAcordo == deAcordo)
                        lista.Add(chamado);
                }
            }

            return lista;
        }
        #endregion

        #region Retorna lista de chamados de acordo com o tipo (Requisição/Incidente)
        public static List<Chamado> RetornaListaTipoChamado(List<Chamado> listaChamado, string tipo)
        {
            List<Chamado> lista = new List<Chamado>();

            foreach (Chamado chamado in listaChamado)
            {
                if (chamado.Tipo == tipo)
                    lista.Add(chamado);
            }

            return lista;
        }
        #endregion

        #region Contabiliza as multas
        public static void ProcessarMulta(List<Chamado> listaChamados)
        {
            foreach (Chamado chamado in listaChamados)
            {
                chamado.Multa = 0;

                if (!chamado.Categoria.Contains("TONNER"))
                {
                    if (chamado.SlaDefinida != null)
                    {
                        try
                        {
                            double aux = 0;

                            TimeSpan tempoFinal = chamado.TempoSLA - chamado.SlaDefinida.Limite;

                            if (tempoFinal < TimeSpan.Zero)
                                tempoFinal = TimeSpan.Zero;

                            TimeSpan tempoContingencia = chamado.TempoSLAContingencia - chamado.SlaDefinida.LimiteContingencia;

                            if (tempoContingencia < TimeSpan.Zero)
                                tempoContingencia = TimeSpan.Zero;

                            double bilhetagem = 0;

                            //if (chamado.MaquinaRelacionada != null)
                            //    bilhetagem = chamado.MaquinaRelacionada.Bilhetagem * 0.03867;

                            Parametro ValorBilhetagem = Parametro.Parametros.Find(p => p.Nome == "ValorImpressao");

                            if (chamado.MaquinaRelacionada != null)
                                bilhetagem = chamado.MaquinaRelacionada.Bilhetagem * float.Parse(ValorBilhetagem.Valor);

                            double percCont = 0;
                            double percFinal = 0;

                            if (chamado.SlaDefinida.Nivel == Chamado.Severidade.Alta)
                            {
                                //percCont = (tempoContingencia.TotalHours - tempoFinal.TotalHours) * 0.02;
                                percFinal = tempoFinal.TotalHours * 0.03;
                            }
                            else if (chamado.SlaDefinida.Nivel == Chamado.Severidade.Baixa)
                            {
                                //percCont = (tempoContingencia.TotalHours - tempoFinal.TotalHours) * 0.01;
                                percFinal = tempoFinal.TotalHours * 0.02;
                            }

                            aux = (percCont + percFinal) * bilhetagem;

                            //if (chamado.SlaDefinida.DeAcordo == "Não" || chamado.SlaDefinida.DeAcordoContingencia == "Não")
                            if (chamado.SlaDefinida.DeAcordo == "Não" || chamado.SlaDefinida.DeAcordoContingencia == "Não")
                                chamado.Multa = aux;
                        }
                        catch (Exception ex)
                        {
                            chamado.Multa = 0;
                        }
                    }
                }
                else
                {
                    if (chamado.SlaDefinida != null)
                    {
                        try
                        {
                            double bilhetagem = 0;

                            Parametro ValorBilhetagem = Parametro.Parametros.Find(p => p.Nome == "ValorImpressao");

                            if (chamado.MaquinaRelacionada != null)
                                bilhetagem = chamado.MaquinaRelacionada.Bilhetagem * float.Parse(ValorBilhetagem.Valor);

                            if (chamado.SlaDefinida.DeAcordo == "Não")
                                chamado.Multa = bilhetagem * (chamado.DiasSLAExcedido * 0.02);
                            else
                                chamado.Multa = 0;
                        }
                        catch (Exception ex)
                        {
                            chamado.Multa = 0;
                        }
                    }
                }
            }
        }
        #endregion
    }
}