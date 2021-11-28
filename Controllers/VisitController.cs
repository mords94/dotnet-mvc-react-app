using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        [HttpGet]
        [Authorize]
        [Route("current_user/all")]
        public async Task<IEnumerable<VisitViewModel>> GetAllVisitForCurrentUser()
        {
            User user = (User)HttpContext.Items["User"];

            Optional<Guest> maybeGuest = await GuestRepository.findByEmail(user.personDetails.Email);
            Guest guest = maybeGuest.OrElseThrow<ResponseStatusException>(() => ResponseStatusException.NotFound("Guest not found by e-mail"));

            IEnumerable<Visit> visits = await VisitRepository.findAllWhere(visit => visit.Guests.Any(vg => vg.Id == guest.Id));


            return ListMapper.Map<Visit, VisitViewModel>(visits);
        }

        // @Operation(summary = "Fetches an in-progress visit for the current logged in user", tags = { "visit" })
        // public ResponseEntity<Visit> getVisit(@RequestHeader("Authorization") String token) {
        //     String email = jwtTokenUtil.getUsernameFromToken(token.replace("Bearer ", ""));
        //     var user = userRepository.findByPersonDetailsEmail(email).get();

        //     Guest guest = guestRepository.findByPersonDetailsEmail(user.getPersonDetails().getEmail()).orElseThrow(
        //             () -> new ResponseStatusException(HttpStatus.NOT_FOUND, "Guest with this e-mail not found"));

        //     var visits = guest.getVisits();

        //     Visit visit = visits.stream().filter(v -> v.getFinishDate() == null).findFirst()
        //             .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND, "Visit not found"));

        //     return ResponseEntity.ok(visit);
        // }

        [HttpGet]
        [Authorize]
        [Route("current_user")]
        public async Task<VisitViewModel> GetVisitForCurrentUser()
        {
            User user = (User)HttpContext.Items["User"];

            Optional<Guest> maybeGuest = await GuestRepository.findByEmail(user.personDetails.Email);
            Guest guest = maybeGuest.OrElseThrow<ResponseStatusException>(() => ResponseStatusException.NotFound("Guest with this e-mail not found"));

            Optional<Visit> maybeVisit = await VisitRepository.findWhere(visit => visit.Guests.Any(vg => vg.Id == guest.Id), last: true);

            Visit visit = maybeVisit.OrElseThrow(() => ResponseStatusException.NotFound("Visit not found"));

            return Mapper.Map<Visit, VisitViewModel>(visit);
        }


        //     @Operation(summary = "Create a new visit", tags = { "visit" })
        //     @ResponseStatus(HttpStatus.CREATED)
        //     public ResponseEntity<?> createVisit(@Valid @RequestBody CreateVisitRequest dto) {
        //         Visit visit = new Visit();
        //         List<Guest> guestsToSave = new ArrayList<Guest>();

        //         for (Guest guest : dto.getGuests()) {
        //             Optional<Guest> guestInDatabase = guestRepository
        //                     .findByPersonDetailsEmail(guest.getPersonDetails().getEmail());

        //             if (guestInDatabase.isPresent()) {
        //                 guestsToSave.add(guestInDatabase.get());
        //             } else {
        //                 guestsToSave.add(guestRepository.save(guest));
        //             }
        //         }
        //         visit.setGuests(guestsToSave);
        //         visit.setPlace(placeRepository.findById(dto.getPlace().getId()).get());

        //         visitRepository.save(visit);

        //         return new ResponseEntity<Visit>(visit, HttpStatus.CREATED);
        //     }

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
