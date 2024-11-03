using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnaprint.Testes
{
    class Log
    {
        public enum tpLog { Info, Erro };

        public static void Adicionar(string filename, tpLog Tipo, string Mensagem)
        {
                switch (Tipo)
                {
                    case tpLog.Erro:
                        Escrever(filename, string.Format("ERRO {0}: {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Mensagem));
                        break;
                    case tpLog.Info:
                        Escrever(filename, string.Format("INFO {0}: {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Mensagem));
                        break;
                    default:
                        break;
                }
        }

        public static bool Escrever(string NomeArquivo, string Texto)
        {
            bool result = false;
            if (!File.Exists(NomeArquivo))
            {
                File.Create(NomeArquivo).Close();
            }
            try
            {
                TextWriter arquivo = File.AppendText(NomeArquivo);
                arquivo.WriteLine(Texto);
                arquivo.Flush();
                arquivo.Close();
                result = true;
            }
            catch
            {

            }
            return result;
        }
    }
}
