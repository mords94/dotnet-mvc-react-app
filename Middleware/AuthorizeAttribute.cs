using System;
using System.Net;
using dotnet.Models;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{

    private string type;

    public AuthorizeAttribute(string roleType)
    {
        type = roleType;
    }

    public AuthorizeAttribute()
    {
        type = RoleType.ROLE_GUEST;
    }

    private bool CheckPermission(User user)
    {
        if (type == RoleType.ROLE_GUEST)
        {
            return true;
        }

        if (type == RoleType.ROLE_ADMIN && user.Role.RoleType == type)
        {
            return true;
        }

        if (type == RoleType.ROLE_OWNER && (user.Role.RoleType == RoleType.ROLE_OWNER || user.Role.RoleType == RoleType.ROLE_ADMIN))
        {
            return true;
        }

        return false;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (User)context.HttpContext.Items["User"];

        // TODO: it can be removed, since the JWT middleware handles this case
        if (user == null)
        {
            throw ResponseStatusException.Unauthorized("You have to be logged in.");
        }

        if (!CheckPermission(user))
        {
            throw ResponseStatusException.Forbidden("Permission denied");
        }

    }
}