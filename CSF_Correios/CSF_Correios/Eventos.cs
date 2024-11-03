using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSF_Correios
{
    public class Eventos
    {
        public enum servicoCorreios{sedex, pac};
        #region Campos

        private string _cidade;
        private string _descricao;
        private string _data;
        private string _local;
        private string _recebedor;
        private string _uf;
        private string _status;
        private string _tipo;
        private string _documento;
        private string _hora;
        private string _codigo;
        private string _detalhe;

        public string Cidade
        {
            get
            {
                return _cidade;
            }

            set
            {
                _cidade = value;
            }
        }

        public string Descricao
        {
            get
            {
                return _descricao;
            }

            set
            {
                _descricao = value;
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

        public string Recebedor
        {
            get
            {
                return _recebedor;
            }

            set
            {
                _recebedor = value;
            }
        }

        public string Uf
        {
            get
            {
                return _uf;
            }

            set
            {
                _uf = value;
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

        public string Tipo
        {
            get
            {
                return _tipo;
            }

            set
            {
                _tipo = value;
            }
        }

        public string Documento
        {
            get
            {
                return _documento;
            }

            set
            {
                _documento = value;
            }
        }

        public string Hora
        {
            get
            {
                return _hora;
            }

            set
            {
                _hora = value;
            }
        }

        public string Codigo
        {
            get
            {
                return _codigo;
            }

            set
            {
                _codigo = value;
            }
        }

        public string Detalhe
        {
            get
            {
                return _detalhe;
            }

            set
            {
                _detalhe = value;
            }
        }

        #endregion

        public Eventos(string logs)
        {
            this.Cidade = ExtrairCampo("cidade", logs);
            this.Descricao = ExtrairCampo("descricao", logs);
            this.Data = ExtrairCampo("data", logs);
            this.Local = ExtrairCampo("local", logs);
            this.Recebedor = ExtrairCampo("recebedor", logs);
            this.Uf = ExtrairCampo("uf", logs);
            this.Status = ExtrairCampo("status", logs);
            this.Tipo = ExtrairCampo("tipo", logs);
            this.Documento = ExtrairCampo("documento", logs);
            this.Hora = ExtrairCampo("hora", logs);
            this.Codigo = ExtrairCampo("codigo", logs);
            this.Detalhe = ExtrairCampo("detalhe", logs);
        }

        private string ExtrairCampo(string campo, string logs)
        {
            int inicio = logs.IndexOf(campo);
            int fim = 0;
            try
            {
                fim = logs.IndexOf(",", inicio);
            }
            catch
            {

            }
                
            inicio += campo.Length;
            string result = null;
            try
            {
                result = logs.Substring(inicio, fim - inicio).Replace(":", "");
            }
            catch
            {
                result = "ERRO";
            }
            return result;
        }

        public static Eventos Rastrear(string codPostagem)
        {
            Correios.rastro rastreio = new Correios.rastro();
            Correios.sroxml xml = new Correios.sroxml();
            
            Eventos ev = null;
            try
            {
                //string eventos = rastreio.RastroJson("ECT", "SRO", "L", "T", "101", codPostagem.ToUpper().Trim());
                //ev = new Eventos(Limpar(eventos, "}"));
                //if (ev.Descricao == "ERRO")
                //{
                //    ev = new Eventos(Limpar(eventos, "detalhe"));
                //}

                xml = rastreio.buscaEventos("ECT", "SRO", "L", "T", "101", codPostagem.ToUpper().Trim());
            }
            catch(Exception ex)
            {
                string exc = ex.ToString();
            }
            return ev;
        }

        private static string Limpar(string resultados, string Caractere)
        {
            int inicio = resultados.IndexOf("evento");
            int fim = resultados.IndexOf(Caractere, inicio);
            string result = null;
            if (inicio != 0 && fim != 0)
            {
                result = resultados.Substring(inicio, fim - inicio).Replace("\"", "").Replace("evento:[{", "");
            }
            return result;
        }

        public static int CalculaPrazo(servicoCorreios servico, string cepOrigem, string cepDestino)
        {
            int qtdDias = 0;
            string codicoServico = null;
            switch (servico)
            {
                case servicoCorreios.sedex:
                    codicoServico = "40010";
                    break;
                case servicoCorreios.pac:
                    codicoServico = "41106";
                    break;
                default:
                    codicoServico = "41106";
                    break;
            }

            CorreiosPrazo.CalcPrecoPrazoWS calc = new CorreiosPrazo.CalcPrecoPrazoWS();
            CorreiosPrazo.cResultado result = null;
            try
            {
                result = calc.CalcPrazo(codicoServico, cepOrigem, cepDestino);
            }
            catch { }
            
            qtdDias = int.Parse(result.Servicos[0].PrazoEntrega);
            return qtdDias;
        }
        public static bool cepValido(string cep)
        {
            bool result = false;
            AtendeCliente.AtendeClienteService consulta = new AtendeCliente.AtendeClienteService();
            AtendeCliente.enderecoERP endereco;
            try
            {
                endereco = consulta.consultaCEP(cep);
            }
            catch { }
            return result;
        }

      
    }
}
