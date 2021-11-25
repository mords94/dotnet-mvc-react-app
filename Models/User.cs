using System.Collections.Generic;
using dotnet.Models.Owned;


namespace dotnet.Models
{
    public partial class User
    {
        public User()
        {
            Places = new HashSet<Place>();
        }

        public int Id { get; set; }
        public string Password { get; set; }

        public PersonDetails PersonDetails { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Place> Places { get; set; }
    }
}
