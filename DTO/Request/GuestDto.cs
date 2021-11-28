using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dotnet.Models.Owned;
using Newtonsoft.Json;

#nullable disable

namespace dotnet.Models
{
    public class GuestDto
    {
        [Required]
        public PersonDetails PersonDetails { get; set; }
    }
}
