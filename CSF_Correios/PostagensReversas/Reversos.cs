using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostagensReversas
{
    public class Reversos
    {
        #region Campos
        private string _idPostagemReversa;
        private string _cepOrigem;
        private string _cepDestino;
        private string _postagem;
        private string _operador;
        private string _data;
        private string _dtEnvio;
        private string _dtEntrega;
        private string _prazoEntrega;
        private string _entregueEm;
        private string _verificado;

        public string IdPostagemReversa
        {
            get
            {
                return _idPostagemReversa;
            }

            set
            {
                _idPostagemReversa = value;
            }
        }

        public string CepOrigem
        {
            get
            {
                return _cepOrigem;
            }

            set
            {
                _cepOrigem = value;
            }
        }

        public string CepDestino
        {
            get
            {
                return _cepDestino;
            }

            set
            {
                _cepDestino = value;
            }
        }

        public string Postagem
        {
            get
            {
                return _postagem;
            }

            set
            {
                _postagem = value;
            }
        }

        public string Operador
        {
            get
            {
                return _operador;
            }

            set
            {
                _operador = value;
            }
        }

        public string Data
        {
            get
            {
                return _data;
            }

            set
            {
                _data = value;
            }
        }

        public string DtEnvio
        {
            get
            {
                return _dtEnvio;
            }

            set
            {
                _dtEnvio = value;
            }
        }

        public string DtEntrega
        {
            get
            {
                return _dtEntrega;
            }

            set
            {
                _dtEntrega = value;
            }
        }

        public string PrazoEntrega
        {
            get
            {
                return _prazoEntrega;
            }

            set
            {
                _prazoEntrega = value;
            }
        }

        public string EntregueEm
        {
            get
            {
                return _entregueEm;
            }

            set
            {
                _entregueEm = value;
            }
        }

        public string Verificado
        {
            get
            {
                return _verificado;
            }

            set
            {
                _verificado = value;
            }
        }
        #endregion

       
    }
}
