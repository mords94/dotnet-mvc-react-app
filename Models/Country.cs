﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace dotnet.Models
{
    public partial class Country : BaseModel<int>
    {
        public string Name { get; set; }
    }
}
