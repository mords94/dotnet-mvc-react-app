using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public ErrorViewModel Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context.Error;
        var code = 500;

        if (exception is ResponseStatusException)
        {
            ResponseStatusException responseStatusException = (ResponseStatusException)exception;
            code = responseStatusException.status;
        }

        Response.StatusCode = code;

        return new ErrorViewModel(code, exception.Message, exception.ToString());
    }
}