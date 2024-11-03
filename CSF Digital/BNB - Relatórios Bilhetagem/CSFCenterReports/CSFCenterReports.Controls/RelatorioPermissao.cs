using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CSFCenterReports.Controls
{
    public class RelatorioPermissao
    {
        #region Atributos
        private string _Pasta;

        #endregion

        #region Métodos Get / Set
        public string Pasta
        {
            get { return _Pasta; }
            set { _Pasta = value; }
        }

        #endregion
    }
}