using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dotnet.Models.Owned;
using Newtonsoft.Json;

#nullable disable

namespace dotnet.Models
{
    public partial class ChangeProfileDto
    {
        [Required]
        public PersonDetails personDetails { get; set; }
    }
}
