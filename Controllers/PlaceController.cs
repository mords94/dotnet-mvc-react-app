using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using dotnet.Models;
using dotnet.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/place")]
    public class PlaceController : ControllerBase
    {
        private readonly IMapper Mapper;

        private PlaceRepository PlaceRepository;
        private IListMapper ListMapper;

        public PlaceController(PlaceRepository placeRepository, IMapper mapper, IListMapper listMapper)
        {
            PlaceRepository = placeRepository;
            Mapper = mapper;
            ListMapper = listMapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PlaceViewModel>> Get()
        {
            var placeList = await PlaceRepository.findAll();

            return ListMapper.Map<Place, PlaceViewModel>(placeList);
        }

        [HttpPost]
        public async Task<PlaceViewModel> Create([FromBody] PlaceDto model)
        {
            Place place = Mapper.Map<PlaceDto, Place>(model);
            return Mapper.Map<Place, PlaceViewModel>(await PlaceRepository.save(place));
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<Place> Update([FromRoute] Guid id, [FromBody] PlaceDto model)
        {
            Place place = Mapper.Map<PlaceDto, Place>(model);
            place.Id = id;
            return await PlaceRepository.save(place);
        }
    }
}
