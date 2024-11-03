using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace dnaPrintJobs
{
    class Log
    {
        public enum TipoLogs { erro, info };
        private string _filename;
        private string _diretorio;

        private string Diretorio
        {
            get { return _diretorio; }
            set { _diretorio = value; }
        }

        private string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        public Log(string Programa, string Diretorio)
        {
            this.Diretorio = Diretorio;

            if (!Directory.Exists(this.Diretorio))
            {
                Directory.CreateDirectory(this.Diretorio);
            }
            this.Nomear(Programa);

        }

        public void Escrever(TipoLogs tipo, string Mensagem)
        {
            switch (tipo)
            {
                case TipoLogs.erro:
                    Escrever("ERRO : " + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + " : " + Mensagem);
                    break;
                case TipoLogs.info:
                    Escrever("INFO : " + DateTime.Now.ToString("dd-MM-yy HH:mm:ss") + " : " + Mensagem);
                    break;
            }
        }

        private void Escrever(string Texto)
        {
            if (!File.Exists(this.Filename))
            {
                File.Create(this.Filename).Close();
            }
            TextWriter arquivo = File.AppendText(this.Filename);
            arquivo.WriteLine(Texto);
            arquivo.Flush();
            arquivo.Close();
        }

        private void Nomear(string Programa)
        {
            string data = DateTime.Now.ToString("ddMMyyyy");
            this.Filename = this.Diretorio + @"\" + Programa + "_log_" + data + ".txt";
        }
    }
}