using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dnaPrint.Web.App_Code
{
    public class Account
    {
        #region Campos

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Grupo { get; set; }

        #endregion

        public static bool Validar(string nome, string senha)
        {
            bool result = true;
            return result;
        }
    }
}