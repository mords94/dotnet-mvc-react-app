using System;
using dotnet.Data.Repository;
using dotnet.Models;

namespace dotnet.Repository
{
    public class CountryRepository : CrudRepository<Country, int>, ICountryRepository
    {
        public CountryRepository(DefaultdbContext context) : base(context)
        {
        }
    }
}