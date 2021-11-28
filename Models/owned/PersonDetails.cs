using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace dotnet.Models.Owned
{
    [Owned]
    public class PersonDetails
    {

        [Column("first_name")]
        [JsonProperty("firstName")]
        [Required(AllowEmptyStrings = false), Display(Name = "First name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        [JsonProperty("lastName")]
        [Required(AllowEmptyStrings = false), Display(Name = "Last name")]
        public string LastName { get; set; }

        [Column("email")]
        [JsonProperty("email")]
        [Required(AllowEmptyStrings = false), Display(Name = "E-mail")]
        public string Email { get; set; }

        [Column("phone")]
        [Required(AllowEmptyStrings = false), Display(Name = "Phone")]
        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}