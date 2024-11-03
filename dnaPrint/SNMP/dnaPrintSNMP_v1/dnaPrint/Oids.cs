using System.Collections.Generic;
using System.Xml;

namespace dnaPrint
{
    class Oids
    {
        public Oids(string fabricante, string firmware, string oid, string propriedade, string idperfil)
        {
            if (fabricante != null)
                this.Fabricante = fabricante;
            else
                this.Fabricante = "";

            if (firmware != null)
                this.Firmware = firmware;
            else
                this.Firmware = "";

            if (oid != null)
                this.Oid = oid;
            else
                this.Oid = "";

            if (propriedade != null)
                this.Propriedade = propriedade;
            else
                this.Propriedade = "";

            if (idperfil != null)
                this.IdPerfil = idperfil;
            else
                this.IdPerfil = "";
        }
        private string _fabricante;

        public string Fabricante
        {
            get { return _fabricante; }
            set { _fabricante = value; }
        }

        private string _firmware;

        public string Firmware
        {
            get { return _firmware; }
            set { _firmware = value; }
        }
        private string _oid;

        public string Oid
        {
            get { return _oid; }
            set { _oid = value; }
        }
        private string _propriedade;

        public string Propriedade
        {
            get { return _propriedade; }
            set { _propriedade = value; }
        }

        private string _idPerfil;

        public string IdPerfil
        {
            get { return _idPerfil; }
            set { _idPerfil = value; }
        }

        public static List<Oids> RetornaOidsXml(string ArquivoXml)
        {
            List<Oids> lista = new List<Oids>();
            XmlTextReader reader = new XmlTextReader(ArquivoXml);
            string l_fabricante = "";
            string l_firmware = "";
            string l_oid = "";
            string l_propriedade = "";
            string l_idperfil = "";

            while (reader.Read())
            {
                if (reader.AttributeCount > 0)
                {
                    reader.MoveToAttribute("fabricante");
                    l_fabricante = reader.Value;
                    reader.MoveToAttribute("firmware");
                    l_firmware = reader.Value;
                    reader.MoveToAttribute("oid");
                    l_oid = reader.Value;
                    reader.MoveToAttribute("propriedade");
                    l_propriedade = reader.Value;
                    reader.MoveToAttribute("idPerfil");
                    l_idperfil = reader.Value;
                    Oids _oid = new Oids(l_fabricante, l_firmware, l_oid, l_propriedade, l_idperfil);
                    lista.Add(_oid);
                }
            }
            return lista;
        }
    }

    class oidsPadrao
    {
        private string _fabricante;

        public string Fabricante
        {
            get { return _fabricante; }
            set { _fabricante = value; }
        }

        private string _oid;

        public string Oid
        {
            get { return _oid; }
            set { _oid = value; }
        }

        private string _firmware;

        public string Firmware
        {
            get { return _firmware; }
            set { _firmware = value; }
        }

        public static List<oidsPadrao> RetornaOidsPadraoXml(string ArquivoXml)
        {
            List<oidsPadrao> lista = new List<oidsPadrao>();
            XmlTextReader reader = new XmlTextReader(ArquivoXml);

            while (reader.Read())
            {

                if (reader.AttributeCount > 0)
                {
                    oidsPadrao _oidPadrao = new oidsPadrao();
                    reader.MoveToAttribute("fabricante");
                    _oidPadrao.Fabricante = reader.Value;
                    reader.MoveToAttribute("firmware");
                    _oidPadrao.Firmware = reader.Value;
                    reader.MoveToAttribute("oidPadrao");
                    _oidPadrao.Oid = reader.Value;
                    lista.Add(_oidPadrao);
                }

            }
            return lista;
        }
    }
}
