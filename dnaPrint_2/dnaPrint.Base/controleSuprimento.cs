using dnaPrint.DAO;
using System;
using System.Collections.Generic;
using System.Data;

namespace dnaPrint.Base
{
    public class controleSuprimento : IBase<controleSuprimento>
    {
        #region Campos

        public enum TipoSuprimento { Toner, Cilindro}
        public enum Status { Em_uso, Substituido, Todos }

        public int controleSuprimentoID { get; set; }
        public TipoSuprimento Tipo { get; set; }
        public string Serial { get; set; }
        public int ContInicial { get; set; }
        public int ContFinal { get; set; }
        public DateTime DtInicial { get; set; }
        public DateTime DtFinal { get; set; }
        public int SuprimentoTotal { get; set; }
        public int SuprimentoAtual { get; set; }
        public int SuprimentoMedio { get; set; }
        public int SuprimentoValor { get; set; }
        public Status StatusSuprimento { get; set; }
        public int MediaDiaria { get; set; }
        public int DuracaoEstimada { get; set; }
        public string serie { get; set; }

        #endregion

        public controleSuprimento()
        {

        }

        public controleSuprimento(TipoSuprimento Suprimento, string SerialSuprimento, int ContadorInicial,int _SuprimentoTotal, int _SuprimentoAtual, int MediaDiaria)
        {
            this.Tipo = Suprimento;
            this.Serial = SerialSuprimento;
            this.ContInicial = ContadorInicial;
            this.SuprimentoTotal = _SuprimentoTotal;
            this.SuprimentoAtual = _SuprimentoAtual;
            this.MediaDiaria = MediaDiaria;
        }

        public bool Adicionar(string connString, Operacoes.tipo TipoDB)
        {
            Excluir(connString, TipoDB);
            CalcularSuprimentoMedio(connString, TipoDB, this.Tipo);

            bool result = false;
            int qtdlinhas = 0;
            string tsql = "INSERT INTO controleSuprimentos(suprimento, serial, contInicial, suprimentoTotal, suprimentoAtual, status, mediaDiaria, suprimentoValor, serie) VALUES(@suprimento, @serial, @contInicial, @suprimentoTotal, @suprimentoAtual, @status, @mediaDiaria, @suprimentoValor, @serie);";

            List<string[]> parametros = new List<string[]>();

            #region Parametros

            parametros.Add(new string[] {"@suprimento", this.Tipo.ToString()});
            parametros.Add(new string[] {"@serial", this.Serial });
            parametros.Add(new string[] {"@contInicial", this.ContInicial.ToString() });
            parametros.Add(new string[] {"@suprimentoTotal", this.SuprimentoTotal.ToString() });
            parametros.Add(new string[] {"@suprimentoAtual", this.SuprimentoAtual.ToString() });
            parametros.Add(new string[] {"@status", this.StatusSuprimento.ToString()});
            parametros.Add(new string[] {"@mediaDiaria", this.MediaDiaria.ToString() });
            parametros.Add(new string[] {"@serie", this.serie});
            parametros.Add(new string[] { "@suprimentoValor", ((float.Parse(this.SuprimentoAtual.ToString()) / float.Parse(this.SuprimentoAtual.ToString())) * 100).ToString()});

            #endregion
            if (this.SuprimentoTotal > 0)
                qtdlinhas = new Operacoes(connString, TipoDB).ExecuteNonQuery(tsql, parametros);

            if (qtdlinhas > 0)
                result = true;

            return result;
        }

        public bool Atualizar(string connString, Operacoes.tipo TipoDB)
        {
            bool result = false;
            string tsql = "update controleSuprimentos set contFinal = @contFinal, suprimentoAtual = @suprimentoAtual, mediaDiaria = @mediaDiaria, producaoSuprimento = (contFinal - contInicial), status = @status where idControle = @id;";

            #region Parametros

            List<string[]> parametros = new List<string[]>();
            parametros.Add(new string[] { "@contFinal", this.ContFinal.ToString() });
            parametros.Add(new string[] { "@suprimentoAtual", this.SuprimentoAtual.ToString() });
            parametros.Add(new string[] { "@status", this.StatusSuprimento.ToString() });
            parametros.Add(new string[] { "@mediaDiaria", this.MediaDiaria.ToString() });
            parametros.Add(new string[] { "@id", this.controleSuprimentoID.ToString()});

            #endregion
            int qtdLinhas = 0;

            qtdLinhas = new Operacoes(connString, TipoDB).ExecuteNonQuery(tsql, parametros);

            if (qtdLinhas > 0)
                result = true;

            return result;
        }


