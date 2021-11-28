using System.Collections.Generic;
using dotnet.Models;

namespace dotnet.DTO.Request
{
    public class VisitDto
    {
        public Place Place { get; set; }
        public ICollection<GuestDto> Guests { get; set; }
    }
}