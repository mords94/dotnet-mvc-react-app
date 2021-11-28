using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using dotnet.Models;
using dotnet.Repository;
using dotnet.ViewModel.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/guest")]
    public class GuestController : ControllerBase
    {
        private readonly IMapper Mapper;

        private GuestRepository GuestRepository;

        private IListMapper ListMapper;

        public GuestController(GuestRepository guestRepository, IMapper mapper, IListMapper listMapper)
        {
            GuestRepository = guestRepository;
            Mapper = mapper;
            ListMapper = listMapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<Page<GuestViewModel>> Get([FromQuery] int page, [FromQuery] int? size)
        {
            return ListMapper.MapPage<Guest, GuestViewModel>(await GuestRepository.findAll(new Pageable(page, size)));
        }

        [HttpPost]
        [Authorize]
        public async Task<GuestViewModel> Create([FromBody] GuestDto model)
        {
            Guest guest = Mapper.Map<GuestDto, Guest>(model);
            return Mapper.Map<Guest, GuestViewModel>(await GuestRepository.save(guest));
        }

        [HttpPatch]
        [Authorize]
        [Route("{id}")]
        public async Task<GuestViewModel> Update([FromRoute] int id, [FromBody] GuestDto model)
        {
            Guest guest = Mapper.Map<GuestDto, Guest>(model);
            guest.Id = id;
            return Mapper.Map<Guest, GuestViewModel>(await GuestRepository.save(guest));
        }
    }
}
