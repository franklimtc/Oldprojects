using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Postagens
{
    public class Postagem
    {
        public static void Rastrear(string postagen)
        {
            correios.rastro r = new correios.rastro();
            
            correios.sroxml eventos = new correios.sroxml();
            eventos.TipoPesquisa = "";
            //correios.sroxml eventos = r.buscaEventos("ECT", "SRO", "T", "L", "101", postagen);
        }

        static string url(string postagem)
        {
            return string.Format(@"http://websro.correios.com.br/sro_bin/txect01$.QueryList?P_LINGUA=001&P_TIPO=001&P_COD_UNI={0}", postagem);
        }
    }
}
