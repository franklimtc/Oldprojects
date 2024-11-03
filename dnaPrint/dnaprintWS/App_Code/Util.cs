﻿using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

/// <summary>
/// Summary description for Util
/// </summary>
public class Util
{
	public Util()
	{
		//
		// TODO: Add constructor logic here
		//
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
                } return ArrayBytesToHexString(streamResultado.ToArray());
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
}