        public bool Excluir(string connString, Operacoes.tipo TipoDB)
        {
            string tsql = $"update controleSuprimentos set status = '{Status.Substituido.ToString()}', dtFinal = '{DateTime.Now.ToString("yyyyMMdd")}' where suprimento = '{this.Tipo.ToString()}' and serie = '{this.serie}' and status = '{Status.Em_uso.ToString()}';";
            bool result = false;
            int qtdLinhas = 0;

            qtdLinhas = new Operacoes(connString, TipoDB).ExecuteNonQuery(tsql);
            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public static controleSuprimento BuscarPorSerial(string connString, Operacoes.tipo TipoDB, string serial)
        {
            controleSuprimento c = new controleSuprimento();
            DataTable dt = new DataTable();
            string tsql = $"select * from controleSuprimentos where serial = '{serial}' and status = '{Status.Em_uso.ToString()}';";

            dt = new Operacoes(connString, TipoDB).ReturnDt(tsql);

            int iTemp = 0;
            DateTime dTemp = DateTime.Now;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow linha in dt.Rows)
                {
                    c.controleSuprimentoID = int.Parse(linha["id"].ToString());

                    switch (linha["suprimento"].ToString())
                    {
                        case "Toner":
                            c.Tipo = TipoSuprimento.Toner;
                            break;
                        case "Cilindro":
                            c.Tipo = TipoSuprimento.Cilindro;
                            break;
                        default:
                            c.Tipo = TipoSuprimento.Toner;
                            break;
                    }

                    c.Serial = linha["serial"].ToString();
                    c.ContInicial = int.Parse(linha["contInicial"].ToString());
                    c.serie = linha["serie"].ToString();

                    if (int.TryParse(linha["contFinal"].ToString(), out iTemp))
                    {
                        c.ContFinal = int.Parse(linha["contFinal"].ToString());
                    }

                    c.DtInicial = DateTime.Parse(linha["dtInicial"].ToString());


                    if (DateTime.TryParse(linha["dtFinal"].ToString(), out dTemp))
                    {
                        c.DtFinal = DateTime.Parse(linha["dtFinal"].ToString());
                    }

                    c.SuprimentoTotal = int.Parse(linha["suprimentoTotal"].ToString());
                    c.SuprimentoAtual = int.Parse(linha["suprimentoAtual"].ToString());
                    c.SuprimentoValor = int.Parse(linha["suprimentoValor"].ToString());

                    switch (linha["status"].ToString())
                    {
                        case "Em_uso":
                            c.StatusSuprimento = Status.Em_uso;
                            break;
                        case "Substituido":
                            c.StatusSuprimento = Status.Substituido;
                            break;
                        default:
                            break;
                    }

                    c.MediaDiaria = int.Parse(linha["mediaDiaria"].ToString());
                    if (int.TryParse(linha["duracaoEstimada"].ToString(), out iTemp))
                    {
                        c.DuracaoEstimada = iTemp;
                    }
                }

                return c;
            }
            else
            {
                return null;
            }
            
        }

        public static controleSuprimento BuscarPorSerial(string connString, Operacoes.tipo TipoDB, string serial, string serieEqpto)
        {
            controleSuprimento c = new controleSuprimento();
            DataTable dt = new DataTable();
            string tsql = $"select * from controleSuprimentos where serial = '{serial}' and status = '{Status.Em_uso.ToString()}' and serie = '{serieEqpto}';";

            dt = new Operacoes(connString, TipoDB).ReturnDt(tsql);

            int iTemp = 0;
            DateTime dTemp = DateTime.Now;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow linha in dt.Rows)
                {
                    c.controleSuprimentoID = int.Parse(linha["idControle"].ToString());

                    switch (linha["suprimento"].ToString())
                    {
                        case "Toner":
                            c.Tipo = TipoSuprimento.Toner;
                            break;
                        case "Cilindro":
                            c.Tipo = TipoSuprimento.Cilindro;
                            break;
                        default:
                            c.Tipo = TipoSuprimento.Toner;
                            break;
                    }

                    c.Serial = linha["serial"].ToString();
                    c.ContInicial = int.Parse(linha["contInicial"].ToString());
                    c.serie = linha["serie"].ToString();

                    if (int.TryParse(linha["contFinal"].ToString(), out iTemp))
                    {
                        c.ContFinal = int.Parse(linha["contFinal"].ToString());
                    }

                    c.DtInicial = DateTime.Parse(linha["dtInicial"].ToString());


                    if (DateTime.TryParse(linha["dtFinal"].ToString(), out dTemp))
                    {
                        c.DtFinal = DateTime.Parse(linha["dtFinal"].ToString());
                    }

                    c.SuprimentoTotal = int.Parse(linha["suprimentoTotal"].ToString());
                    c.SuprimentoAtual = int.Parse(linha["suprimentoAtual"].ToString());
                    c.SuprimentoValor = int.Parse(linha["suprimentoValor"].ToString());

                    switch (linha["status"].ToString())
                    {
                        case "Em_uso":
                            c.StatusSuprimento = Status.Em_uso;
                            break;
                        case "Substituido":
                            c.StatusSuprimento = Status.Substituido;
                            break;
                        default:
                            break;
                    }

                    c.MediaDiaria = int.Parse(linha["mediaDiaria"].ToString());
                    if (int.TryParse(linha["duracaoEstimada"].ToString(), out iTemp))
                    {
                        c.DuracaoEstimada = iTemp;
                    }
                }

