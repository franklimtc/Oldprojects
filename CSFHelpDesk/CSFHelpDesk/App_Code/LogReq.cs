using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for LogReq
/// </summary>
public class LogReq
{
    public enum tpComentario { Comentario, Atualizar_Status};
    
    #region Campos
    private string idlog;
    private string codReq;
    private string usuario;
    private string tipo;
    private string descricao;
    private string data;

    public string Idlog
    {
        get
        {
            return idlog;
        }

        set
        {
            idlog = value;
        }
    }

    public string CodReq
    {
        get
        {
            return codReq;
        }

        set
        {
            codReq = value;
        }
    }

    public string Usuario
    {
        get
        {
            return usuario;
        }

        set
        {
            usuario = value;
        }
    }

    public string Tipo
    {
        get
        {
            return tipo;
        }

        set
        {
            tipo = value;
        }
    }

    public string Descricao
    {
        get
        {
            return descricao;
        }

        set
        {
            descricao = value;
        }
    }

    public string Data
    {
        get
        {
            return data;
        }

        set
        {
            data = value;
        }
    }

    #endregion
    public LogReq()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static List<LogReq> Listar(string ReqCode)
    {
        List<LogReq> lista = new List<LogReq>();
        string tsql = string.Format("SELECT data, usuario, tipo, descricao FROM logsRequisicoes WHERE codReq = '{0}' ORDER BY DATA DESC", ReqCode);

        DataTable dtLogs = DAO.retornadt(DAO.connection.DefaultConnection.ToString(), tsql);
        if (dtLogs.Rows.Count > 0)
        {
            foreach (DataRow req in dtLogs.Rows)
            {
                LogReq log = new LogReq();
                log.Data = req["data"].ToString();
                log.Usuario = req["usuario"].ToString();
                log.Tipo = req["tipo"].ToString();
                log.Descricao = req["descricao"].ToString();
                lista.Add(log);
            }
        }
        return lista;
    }

    public bool Adicionar(string reqCod, tpComentario tipo, string UserName, string Descricao)
    {
        bool result = false;
        string tp = null;
        switch (tipo)
        {
            case tpComentario.Comentario:
                tp = "Comentário";
                break;
            case tpComentario.Atualizar_Status:
                tp = "Atualização de status";
                    break;
            default:
                break;
        }

        string tsql = string.Format("INSERT INTO logsRequisicoes(codReq, tipo, usuario, descricao) values('{0}', '{1}', '{2}', '{3}')"
            , reqCod, tp, UserName, Descricao);

        result = DAO.ExecuteNonQuery(DAO.connection.DefaultConnection.ToString(), tsql);

        Requisicao.AtualizarModificacao(reqCod, UserName);
        return result;
    }

  

    
}