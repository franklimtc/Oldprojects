using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Envios
/// </summary>
public class Envios
{

    #region Campos
    private string _idEnvio;
    private string _serie;
    private string _partNumber;
    private string _etiqueta;
    private string _tpEnvio;
    private string _postagem;
    private string _valor;
    private string _usuario;
    private string _dtEnvio;
    private string _statusEntrega;
    private string _idRequisicao;
    private string _prazoEntrega;
    private string _dtEntrega;
    private string _entregueEm;
    private string _verificado;

    public string IdEnvio
    {
        get
        {
            return _idEnvio;
        }

        set
        {
            _idEnvio = value;
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

    public string PartNumber
    {
        get
        {
            return _partNumber;
        }

        set
        {
            _partNumber = value;
        }
    }

    public string Etiqueta
    {
        get
        {
            return _etiqueta;
        }

        set
        {
            _etiqueta = value;
        }
    }

    public string TpEnvio
    {
        get
        {
            return _tpEnvio;
        }

        set
        {
            _tpEnvio = value;
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

    public string Valor
    {
        get
        {
            return _valor;
        }

        set
        {
            _valor = value;
        }
    }

    public string Usuario
    {
        get
        {
            return _usuario;
        }

        set
        {
            _usuario = value;
        }
    }

    public string DtEnvio
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

    public string StatusEntrega
    {
        get
        {
            return _statusEntrega;
        }

        set
        {
            _statusEntrega = value;
        }
    }

    public string IdRequisicao
    {
        get
        {
            return _idRequisicao;
        }

        set
        {
            _idRequisicao = value;
        }
    }

    public string PrazoEntrega
    {
        get
        {
            return _prazoEntrega;
        }

        set
        {
            _prazoEntrega = value;
        }
    }

    public string DtEntrega
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

    public string EntregueEm
    {
        get
        {
            return _entregueEm;
        }

        set
        {
            _entregueEm = value;
        }
    }

    public string Verificado
    {
        get
        {
            return _verificado;
        }

        set
        {
            _verificado = value;
        }
    }
    #endregion

    public Envios()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool Excluir(string id)
    {
        bool result = false;

        string tsqlDelete = string.Format("delete EnviosSuprimentos where idEnvio = {0}", id);
        result = DAO.ExecuteNonQuery(DAO.connString(), tsqlDelete);

        return result;
    }
}

