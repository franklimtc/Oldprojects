using System;
using System.Text;
using System.Globalization;

namespace CSFDigital.Controls
{
    #region Enum's de parametrização do ShowReports
    public enum Relatorios
    {
        RelatorioToner,
        RelatorioAssitenciaTecnica,
        RelatorioConsolidado,
        RelatorioAnalitico,
        RelatorioAssitenciaTecnicaMulta,
        RelatorioMaquinasParadas
    }

    public enum VisoesConsolidado
    {
        VisaoBNB,
        VisaoGeral
    }

    public enum VisoesAnalitico
    {
        VisaoBNB,
        VisaoGeral
    }

    public enum TipoChaves
    {
        DtFechamento,
        DtAbertura,
        AbertosOuFechados,
        Ativas,
        NumChamado,
        NumSerie
    }

    public enum TipoEmissao
    {
        Planilha,
        Relatorio
    }
    #endregion

    public static class Util
    {
        #region Retorna status
        public static object ObterStatus(Atividade.TipoAtividade tipoAtividade, string descricaoFluxo)
        {
            switch (tipoAtividade)
            {
                case Atividade.TipoAtividade.Inicio:
                    return TipoStatusChamado.Aberto;
                case Atividade.TipoAtividade.AtualizarStatus:
                    return ProcessarAlteracaoStatus(descricaoFluxo);
                case Atividade.TipoAtividade.Solucao:
                    {
                        if (descricaoFluxo.Contains("Status changed from"))
                            return ProcessarAlteracaoStatus(descricaoFluxo);
                        else
                            return TipoStatusChamado.Solucionado;
                    }
                case Atividade.TipoAtividade.Fechado:
                    return TipoStatusChamado.Fechado;
                case Atividade.TipoAtividade.Outros:
                    return TipoStatusChamado.Outros;
                default:
                    return TipoStatusChamado.Outros;
            }
        }
        private static object ProcessarAlteracaoStatus(string descricaoFluxo)
        {
            if (descricaoFluxo == "Nenhuma Mudança" || descricaoFluxo == "Status Atualizado")
                return TipoStatusChamado.NenhumaAlteracaoStatus;

            object tipo;

            tipo = TipoStatusChamado.Outros;

            int index = descricaoFluxo.ToUpper().Trim().IndexOf("' TO '") + 6;

            if (index > 0)
            {
                string statusDestino = descricaoFluxo.ToUpper().Trim().Substring(index);

                statusDestino = statusDestino.Substring(0, statusDestino.IndexOf("'"));

                tipo = Status.ConvertDescriptionToStatus(statusDestino).StatusOcorrencia;
            }
            return tipo;
        }
        #endregion

        #region Retorna Data atual
        public static DateTime DataAtual()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }
        #endregion

        #region Formata TimeSpan e DateTime
        public static string FormatarTimeSpan(TimeSpan time)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", (int)time.TotalHours, time.Minutes, time.Seconds);
        }

        public static string FormatarDateTime(DateTime data)
        {
            string dataFormatada = "";

            if (data.Month < 10)
            {
                if (data.Day < 10)
                    dataFormatada = "0" + data.Day.ToString() + "/0" + data.Month.ToString() + "/" + data.Year.ToString();
                else
                    dataFormatada = data.Day.ToString() + "/0" + data.Month.ToString() + "/" + data.Year.ToString();
            }
            else
            {
                if (data.Day < 10)
                    dataFormatada = "0" + data.Day.ToString() + "/" + data.Month.ToString() + "/" + data.Year.ToString();
                else
                    dataFormatada = data.Day.ToString() + "/" + data.Month.ToString() + "/" + data.Year.ToString();
            }

            return dataFormatada;
        }
        #endregion

        #region Retorna filtro para os relatórios
        public static string RetornaFiltroData(TipoChaves filtro)
        {
            switch (filtro)
            {
                case TipoChaves.NumChamado:
                    return "dbo.call_req.ref_num";
                case TipoChaves.NumSerie:
                    return "dbo.call_req.ref_num";
                case TipoChaves.Ativas:
                    return "dbo.call_req.active_flag = 1";
                case TipoChaves.DtAbertura:
                    return "dbo.call_req.open_date";
                case TipoChaves.DtFechamento:
                    return "dbo.call_req.close_date";

                default:
                    return "";
            }
        }
        #endregion

        #region Retorna string sem acentos
        public static string RemoveAcentos(string texto)
        {
            string textor = "";

            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i].ToString() == "ã") textor += "a";
                else if (texto[i].ToString() == "á") textor += "a";
                else if (texto[i].ToString() == "à") textor += "a";
                else if (texto[i].ToString() == "â") textor += "a";
                else if (texto[i].ToString() == "ä") textor += "a";
                else if (texto[i].ToString() == "é") textor += "e";
                else if (texto[i].ToString() == "è") textor += "e";
                else if (texto[i].ToString() == "ê") textor += "e";
                else if (texto[i].ToString() == "ë") textor += "e";
                else if (texto[i].ToString() == "í") textor += "i";
                else if (texto[i].ToString() == "ì") textor += "i";
                else if (texto[i].ToString() == "ï") textor += "i";
                else if (texto[i].ToString() == "õ") textor += "o";
                else if (texto[i].ToString() == "ó") textor += "o";
                else if (texto[i].ToString() == "ò") textor += "o";
                else if (texto[i].ToString() == "ö") textor += "o";
                else if (texto[i].ToString() == "ú") textor += "u";
                else if (texto[i].ToString() == "ù") textor += "u";
                else if (texto[i].ToString() == "ü") textor += "u";
                else if (texto[i].ToString() == "ç") textor += "c";
                else if (texto[i].ToString() == "Ã") textor += "A";
                else if (texto[i].ToString() == "Á") textor += "A";
                else if (texto[i].ToString() == "À") textor += "A";
                else if (texto[i].ToString() == "Â") textor += "A";
                else if (texto[i].ToString() == "Ä") textor += "A";
                else if (texto[i].ToString() == "É") textor += "E";
                else if (texto[i].ToString() == "È") textor += "E";
                else if (texto[i].ToString() == "Ê") textor += "E";
                else if (texto[i].ToString() == "Ë") textor += "E";
                else if (texto[i].ToString() == "Í") textor += "I";
                else if (texto[i].ToString() == "Ì") textor += "I";
                else if (texto[i].ToString() == "Ï") textor += "I";
                else if (texto[i].ToString() == "Õ") textor += "O";
                else if (texto[i].ToString() == "Ó") textor += "O";
                else if (texto[i].ToString() == "Ò") textor += "O";
                else if (texto[i].ToString() == "Ö") textor += "O";
                else if (texto[i].ToString() == "Ú") textor += "U";
                else if (texto[i].ToString() == "Ù") textor += "U";
                else if (texto[i].ToString() == "Ü") textor += "U";
                else if (texto[i].ToString() == "Ç") textor += "C";
                else textor += texto[i];
            }
            return textor;
        }
        #endregion
    }
}