using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace dotnet.Models
{
    public partial class Visit : BaseModel<int?>
    {
        [DataType(DataType.Date)]
        public DateTime? FinishDate { get; set; }

        [DataType(DataType.Date)]

        public DateTime? VisitDate { get; set; }

        public Guid PlaceId { get; set; }


        public Place Place { get; set; }

        public ICollection<Guest> Guests { get; set; }
    }
}
