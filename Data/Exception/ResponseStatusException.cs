

using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;

class ResponseStatusException : Exception
{
    private HttpStatusCode statusCode { get; set; }

    public string statusMessage { get; set; }
    public IDictionary<string, string> fieldErrors { get; set; } = null;

    public int status => (int)statusCode;

    public ResponseStatusException(HttpStatusCode statusCode = HttpStatusCode.NoContent, string message = "") : base(message)
    {
        this.statusMessage = ReasonPhrases.GetReasonPhrase((int)statusCode);
        this.statusCode = statusCode;
    }

    public ResponseStatusException(IDictionary<string, string> fieldErrors) : base("Validation error")
    {
        this.statusMessage = ReasonPhrases.GetReasonPhrase((int)statusCode);
        this.statusCode = HttpStatusCode.UnprocessableEntity;
        this.fieldErrors = fieldErrors;
    }

    public static ResponseStatusException NotFound(string message = "")
    {
        return new ResponseStatusException(HttpStatusCode.NotFound, message);
    }

    public static ResponseStatusException Forbidden(string message = "")
    {
        return new ResponseStatusException(HttpStatusCode.Forbidden, message);
    }

    public static ResponseStatusException Unauthorized(string message = "")
    {
        return new ResponseStatusException(HttpStatusCode.Unauthorized, message);
    }

    public static ResponseStatusException UnprocessableEntity(IDictionary<string, string> fieldErrors)
    {
        return new ResponseStatusException(fieldErrors);
    }
}