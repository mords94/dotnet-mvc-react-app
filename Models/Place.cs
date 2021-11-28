using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dotnet.Models.Owned;
using Newtonsoft.Json;

#nullable disable

namespace dotnet.Models
{
    public partial class Place : BaseModel<Guid>
    {
        public Place()
        {
            Visits = new HashSet<Visit>();
        }

        public Address Address { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public int? OwnerId { get; set; }

        public User Owner { get; set; }

        [JsonIgnore]
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
