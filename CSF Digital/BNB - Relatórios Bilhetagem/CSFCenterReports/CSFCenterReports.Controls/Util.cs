using System;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    #region Enum's de parametrização do ShowReports
    public enum Relatorios
    {
        R1_Produção_CopiaImpressaoFax,
        R2_AnaliticoDeBilhetagem,
        R3_ImpressaoPorUsuario,
        R4_ImpressaoPorUsuario_Detalhado,
        //R4_ImpressãoPorImpressora_Detalhado
        R5_ImpressaoPorUsuario_Consolidado
    }

    public enum TipoFiltro
    {
        Uf,
        Cidade,
        Unidade,
        Ambiente,
        Impressora,
        Usuario
    }

    public enum TipoCorImpressao
    { 
        Cor,
        Mono,
        Indiferente
    }

    public enum TipoFrenteVerso
    {
        Frente,
        FrenteVerso,
        Indiferente
    }

    public enum TipoFormato
    {
        A3,
        A4
    }
    #endregion

    public static class Util
    {
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

        public static string FormatarDateTimeSQL(DateTime data)
        {
            return data.ToString("yyyy-MM-ddTHH:mm:ss");
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

        #region Trata status [0-Ativo / 1-Inativo]
        public static int Status(bool ativo)
        {
            if (ativo)
                return 0;
            else
                return 1;
        }

        public static bool Status(int ativo)
        {
            if (ativo == 0)
                return true;
            else
                return false;
        }
        #endregion

        public static string MontarListaStringConsulta(string itens, char strToken)
        {
            List<string> lista = new List<string>();

            if (itens.Contains(strToken.ToString()))
            {
                string[] imp = itens.Split(strToken);

                for (int i = 0; i <= imp.Length - 1; i++)
                    lista.Add(imp[i]);
            }
            else
            {
                lista.Add(itens);
            }

            if (lista.Count == 0)
                return String.Empty;
            else
            {
                string listaStr = String.Empty;

                foreach (string item in lista)
                {
                    listaStr = String.Concat(listaStr, "'", item, "',");
                }
                return listaStr.Remove(listaStr.Length - 1);
            }
        }
    }
}