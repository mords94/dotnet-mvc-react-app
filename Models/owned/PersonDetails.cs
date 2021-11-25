using Microsoft.EntityFrameworkCore;

namespace dotnet.Models.Owned
{
    [Owned]
    public class PersonDetails
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}