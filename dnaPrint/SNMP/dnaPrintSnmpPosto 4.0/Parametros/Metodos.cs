using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parametros
{
    public static class Metodos
    {
        public static WebService.Oids[] RetornaOids()
        {
            WebService.DisparosSoapClient disparo = new WebService.DisparosSoapClient();
            WebService.Oids[] listaoids = null;
            try
            {
                listaoids = disparo.RetornaOids();
            }
            catch
            {

            }
            return listaoids;
        }
        public static WebService.Oids[] RetornaOidsParcial(string fabricante, string modelo, string firmware)
        {
            WebService.DisparosSoapClient disparo = new WebService.DisparosSoapClient();
            WebService.Oids[] listaoids = null;
            try
            {
                listaoids = disparo.RetornaOidsParcial(fabricante, modelo, firmware);
            }
            catch
            { }
            return listaoids;
        }
        public static int retornaID(string serie)
        {
            WebService.DisparosSoapClient disparo = new WebService.DisparosSoapClient();
            int idEquipamento = 0;
            try
            {
                idEquipamento = disparo.RetornaIdEquipamento(serie);
            }
            catch
            { }
            return idEquipamento;
        }
        public static WebService.PerfilOID[] RetornaPerfis()
        {
            WebService.DisparosSoapClient disparo = new WebService.DisparosSoapClient();
            WebService.PerfilOID[] listaPerfis = null;
            try { listaPerfis = disparo.RetornaPerfis(); }
            catch { }
            return listaPerfis;
        }
        public static WebService.Modelos[] RetornaModelos()
        {
            WebService.DisparosSoapClient disparo = new WebService.DisparosSoapClient();
            WebService.Modelos[] listaModelos = null;
            try
            {
                listaModelos = disparo.RetornaModelos();
            }
            catch { }
            return listaModelos;
        }

        public static WebService.Firmwares[] RetornaFirmwares(string fabricante, string modelo)
        {
            WebService.DisparosSoapClient disparo = new WebService.DisparosSoapClient();
            WebService.Firmwares[] listaFirmwares = null;
            try
            {
                listaFirmwares = disparo.RetornaFirmwares(fabricante, modelo);
            }
            catch { }
            return listaFirmwares;
        }

        public static void CadastrarDisparo(string tsql)
        {
            WebService.DisparosSoapClient disparo = new WebService.DisparosSoapClient();
            try { disparo.CadastrarDisparo(tsql); }
            catch { }
        }
    }
}