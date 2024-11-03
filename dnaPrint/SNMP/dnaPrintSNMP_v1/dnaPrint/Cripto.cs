using System;
using System.Security.Cryptography;

namespace dnaPrint
{
    class Cripto
    {
        #region campos
        private string sKey;
        private byte[] Buffer;
        private TripleDESCryptoServiceProvider DES;
        private MD5CryptoServiceProvider hashMD5;
        private ICryptoTransform Crypt = null;
        #endregion

        #region construtores

        public Cripto()
        {
            //Inicializa os objetos
            sKey = "_Thais_Daniel_";
            DES = new TripleDESCryptoServiceProvider();
            hashMD5 = new MD5CryptoServiceProvider();
            //Computa o hash MD5.
            DES.Key = hashMD5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(sKey));
            //Indica o modo de cifragem.
            DES.Mode = CipherMode.ECB;
        }

        #endregion

        #region métodos
        public string Encriptar(string sValor)
        {
            Crypt = DES.CreateEncryptor();

            try
            {
                Buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(sValor);
            }
            catch
            {
                throw new ExecutionEngineException();
            }

            return Convert.ToBase64String(Crypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        public string Decriptar(string sValor)
        {
            Crypt = DES.CreateDecryptor();

            try
            {
                Buffer = Convert.FromBase64String(sValor);
            }
            catch
            {
                throw new ExecutionEngineException();
            }

            return System.Text.ASCIIEncoding.ASCII.GetString(Crypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        #endregion
    }
}
