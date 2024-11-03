using System;

namespace dnaprint.Testes
{
    class Program
    {

        static void Main(string[] args)
        {
            string chave = "csfdigital2017ce";
            string vetor = "csfdigital2017ce";
            string text = "Mensagem criptografada";
            string mensagemCriptografada = dnaPrint.Base.Cripto.Criptografar(chave, vetor, text);
            Console.WriteLine(mensagemCriptografada);

            Console.WriteLine(dnaPrint.Base.Cripto.Descriptografar(chave, vetor, mensagemCriptografada));

            Console.ReadLine();
        }
    }
}
