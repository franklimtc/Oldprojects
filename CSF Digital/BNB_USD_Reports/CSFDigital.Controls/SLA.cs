using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace CSFDigital.Controls
{
    public class SLA
    {
        private static List<SLA> Quadro_Assistencia = new List<SLA>();
        private static List<SLA> Quadro_Toner = new List<SLA>();

        #region Atributos
        private int _id;
        private int _prioridade;
        private Chamado.Severidade _nivel;
        private TimeSpan _limite;
        private TimeSpan _limiteContingencia;
        private Localidade.TipoLocalidade _local;
        private Localidade.Regiao _localRegiao;
        private string _deAcordo;
        private string _deAcordoContingencia;
        private string _excedido;
        #endregion

        #region Métodos Get / Set
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public int Prioridade
        {
            get { return _prioridade; }
            set { _prioridade = value; }
        }
        public Chamado.Severidade Nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }
        public TimeSpan Limite
        {
            get { return _limite; }
            set { _limite = value; }
        }
        public string LimiteFormatado
        {
            get { return Util.FormatarTimeSpan(_limite); }
            set
            {
                TimeSpan tempo = TimeSpan.Zero;
                try
                {
                    string[] ArrayTempo = value.Split(':');

                    tempo = new TimeSpan(int.Parse(ArrayTempo[0]), int.Parse(ArrayTempo[1]), int.Parse(ArrayTempo[2]));
                }
                finally
                {
                    _limite = tempo;
                }
            }
        }
        public TimeSpan LimiteContingencia
        {
            get { return _limiteContingencia; }
            set { _limiteContingencia = value; }
        }
        public string LimiteContingenciaFormatado
        {
            get { return Util.FormatarTimeSpan(_limiteContingencia); }
            set
            {
                TimeSpan tempo = TimeSpan.Zero;
                try
                {
                    string[] ArrayTempo = value.Split(':');

                    tempo = new TimeSpan(int.Parse(ArrayTempo[0]), int.Parse(ArrayTempo[1]), int.Parse(ArrayTempo[2]));
                }
                finally
                {
                    _limiteContingencia = tempo;
                }
            }
        }
        public Localidade.TipoLocalidade Local
        {
            get { return _local; }
            set { _local = value; }
        }
        public string LocalFormatado
        {
            get
            {
                if (_local == Localidade.TipoLocalidade.Vazio)
                    return "";
                else
                    return _local.ToString();
            }
        }
        public Localidade.Regiao LocalRegiao
        {
            get { return _localRegiao; }
            set { _localRegiao = value; }
        }
        public string DeAcordo
        {
            get { return _deAcordo; }
            set { _deAcordo = value; }
        }
        public string DeAcordoContingencia
        {
            get {
                if (System.Configuration.ConfigurationManager.AppSettings["ContingenciaAtiva"] == "N")
                    return "---";
                else
                    return _deAcordoContingencia; 
            }
            set { _deAcordoContingencia = value; }
        }
        public string Excedido
        {
            get { return _excedido; }
            set { _excedido = value; }
        }
        #endregion

        #region Construtores
        public SLA(int id, int prioridade, Chamado.Severidade nivel, TimeSpan limite, Localidade.TipoLocalidade local, Localidade.Regiao localRegiao, string deAcordo)
        {
            _id = id;
            _prioridade = prioridade;
            _nivel = nivel;
            _limite = limite;
            _local = local;
            _localRegiao = localRegiao;
            _deAcordo = deAcordo;
        }

        public SLA(int id, int prioridade, Chamado.Severidade nivel,Localidade.TipoLocalidade local, TimeSpan limite, TimeSpan limiteContingencia)
        {
            _id = id;
            _prioridade = prioridade;
            _nivel = nivel;
            _local = local;
            _limite = limite;
            _limiteContingencia = limiteContingencia;
        }

        public SLA(int id, Localidade.Regiao localRegiao, Localidade.TipoLocalidade local, TimeSpan limite)
        {
            _id = id;
            _localRegiao = localRegiao;
            _local = local;
            _limite = limite;
        }
        #endregion

        #region Constroi SLA's
        public static List<SLA> SLA_AtendimentoTecnico()
        {
            return Quadro_Assistencia;
        }

        public static List<SLA> SLA_EntregaInsumos()
        {
            return Quadro_Toner;
        }

        public static void RetornarSLA(string diretorio, bool toner)
        {
            string diretorioXML = diretorio;

            List<SLA> SLAs = new List<SLA>();

            if (File.Exists(diretorioXML))
            {
                DataSet ds = new DataSet();

                try
                {
                    ds.ReadXml(diretorioXML);

                    if (ds.Tables.Contains("SLA"))
                    {
                        DataTable dt = ds.Tables["SLA"];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (toner)
                            {
                                int Id = 0;
                                Localidade.Regiao Regiao = Localidade.Regiao.Vazio;
                                Localidade.TipoLocalidade TipoLocalidade = Localidade.TipoLocalidade.Vazio;
                                TimeSpan Limite = TimeSpan.Zero;

                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    if (dt.Columns[j].Caption == "Id")
                                        Id = int.Parse(dt.Rows[i].ItemArray[j].ToString());
                                    if (dt.Columns[j].Caption == "Regiao")
                                        Regiao = Localidade.RetornaRegiaoNome(dt.Rows[i].ItemArray[j].ToString().ToUpper());
                                    if (dt.Columns[j].Caption == "TipoLocalidade")
                                        TipoLocalidade = Localidade.RetornaTipoLocalidadeNome(dt.Rows[i].ItemArray[j].ToString().ToUpper());
                                    if (dt.Columns[j].Caption == "Limite")
                                    {
                                        string[] tempo = dt.Rows[i].ItemArray[j].ToString().Trim().Split(':');

                                        Limite = new TimeSpan(int.Parse(tempo[0]), int.Parse(tempo[1]), int.Parse(tempo[2]));
                                    }
                                }

                                SLA sla = new SLA(Id, Regiao, TipoLocalidade, Limite);
                                SLAs.Add(sla);
                            }
                            else
                            {
                                int Id = 0;
                                int Prioridade = 0;
                                Chamado.Severidade Severidade = Chamado.Severidade.Baixa;
                                Localidade.TipoLocalidade TipoLocalidade = Localidade.TipoLocalidade.Vazio;
                                TimeSpan Limite = TimeSpan.Zero;
                                TimeSpan LimiteContingencia = TimeSpan.Zero;

                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    if (dt.Columns[j].Caption == "Id")
                                        Id = int.Parse(dt.Rows[i].ItemArray[j].ToString());
                                    if (dt.Columns[j].Caption == "Prioridade")
                                    {
                                        Prioridade = int.Parse(dt.Rows[i].ItemArray[j].ToString());
                                        Severidade = Chamado.RetornaNivelSeveridade(Prioridade);
                                    }
                                    if (dt.Columns[j].Caption == "TipoLocalidade")
                                        TipoLocalidade = Localidade.RetornaTipoLocalidadeNome(dt.Rows[i].ItemArray[j].ToString().ToUpper());
                                    if (dt.Columns[j].Caption == "Limite")
                                    {
                                        string[] tempo = dt.Rows[i].ItemArray[j].ToString().Trim().Split(':');

                                        Limite = new TimeSpan(int.Parse(tempo[0]), int.Parse(tempo[1]), int.Parse(tempo[2]));
                                    }
                                    if (dt.Columns[j].Caption == "LimiteContingencia")
                                    {
                                        string[] tempo = dt.Rows[i].ItemArray[j].ToString().Trim().Split(':');

                                        LimiteContingencia = new TimeSpan(int.Parse(tempo[0]), int.Parse(tempo[1]), int.Parse(tempo[2]));
                                    }
                                }

                                SLA sla = new SLA(Id, Prioridade, Severidade, TipoLocalidade, Limite, LimiteContingencia);
                                SLAs.Add(sla);
                            }                            
                        }
                    }
                }
                catch (Exception ex)
                { }
                finally
                { }
            }

            if (toner)
                Quadro_Toner = SLAs;
            else
                Quadro_Assistencia = SLAs;
        }
        #endregion

        #region Calcula SLA

        #region Regra 1 - Assistência Técnica
        public static TimeSpan timeInicialValidoR1 = new TimeSpan(8, 0, 0); //08:00 hrs
        public static TimeSpan timeFinalValidoR1 = new TimeSpan(18, 00, 0); //18:00 hrs

        public static TimeSpan CalcularR1(DateTime dataInicial, DateTime dataFinal, string cidade, string estado)
        {
            TimeSpan tempoTotal = TimeSpan.Zero;

            if (dataInicial.Date == dataFinal.Date)
            {
                if (DiaUtil(dataInicial, cidade, estado))
                {
                    tempoTotal += CalcularTempoRealR1(new TimeSpan(dataInicial.Hour, dataInicial.Minute, dataInicial.Second),
                                                    new TimeSpan(dataFinal.Hour, dataFinal.Minute, dataFinal.Second));
                }
            }
            else if (dataFinal.Date > dataInicial.Date)
            {
                if (DiaUtil(dataInicial, cidade, estado))
                    tempoTotal += CalcularTempoRealR1(new TimeSpan(dataInicial.Hour, dataInicial.Minute, dataInicial.Second), null);

                DateTime dataControl = dataInicial.Date.AddDays(1);

                while (dataControl.Date < dataFinal.Date)
                {
                    if (DiaUtil(dataControl, cidade, estado))
                        tempoTotal += CalcularTempoRealR1(null, null); //Calcula um dia inteiro
                    dataControl = dataControl.AddDays(1);
                }

                if (DiaUtil(dataFinal, cidade, estado))
                    tempoTotal += CalcularTempoRealR1(null, new TimeSpan(dataFinal.Hour, dataFinal.Minute, dataFinal.Second));
            }
            return tempoTotal;
        }

        private static TimeSpan CalcularTempoRealR1(TimeSpan? timeInicial, TimeSpan? timeFinal)
        {
            TimeSpan timeRealInicial = timeInicial.HasValue ? ObterTempoRealR1(timeInicial.Value) : timeInicialValidoR1;
            TimeSpan timeRealFinal = timeFinal.HasValue ? ObterTempoRealR1(timeFinal.Value) : timeFinalValidoR1;

            return timeRealFinal.Subtract(timeRealInicial);
        }

        private static TimeSpan ObterTempoRealR1(TimeSpan time)
        {
            TimeSpan retorno = time;

            if (((time.Hours == timeInicialValidoR1.Hours) && (time.Minutes < timeInicialValidoR1.Minutes)) || (time.Hours < timeInicialValidoR1.Hours)) //antes das 08:00h
                retorno = timeInicialValidoR1; //seta a hora inicial valida (08:00h)
            else if (((time.Hours == timeFinalValidoR1.Hours) && (time.Minutes > timeFinalValidoR1.Minutes)) || (time.Hours > timeFinalValidoR1.Hours)) //depois das 18:00h
                retorno = timeFinalValidoR1;  //seta a hora final valida (18:00h)

            return retorno;
        }
        #endregion

        #region Regra 2 - Assistência Técnica
        public static TimeSpan timeInicialValidoR2 = new TimeSpan(8, 0, 0);       //08:00 hrs
        public static TimeSpan timeInicialValidoCorteR2 = new TimeSpan(12, 0, 0); //12:00 hrs
        public static TimeSpan timeFinalValidoCorteR2 = new TimeSpan(14, 0, 0);   //14:00 hrs
        public static TimeSpan timeFinalValidoR2 = new TimeSpan(18, 0, 0);        //18:00 hrs

        public static TimeSpan CalcularR2(DateTime dataInicial, DateTime dataFinal, string cidade, string estado)
        {
            TimeSpan tempoTotal = TimeSpan.Zero;

            if (dataInicial.Date == dataFinal.Date)
            {
                if (DiaUtil(dataInicial, cidade, estado))
                {
                    tempoTotal += CalcularTempoRealR2(
                        new TimeSpan(dataInicial.Hour, dataInicial.Minute, dataInicial.Second),
                        new TimeSpan(dataFinal.Hour, dataFinal.Minute, dataFinal.Second)); ;
                }
            }
            else if (dataFinal.Date > dataInicial.Date)
            {
                if (DiaUtil(dataInicial, cidade, estado))
                {
                    tempoTotal += CalcularTempoRealR2(
                        new TimeSpan(dataInicial.Hour, dataInicial.Minute, dataInicial.Second),
                        null);
                }
                DateTime dataControl = dataInicial.Date.AddDays(1);

                while (dataControl.Date < dataFinal.Date)
                {
                    if (DiaUtil(dataControl, cidade, estado))
                    {
                        tempoTotal += CalcularTempoRealR2(null, null); //Calcula um dia inteiro
                    }
                    dataControl = dataControl.AddDays(1);
                }

                if (DiaUtil(dataFinal, cidade, estado))
                {
                    tempoTotal += CalcularTempoRealR2(
                        null,
                        new TimeSpan(dataFinal.Hour, dataFinal.Minute, dataFinal.Second));
                }
            }
            return tempoTotal;
        }
        private static TimeSpan CalcularTempoRealR2(TimeSpan? timeInicial, TimeSpan? timeFinal)
        {
            TimeSpan timeRealInicial = timeInicial.HasValue ? ObterTempoRealR2(timeInicial.Value) : timeInicialValidoR2;
            TimeSpan timeRealFinal = timeFinal.HasValue ? ObterTempoRealR2(timeFinal.Value) : timeFinalValidoR2;

            if (timeRealInicial > timeInicialValidoCorteR2 && timeRealInicial < timeFinalValidoCorteR2)
                timeRealInicial = timeFinalValidoCorteR2;//seta a hora inicial para (14:00h)

            if (timeRealFinal > timeInicialValidoCorteR2 && timeRealFinal < timeFinalValidoCorteR2)
                timeRealFinal = timeInicialValidoCorteR2;//seta a hora final para (12:00h)

            if (timeRealInicial >= timeInicialValidoR2 && timeRealInicial < timeInicialValidoCorteR2 &&
                timeRealFinal > timeFinalValidoCorteR2 && timeRealFinal <= timeFinalValidoR2)//Desconta duas horas
                return timeRealFinal.Subtract(timeRealInicial).Subtract(timeFinalValidoCorteR2.Subtract(timeInicialValidoCorteR2));
            else
            {
                if (timeRealInicial > timeRealFinal)
                    return TimeSpan.Zero;
                else
                    return timeRealFinal.Subtract(timeRealInicial);
            }
        }
        private static TimeSpan ObterTempoRealR2(TimeSpan time)
        {
            TimeSpan retorno = time;

            if (((time.Hours == timeInicialValidoR2.Hours) && (time.Minutes < timeInicialValidoR2.Minutes)) || (time.Hours < timeInicialValidoR2.Hours))
            {//antes das 08:00h
                retorno = timeInicialValidoR2; //seta a hora inicial valida (08:00h)
            }
            else if (((time.Hours == timeFinalValidoR2.Hours) && (time.Minutes > timeFinalValidoR2.Minutes)) || (time.Hours > timeFinalValidoR2.Hours))
            {//depois das 18:00h
                retorno = timeFinalValidoR2;  //seta a hora final valida (18:00h)
            }
            return retorno;
        }

        #endregion

        #region Regra 3 - Solicitação de Toner
        public static TimeSpan CalcularR3(DateTime dataInicial, DateTime dataFinal, string cidade, string estado)
        {
            TimeSpan tempoTotal = TimeSpan.Zero;

            if (dataInicial.Date == dataFinal.Date)
            {
                if (DiaUtil(dataInicial, cidade, estado))
                {
                    tempoTotal += dataFinal.Subtract(dataInicial);
                }
            }
            else if (dataFinal.Date > dataInicial.Date)
            {
                if (DiaUtil(dataInicial, cidade, estado))
                    tempoTotal += dataInicial.Date.Add(new TimeSpan(1, 0, 0, 0)).Subtract(dataInicial);

                DateTime dataControl = dataInicial.Date.AddDays(1);

                while (dataControl.Date < dataFinal.Date)
                {
                    if (DiaUtil(dataControl, cidade, estado))
                        tempoTotal += new TimeSpan(1, 0, 0, 0);

                    dataControl = dataControl.AddDays(1);
                }

                if (DiaUtil(dataFinal, cidade, estado))
                    tempoTotal += dataFinal.Subtract(dataControl);
            }
            return tempoTotal;
        }
        #endregion

        private static bool DiaUtil(DateTime dia, string cidade, string estado)
        {
            if (dia.DayOfWeek != DayOfWeek.Saturday && dia.DayOfWeek != DayOfWeek.Sunday)
            {
                if ((Feriado.Feriados.Find(feriado => feriado.DataFeriado.Date == dia.Date && feriado.Cidade == "" && feriado.Estado == "") != null) ||
                   (Feriado.Feriados.Find(feriado => feriado.DataFeriado.Date == dia.Date && feriado.Cidade == cidade && feriado.Estado == estado) != null))
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        #endregion

        public static List<SLA> RetornaQuadro_Assistencia()
        {
            return Quadro_Assistencia;
        }

        public static List<SLA> RetornaQuadro_Toner()
        {
            return Quadro_Toner;
        }

        public static void RetornarSLAs_Assistencia(string diretorio)
        {
            Quadro_Assistencia = RetornarListaSLA(diretorio, false);
        }

        public static void RetornarSLAs_Toner(string diretorio)
        {
            Quadro_Assistencia = RetornarListaSLA(diretorio, true);
        }

        private static List<SLA> RetornarListaSLA(string diretorio, bool toner)
        {
            string diretorioXML = diretorio;

            List<SLA> SLAs = new List<SLA>();

            if (File.Exists(diretorioXML))
            {
                DataSet ds = new DataSet();

                try
                {
                    ds.ReadXml(diretorioXML);

                    if (ds.Tables.Contains("SLA"))
                    {
                        DataTable dt = ds.Tables["SLA"];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (toner)
                            {
                                int Id = 0;
                                string Regiao = "";
                                string TipoLocalidade = "";
                                TimeSpan Limite = TimeSpan.Zero;

                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    if (dt.Columns[j].Caption == "Id")
                                        Id = int.Parse(dt.Rows[i].ItemArray[j].ToString());
                                    if (dt.Columns[j].Caption == "Regiao")
                                        Regiao = dt.Rows[i].ItemArray[j].ToString();
                                    if (dt.Columns[j].Caption == "TipoLocalidade")
                                        TipoLocalidade = dt.Rows[i].ItemArray[j].ToString();
                                    if (dt.Columns[j].Caption == "Limite")
                                        Limite = TimeSpan.Parse(dt.Rows[i].ItemArray[j].ToString());
                                }

                                SLA sla = new SLA(Id, Localidade.RetornaRegiaoNome(Regiao), Localidade.RetornaTipoLocalidadeNome(TipoLocalidade), Limite);
                                SLAs.Add(sla);
                            }
                            else
                            {
                                int Id = 0;
                                int Prioridade = 0;
                                Chamado.Severidade Severidade;
                                string TipoLocalidade = "";
                                TimeSpan Limite = TimeSpan.Zero;
                                TimeSpan LimiteContingencia = TimeSpan.Zero;

                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    if (dt.Columns[j].Caption == "Id")
                                        Id = int.Parse(dt.Rows[i].ItemArray[j].ToString());
                                    if (dt.Columns[j].Caption == "Prioridade")
                                        Prioridade = int.Parse(dt.Rows[i].ItemArray[j].ToString());
                                    if (dt.Columns[j].Caption == "TipoLocalidade")
                                        TipoLocalidade = dt.Rows[i].ItemArray[j].ToString();
                                    if (dt.Columns[j].Caption == "Limite")
                                        Limite = TimeSpan.Parse(dt.Rows[i].ItemArray[j].ToString());
                                    if (dt.Columns[j].Caption == "LimiteContingencia")
                                        LimiteContingencia = TimeSpan.Parse(dt.Rows[i].ItemArray[j].ToString());
                                }

                                if (Prioridade == 1)
                                    Severidade = Chamado.Severidade.Alta;
                                else
                                    Severidade = Chamado.Severidade.Baixa;

                                SLA sla = new SLA(Id, Prioridade, Severidade, Localidade.RetornaTipoLocalidadeNome(TipoLocalidade), Limite, LimiteContingencia);
                                SLAs.Add(sla);
                            }
                        }
                    }
                }
                catch (Exception ex)
                { }
                finally
                { }
            }

            return SLAs;
        }
    }
}