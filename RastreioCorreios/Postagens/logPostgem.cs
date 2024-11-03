using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RastroCorreios;


namespace Postagens
{
    public class logPostgem
    {
        #region Campos

        private string _data;
        private string _local;
        private string _status;
        private string _postagem;
        private string _cepDestino;
        private string _cidadeDestino;
        private string _ufDestino;
        private string _tpEnvio;
        private bool _ok;


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

        public string Local
        {
            get
            {
                return _local;
            }

            set
            {
                _local = value;
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

        public string CidadeDestino
        {
            get
            {
                return _cidadeDestino;
            }

            set
            {
                _cidadeDestino = value;
            }
        }

        public string UfDestino
        {
            get
            {
                return _ufDestino;
            }

            set
            {
                _ufDestino = value;
            }
        }

        public string TpEnvio
        {
            get
            {
                return _tpEnvio;
            }

            set
            {
                _tpEnvio = value;
            }
        }

        public bool Ok
        {
            get
            {
                return _ok;
            }

            set
            {
                _ok = value;
            }
        }
        #endregion

        public logPostgem()
        {

        }

        public logPostgem(string Postagem)
        {
            RastroCorreios.returnObjeto obj = null;
            try
            {
                obj = RastroCorreios.Rastreio.RastrearObjeto(Postagem);
            }
            catch(Exception ex)
            {
            }
            if (obj != null)
            {
                if (obj.evento != null)
                {
                    if (obj.evento[0] != null)
                    {
                        this.Status = obj.evento[0].descricao;
                        this.Data = obj.evento[0].data;
                    }
                }

                try {
                    this.CepDestino = obj.evento[0].destino.codigo.ToString();
                    this.CidadeDestino = obj.evento[0].destino.cidade;
                    this.UfDestino = obj.evento[0].destino.uf;
                    this.TpEnvio = obj.categoria;
                    this.Local = obj.evento[0].cidade;
                    this.Postagem = Postagem;
                }
                catch {
                    this.Ok = false;
                }

                if (this.Status != null && this.Data != null)
                {
                    this.Ok = true;
                }
                
                
            }

        }
    }
}
