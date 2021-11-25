using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Data.Repository
{
    public class CrudRepository<T, ID> : ICrudRepository<T, ID> where T : BaseModel
    {
        internal readonly DefaultdbContext Context;

        internal DbSet<T> dbSet;

        public CrudRepository(DefaultdbContext context)
        {
            Context = context;
            dbSet = context.Set<T>();
        }

        public async Task<long> count()
        {
            return await dbSet.CountAsync<T>();
        }

        public Task delete(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task deleteAll(IEnumerable<T> entities)
        {
            throw new System.NotImplementedException();
        }

        public Task deleteAll()
        {
            throw new System.NotImplementedException();
        }

        public Task deleteAllById(IEnumerable<ID> ids)
        {
            throw new System.NotImplementedException();
        }

        public Task deleteById(ID id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> existsById(ID id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<T>> findAll()
        {
            return await dbSet.ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> findAllById(IEnumerable<ID> ids)
        {
            throw new System.NotImplementedException();

        }

        public async Task<Optional<T>> findById(ID id)
        {
            var entity = await dbSet.FindAsync<T>(id);
            return Optional<T>.Of(entity);

        }

        public Task<T> save(T entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> saveAll(IEnumerable<T> entities)
        {
            throw new System.NotImplementedException();
        }

    }
}