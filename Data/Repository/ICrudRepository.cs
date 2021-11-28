using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet.Data.Repository
{


    public interface ICrudRepository<T, ID> : IRepository where T : BaseModel<ID>
    {
        Task<T> save(T entity);
        Task<Optional<T>> findById(ID id);
        Task<bool> existsById(ID id);
        Task<IEnumerable<T>> findAll();
        Task<IEnumerable<T>> findAllById(IEnumerable<ID> ids);
        Task<long> count();
        Task<bool> deleteById(ID id);
        bool delete(T entity);
        Task<int> getNextId();
    }
}