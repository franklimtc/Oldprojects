using System;
using System.Collections.Generic;

namespace CSFDigital.Controls
{
    public class Localidade
    {
        #region Atributos
        private string _cidade;
        private string _estado;
        private string _nomeUnidade;
        private string _tipoUnidade;
        private TipoLocalidade _tipo_localidade;
        private Regiao _regiao;
        private string _nomeAmbiente;
        #endregion

        #region Métodos Get / Set
        public string Cidade
        {
            get { return _cidade; }
            set { _cidade = (string)value; }
        }
        public string Estado
        {
            get { return _estado; }
            set { _estado = (string)value; }
        }
        public string NomeUnidade
        {
            get { return _nomeUnidade; }
            set { _nomeUnidade = (string)value; }
        }
        public string TipoUnidade
        {
            get { return _tipoUnidade; }
            set { _tipoUnidade = (string)value; }
        }
        public TipoLocalidade Tipo_Localidade
        {
            get { return _tipo_localidade; }
            set { _tipo_localidade = (TipoLocalidade)value; }
        }
        public Regiao LocalRegiao
        {
            get { return _regiao; }
            set { _regiao = (Regiao)value; }
        }
        public string NomeAmbiente
        {
            get { return _nomeAmbiente; }
            set { _nomeAmbiente = (string)value; }
        }
        #endregion

        #region Dicionário de capitais
        private static Dictionary<string, string> CriaDicionarioCapitais()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();

            dicionario.Add("AM", "MANAUS");
            dicionario.Add("PA", "BELEM");
            dicionario.Add("AP", "MACAPA");
            dicionario.Add("AC", "RIO BRANCO");
            dicionario.Add("RO", "PORTO VELHO");
            dicionario.Add("RR", "BOA VISTA");
            dicionario.Add("TO", "PALMAS");

            dicionario.Add("CE", "FORTALEZA");
            dicionario.Add("PI", "TERESINA");
            dicionario.Add("MA", "SAO LUIS");
            dicionario.Add("RN", "NATAL");
            dicionario.Add("PB", "JOAO PESSOA");
            dicionario.Add("PE", "RECIFE");
            dicionario.Add("AL", "MACEIO");
            dicionario.Add("SE", "ARACAJU");
            dicionario.Add("BA", "SALVADOR");

            dicionario.Add("MT", "CUIABA");
            dicionario.Add("MS", "CAMPO GRANDE");
            dicionario.Add("DF", "BRASILIA");
            dicionario.Add("GO", "GOIANIA");

            dicionario.Add("MG", "BELO HORIZONTE");
            dicionario.Add("ES", "VITORIA");
            dicionario.Add("RJ", "RIO DE JANEIRO");
            dicionario.Add("SP", "SAO PAULO");

            dicionario.Add("PR", "CURITIBA");
            dicionario.Add("SC", "PORTO ALEGRE");
            dicionario.Add("RS", "FLORIANOPOLIS");

            return dicionario;
        }
        #endregion

        #region Dicionário de estados
        private static Dictionary<string, Regiao> CriaDicionarioRegiao()
        {
            Dictionary<string, Regiao> dicionario = new Dictionary<string, Regiao>();

            dicionario.Add("AM", Regiao.Norte);
            dicionario.Add("PA", Regiao.Norte);
            dicionario.Add("AP", Regiao.Norte);
            dicionario.Add("AC", Regiao.Norte);
            dicionario.Add("RO", Regiao.Norte);
            dicionario.Add("RR", Regiao.Norte);
            dicionario.Add("TO", Regiao.Norte);

            dicionario.Add("CE", Regiao.Nordeste);
            dicionario.Add("PI", Regiao.Nordeste);
            dicionario.Add("MA", Regiao.Nordeste);
            dicionario.Add("RN", Regiao.Nordeste);
            dicionario.Add("PB", Regiao.Nordeste);
            dicionario.Add("PE", Regiao.Nordeste);
            dicionario.Add("AL", Regiao.Nordeste);
            dicionario.Add("SE", Regiao.Nordeste);
            dicionario.Add("BA", Regiao.Nordeste);

            dicionario.Add("MT", Regiao.CentroOeste);
            dicionario.Add("MS", Regiao.CentroOeste);
            dicionario.Add("DF", Regiao.CentroOeste);
            dicionario.Add("GO", Regiao.CentroOeste);

            dicionario.Add("MG", Regiao.Sudeste);
            dicionario.Add("ES", Regiao.Sudeste);
            dicionario.Add("RJ", Regiao.Sudeste);
            dicionario.Add("SP", Regiao.Sudeste);

            dicionario.Add("PR", Regiao.Sul);
            dicionario.Add("SC", Regiao.Sul);
            dicionario.Add("RS", Regiao.Sul);

            return dicionario;
        }
        #endregion

        #region Define informações
        public enum TipoLocalidade
        {
            Capital,
            Interior,
            CAPGV,
            Vazio
        }

        public enum Regiao
        {
            CentroOeste,
            Nordeste,
            Norte,
            Sudeste,
            Sul,
            Vazio
        }

        public static Dictionary<string, string> DicionarioCapitais = CriaDicionarioCapitais();
        public static Dictionary<string, Regiao> DicionarioRegiao = CriaDicionarioRegiao();

        public static Regiao RetornaRegiao(string estado)
        {
            if(estado == null)
                return Regiao.Vazio;
            else if (estado == "NULL" || estado == "")
                return Regiao.Vazio;
            else
                return DicionarioRegiao[estado];
        }

        public static bool EhCapital(string cidade)
        {
            bool flag = false;

            foreach (var chave in DicionarioCapitais)
            {
                string cidadeForm = Util.RemoveAcentos(cidade).Trim();

                if (chave.Value == cidade || Util.RemoveAcentos(chave.Value) == cidadeForm)
                    flag = true;
            }

            return flag;
        }

        public static Regiao RetornaRegiaoNome(string nome)
        {
            switch (nome)
            {
                case "CENTRO-OESTE":
                    return Regiao.CentroOeste;
                case "NORDESTE":
                    return Regiao.Nordeste;
                case "SUDESTE":
                    return Regiao.Sudeste;
                case "SUL":
                    return Regiao.Sul;
                case "NORTE":
                    return Regiao.Norte;

                default:
                    return Regiao.Vazio;
            }
        }

        public static string RetornaRegiaoNome(Regiao regiao)
        {
            switch (regiao)
            {
                case Regiao.CentroOeste:
                    return "CENTRO-OESTE";
                case Regiao.Nordeste:
                    return "NORDESTE";
                case Regiao.Sudeste:
                    return "SUDESTE";
                case Regiao.Sul:
                    return "SUL";
                case Regiao.Norte:
                    return "NORTE";

                default:
                    return "";
            }
        }

        public static TipoLocalidade RetornaTipoLocalidadeNome(string nome)
        {
            switch (nome)
            {
                case "CAPITAL":
                    return TipoLocalidade.Capital;
                case "INTERIOR":
                    return TipoLocalidade.Interior;
                case "CAPGV":
                    return TipoLocalidade.CAPGV;

                default:
                    return TipoLocalidade.Vazio;
            }
        }
        #endregion
    }
}