using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de UF
/// </summary>
public class UF
{
    private string _uf;

    public UF(string uf)
    {
        //
        // TODO: Adicionar lógica do construtor aqui
        //
        this.Uf = uf;
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

    public static List<UF> Listar(int idCliente)
    {
        List<UF> lista = new List<UF>();

        DataTable dt = DAO.RetornaDt(DAO.connString(), string.Format("select distinct UF from equipamentos where idCliente = {0} order by 1", idCliente));

        foreach (DataRow row in dt.Rows)
        {
            lista.Add(new UF(row["UF"].ToString()));
        }

        return lista;
    }
}