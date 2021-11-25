using System;
using System.Collections.Generic;

namespace dotnet.Models
{
    public partial class Country : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
