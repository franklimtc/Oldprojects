using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.Objetos
{
    public interface IBase<TEntity> where TEntity : class
    {
        IQueryable<TEntity> ListarTodos();
        IQueryable<TEntity> Listar(Func<TEntity , bool> predicate);
        void Atualizar(TEntity obj);
    }
}
