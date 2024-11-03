using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Equipamentos
/// </summary>
public class Equipamentos
{
    #region Campos
    private int _idEquipamento;
    private int _idCliente;
    private int _idModelo;
    public int idTecnico { get; set; }
    private string _serie;
    private string _uf;
    private string _cidade;
    private string _endereco;
    public string enderecoNumero { get; set; }
    private string _bairro;
    private string _cep;
    private string _contato;
    private string _fone;
    private string _email;
    private string _status;
    private string _unidade;
    private string _setor;
    public List<Suprimento> ListaSuprimentos { get; set; }

    public int IdEquipamento
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



    public int IdModelo
    {
        get
        {
            return _idModelo;
        }

        set
        {
            _idModelo = value;
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

    public string Cidade
    {
        get
        {
            return _cidade;
        }

        set
        {
            _cidade = value;
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

    public string Bairro
    {
        get
        {
            return _bairro;
        }

        set
        {
            _bairro = value;
        }
    }

    public string Cep
    {
        get
        {
            return _cep;
        }

        set
        {
            _cep = value;
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

    public string Fone
    {
        get
        {
            return _fone;
        }

        set
        {
            _fone = value;
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

    public string Status
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
    #endregion
    public Equipamentos()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Equipamentos(int _idEquipamento, string serie)
    {
        //
        // TODO: Add constructor logic here
        //
        this.IdEquipamento = _idEquipamento;
        this.Serie = serie;
    }

    public bool adicionar(string Usuario)
    {
        bool result = false;

        string tsql = @"insert into equipamentos(idTecnico, idCliente, idModelo, uf, cidade, endereco, enderecoNumero, unidade, setor, bairro, cep, contato, fone, email, serie, atualizadoPor)
values(@idTecnico, @idCliente, @idModelo, @uf, @cidade, @endereco, @enderecoNumero, @unidade, @setor, @bairro, @cep, @contato, @fone, @email, @serie, @atualizadoPor) ";

        List<object[]> parametros = new List<object[]>();

        parametros.Add(new object[] { "@idCliente", this.IdCliente });
        parametros.Add(new object[] { "@idTecnico", this.idTecnico });
        parametros.Add(new object[] { "@idModelo", this.IdModelo });
        parametros.Add(new object[] { "@uf", this.Uf });
        parametros.Add(new object[] { "@cidade", this.Cidade });
        parametros.Add(new object[] { "@endereco", this.Endereco });
        parametros.Add(new object[] { "@enderecoNumero", this.enderecoNumero });
        parametros.Add(new object[] { "@unidade", this.Unidade });
        parametros.Add(new object[] { "@setor", this.Setor });
        parametros.Add(new object[] { "@bairro", this.Bairro });
        parametros.Add(new object[] { "@cep", this.Cep });
        parametros.Add(new object[] { "@fone", this.Fone });
        parametros.Add(new object[] { "@email", this.Email });
        parametros.Add(new object[] { "@serie", this.Serie });
        parametros.Add(new object[] { "@atualizadoPor", Usuario});
        parametros.Add(new object[] { "@contato", this.Contato});
        result = DAO.ExecuteNonQuery(DAO.connString(), tsql, parametros);

        return result;
    }
    public bool Atualizar(string Usuario)
    {
        bool result = false;
        //        string tsql = string.Format(@"UPDATE [dbo].[equipamentos] SET [endereco] = '{1}',[bairro] = '{2}',[cep] = '{3}',[contato] = '{4}',[fone] = '{5}',[email] = '{6}',dtAtualizacao = getdate(), atualizadoPor = '{7}' WHERE serie = '{0}'"
        //, this.Serie, this.Endereco, this.Bairro, this.Cep, this.Contato, this.Fone, this.Email, usuario);
        //        result = DAO.ExecuteNonQuery(DAO.connString(), tsql);

        #region TSQL

        string tsql = @"update equipamentos set 
idCliente = @idCliente
, uf = @uf
, cidade = @cidade
, idTecnico = @idTecnico
, endereco = @endereco
, enderecoNumero = @enderecoNumero
, unidade = @unidade
, setor = @setor
, bairro = @bairro
, cep = @cep
, contato = @contato
, fone = @fone
, email = @email
, atualizadoPor = @atualizadoPor
, dtAtualizacao = getdate()
 where idEquipamento = @idEquipamento";
        #endregion

        List<object[]> parametros = new List<object[]>();

        #region Parametros
        parametros.Add(new object[] { "@idEquipamento", this.IdEquipamento });
        parametros.Add(new object[] { "@idCliente", this.IdCliente });
        parametros.Add(new object[] { "@idTecnico", this.idTecnico });
        parametros.Add(new object[] { "@uf", this.Uf });
        parametros.Add(new object[] { "@cidade", this.Cidade });
        parametros.Add(new object[] { "@endereco", this.Endereco });
        parametros.Add(new object[] { "@enderecoNumero", this.enderecoNumero });
        parametros.Add(new object[] { "@unidade", this.Unidade });
        parametros.Add(new object[] { "@setor", this.Setor });
        parametros.Add(new object[] { "@bairro", this.Bairro });
        parametros.Add(new object[] { "@cep", this.Cep });
        parametros.Add(new object[] { "@fone", this.Fone });
        parametros.Add(new object[] { "@email", this.Email });
        parametros.Add(new object[] { "@atualizadoPor", Usuario });
        parametros.Add(new object[] { "@contato", this.Contato });
        #endregion

        result = DAO.ExecuteNonQuery(DAO.connString(), tsql, parametros);
        return result;
    }

    public static Equipamentos Localizar(string serie)
    {
        string tsql = string.Format("select top 1 idcliente, idTecnico, idEquipamento, idModelo, serie, uf, cidade, endereco, enderecoNumero, bairro, cep, fone, contato, email, unidade, setor from equipamentos where serie = '{0}' and status = 1", serie);

        DataTable dtEquipamentos = DAO.RetornaDt(DAO.connString(), tsql);
        Equipamentos eqp = new Equipamentos();
        if(dtEquipamentos.Rows.Count >0)
        {
            foreach (DataRow eqpto in dtEquipamentos.Rows)
            {
                eqp.IdEquipamento = int.Parse(eqpto["idEquipamento"].ToString());
                eqp.Serie = eqpto["serie"].ToString();
                eqp.Uf = eqpto["uf"].ToString();
                eqp.Cidade = eqpto["cidade"].ToString();
                eqp.Endereco = eqpto["endereco"].ToString();
                eqp.Bairro = eqpto["bairro"].ToString();
                eqp.Cep = eqpto["cep"].ToString();
                eqp.Fone = eqpto["fone"].ToString();
                eqp.Email = eqpto["email"].ToString();
                eqp.Contato = eqpto["contato"].ToString();
                eqp.Unidade = eqpto["unidade"].ToString();
                eqp.Setor = eqpto["setor"].ToString();
                eqp.enderecoNumero = eqpto["enderecoNumero"].ToString();
                eqp.IdCliente = int.Parse(eqpto["idcliente"].ToString());
                eqp.idTecnico = int.Parse(eqpto["idTecnico"].ToString());
                eqp.IdModelo = int.Parse(eqpto["idModelo"].ToString());
            }
        }

        return eqp;
    }

    public static void CorrigirCeps(string cepAntigo, string cepNovo)
    {
        string tsqlUpdate = string.Format("update equipamentos set cep = '{0}', cepValido = 1 where cep = '{1}';", cepNovo, cepAntigo);
        DAO.ExecuteNonQuery(DAO.connString(), tsqlUpdate);
    }

    public static bool Existe(string serie)
    {
        bool result = false;
        int qtd = 0;
        string tsql = string.Format("select count(*) 'qtd' from equipamentos where serie like '%{0}%'", serie.Trim());
        string valor = DAO.ExecuteScalar(DAO.connString(), tsql);
        int.TryParse(valor, out qtd);
        if (qtd > 0)
            result = true;

        return result;
    }

    public static bool CadastroAtualizado(string serie)
    {
        bool result = false;
        string tsql = string.Format("select dtAtualizacao from equipamentos where serie = '{0}'", serie.Trim());

        DateTime dtAtualizacao;
        if (DateTime.TryParse(DAO.ExecuteScalar(DAO.connString(), tsql), out dtAtualizacao))
        {
            if (DateTime.Now.Subtract(dtAtualizacao).Days < 30)
                result = true;
        }

        return result;
    }

    public List<Suprimento> CarregarSuprimentos()
    {
        List<Suprimento> listaSuprimentos = new List<Suprimento>();

        string tsql = string.Format("select idModeloSuprimento, suprimento from modelosSuprimentos where idModelo = {0}", this.IdModelo);
        DataTable dtSuprimentos = DAO.RetornaDt(DAO.connString(), tsql);

        if (dtSuprimentos.Rows.Count > 0)
        {
            foreach (DataRow supr in dtSuprimentos.Rows)
            {
                listaSuprimentos.Add(new Suprimento(int.Parse(supr["idModeloSuprimento"].ToString()), supr["suprimento"].ToString()));
            }
        }

        return listaSuprimentos;
    }

    #region Novo Código

    public static List<Equipamentos> Listar(int idCliente, string uf, string cidade, string unidade)
    {
        List<Equipamentos> lista = new List<Equipamentos>();

        DataTable dt = DAO.RetornaDt(DAO.connString(), string.Format("select idEquipamento, serie from equipamentos WHERE idCliente = {0} and UF like '%{1}%' and Cidade like '%{2}%' and Unidade like '%{3}%' order by 1", idCliente, uf, cidade, unidade));

        foreach (DataRow row in dt.Rows)
        {
            lista.Add(new Equipamentos(int.Parse(row["idEquipamento"].ToString()), row["serie"].ToString()));
        }

        return lista;
    }

    #endregion
}