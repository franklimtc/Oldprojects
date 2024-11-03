using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de EnvioSuprimento
/// </summary>
public class EnvioSuprimento :IBase
{
    #region Campos

    private int _idEnvio;
    private string _serie;
    private string _partNumber;
    private string _etiqueta;
    private string _tpEnvio;
    private string _postagem;
    private double _valor;
    private string _usuario;
    private DateTime _dtEnvio;
    private string _statusEntrega;
    private int _idRequisicao;
    private int _prazoEntrega;
    private DateTime _dtEntrega;
    private int _entregueEm;
    private bool _verificado;
    private string _filial;
    private string _AR;
    private bool _inserida;

    
   
    public int IdEnvio
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

    public double Valor
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

    public int IdRequisicao
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

    public int PrazoEntrega
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

    public int EntregueEm
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

    public bool Verificado
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

    public string Filial
    {
        get
        {
            return _filial;
        }

        set
        {
            _filial = value;
        }
    }

    public string AR
    {
        get
        {
            return _AR;
        }

        set
        {
            _AR = value;
        }
    }

 

    public bool Inserida
    {
        get
        {
            return _inserida;
        }

        set
        {
            _inserida = value;
        }
    }

    #endregion

    public EnvioSuprimento()
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
    }

    public EnvioSuprimento(string _serie, string _partnumber, string _etiqueta, string _tpenvio, string _usuario)
    {
        this.Serie = _serie;
        this.PartNumber = _partnumber;
        this.Etiqueta = _etiqueta;
        this.TpEnvio = _tpenvio;
        this.Usuario = _usuario;
    }

    public bool Salvar()
    {
        throw new NotImplementedException();
    }

    public bool Adicionar()
    {
        bool result = false;
        string tsql = "insert into enviosSuprimentos(serie, partNumber, etiqueta, tpEnvio, usuario, filial, idRequisicao, valor, postagem) values(@serie, @partNumber, @etiqueta, @tpEnvio, @usuario, @filial, @idRequisicao, @valor, @postagem);";

        List<string[]> parametros = new List<string[]>();

        parametros.Add(new string[] { "@serie", this.Serie });
        parametros.Add(new string[] { "@partNumber", this.PartNumber});
        parametros.Add(new string[] { "@etiqueta", this.Etiqueta});
        parametros.Add(new string[] { "@tpEnvio", this.TpEnvio});
        parametros.Add(new string[] { "@usuario", this.Usuario});
        parametros.Add(new string[] { "@filial", this.Filial});
        parametros.Add(new string[] { "@valor", this.Valor.ToString().Replace(",", ".") });
        parametros.Add(new string[] { "@idRequisicao", this.IdRequisicao.ToString()});
        parametros.Add(new string[] { "@postagem", this.Postagem});

        result = DAO.ExecuteNonQuery(DAO.connString(), tsql, parametros);

        return result;
    }

    public bool Deletar()
    {
        throw new NotImplementedException();
    }

    public bool BaixaEstoque(int idEstoque, int idProduto, string Usuario)
    {
        movimentacao novaMov = new movimentacao(idEstoque, idProduto, 1, movimentacao.descricao.Saida, Usuario);
        return novaMov.Adicionar();
    }
    public bool BaixaEstoque(int idEstoque, int idProduto, string Usuario, string Serie)
    {
        movimentacao novaMov = new movimentacao(idEstoque, idProduto, 1, movimentacao.descricao.Saida, Usuario);
        novaMov.Serie = Serie;
        return novaMov.Adicionar();
    }

    public static List<EnvioSuprimento> ListarPorData(DateTime dataInicial, DateTime dataFinal)
    {
        List<EnvioSuprimento> Lista = new List<EnvioSuprimento>();

        string tsql = @"select idEnvio, serie, partNumber, etiqueta, tpEnvio, postagem, valor, usuario
        , dtEnvio, statusEntrega, idRequisicao, prazoEntrega, dtEntrega, entregueEm
        , verificado, filial, AR, inserida 
        from enviosSuprimentos WHERE dtenvio between @dtInicial and DATEADD(DAY,1,@dtFinal)";

        List<string[]> parametros = new List<string[]>();
        parametros.Add(new string[] { "@dtInicial", dataInicial.ToString() });
        parametros.Add(new string[] { "@dtFinal", dataFinal.ToString() });

        DataTable dt = DAO.RetornaDt(DAO.connString(), tsql, parametros);
        if(dt.Rows.Count>0)
        {
            foreach (DataRow row in dt.Rows)
            {
                EnvioSuprimento envio = new EnvioSuprimento();
                //idEnvio
                envio.IdEnvio = int.Parse(row["idEnvio"].ToString());
                //serie
                envio.Serie = row["serie"].ToString();

                //partNumber
                envio.PartNumber = row["partNumber"].ToString();

                //etiqueta
                envio.Etiqueta = row["etiqueta"].ToString();

                //tpEnvio
                envio.TpEnvio = row["tpEnvio"].ToString();

                //postagem
                envio.Postagem = row["postagem"].ToString();
                
                //dtEnvio
                envio.DtEnvio = DateTime.Parse(row["dtEnvio"].ToString());
                
                //usuario
                envio.Usuario = row["usuario"].ToString();

                //filial
                envio.Filial = row["filial"].ToString();

                Lista.Add(envio);
            }
        }

        return Lista;
    }
}