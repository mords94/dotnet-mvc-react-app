using System;
using dotnet.Data.Repository;
using dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Repository
{
    public class CountryRepository : CrudRepository<Country, int>
    {
        public CountryRepository(DefaultdbContext context) : base(context)
        {
        }
    }
}