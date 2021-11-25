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
        private ICountryRepository CountryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            CountryRepository = countryRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Country>> Get()
        {
            return await CountryRepository.findAll();
        }

        [HttpGet]
        [Route("/all")]
        public NoContentResult GetAll()
        {
            return NoContent();
        }
    }
}
