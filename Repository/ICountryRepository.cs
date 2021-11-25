using dotnet.Data.Repository;
using dotnet.Models;

namespace dotnet.Repository
{
    public interface ICountryRepository : ICrudRepository<Country, int>
    {
    }
}