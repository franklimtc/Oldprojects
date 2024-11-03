using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPS_3.Repositorios
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<IList<T>> GetAll();
        Task Save(T obj);
        Task Update(T obj);
        Task Delete(Guid id);

    }
}