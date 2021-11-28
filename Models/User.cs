using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dotnet.Models.Owned;
using Newtonsoft.Json;

#nullable disable

namespace dotnet.Models
{
    public partial class User : BaseModel<int>
    {
        public User()
        {
            Places = new HashSet<Place>();
        }


        [JsonIgnore]
        [Required]
        public string Password { get; set; }

        public PersonDetails personDetails { get; set; }

        [JsonIgnore]
        public int? RoleId { get; set; }

        [JsonProperty("role")]
        public virtual Role Role { get; set; }

        [JsonIgnore]
        public virtual ICollection<Place> Places { get; set; }
    }
}
