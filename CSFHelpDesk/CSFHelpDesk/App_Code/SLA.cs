using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SLA
/// </summary>
public class SLA
{
    #region Campos
    DateTime _abertura;
    DateTime _fechamento;
    TimeSpan _tempoTotal;
    TimeSpan _tempoPrazo;
    bool _atendido;

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
    #endregion

    public void CalculoTempo()
    {
        DateTime inicial = this.Abertura;
        DateTime final = this.Fechamento;
        
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
            else if (inicial.Hour >= 18)
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
            else if (final.Hour >= 18)
            {
                final = ProximodiaUtil(final, 0, 18);
            }
        }


        #endregion

        this.TempoTotal = CalcularTempoTotal(inicial, final);
    }

    private static TimeSpan CalcularTempoTotal(DateTime inicial, DateTime final)
    {
        DateTime dtInicial = inicial.Date;
        TimeSpan tempoTotal = new TimeSpan();
        if (inicial.Date != final.Date)
        {
            tempoTotal += new DateTime(inicial.Year, inicial.Month, inicial.Day, 18, 0, 0).Subtract(inicial);
            tempoTotal += final.Subtract(new DateTime(final.Year, final.Month, final.Day, 8, 0, 0));
            dtInicial = ProximodiaUtil(dtInicial, 1, 8);

            while (dtInicial < final.Date)
            {
                if (dtInicial.DayOfWeek != DayOfWeek.Saturday && dtInicial.DayOfWeek != DayOfWeek.Sunday)
                {
                    tempoTotal += new TimeSpan(10, 0, 0);
                }
                dtInicial = dtInicial.AddDays(1);
            }
        }
        else
        {
            tempoTotal = final.Subtract(inicial);
        }

        return tempoTotal;
    }

    private static DateTime ProximodiaUtil(DateTime data, int addDays, int Hora)
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