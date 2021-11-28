using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dotnet.Models.Owned;
using Newtonsoft.Json;

#nullable disable

namespace dotnet.Models
{
    public partial class Guest : BaseModel<int?>
    {
        [Required]
        public PersonDetails PersonDetails { get; set; }

        [JsonIgnore]
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
