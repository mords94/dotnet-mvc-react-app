


using System;
using Newtonsoft.Json;

public class ErrorViewModel
{
    public ErrorViewModel(int code, string message, string trace)
    {
        Status = code;
        Message = message;
        Trace = trace;
    }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("status")]
    public int Status { get; set; }
    [JsonProperty("trace")]
    public string Trace { get; set; }
}