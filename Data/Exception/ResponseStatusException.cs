

using System;
using System.Net;
using Microsoft.AspNetCore.WebUtilities;

class ResponseStatusException : Exception
{
    private HttpStatusCode statusCode { get; set; }

    public string statusMessage { get; set; }


    public int status => (int)statusCode;

    public ResponseStatusException(HttpStatusCode statusCode = HttpStatusCode.NoContent, string message = "") : base(message)
    {
        this.statusMessage = ReasonPhrases.GetReasonPhrase((int)statusCode);
        this.statusCode = statusCode;
    }

    public static ResponseStatusException NotFound(string message = "")
    {
        return new ResponseStatusException(HttpStatusCode.NotFound, message);
    }
}