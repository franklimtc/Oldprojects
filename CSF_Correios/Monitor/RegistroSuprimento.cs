using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitor
{
    class RegistroSuprimento
    {
        #region Campos
        private string _idEquipamento;
        private string _UF;
        private string _Cidade;
        private string _Unidade;
        private string _Cod;
        private string _Ambiente;
        private string _serie;
        private string _ip;
        private string _MediaDia;
        private string _ContadorAtual;
        private string _Toner;
        private string _TonerEstimativaDias;
        private string _TonerSerial;
        private string _DataEnvioToner;
        private string _DataTrocaTonner;
        private string _PostagemToner1;
        private string _SugestaoToner;
        private string _Cilindro;
        private string _CilindroEstimativaDias;
        private string _CilindroSerial;
        private string _DataEnvioCilindro;
        private string _DataTrocaCilindro;
        private string _PostagemCilindro;
        private string _SugestaoCilindro;
        private string _DataUltimaLeitura;

        public string IdEquipamento
        {
            get
            {
                return _idEquipamento;
            }

            set
            {
                _idEquipamento = value;
            }
        }

        public string UF
        {
            get
            {
                return _UF;
            }

            set
            {
                _UF = value;
            }
        }

        public string Cidade
        {
            get
            {
                return _Cidade;
            }

            set
            {
                _Cidade = value;
            }
        }

        internal bool Isnull()
        {
            bool result = false;

            if (this.IdEquipamento == null || this.IdEquipamento == "")
            {
                result = true;
            }
            if(this.Serie== null || this.Serie == "")
            {
                result = true;
            }

            return result;
        }

        public string Unidade
        {
            get
            {
                return _Unidade;
            }

            set
            {
                _Unidade = value;
            }
        }

        public string Cod
        {
            get
            {
                return _Cod;
            }

            set
            {
                _Cod = value;
            }
        }

        public string Ambiente
        {
            get
            {
                return _Ambiente;
            }

            set
            {
                _Ambiente = value;
            }
        }

        public string Serie
        {
            get
            {
                return _serie;
            }

            set
            {
                _serie = value;
            }
        }

        public string Ip
        {
            get
            {
                return _ip;
            }

            set
            {
                _ip = value;
            }
        }

        public string MediaDia
        {
            get
            {
                return _MediaDia;
            }

            set
            {
                _MediaDia = value;
            }
        }

        public string ContadorAtual
        {
            get
            {
                return _ContadorAtual;
            }

            set
            {
                _ContadorAtual = value;
            }
        }

        public string Toner
        {
            get
            {
                return _Toner;
            }

            set
            {
                _Toner = value;
            }
        }

        public string TonerEstimativaDias
        {
            get
            {
                return _TonerEstimativaDias;
            }

            set
            {
                _TonerEstimativaDias = value;
            }
        }

        public string DataEnvioToner
        {
            get
            {
                return _DataEnvioToner;
            }

            set
            {
                _DataEnvioToner = value;
            }
        }

        public string DataTrocaTonner
        {
            get
            {
                return _DataTrocaTonner;
            }

            set
            {
                _DataTrocaTonner = value;
            }
        }

        public string PostagemToner1
        {
            get
            {
                return _PostagemToner1;
            }

            set
            {
                _PostagemToner1 = value;
            }
        }

        public string SugestaoToner
        {
            get
            {
                return _SugestaoToner;
            }

            set
            {
                _SugestaoToner = value;
            }
        }

        public string Cilindro
        {
            get
            {
                return _Cilindro;
            }

            set
            {
                _Cilindro = value;
            }
        }

        public string CilindroEstimativaDias
        {
            get
            {
                return _CilindroEstimativaDias;
            }

            set
            {
                _CilindroEstimativaDias = value;
            }
        }

        public string DataEnvioCilindro
        {
            get
            {
                return _DataEnvioCilindro;
            }

            set
            {
                _DataEnvioCilindro = value;
            }
        }

        public string DataTrocaCilindro
        {
            get
            {
                return _DataTrocaCilindro;
            }

            set
            {
                _DataTrocaCilindro = value;
            }
        }

        public string PostagemCilindro
        {
            get
            {
                return _PostagemCilindro;
            }

            set
            {
                _PostagemCilindro = value;
            }
        }

        public string SugestaoCilindro
        {
            get
            {
                return _SugestaoCilindro;
            }

            set
            {
                _SugestaoCilindro = value;
            }
        }

        public string DataUltimaLeitura
        {
            get
            {
                return _DataUltimaLeitura;
            }

            set
            {
                _DataUltimaLeitura = value;
            }
        }

        public string TonerSerial
        {
            get
            {
                return _TonerSerial;
            }

            set
            {
                _TonerSerial = value;
            }
        }

        public string CilindroSerial
        {
            get
            {
                return _CilindroSerial;
            }

            set
            {
                _CilindroSerial = value;
            }
        }
        #endregion

        public RegistroSuprimento(string[] registro)
        {
            if (registro.Length == 25)
            {
                this.IdEquipamento = registro[0];
                this.UF = registro[1];
                this.Cidade = registro[2];
                this.Unidade = registro[3];
                this.Cod = registro[4];
                this.Ambiente = registro[5];
                this.Serie = registro[6];
                this.Ip = registro[7];
                this.MediaDia = registro[8];
                this.ContadorAtual = registro[9];
                this.Toner = registro[10];
                this.TonerEstimativaDias = registro[11];
                this.DataEnvioToner = registro[12];
                this.DataTrocaTonner = registro[13];
                this.TonerSerial = registro[14];
                this.PostagemToner1 = registro[15];
                this.SugestaoToner = registro[16];
                this.Cilindro = registro[17];
                this.CilindroEstimativaDias = registro[18];
                this.CilindroSerial = registro[21];
                this.DataEnvioCilindro = registro[19];
                this.DataTrocaCilindro = registro[20];
                this.PostagemCilindro = registro[22];
                this.SugestaoCilindro = registro[23];
                this.DataUltimaLeitura = registro[24];
            }
        }

        public static List<RegistroSuprimento> Listar(string filename)
        {
            List<RegistroSuprimento> lista = new List<RegistroSuprimento>();

            List<string> listaTexto = DAO.LerTXT(filename);
            foreach (string s in listaTexto)
            {
                RegistroSuprimento reg = new RegistroSuprimento(s.Split(','));
                lista.Add(reg);
            }

            return lista;
        }

    }
}
