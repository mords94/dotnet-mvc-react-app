using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Models.Owned
{
    [Owned]
    public class PersonDetails
    {

        [Column("first_name")]
        [Required]
        public string FirstName { get; set; }

        [Column("last_name")]
        [Required]
        public string LastName { get; set; }

        [Column("email")]
        [Required]
        public string Email { get; set; }
        [Column("phone")]
        [Required]
        public string Phone { get; set; }
    }
}