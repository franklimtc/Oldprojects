using System.Collections.Generic;

namespace dnaPrint.Base
{
    public interface IBase<TEntity> where TEntity : class
    {
        bool Atualizar(string connString, DAO.Operacoes.tipo Tipo);
        bool Adicionar(string connString, DAO.Operacoes.tipo Tipo);
        bool Excluir(string connString, DAO.Operacoes.tipo Tipo);
        TEntity ListarByID(string connString, DAO.Operacoes.tipo Tipo, int id);
    }
}
