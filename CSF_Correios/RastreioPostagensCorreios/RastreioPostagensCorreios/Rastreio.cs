using System;
using System.Xml;

namespace RastreioPostagensCorreios
{
    public class Rastreio
    {
        #region Campos
        private DateTime data;
        private string status;
        private string local;

        public DateTime Data { get => data; set => data = value; }
        public string Status { get => status; set => status = value; }
        public string Local { get => local; set => local = value; }
        #endregion

        public Rastreio()
        {

        }

        public Rastreio(string Rastro)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.LoadXml(Rastro);
            XmlNode v1 = null;

            if (doc.LastChild.Name == "rastro")
                v1 = doc.LastChild;
            if (v1.LastChild.Name == "objeto")
                v1 = v1.LastChild;
            if (v1.LastChild.Name == "evento")
            {
                v1 = v1.LastChild;
                foreach (XmlNode x in v1.ChildNodes)
                {
                    switch (x.Name)
                    {
                        case "data":
                            this.data = DateTime.Parse(x.LastChild.Value);
                            break;
                        case "descricao":
                            this.status = x.LastChild.Value;
                            break;
                        case "local":
                            this.local = x.LastChild.Value;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}