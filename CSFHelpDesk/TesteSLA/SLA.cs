using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteSLA
{
  
    class SLA
    {
        #region Campos
        DateTime _abertura;
        DateTime _fechamento;
        TimeSpan _tempoTotal;
        TimeSpan _tempoPrazo;
        bool _atendido;
        bool _fechado;

        public DateTime Abertura
        {
            get
            {
                return _abertura;
            }

            set
            {
                _abertura = value;
            }
        }

        public DateTime Fechamento
        {
            get
            {
                return _fechamento;
            }

            set
            {
                _fechamento = value;
            }
        }

        public TimeSpan TempoTotal
        {
            get
            {
                return _tempoTotal;
            }

            set
            {
                _tempoTotal = value;
            }
        }

        public TimeSpan TempoPrazo
        {
            get
            {
                return _tempoPrazo;
            }

            set
            {
                _tempoPrazo = value;
            }
        }

        public bool Atendido
        {
            get
            {
                return _atendido;
            }

            set
            {
                _atendido = value;
            }
        }

        public bool Fechado
        {
            get
            {
                return _fechado;
            }

            set
            {
                _fechado = value;
            }
        }
        #endregion

        public SLA(bool fechado)
        {
            this.Fechado = fechado;
        }
        //public void Calcular()
        //{
        //    this.Abertura = DateTime.Now.Subtract(new TimeSpan(5,0,0));

        //    if (!this.Fechado)
        //    {
        //        if (DateTime.Now.Date == this.Abertura.Date)
        //        {
        //            if (this.Abertura.Hour < 8)
        //            {
        //                this.Abertura = new DateTime(this.Abertura.Year, this.Abertura.Month, this.Abertura.Day, 8, 0, 0);
        //                if (DateTime.Now.Hour < 18)
        //                    this.TempoTotal = DateTime.Now.Subtract(this.Abertura);
        //                else
        //                {
        //                    DateTime d = DateTime.Now.Date;
        //                    this.TempoTotal = new DateTime(d.Year, d.Month, d.Day, 18, 0, 0).Subtract(this.Abertura);
        //                }
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {

        //        }

        //    }
        //}

        public void Calcular()
        {
            DateTime dtInicial = new DateTime(2017, 1, 2, 7, 30, 43);
            DateTime dtfinal = new DateTime(2017, 1, 2, 17, 30, 43);
            CalculoTempo(dtInicial, dtfinal);
        }

     

        public static void CalculoTempo(DateTime inicial, DateTime final)
        {
            #region Tratar data inicial
            if (inicial.DayOfWeek == DayOfWeek.Sunday || inicial.DayOfWeek == DayOfWeek.Saturday)
            {
                inicial = ProximodiaUtil(inicial, 1, 8);
            }
            else
            {
                if (inicial.Hour < 8)
                {
                    inicial = new DateTime(inicial.Year, inicial.Month, inicial.Day, 8, 0, 0);
                }
                else if (inicial.Hour > 18)
                {
                    inicial = inicial = ProximodiaUtil(inicial, 1, 8);
                }
            }
            #endregion

            #region Tratar data final
            if (final.DayOfWeek == DayOfWeek.Sunday || final.DayOfWeek == DayOfWeek.Saturday)
            {
                final = ProximodiaUtil(final, -1, 18);
            }
            else
            {
                if (final.Hour < 8)
                {
                    final = ProximodiaUtil(final, -1, 18);
                }
                else if (final.Hour > 18)
                {
                    final = ProximodiaUtil(final, 0, 18);
                }
            }
            

            #endregion

            TimeSpan tempoTotal = CalcularTempoTotal(inicial, final);
        }

        private static TimeSpan CalcularTempoTotal(DateTime inicial, DateTime final)
        {
            DateTime dtInicial = inicial.Date;
            TimeSpan tempoTotal = new TimeSpan();

            tempoTotal += inicial.Subtract(new DateTime(inicial.Year, inicial.Month, inicial.Day, 8, 0, 0));
            tempoTotal += final.Subtract(new DateTime(final.Year, final.Month, final.Day, 8, 0, 0));

            while (dtInicial < final.Date)
            {
                if (dtInicial.DayOfWeek != DayOfWeek.Saturday && dtInicial.DayOfWeek != DayOfWeek.Sunday)
                {
                    tempoTotal += new TimeSpan(1, 0, 0, 0);
                }
                dtInicial = dtInicial.AddDays(1);
            }

            return tempoTotal;
        }

        static DateTime ProximodiaUtil(DateTime data, int addDays, int Hora)
        {
            data = data.Date.AddDays(addDays);
            data = new DateTime(data.Year, data.Month, data.Day, Hora, 0, 0);
            while (data.DayOfWeek == DayOfWeek.Sunday || data.DayOfWeek == DayOfWeek.Saturday)
            {
                data = data.AddDays(addDays);
            }
            return data;
        }
    }
}
