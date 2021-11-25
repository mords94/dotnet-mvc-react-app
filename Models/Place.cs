using System;
using System.Collections.Generic;
using dotnet.Models.Owned;



namespace dotnet.Models
{
    public partial class Place
    {
        public Place()
        {
            Visits = new HashSet<Visit>();
        }

        public string Id { get; set; }
        public Address Address { get; set; }
        public string Name { get; set; }

        public virtual User Owner { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
