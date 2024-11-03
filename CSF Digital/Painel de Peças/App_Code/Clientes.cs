using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Clientes
/// </summary>
public class Clientes : IBase
{
    #region
    private string _cliente;
    private string _endereco;
    private string _responsavel;
    private string _telefone;
    private string _email;
    private int _idCliente;
    private int _qtd;

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

    public string Responsavel
    {
        get
        {
            return _responsavel;
        }

        set
        {
            _responsavel = value;
        }
    }

    public string Telefone
    {
        get
        {
            return _telefone;
        }

        set
        {
            _telefone = value;
        }
    }

    public string Email
    {
        get
        {
            return _email;
        }

        set
        {
            _email = value;
        }
    }

    public int IdCliente
    {
        get
        {
            return _idCliente;
        }

        set
        {
            _idCliente = value;
        }
    }

    public int Qtd
    {
        get
        {
            return _qtd;
        }

        set
        {
            _qtd = value;
        }
    }

    #endregion

    public Clientes()
    {

    }

    public Clientes(string cliente, string endereco, string responsavel, string telefone, string email)
    {
        //
        // TODO: Add constructor logic here
        //

        this.Cliente = cliente;
        this.Endereco = endereco;
        this.Responsavel = responsavel;
        this.Telefone = telefone;
        this.Email = email;
    }

    public bool Inserir()
    {
        string tsql = string.Format("insert into clientes(cliente,endereco,responsavel,telefone,email) values('{0}','{1}','{2}','{3}','{4}');",
            this.Cliente, this.Endereco, this.Responsavel, this.Telefone, this.Email);
        return DAO.ExecuteNonQuery(DAO.connString(), tsql);

    }

    public bool Salvar()
    {
        bool result = false;
        string tsql = "update clientes set endereco = @endereco, responsavel = @responsavel, email = @email where idCliente = @idCliente;";

        List<string[]> parametros = new List<string[]>();

        parametros.Add(new string[] { "@endereco", this.Endereco });
        parametros.Add(new string[] { "@responsavel", this.Responsavel });
        parametros.Add(new string[] { "@telefone", this.Telefone });
        parametros.Add(new string[] { "@email", this.Email });
        parametros.Add(new string[] { "@idCliente", this.IdCliente.ToString() });

        result = DAO.ExecuteNonQuery(DAO.connString(), tsql, parametros);

        return result;
    }

    public bool Adicionar()
    {
        bool result = false;
        string tsql = "insert into clientes(cliente, endereco, responsavel, telefone, email) values(@cliente, @endereco, @responsavel, @telefone, @email);";

        List<string[]> parametros = new List<string[]>();

        parametros.Add(new string[] { "@cliente", this.Cliente });
        parametros.Add(new string[] { "@endereco", this.Endereco });
        parametros.Add(new string[] { "@responsavel", this.Responsavel });
        parametros.Add(new string[] { "@telefone", this.Telefone });
        parametros.Add(new string[] { "@email", this.Email });

        result = DAO.ExecuteNonQuery(DAO.connString(), tsql, parametros);

        return result;
    }

    public static Clientes BuscaPorId(int _idCliente)
    {
        Clientes c = new Clientes();

        DataTable dt = DAO.RetornaDt(DAO.connString(), string.Format("select idCliente, cliente, endereco, responsavel, telefone, email, qtd, dtCriacao from vw_clientes where idcliente = {0}", _idCliente));

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                c.IdCliente = _idCliente;
                c.Cliente = row["cliente"].ToString();
                c.Endereco = row["endereco"].ToString();
                c.Responsavel = row["responsavel"].ToString();
                c.Telefone = row["telefone"].ToString();
                c.Email = row["email"].ToString();
                c.Qtd = int.Parse(row["qtd"].ToString());

            }
        }

        return c;
    }

    public static List<Clientes> Listar()
    {
        List<Clientes> lista = new List<Clientes>();
        DataTable dt = DAO.RetornaDt(DAO.connString(), string.Format("select idCliente, cliente, endereco, responsavel, telefone, email, qtd, dtCriacao from vw_clientes"));

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                Clientes c = new Clientes();
                c.IdCliente = int.Parse(row["idCliente"].ToString());
                c.Cliente = row["cliente"].ToString();
                c.Endereco = row["endereco"].ToString();
                c.Responsavel = row["responsavel"].ToString();
                c.Telefone = row["telefone"].ToString();
                c.Email = row["email"].ToString();
                c.Qtd = int.Parse(row["qtd"].ToString());
                lista.Add(c);
            }
        }
        return lista;
    }


    public bool Deletar()
    {
        throw new NotImplementedException();
    }
}