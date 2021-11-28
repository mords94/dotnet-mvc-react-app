using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dotnet.Models.Owned;

#nullable disable

namespace dotnet.Models
{
    public partial class PlaceViewModel : BaseModel<Guid>
    {
        public Address Address { get; set; }
        public string Name { get; set; }

    }
}
