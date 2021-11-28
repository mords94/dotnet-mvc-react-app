using System;
using System.Threading.Tasks;
using dotnet.Data.Repository;
using dotnet.Models;

namespace dotnet.Repository
{
    public class PlaceRepository : CrudRepository<Place, Guid>
    {
        public PlaceRepository(DefaultdbContext context) : base(context)
        {
        }
    }
}