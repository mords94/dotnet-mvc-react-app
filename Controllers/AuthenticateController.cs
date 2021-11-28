using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using dotnet.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace dotnet.Controllers
{
    [ApiController]
    [Route("/api/authenticate")]
    public class AuthenticateController : ControllerBase
    {
        private UserRepository UserRepository;
        public AuthenticateController(UserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        [HttpPost]
        public async Task<AuthenicateViewModel> Authenticate(AuthenticateRequestDto model)
        {

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage clientResponse = await client.PostAsJsonAsync<AuthenticateRequestDto>("http://localhost:8080/api/authenticate", model);
                if (!clientResponse.IsSuccessStatusCode)
                {
                    throw new ResponseStatusException(HttpStatusCode.BadGateway, await clientResponse.Content.ReadAsStringAsync());
                }


                return await clientResponse.Content.ReadAsAsync<AuthenicateViewModel>();

            }
        }
    }
}