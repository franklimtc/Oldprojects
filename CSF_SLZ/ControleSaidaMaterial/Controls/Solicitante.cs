using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls
{
    class Solicitante
    {
        #region Campos

        private string _idSolicitante;
        private string _nome;
        private string _cargo;
        private string _dtCadastro;
        private string _dtAtualizacao;
        private string _status;
        private string _operador;

        public string IdSolicitante
        {
            get
            {
                return _idSolicitante;
            }

            set
            {
                _idSolicitante = value;
            }
        }

        public string Nome
        {
            get
            {
                return _nome;
            }

            set
            {
                _nome = value;
            }
        }

        public string Cargo
        {
            get
            {
                return _cargo;
            }

            set
            {
                _cargo = value;
            }
        }

        public string DtCadastro
        {
            get
            {
                return _dtCadastro;
            }

            set
            {
                _dtCadastro = value;
            }
        }

        public string DtAtualizacao
        {
            get
            {
                return _dtAtualizacao;
            }

            set
            {
                _dtAtualizacao = value;
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
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

        #endregion

        public static bool Adicionar()
        {
            bool result = false;
            return result;
        }

        public static bool Atualizar()
        {
            bool result = false;
            return result;
        }

        public static bool Desativar()
        {
            bool result = false;
            return result;
        }
    }
}
