using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

[AllowAnonymous]
public class ErrorsController : ControllerBase
{
    private IWebHostEnvironment env;

    public ErrorsController(IWebHostEnvironment hostingEnvironment)
    {
        env = hostingEnvironment;
    }

    [Route("/error")]
    public ErrorViewModel Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context.Error;
        var code = (int)HttpStatusCode.InternalServerError;
        IDictionary<string, string> fieldErrors = new Dictionary<string, string>();
        if (exception is ResponseStatusException)
        {
            ResponseStatusException responseStatusException = (ResponseStatusException)exception;
            code = responseStatusException.status;
            fieldErrors = responseStatusException.fieldErrors;
        }

        Response.StatusCode = code;

        string[] trace = env.IsDevelopment() && code == (int)HttpStatusCode.InternalServerError ? exception.ToString().Split("\n") : null;

        var viewModel = new ErrorViewModel { Status = code, Message = exception.Message, Trace = trace, Type = exception.GetType().ToString() };

        if (code == (int)HttpStatusCode.UnprocessableEntity)
        {
            viewModel.FieldErrors = fieldErrors;
        }

        return viewModel;
    }
}