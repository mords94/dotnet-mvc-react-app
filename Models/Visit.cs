using System;
using System.Collections.Generic;



namespace dotnet.Models
{
    public partial class Visit
    {

        public Visit()
        {
            Guests = new HashSet<Guest>();

        }
        public int Id { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime? VisitDate { get; set; }

        public virtual Place Place { get; set; }

        public virtual ICollection<Guest> Guests { get; set; }
    }
}
