using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

public class BaseModel<ID>
{
    [Key]
    [JsonProperty("id")]

    public ID Id { get; set; }
}