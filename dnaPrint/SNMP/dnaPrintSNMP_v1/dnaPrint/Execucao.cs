using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;


namespace dnaPrint
{
    public class Execucao
    {
        static string dirAtual = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        static string dirDados = dirAtual + @"\Dados";
        static string dirConfig = dirAtual + @"\Config";
        static string config = dirConfig + @"\Config.xml";
        static string dirLogs = dirAtual + @"\Logs";
        static string data = DateTime.Now.ToString("dd-MM-yy");
        static string msg = "";

        public static void Iniciar()
        {
            #region Bloqueio
            //Cripto c = new Cripto();
            DateTime dtInicial = new DateTime(2013, 3, 1);
            DateTime dtFinal = new DateTime(2020, 12, 31);
            DateTime dtAtual = DateTime.Now;
            #endregion

            if (dtAtual > dtInicial && dtAtual <= dtFinal)
            {
                String msg = "Início do disparo SNMP.";

                if (!Directory.Exists(dirDados))
                {
                    Directory.CreateDirectory(dirDados);
                }

                if (!Directory.Exists(dirLogs))
                {
                    Directory.CreateDirectory(dirLogs);
                }

                if (!Directory.Exists(dirConfig))
                {
                    Directory.CreateDirectory(dirConfig);
                }

                if (!File.Exists(dirConfig + @"\Config.xml"))
                {
                    File.Create(dirConfig + @"\Config.xml").Close();
                    StringBuilder xml = new StringBuilder();
                    xml.AppendLine("<Config>");
                    xml.AppendLine(@"<Config TipoEntrada='null' Servidor='null' Porta='null' Database='null' SSL='null' Usuario='null' Senha='null' Email='null' Cliente='null' Endereco='null' />");
                    xml.AppendLine(@"</Config>");
                    DAO.GerarTXT(dirConfig + @"\Config.xml", xml.ToString());
                }

                Logs.GerarLogs(Logs.TipoLogs.geral, msg);

                // Exclui os arquivos antigos
                try
                {
                    ExcluirArquivosAntigos(dirDados);
                    ExcluirArquivosAntigos(dirLogs);
                }
                catch
                {
                    Logs.GerarLogs(Logs.TipoLogs.geral, "Falha ao tentar excluir arquivos antigos.");
                }

                // ###########################

                //MarcarArquivosEnviados();

                Disparo d = new Disparo();

                try
                {
                    //Disparo.TipoConexao tipo = d.DefineConexao(dirConfig + @"\Config.xml");
                    d.IniciarProcesso(d.DefineConexao(dirConfig + @"\Config.xml"));
                }
                catch
                {
                    msg = "Falha no arquivo de configuração. Reveja os dados informados no arquivo " + dirConfig + @"\Config.xml";
                    Logs.GerarLogs(Logs.TipoLogs.geral, msg);
                }
                msg = "Fim do disparo SNMP.";
                Logs.GerarLogs(Logs.TipoLogs.geral, msg);
            }
            else
            {
                Logs.GerarLogs(Logs.TipoLogs.geral, "Período de teste encerrado. Entre em contato com o administrador!");
            }
        }

        public static void MarcarArquivosEnviados()
        {
            string ArquivosEnviados = dirDados + @"\Arquivos Enviados.txt";
            List<string> arquivos = DAO.LerTxt(ArquivosEnviados);

            foreach (string arquivo in arquivos)
            {
                //if (File.Exists(dirDados + @"\" + arquivo))
                try
                {
                    File.Move(dirDados + @"\" + arquivo, dirDados + @"\" + arquivo.Replace(".csv", "_Enviado.csv"));
                }
                catch
                {
                    Logs.GerarLogs(Logs.TipoLogs.geral, "Falha ao renomear o arquivo '" + arquivo + "'.");
                }

                //FileInfo f = new FileInfo(dirDados + @"\" + arquivo);
                //if (f.Name.Equals(arquivo))
                //{
                //    f.Delete();
                //}

            }
            try
            {
                File.Delete(ArquivosEnviados);
            }
            catch
            {
                Logs.GerarLogs(Logs.TipoLogs.geral, "Falha ao excluir o arquivo 'Arquivos Enviados.txt'.");
            }
        }

        public static void ExcluirArquivosAntigos(string diretorio)
        {
            string[] files = Directory.GetFiles(diretorio);
            DateTime dtLimite = DateTime.Now.AddDays(-7);

            foreach (string f in files)
            {
                FileInfo fInfo = new FileInfo(f);
                if (fInfo.LastWriteTime < dtLimite)
                {
                    fInfo.Delete();
                }
            }
        }
    }
}
