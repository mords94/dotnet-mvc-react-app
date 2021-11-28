

using System.Threading.Tasks;
using dotnet.ViewModel.Paging;

namespace dotnet.Data.Repository
{
    public interface IPageRepository<T, ID> : ICrudRepository<T, ID> where T : BaseModel<ID>
    {
        Task<Page<T>> findAll(Pageable pageable);
    }
}