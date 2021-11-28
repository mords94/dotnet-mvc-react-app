using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet.DTO;
using dotnet.DTO.Request;
using dotnet.Models;
using dotnet.Repository;
using dotnet.ViewModel.Paging;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers
{
    [ApiController]
    [Route("/api/visit")]
    public class VisitController : ControllerBase
    {
        private IMapper Mapper;
        private IListMapper ListMapper;

        private VisitRepository VisitRepository;
        private GuestRepository GuestRepository;
        private PlaceRepository PlaceRepository;

        public VisitController(VisitRepository visitRepository, GuestRepository guestRepository, PlaceRepository placeRepository, IMapper mapper, IListMapper listMapper)
        {
            VisitRepository = visitRepository;
            GuestRepository = guestRepository;
            PlaceRepository = placeRepository;
            Mapper = mapper;
            ListMapper = listMapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<Page<VisitViewModel>> Get([FromQuery] int page, [FromQuery] int? size)
        {
            Pageable validPageFilter = new Pageable(page, size);
            var visits = await VisitRepository.findAll(validPageFilter);


            return ListMapper.MapPage<Visit, VisitViewModel>(visits);
        }


        // @PatchMapping("/{id}")
        // @Operation(summary = "Updates a visit", description = "Only a check-out date can be set ", tags = { "visit" })
        // public ResponseEntity<?> updateVisit(@PathVariable Integer id, @Valid @RequestBody UpdateVisitRequest dto) {
        //     Visit visit = visitRepository.findById(id).orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND));

        //     visit.setFinishDate(dto.getFinishDate());
        //     visitRepository.save(visit);
        //     return ResponseEntity.ok(visit);
        // }

        // @DeleteMapping("/{id}")
        // @Operation(summary = "Deletes a visit", tags = { "visit" })
        // @ResponseStatus(HttpStatus.NO_CONTENT)
        // public ResponseEntity<Void> deleteVisit(@PathVariable Integer id) {
        //     visitRepository.deleteById(id);
        //     return ResponseEntity.noContent().build();
        // }


        [HttpPatch("{id}")]
        [Authorize]
        public async Task<VisitViewModel> UpdateVisit([FromRoute] int id, [FromBody] UpdateVisitDto updateVisitDto)
        {
            var maybeVisit = await VisitRepository.findById(id);
            Visit visit = maybeVisit.OrElseThrow(() => ResponseStatusException.NotFound("Visit not found with id: " + id));

            visit.FinishDate = updateVisitDto.FinishDate;
            return Mapper.Map<Visit, VisitViewModel>(await VisitRepository.save(visit));
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteVisit([FromRoute] int id)
        {
            await VisitRepository.deleteById(id);

            return NoContent();
        }


        [HttpGet("current_user/all")]
        [Authorize]
        public async Task<IEnumerable<VisitViewModel>> GetAllVisitForCurrentUser()
        {
            User user = (User)HttpContext.Items["User"];

            Optional<Guest> maybeGuest = await GuestRepository.findByEmail(user.personDetails.Email);
            Guest guest = maybeGuest.OrElseThrow<ResponseStatusException>(() => ResponseStatusException.NotFound("Guest not found by e-mail"));

            IEnumerable<Visit> visits = await VisitRepository.findAllWhere(visit => visit.Guests.Any(vg => vg.Id == guest.Id));


            return ListMapper.Map<Visit, VisitViewModel>(visits);
        }

        [HttpGet("current_user")]
        [Authorize]
        public async Task<VisitViewModel> GetVisitForCurrentUser()
        {
            User user = (User)HttpContext.Items["User"];

            Optional<Guest> maybeGuest = await GuestRepository.findByEmail(user.personDetails.Email);
            Guest guest = maybeGuest.OrElseThrow<ResponseStatusException>(() => ResponseStatusException.NotFound("Guest with this e-mail not found"));

            Optional<Visit> maybeVisit = await VisitRepository.findWhere(visit => visit.Guests.Any(vg => vg.Id == guest.Id) && visit.FinishDate == null, last: true);

            Visit visit = maybeVisit.OrElseThrow(() => ResponseStatusException.NotFound("Visit not found"));

            return Mapper.Map<Visit, VisitViewModel>(visit);
        }


        [HttpPost]
        [Authorize]
        public async Task<VisitViewModel> CreateVisit([FromBody] VisitDto dto)
        {
            User user = (User)HttpContext.Items["User"];


            Visit visit = new Visit();

            List<Guest> guestsToSave = new List<Guest>();

            var guestsInDatabase = await GuestRepository.findAllByEmails(dto.Guests.Select(g => g.PersonDetails.Email));

            guestsToSave.AddRange(guestsInDatabase);
            var emails = guestsToSave.Select(gts => gts.PersonDetails.Email).ToList();
            IEnumerable<Guest> newGuests = ListMapper.Map<GuestDto, Guest>(dto.Guests.Where(dtog => !emails.Contains(dtog.PersonDetails.Email)));
            foreach (Guest guestToSave in newGuests)
            {
                guestsToSave.Add(await GuestRepository.save(guestToSave));
            }


            var maybePlace = await PlaceRepository.findById(dto.Place.Id);

            var Place = maybePlace.OrElseThrow<ResponseStatusException>(() => ResponseStatusException.NotFound("Place with this uuid not found"));

            visit.Guests = guestsToSave;
            visit.Place = Place;
            visit.PlaceId = Place.Id;
            visit.VisitDate = System.DateTime.Now;


            return Mapper.Map<Visit, VisitViewModel>(await VisitRepository.save(visit));
        }

    }
}
