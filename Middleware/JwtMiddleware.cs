using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using dotnet.Helpers;
using dotnet.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    private readonly IMemoryCache _memoryCache;

    private readonly ILogger<JwtMiddleware> _logger;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, ILogger<JwtMiddleware> logger, IMemoryCache memoryCache)
    {
        _next = next;
        _appSettings = appSettings.Value;
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public async Task Invoke(HttpContext context, UserRepository userRepository)
    {
        var endpoint = context.GetEndpoint();
        var allowAnonymous = context.GetEndpoint().Metadata.OfType<AllowAnonymousAttribute>().Any();

        if (!allowAnonymous)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            _logger.LogDebug("Token: " + token);
            if (token != null)
                await attachUserToContext(context, userRepository, token);

        }
        await _next(context);
    }

    private async Task attachUserToContext(HttpContext context, UserRepository userRepository, string token)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8080/api/user/profile"))
                {
                    if (!_memoryCache.TryGetValue(token, out String email))
                    {
                        var cacheExpiryOptions = new MemoryCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                            Priority = CacheItemPriority.High,
                            SlidingExpiration = TimeSpan.FromSeconds(20)
                        };


                        requestMessage.Headers.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);




                        HttpResponseMessage clientResponse = await client.SendAsync(requestMessage);

                        if (!clientResponse.IsSuccessStatusCode)
                        {
                            throw ResponseStatusException.Unauthorized("You need to be logged in.");
                        }

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var nonValidatedToken = tokenHandler.ReadJwtToken(token);
                        email = nonValidatedToken.Subject;

                        _memoryCache.Set(token, email, cacheExpiryOptions);
                    }

                    context.Items["User"] = (await userRepository.findByEmail(email)).Get();
                }
            }
            catch
            {
                _logger.LogDebug("Uncaught error in the JWT middleware");
            }
        }
    }
}