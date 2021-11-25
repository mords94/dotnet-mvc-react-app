using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet.Data.Repository
{


    public interface ICrudRepository<T, ID> : IRepository
    {
        Task<T> save(T entity);
        IEnumerable<T> saveAll(IEnumerable<T> entities);
        Task<Optional<T>> findById(ID id);
        Task<bool> existsById(ID id);
        Task<IEnumerable<T>> findAll();
        Task<IEnumerable<T>> findAllById(IEnumerable<ID> ids);
        Task<long> count();
        Task deleteById(ID id);
        Task delete(T entity);
        Task deleteAllById(IEnumerable<ID> ids);
        Task deleteAll(IEnumerable<T> entities);
        Task deleteAll();
    }
}