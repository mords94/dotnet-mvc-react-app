using dotnet.Models;
using dotnet.Models.Owned;
using Newtonsoft.Json;

public class GuestViewModel : BaseModel<int?>
{


    public GuestViewModel()
    {

    }


    [JsonProperty("personDetails")]
    public PersonDetails PersonDetails { get; set; }

}