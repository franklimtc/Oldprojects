using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Reflection;

namespace dnaPrintJobs
{
    public static class Util
    {

        static string diretorio = Util.RetornaDiretorio() + @"\Logs";
        //private static Log filelog = new Log("dnaPrintJobs", diretorio);

        #region FTP - Listar
        public static List<string> ListarArquivos(string usuario, string senha, string diretorio)
        {
            Log filelog = new Log("dnaPrintJobs", diretorio);
            List<string> liArquivos = new List<string>();

            try
            {
                try
                {
                    //Cria comunicação com o servidor
                    //Definir o diretório a ser listado
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(diretorio);
                    //Define que a ação vai ser de listar diretório
                    request.Method = WebRequestMethods.Ftp.ListDirectory;
                    //Credenciais para o login (usuario, senha)
                    request.Credentials = new NetworkCredential(usuario, senha);
                    //modo passivo
                    request.UsePassive = true;
                    //dados binarios
                    request.UseBinary = true;
                    //setar o KeepAlive para true
                    request.KeepAlive = true;

                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                        //Criando a Stream para pegar o retorno
                        Stream responseStream = response.GetResponseStream();
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            //Adicionar os arquivos na lista
                            liArquivos = reader.ReadToEnd().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                        }
                    }
                }
                catch (Exception ex)
                {
                    filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
                }
            }
            finally
            { }

