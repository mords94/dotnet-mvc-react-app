using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet.Models;
using dotnet.Repository;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers
{
    [ApiController]
    [Route("/api/country")]
    public class CountryController : ControllerBase
    {
        private CountryRepository CountryRepository;

        public CountryController(CountryRepository countryRepository)
        {
            CountryRepository = countryRepository;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<Optional<Country>> Get(int id)
        {
            return await CountryRepository.findById(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Country>> GetAll()
        {
            return await CountryRepository.findAll();
        }

    }
}
