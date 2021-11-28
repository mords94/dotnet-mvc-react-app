using dotnet.Data.Repository;
using dotnet.Models;

namespace dotnet.Repository
{
    public class VisitRepository : PageRepository<Visit, int?>
    {
        public VisitRepository(DefaultdbContext context) : base(context)
        {
        }
    }
}