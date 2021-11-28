using System;
using System.Net;
using dotnet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute
{
    public void OnAuthorization(HttpContext context)
    {
        var user = (User)context.Items["User"];
        if (user == null)
        {
            throw new ResponseStatusException(HttpStatusCode.Unauthorized, "You have to be logged in.");
        }
    }
}