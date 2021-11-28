using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace dotnet.Models.Owned
{
    [Owned]
    public class Address
    {
        [Column("city")]
        public string city { get; set; }
        [Column("address_line")]

        public string addressLine { get; set; }
    }
}