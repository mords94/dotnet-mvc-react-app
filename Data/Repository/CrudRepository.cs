using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using dotnet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace dotnet.Data.Repository
{
    public class CrudRepository<T, ID> : ICrudRepository<T, ID> where T : BaseModel<ID>
    {
        internal readonly DefaultdbContext Context;

        protected DbSet<T> dbSet;

        protected readonly ILogger Logger;


        public CrudRepository(DefaultdbContext context)
        {
            Context = context;
            dbSet = context.Set<T>();
            Logger = new LoggerFactory().CreateLogger("Repository logger");
        }

        public async Task<long> count()
        {
            return await dbSet.CountAsync<T>();
        }

        public bool delete(T entity)
        {

            try
            {
                var res = dbSet.Remove(entity);
                return true;
            }
            catch
            {
                Logger.LogDebug("Cannot delete entity with id: " + entity.Id);
                return false;
            }

        }

        public async Task<bool> deleteById(ID id)
        {
            return delete((await findById(id)).Get());
        }

        public async Task<bool> existsById(ID id)
        {
            return await dbSet.AnyAsync<T>(entity => entity.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> findAll()
        {
            return await dbSet.ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> findAllById(IEnumerable<ID> ids)
        {
            return await dbSet.Where<T>(entity => ids.Contains<ID>(entity.Id)).ToListAsync<T>();
        }

        public async Task<Optional<T>> findById(ID id)
        {
            var entity = await dbSet.FindAsync(id);

            return await Task.FromResult<Optional<T>>(Optional<T>.Of(entity));
        }

        public async Task<T> save(T entity)
        {
            if (entity.Id == null)
            {
                if (typeof(ID) == typeof(int) || typeof(ID) == typeof(int?))
                {
                    entity.Id = (ID)((await getNextId()) as object);
                }
                else if (typeof(ID) == typeof(string))
                {
                    entity.Id = (ID)(Guid.NewGuid().ToString() as object);
                }
                else if (typeof(ID) == typeof(Guid))
                {
                    entity.Id = (ID)(Guid.NewGuid() as object);
                }
                else
                {
                    throw new InvalidCastException("Unsupported Id type: " + typeof(ID));
                }

                dbSet.Add(entity);
            }
            else
            {
                dbSet.Update(entity);
            }

            await Context.SaveChangesAsync();
            return entity;

        }

        public async Task<Optional<T>> findWhere(Expression<Func<T, bool>> predicate, bool last = false)
        {
            try
            {
                var query = dbSet.Where(predicate).OrderBy((e => e.Id));


                var entity = await (last ? query.LastAsync() : query.FirstAsync());

                return Optional<T>.Of(entity);
            }
            catch (InvalidOperationException)
            {
                return Optional<T>.Of(null);
            }
        }

        public async Task<IEnumerable<T>> findAllWhere(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }


        public IQueryable<T> findAllWhereQuery(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public async Task<int> getNextId()
        {
            var sequence = await Context.HibernateSequences.FirstAsync();
            var nextVal = sequence.NextVal + 1;

            Context.HibernateSequences.Remove(sequence);

            await Context.SaveChangesAsync();

            Context.HibernateSequences.Add(new HibernateSequence(nextVal));

            await Context.SaveChangesAsync();

            return (int)nextVal;
        }

        // public DbSet<T> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath)
        // {
        //     dbSet = dbSet.Include<TProperty>(navigationPropertyPath);
        // }
    }
}