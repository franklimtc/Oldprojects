using System;
using System.Collections.Generic;
using dnaPrint.DAO;
using System.Data;

namespace dnaPrint.Base
{
    public class Estacao
    {
        #region Campos

        public int ID { get; set; }
        public string Nome { get; set; }
        public string Versao { get; set; }
        public string dtPrimeiroInv { get; set; }
        public string dtUltimoInv { get; set; }

        public int QtdDias
        {
            get
            {
                int qtdDias = -1;
                if (!string.IsNullOrEmpty(dtPrimeiroInv) && !string.IsNullOrEmpty(dtUltimoInv))
                {
                    qtdDias  = (DateTime.Parse(DateTime.Parse(dtUltimoInv).ToShortDateString()) - DateTime.Parse(DateTime.Parse(dtPrimeiroInv).ToShortDateString())).Days;
                }
                return qtdDias;
            }
        }
        #endregion

        public static List<Estacao> Listar(string connString, Operacoes.tipo TipoDB)
        {
            List<Estacao> Lista = new List<Estacao>();
            string tsql = "select id, nome, agente_versao, dt_primeiro_inv, dt_ultimo_inv from estacao where ativo = '1'";

            DataTable dt = new DataTable();
            DAO.Operacoes database = new DAO.Operacoes(connString, TipoDB);
            dt = database.ReturnDt(tsql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow estacao in dt.Rows)
                {
                    Estacao e = new Estacao();
                    e.ID = int.Parse(estacao["id"].ToString());
                    e.Nome = estacao["nome"].ToString();
                    e.Versao = estacao["agente_versao"].ToString();
                    e.dtPrimeiroInv = estacao["dt_primeiro_inv"].ToString();
                    e.dtUltimoInv = estacao["dt_ultimo_inv"].ToString();
                    Lista.Add(e);
                }
            }
            return Lista;
        }

    }
}
