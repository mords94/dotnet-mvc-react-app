using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnet.Helpers;
using dotnet.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;


    private readonly ILogger<JwtMiddleware> _logger;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, ILogger<JwtMiddleware> logger)
    {
        _next = next;
        _appSettings = appSettings.Value;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, UserRepository userRepository)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();


        _logger.LogDebug("Token: " + token);
        if (token != null)
            await attachUserToContext(context, userRepository, token);

        await _next(context);
    }

    private async Task attachUserToContext(HttpContext context, UserRepository userRepository, string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();


        // var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        // tokenHandler.ValidateToken(token, new TokenValidationParameters
        // {
        //     ValidateIssuerSigningKey = false,
        //     IssuerSigningKey = new SymmetricSecurityKey(key),
        //     ValidateIssuer = false,
        //     ValidateAudience = false,
        //     ClockSkew = TimeSpan.Zero
        // }, out SecurityToken validatedToken);

        // var jwtToken = (JwtSecurityToken)validatedToken;


        //FIXME: validate token

        try
        {

            var nonValidatedToken = tokenHandler.ReadJwtToken(token);
            var email = nonValidatedToken.Subject;

            // attach user to context on successful jwt validation
            context.Items["User"] = (await userRepository.findByEmail(email)).Get();
        }
        catch
        {
            _logger.LogDebug("JWT doesn't contain a valid subject");
        }
    }
}