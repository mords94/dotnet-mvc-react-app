using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet.Models;
using dotnet.ViewModel.Paging;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Data.Repository
{
    public class PageRepository<T, ID> : CrudRepository<T, ID>, IPageRepository<T, ID> where T : BaseModel<ID>
    {
        public PageRepository(DefaultdbContext context) : base(context)
        {
        }

        public async Task<Page<T>> findAll(Pageable pageable)
        {
            var count = await this.count();
            var items = await dbSet.Skip((pageable.Page - 1) * pageable.Size).Take(pageable.Size).ToListAsync();

            return new Page<T>(items, pageable, count);
        }
    }
}