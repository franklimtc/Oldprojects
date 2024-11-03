using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de AtulPeca
/// </summary>
public class AtulPeca
{
    #region Campos

    private int _idatulPeca;
    private int _idreqPeca;
    private string _postagem;
    private string _obs;
    private DateTime _dtEnvio;
    private DateTime _dtCriacao;
    private string _status;
    private string _usuario;
    private DateTime _dtPrevisaoEntrega;
    private int _prazoEntrega;
    private DateTime _dtEntrega;
    private DateTime _entregueEm;
    private bool _verificado;
    private string _AR;
    private string _estoqueOrigem;

    private double _valor;

    #endregion

    public AtulPeca()
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
    }


    public static bool Enviar(string _estoque, string _tipoEnvio, int _requisicao, string _serie, string _partNumber, string _postagem, string _obs, string _usuario, string _ar)
    {
        bool result = false;
        string query = @"INSERT INTO atulPecas(idreqPeca, postagem, obs, dtEnvio, status, usuario, ar, estoqueOrigem) 
values(@idreqPeca, @postagem, @obs, @dtEnvio, @status, @usuario, @ar, @estoqueOrigem)";

        var parametros = new List<object[]>();
        parametros.Add(new object[] { "@idreqPeca", _requisicao});
        parametros.Add(new object[] { "@postagem", _postagem });
        parametros.Add(new object[] { "@obs", _obs });
        parametros.Add(new object[] { "@dtEnvio", DateTime.Now});
        parametros.Add(new object[] { "@status", "Atendido" });
        parametros.Add(new object[] { "@usuario", _usuario});
        parametros.Add(new object[] { "@ar", _ar});
        parametros.Add(new object[] { "@estoqueOrigem", _estoque });

        result = DAO.ExecuteNonQuery(DAO.connString(), query, parametros);
        if (result)
        {
            string updateSql = "UPDATE reqPecas SET status = 'Atendido' WHERE idreqPeca = @idreqPeca";
            var parametros2 = new List<object[]>();
            parametros2.Add(new object[] { "@idreqPeca", _requisicao });

            result = DAO.ExecuteNonQuery(DAO.connString(), updateSql, parametros2);
        }
        return result;
    }

    public static bool Enviar(string _estoque, string _tipoEnvio, int _requisicao, string _serie, string _partNumber, string _postagem, string _obs, string _usuario, string _ar, double _valor)
    {
        bool result = false;
        string query = @"INSERT INTO atulPecas(idreqPeca, postagem, obs, dtEnvio, status, usuario, ar, estoqueOrigem, valor) 
values(@idreqPeca, @postagem, @obs, @dtEnvio, @status, @usuario, @ar, @estoqueOrigem, @valor)";

        var parametros = new List<object[]>();
        parametros.Add(new object[] { "@idreqPeca", _requisicao });
        parametros.Add(new object[] { "@postagem", _postagem });
        parametros.Add(new object[] { "@obs", _obs });
        parametros.Add(new object[] { "@dtEnvio", DateTime.Now });
        parametros.Add(new object[] { "@status", "Atendido" });
        parametros.Add(new object[] { "@usuario", _usuario });
        parametros.Add(new object[] { "@ar", _ar });
        parametros.Add(new object[] { "@estoqueOrigem", _estoque });
        parametros.Add(new object[] { "@valor", _valor });

        result = DAO.ExecuteNonQuery(DAO.connString(), query, parametros);
        if (result)
        {
            string updateSql = "UPDATE reqPecas SET status = 'Atendido' WHERE idreqPeca = @idreqPeca";
            var parametros2 = new List<object[]>();
            parametros2.Add(new object[] { "@idreqPeca", _requisicao });

            result = DAO.ExecuteNonQuery(DAO.connString(), updateSql, parametros2);
        }
        return result;
    }
}