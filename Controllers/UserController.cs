using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet.Models;
using dotnet.Repository;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {

        private UserRepository UserRepository;

        public UserController(UserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        [HttpGet]
        public Task<IEnumerable<User>> GetAll()
        {
            return UserRepository.findAll();
        }


        [HttpGet]
        [Authorize]
        [Route("profile")]
        public async Task<Optional<User>> GetProfile()
        {

            User user = (User)HttpContext.Items["User"];
            return await UserRepository.findByEmail(user.personDetails.Email);
        }

        [HttpPatch]
        [Authorize]
        [Route("profile")]
        public async Task<User> UpdateProfile([FromBody] ChangeProfileDto model)
        {

            User user = (User)HttpContext.Items["User"];
            user.personDetails = model.personDetails;

            await UserRepository.save(user);

            return user;
        }


    }
}
