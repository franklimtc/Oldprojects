using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Postagens
{
    public class Prazo
    {
        public enum servicoCorreios { sedex, pac };

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
    }
}
