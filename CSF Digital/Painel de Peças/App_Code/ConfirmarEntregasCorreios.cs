using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de ConfirmarEntregasCorreios
/// </summary>
public class ConfirmarEntregasCorreios
{
    #region Campos
    public enum Status { Pendente, Em_analise, Concluido }
    private static string conexao = ConfigurationManager.ConnectionStrings["pecasSigep01"].ToString();

    private int _id;
    private string _postagem;
    private string _serie;
    private DateTime _dtEnvio;
    private DateTime _dtEntrega;
    private DateTime _dtConfirmacao;
    private string _operador;
    private string _servico;
    private string _status;
    private string _observacoes;

    public int Id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }

    public string Postagem
    {
        get
        {
            return _postagem;
        }

        set
        {
            _postagem = value;
        }
    }

    public string Serie
    {
        get
        {
            return _serie;
        }

        set
        {
            _serie = value;
        }
    }

    public DateTime DtEnvio
    {
        get
        {
            return _dtEnvio;
        }

        set
        {
            _dtEnvio = value;
        }
    }

    public DateTime DtEntrega
    {
        get
        {
            return _dtEntrega;
        }

        set
        {
            _dtEntrega = value;
        }
    }

    public DateTime DtConfirmacao
    {
        get
        {
            return _dtConfirmacao;
        }

        set
        {
            _dtConfirmacao = value;
        }
    }

    public string Operador
    {
        get
        {
            return _operador;
        }

        set
        {
            _operador = value;
        }
    }

    public string Servico
    {
        get
        {
            return _servico;
        }

        set
        {
            _servico = value;
        }
    }

    public string Status1
    {
        get
        {
            return _status;
        }

        set
        {
            _status = value;
        }
    }

    public string Observacoes
    {
        get
        {
            return _observacoes;
        }

        set
        {
            _observacoes = value;
        }
    }


    #endregion

    public ConfirmarEntregasCorreios(string _post, string _serie, DateTime _dataEnvio, DateTime _dataEntrega, string servico)
    {
        this.Postagem = _post;
        this.Serie = _serie;
        this.DtEnvio = _dataEnvio;
        this.DtEntrega = _dataEntrega;
        this.Servico = servico;
    }

    public void Registrar()
    {
        this.Status1 = Status.Pendente.ToString();

        string tsqlInsert = @"insert into confirmarEntregasCorreios(postagem, serie, dtEnvio, servico, status, dtEntrega)
            values(@postagem, @serie, @dtEnvio, @servico, @status, @dtEntrega)";

        List<object[]> parametros = new List<object[]>();
        parametros.Add(new object[] { "@postagem", this.Postagem });
        parametros.Add(new object[] { "@serie", this.Serie });
        parametros.Add(new object[] { "@dtEnvio", this.DtEnvio });
        parametros.Add(new object[] { "@servico", this.Servico });
        parametros.Add(new object[] { "@status", this.Status1 });
        parametros.Add(new object[] { "@dtEntrega", this.DtEntrega });

        //new dnaPrint.DAO.SQLServer().ExecuteNonQuery(conexao, tsqlInsert, parametros);
    }

    public static bool ConfirmarEntrega(int id, string operador)
    {
        string tsqlUpdate = "update confirmarEntregasCorreios set status = @Status, operador = @operador, dtConfirmacao = getdate() where id = @id;";
        List<object[]> parametros = new List<object[]>();

        parametros.Add(new object[] { "@id", id });
        parametros.Add(new object[] { "@Status", Status.Concluido.ToString() });
        parametros.Add(new object[] { "@operador", operador });

        return DAO.ExecuteNonQuery(conexao, tsqlUpdate, parametros);
    }
}