                return c;
            }
            else
            {
                return null;
            }

        }

        public void CalcularSuprimentoMedio(string connString, Operacoes.tipo TipoDB, TipoSuprimento Suprimento)
        {
            string tsql = $"select AVG(producaoSuprimento) from  controleSuprimentos WHERE suprimento = '{Suprimento}' and status = 'Substituido'";
            object valor = null;

            valor = new Operacoes(connString, TipoDB).ExecuteScalar(tsql);

            int i = 0;
            if (int.TryParse(valor.ToString(), out i))
            {
                this.SuprimentoMedio = i;
            }
        }

        public static List<controleSuprimento> ListarEmUso(string connString, Operacoes.tipo TipoDB)
        {
            List<controleSuprimento> Lista = new List<controleSuprimento>();
            string tsql = @"select id, suprimento, serial, contInicial, contFinal, producaoSuprimento, dtInicial, dtFinal, suprimentoTotal, suprimentoAtual, suprimentoMedio, suprimentoValor, status, mediaDiaria, duracaoEstimada from controleSuprimentos where status = 'Em_uso'";

            DataTable dt = new DataTable();
            dt = new Operacoes(connString, TipoDB).ReturnDt(tsql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    controleSuprimento c = new controleSuprimento();
                    c.controleSuprimentoID = int.Parse(row["id"].ToString());
                    c.Serial = row["serial"].ToString();
                    c.Tipo = DefinirTipoSuprimento(row["suprimento"].ToString());
                    c.ContInicial = int.Parse(row["contInicial"].ToString());
                    c.DtInicial = DateTime.Parse(row["dtInicial"].ToString());
                    c.SuprimentoTotal = int.Parse(row["suprimentoTotal"].ToString());
                    c.SuprimentoAtual = int.Parse(row["suprimentoAtual"].ToString());
                    int intTemp = 0;
                    if (int.TryParse(row["suprimentoMedio"].ToString(), out intTemp))
                        c.SuprimentoMedio = intTemp;
                    else
                        c.SuprimentoAtual = 0;

                    c.SuprimentoValor = int.Parse(row["suprimentoValor"].ToString());
                    c.MediaDiaria = int.Parse(row["mediaDiaria"].ToString());
                    if (int.TryParse(row["duracaoEstimada"].ToString(), out intTemp))
                        c.DuracaoEstimada = intTemp;
                    else
                        c.DuracaoEstimada = 0;
                    Lista.Add(c);
                }
            }
            return Lista;
        }

        private static TipoSuprimento DefinirTipoSuprimento(string suprimento)
        {
            if (TipoSuprimento.Cilindro.ToString().Equals(suprimento))
                return TipoSuprimento.Cilindro;
            else
                return TipoSuprimento.Toner;
        }

        public controleSuprimento ListarByID(string connString, Operacoes.tipo TipoDB, int id)
        {
            string tsql = $"select id, suprimento, serial, contInicial, contFinal, producaoSuprimento, dtInicial, dtFinal, suprimentoTotal, suprimentoAtual, suprimentoMedio, suprimentoValor, status, mediaDiaria, duracaoEstimada from controleSuprimentos where id = {id}";

            DataTable dt = new DataTable();
            dt = new Operacoes(connString, TipoDB).ReturnDt(tsql);
            controleSuprimento c = new controleSuprimento();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    
                    c.controleSuprimentoID = int.Parse(row["id"].ToString());
                    c.Serial = row["serial"].ToString();
                    c.Tipo = DefinirTipoSuprimento(row["suprimento"].ToString());
                    c.ContInicial = int.Parse(row["contInicial"].ToString());
                    c.DtInicial = DateTime.Parse(row["dtInicial"].ToString());
                    c.SuprimentoTotal = int.Parse(row["suprimentoTotal"].ToString());
                    c.SuprimentoAtual = int.Parse(row["suprimentoAtual"].ToString());
                    int intTemp = 0;
                    if (int.TryParse(row["suprimentoMedio"].ToString(), out intTemp))
                        c.SuprimentoMedio = intTemp;
                    else
                        c.SuprimentoAtual = 0;

                    c.SuprimentoValor = int.Parse(row["suprimentoValor"].ToString());
                    c.MediaDiaria = int.Parse(row["mediaDiaria"].ToString());
                    if (int.TryParse(row["duracaoEstimada"].ToString(), out intTemp))
                        c.DuracaoEstimada = intTemp;
                    else
                        c.DuracaoEstimada = 0;
                }
            }
            return c;
        }
    }
}