            return liArquivos;
        }
        #endregion

        #region FTP - Delete
        public static bool ExcluirArquivo(string usuario, string senha, string diretorio, string arquivo)
        {
            Log filelog = new Log("dnaPrintJobs", diretorio);
            bool sucesso = false;

            //Cria comunicação com o servidor
            //Definir o diretório a ser listado
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(@diretorio + @"/" + @arquivo);
            //Define que a ação vai ser de listar diretório
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            //Credenciais para o login (usuario, senha)
            request.Credentials = new NetworkCredential(usuario, senha);
            //modo passivo
            request.UsePassive = true;
            //dados binarios
            request.UseBinary = true;
            //setar o KeepAlive para true
            request.KeepAlive = true;

            try
            {
                try
                {
                    //criando o objeto FtpWebResponse
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                    sucesso = true;
                }
                catch (Exception ex)
                {
                    filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
                    sucesso = false;
                }
            }
            finally
            {

            }

            return sucesso;
        }
        #endregion

        #region FTP - Download
        public static bool Download(string usuario, string senha, string origem, string arquivo, string destino)
        {
            Log filelog = new Log("dnaPrintJobs", diretorio);
            bool sucesso = false;
            try
            {
                try
                {
                    //Cria comunicação com o servidor
                    //definindo o arquivo para download
                    FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(@origem + @"/" + @arquivo);
                    //Define que a ação vai ser de download
                    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    //Credenciais para o login (usuario, senha)
                    request.Credentials = new NetworkCredential(usuario, senha);
                    //modo passivo
                    request.UsePassive = true;
                    //dados binarios
                    request.UseBinary = true;
                    //setar o KeepAlive para true
                    request.KeepAlive = true;

                    //criando o objeto FtpWebResponse
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    //Criando a Stream para ler o arquivo
                    Stream responseStream = response.GetResponseStream();

                    byte[] buffer = new byte[2048];

                    //Definir o local onde o arquivo será criado.
                    FileStream newFile = new FileStream(@destino + @"/" + GerarNome(".log"), FileMode.Create);
                    //FileStream newFile = new FileStream(@destino + @"/" + @arquivo, FileMode.Create);
                    //Ler o arquivo de origem
                    int readCount = responseStream.Read(buffer, 0, buffer.Length);
                    while (readCount > 0)
                    {
                        //Escrever o arquivo
                        newFile.Write(buffer, 0, readCount);
                        readCount = responseStream.Read(buffer, 0, buffer.Length);
                    }
                    newFile.Close();
                    responseStream.Close();
                    response.Close();

                    sucesso = true;
                }
                catch (Exception ex)
                {
                    filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
                    sucesso = false;
                }
            }
            finally
            {

            }
            return sucesso;
        }
        #endregion

        #region FTP - Upload
        public static bool Upload(string usuario, string senha, string diretorio, string arquivo, string origem)
        {
            Log filelog = new Log("dnaPrintJobs", diretorio);
            bool sucesso = false;

            //Caminho do arquivo para upload
            //FileInfo fileInf = new FileInfo("c:\\wwwroot\\exemplos\\logo.png");
            FileInfo fileInf = new FileInfo(@arquivo);
            //Cria comunicação com o servidor
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(@diretorio + @"/" + System.Environment.MachineName + "_" + System.IO.Path.GetFileName(@arquivo));
            //Define que a ação vai ser de upload
            request.Method = WebRequestMethods.Ftp.UploadFile;
            //Credenciais para o login (usuario, senha)
            request.Credentials = new NetworkCredential(usuario, senha);
            //modo passivo
            request.UsePassive = true;
            //dados binarios
            request.UseBinary = true;
            //setar o KeepAlive para false
            request.KeepAlive = false;

            request.ContentLength = fileInf.Length;
            //cria a stream que será usada para mandar o arquivo via FTP
            Stream responseStream = request.GetRequestStream();
            byte[] buffer = new byte[2048];

            //Lê o arquivo de origem
            FileStream fs = fileInf.OpenRead();
            try
            {
                //Enquanto vai lendo o arquivo de origem, vai escrevendo no FTP
                int readCount = fs.Read(buffer, 0, buffer.Length);
                while (readCount > 0)
                {
                    //Esceve o arquivo
                    responseStream.Write(buffer, 0, readCount);
                    readCount = fs.Read(buffer, 0, buffer.Length);
                }

                sucesso = true;
            }
            catch (Exception ex)
            {
                filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
            }

            finally
            {
                fs.Close();
                responseStream.Close();
            }

            return sucesso;
        }
        #endregion

        public static string GerarNome(string extensao)
        {
            string nome = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + extensao;

            return nome;
        }

        #region Listar arquivos
        public static List<string> ListarArquivos(string diretorio)
        {
            Log filelog = new Log("dnaPrintJobs", diretorio);
            List<string> listaArquivos = new List<string>();

            try
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(@diretorio);

                    if (di.Exists)
                    {
                        FileInfo[] fi = di.GetFiles("*.log", SearchOption.TopDirectoryOnly);

                        foreach (FileInfo fidi in fi)
                        {
                            listaArquivos.Add(@fidi.FullName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
                }
            }
            finally
            {

            }

            return listaArquivos;
        }
        #endregion

        #region Deletar arquivo
        public static bool DeletarArquivo(string arquivo)
        {
            Log filelog = new Log("dnaPrintJobs", diretorio);
            bool resultado = false;

            try
            {
                try
                {
                    FileInfo fi = new FileInfo(@arquivo);

                    if (fi.Exists)
                    {
                        fi.Delete();
                    }

                    resultado = true;
                }
                catch (Exception ex)
                {
                    filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
                }
            }
            finally
            {

            }

            return resultado;
        }
        #endregion

        #region Criptografar
        public static string Criptografar(string texto)
        {
            Log filelog = new Log("dnaPrintJobs", diretorio);
            try
            {
                TripleDESCryptoServiceProvider objcriptografaSenha = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objcriptoMd5 = new MD5CryptoServiceProvider();

                byte[] byteHash, byteBuff;
                string strTempKey = "11";

                byteHash = objcriptoMd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objcriptoMd5 = null;
                objcriptografaSenha.Key = byteHash;
                objcriptografaSenha.Mode = CipherMode.ECB;

                byteBuff = ASCIIEncoding.ASCII.GetBytes(texto);
                return Convert.ToBase64String(objcriptografaSenha.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
                return "Digite os valores Corretamente." + ex.Message;
            }
        }
        #endregion

        #region Descriptografar
        public static string Descriptografar(string texto)
        {
            Log filelog = new Log("dnaPrintJobs", diretorio);
            try
            {
                TripleDESCryptoServiceProvider objdescriptografaSenha = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objcriptoMd5 = new MD5CryptoServiceProvider();

                byte[] byteHash, byteBuff;
                string strTempKey = "11";

                byteHash = objcriptoMd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objcriptoMd5 = null;
                objdescriptografaSenha.Key = byteHash;
                objdescriptografaSenha.Mode = CipherMode.ECB;

                byteBuff = Convert.FromBase64String(texto);
                string strDecrypted = ASCIIEncoding.ASCII.GetString(objdescriptografaSenha.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
                objdescriptografaSenha = null;

                return strDecrypted;
            }
            catch (Exception ex)
            {
                filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
                return "Digite os valores Corretamente." + ex.Message;
            }
        }
        #endregion

        public static void EnviarArquivoControle(string diretorio)
        {
            Log filelog = new Log("dnaPrintJobs", diretorio);
            string m_Document = Environment.MachineName + "-config-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xml";
            XmlWriter writer = null;

            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                writer = XmlWriter.Create(m_Document, settings);

                writer.WriteComment("dados do servidor de impressão");


                writer.WriteStartElement("root");

                writer.WriteStartElement("Servidor");
                writer.WriteString(Environment.MachineName);
                writer.WriteEndElement();

                writer.WriteStartElement("LeituraAtual");
                writer.WriteString(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                writer.WriteEndElement();

                writer.WriteStartElement("usuarioFtp");
                writer.WriteString(ConfigurationManager.AppSettings["usuarioFtp"].ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("senhaFtp");
                writer.WriteString(ConfigurationManager.AppSettings["senhaFtp"].ToString());
                writer.WriteEndElement();

                writer.WriteEndElement();

                writer.Flush();
                writer.Close();

            }
            catch (Exception ex)
            {
                filelog.Escrever(Log.TipoLogs.erro, ex.ToString());
                if (writer != null)
                    writer.Close();
            }

        }

        public static string RetornaDiretorio()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        #region Nova Criptografia
        private static Rijndael CriarInstanciaRijndael(string chave, string vetorInicializacao)
        {
            if (!(chave != null && (chave.Length == 16 || chave.Length == 24 || chave.Length == 32)))
            {
                throw new Exception("A chave de criptografia deve possuir " + "16, 24 ou 32 caracteres.");
            }
            if (vetorInicializacao == null || vetorInicializacao.Length != 16)
            {
                throw new Exception("O vetor de inicialização deve possuir " + "16 caracteres.");
            }
            Rijndael algoritmo = Rijndael.Create();
            algoritmo.Key = Encoding.ASCII.GetBytes(chave);
            algoritmo.IV = Encoding.ASCII.GetBytes(vetorInicializacao);
            return algoritmo;
        }
        public static string Criptografar(string chave, string vetorInicializacao, string textoNormal)
        {
            if (String.IsNullOrWhiteSpace(textoNormal))
            {
                throw new Exception("O conteúdo a ser encriptado não pode " + "ser uma string vazia.");
            }
            using (Rijndael algoritmo = CriarInstanciaRijndael(chave, vetorInicializacao))
            {
                ICryptoTransform encryptor = algoritmo.CreateEncryptor(algoritmo.Key, algoritmo.IV);
                using (MemoryStream streamResultado = new MemoryStream())
                {
                    using (CryptoStream csStream = new CryptoStream(streamResultado, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(csStream))
                        {
                            writer.Write(textoNormal);
                        }
                    } 
                    return ArrayBytesToHexString(streamResultado.ToArray());
                }
            }
        }
        private static string ArrayBytesToHexString(byte[] conteudo)
        {
            string[] arrayHex = Array.ConvertAll(conteudo, b => b.ToString("X2"));
            return string.Concat(arrayHex);
        }
        public static string Descriptografar(string chave, string vetorInicializacao, string textoEncriptado)
        {
            if (String.IsNullOrWhiteSpace(textoEncriptado))
            {
                throw new Exception("O conteúdo a ser decriptado não pode " + "ser uma string vazia.");
            }
            if (textoEncriptado.Length % 2 != 0)
            {
                throw new Exception("O conteúdo a ser decriptado é inválido.");
            }
            using (Rijndael algoritmo = CriarInstanciaRijndael(chave, vetorInicializacao))
            {
                ICryptoTransform decryptor = algoritmo.CreateDecryptor(algoritmo.Key, algoritmo.IV);
                string textoDecriptografado = null;
                using (MemoryStream streamTextoEncriptado = new MemoryStream(HexStringToArrayBytes(textoEncriptado)))
                {
                    using (CryptoStream csStream = new CryptoStream(streamTextoEncriptado, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(csStream))
                        {
                            textoDecriptografado = reader.ReadToEnd();
                        }
                    }
                }
                return textoDecriptografado;
            }
        }
        private static byte[] HexStringToArrayBytes(string conteudo)
        {
            int qtdeBytesEncriptados = conteudo.Length / 2; byte[] arrayConteudoEncriptado = new byte[qtdeBytesEncriptados];
            for (int i = 0; i < qtdeBytesEncriptados; i++)
            {
                arrayConteudoEncriptado[i] = Convert.ToByte(conteudo.Substring(i * 2, 2), 16);
            }
            return arrayConteudoEncriptado;
        }

        public static string Chave()
        {

            return "csfdigital2010ce";
        }

        public static string Vetor()
        {
            return "csfdigital2010ma";
        }

        #endregion

        public static string LerTxt(string Arquivo)
        {
            string value = null;
            string line;

            try
            {
                StreamReader sr = new StreamReader(Arquivo);
                while ((line = sr.ReadLine()) != null)
                {
                    value += line.ToString();
                }
                sr.Close();
            }
            catch
            {
                value = "";
            }

            return value;
        }
    }
}