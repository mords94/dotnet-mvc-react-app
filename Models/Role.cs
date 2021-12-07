using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

#nullable disable

namespace dotnet.Models
{
    public partial class Role : BaseModel<int>
    {
        [JsonProperty("roleType")]
        public string RoleType { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }

    }
}
