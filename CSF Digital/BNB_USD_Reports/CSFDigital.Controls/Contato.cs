using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSFDigital.Controls
{
    public class Contato
    {
        #region Atributos
        private string _nome;
        private Localidade _localidade;
        #endregion

        #region Métodos Get / Set
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public Localidade LocalContato
        {
            get { return _localidade; }
            set { _localidade = value; }
        }
        #endregion
    }
}
