using Newtonsoft.Json;

#nullable disable

namespace dotnet.Models
{
    public partial class VisitGuest
    {
        public int VisitId { get; set; }
        public int GuestsId { get; set; }

        public virtual Guest Guests { get; set; }

        [JsonIgnore]
        public virtual Visit Visit { get; set; }
    }
}
