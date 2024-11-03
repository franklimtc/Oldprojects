using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;


namespace Monitor
{
    class CepEqpto
    {
        #region Campos
        private string idEquipamento;
        private string cep;

        public string IdEquipamento
        {
            get
            {
                return idEquipamento;
            }

            set
            {
                idEquipamento = value;
            }
        }

        public string Cep
        {
            get
            {
                return cep;
            }

            set
            {
                cep = value;
            }
        }
        #endregion
        public CepEqpto(string cep)
        {
            this.Cep = cep;
        }

        public static List<CepEqpto> Listar()
        {
            List <CepEqpto> lista = new List<CepEqpto>();
            string tsql = "select distinct cep from equipamentos";
            DataTable dtCeps = DAO.retornaDt(tsql);
            foreach (DataRow cep in dtCeps.Rows)
            {
                lista.Add(new CepEqpto(cep["cep"].ToString()));
            }

            return lista;
        }
        public void Validar(bool valido)
        {
            string tsqlUpdate = null;

            if (valido)
            {
                tsqlUpdate = string.Format("update equipamentos set cepValido = 1 where cep = '{0}';", this.Cep);
            }
            else
            {
                tsqlUpdate = string.Format("update equipamentos set cepValido = 0 where cep = '{0}';", this.Cep);
            }
            DAO.Execute(tsqlUpdate);
        }

        internal bool Valido()
        {
            return CSF_Correios.Eventos.cepValido(this.Cep);
        }
    }
}
