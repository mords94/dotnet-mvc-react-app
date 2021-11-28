using System.Collections.Generic;
using Newtonsoft.Json;

public class ErrorViewModel
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("status")]
    public int Status { get; set; }
    [JsonProperty("trace")]
    public string[] Trace { get; set; } = null;

    [JsonProperty("fieldMessage")]
    public IDictionary<string, string> FieldErrors { get; set; } = null;
}