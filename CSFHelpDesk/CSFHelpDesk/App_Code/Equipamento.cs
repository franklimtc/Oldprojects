using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Equipamentos
/// </summary>
public class Equipamento
{
    #region Campos
    private string _idEquipamento;
    private string _uf;
    private string _municipio;
    private string _unidade;
    private string _endereco;
    private string _contato;
    private string _setor;
    private string _marca;
    private string _modelo;
    private string _serie;
    private string _contador;
    private string _lote;
    private string _nf;
    private string _cliente;

    public string IdEquipamento
    {
        get
        {
            return _idEquipamento;
        }

        set
        {
            _idEquipamento = value;
        }
    }

   

    public string Uf
    {
        get
        {
            return _uf;
        }

        set
        {
            _uf = value;
        }
    }

    public string Municipio
    {
        get
        {
            return _municipio;
        }

        set
        {
            _municipio = value;
        }
    }

   

    public string Unidade
    {
        get
        {
            return _unidade;
        }

        set
        {
            _unidade = value;
        }
    }

    public string Endereco
    {
        get
        {
            return _endereco;
        }

        set
        {
            _endereco = value;
        }
    }

    public string Contato
    {
        get
        {
            return _contato;
        }

        set
        {
            _contato = value;
        }
    }

   
    public string Setor
    {
        get
        {
            return _setor;
        }

        set
        {
            _setor = value;
        }
    }

    public string Marca
    {
        get
        {
            return _marca;
        }

        set
        {
            _marca = value;
        }
    }

    public string Modelo
    {
        get
        {
            return _modelo;
        }

        set
        {
            _modelo = value;
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

    public string Contador
    {
        get
        {
            return _contador;
        }

        set
        {
            _contador = value;
        }
    }

    public string Lote
    {
        get
        {
            return _lote;
        }

        set
        {
            _lote = value;
        }
    }

    public string Nf
    {
        get
        {
            return _nf;
        }

        set
        {
            _nf = value;
        }
    }

    public string Cliente
    {
        get
        {
            return _cliente;
        }

        set
        {
            _cliente = value;
        }
    }
    #endregion
    public Equipamento()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static List<Equipamento> Listar()
    {
        List<Equipamento> lista = new List<Equipamento>();

        string tsql = "SELECT idEquipamento, uf, municipio, unidade, endereco, contato, setor, marca, modelo, serie,contador,lote,nf,cliente from dbo.equipamentos";
        DataTable dtEquipamentos = DAO.retornadt("DefaultConnection", tsql);
        foreach (DataRow eqp in dtEquipamentos.Rows)
        {
            Equipamento eqpto = new Equipamento();
            eqpto.IdEquipamento = eqp["idEquipamento"].ToString();
            eqpto.Uf = eqp["uf"].ToString();
            eqpto.Municipio = eqp["municipio"].ToString();
            eqpto.Unidade = eqp["unidade"].ToString();
            eqpto.Endereco = eqp["endereco"].ToString();
            eqpto.Contato = eqp["contato"].ToString();
            eqpto.Setor = eqp["setor"].ToString();
            eqpto.Marca = eqp["marca"].ToString();
            eqpto.Modelo = eqp["modelo"].ToString();
            eqpto.Serie = eqp["serie"].ToString();
            eqpto.Contador = eqp["contador"].ToString();
            eqpto.Lote = eqp["lote"].ToString();
            eqpto.Nf = eqp["nf"].ToString();
            eqpto.Cliente = eqp["cliente"].ToString();
            lista.Add(eqpto);
        }
        return lista;
    }

    public static List<Equipamento> ListarporContrato(string Cliente, string UserID)
    {
        List<Equipamento> lista = new List<Equipamento>();

        string tsql = string.Format(@"SELECT idEquipamento, uf, municipio, unidade, endereco, contato, setor, marca, modelo, serie,contador,lote,nf,cliente 
from dbo.equipamentos where cliente = '{0}' AND idEquipamento not in (SELECT idEquipamento FROM UsersEquipamentos WHERE userId='{1}')"
            , Cliente, UserID);
        DataTable dtEquipamentos = DAO.retornadt("DefaultConnection", tsql);
        foreach (DataRow eqp in dtEquipamentos.Rows)
        {
            Equipamento eqpto = new Equipamento();
            eqpto.IdEquipamento = eqp["idEquipamento"].ToString();
            eqpto.Uf = eqp["uf"].ToString();
            eqpto.Municipio = eqp["municipio"].ToString();
            eqpto.Unidade = eqp["unidade"].ToString();
            eqpto.Endereco = eqp["endereco"].ToString();
            eqpto.Contato = eqp["contato"].ToString();
            eqpto.Setor = eqp["setor"].ToString();
            eqpto.Marca = eqp["marca"].ToString();
            eqpto.Modelo = eqp["modelo"].ToString();
            eqpto.Serie = eqp["serie"].ToString();
            eqpto.Contador = eqp["contador"].ToString();
            eqpto.Lote = eqp["lote"].ToString();
            eqpto.Nf = eqp["nf"].ToString();
            eqpto.Cliente = eqp["cliente"].ToString();
            lista.Add(eqpto);
        }
        return lista;
    }

    public static List<Equipamento> ListarporUsuario(string UserId)
    {
        List<Equipamento> lista = new List<Equipamento>();

        string tsql = string.Format(@"SELECT idEquipamento, uf, municipio, unidade, endereco, contato, setor, marca, modelo, serie,contador,lote,nf,cliente 
        from dbo.equipamentos WHERE idEquipamento IN 
        (select idEquipamento from UsersEquipamentos where userId = '{0}')", UserId);

        DataTable dtEquipamentos = DAO.retornadt("DefaultConnection", tsql);
        foreach (DataRow eqp in dtEquipamentos.Rows)
        {
            Equipamento eqpto = new Equipamento();
            eqpto.IdEquipamento = eqp["idEquipamento"].ToString();
            eqpto.Uf = eqp["uf"].ToString();
            eqpto.Municipio = eqp["municipio"].ToString();
            eqpto.Unidade = eqp["unidade"].ToString();
            eqpto.Endereco = eqp["endereco"].ToString();
            eqpto.Contato = eqp["contato"].ToString();
            eqpto.Setor = eqp["setor"].ToString();
            eqpto.Marca = eqp["marca"].ToString();
            eqpto.Modelo = eqp["modelo"].ToString();
            eqpto.Serie = eqp["serie"].ToString();
            eqpto.Contador = eqp["contador"].ToString();
            eqpto.Lote = eqp["lote"].ToString();
            eqpto.Nf = eqp["nf"].ToString();
            eqpto.Cliente = eqp["cliente"].ToString();
            lista.Add(eqpto);
        }
        return lista;
    }

    public static bool AssociarEquipamento(string UserID, string idEquipamento)
    {
        bool result = false;
        string tsql = string.Format("INSERT INTO UsersEquipamentos(userId, idEquipamento) VALUES('{0}',{1});", UserID, idEquipamento);

        result = DAO.ExecuteNonQuery(DAO.connection.DefaultConnection.ToString(), tsql);

        return result;
    }

}