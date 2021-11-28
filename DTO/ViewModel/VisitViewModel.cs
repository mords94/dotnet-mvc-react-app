using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dotnet.Models;
using Newtonsoft.Json;

public class VisitViewModel
{
    public VisitViewModel()
    {

    }

    public int Id { get; set; }
    public DateTime? FinishDate { get; set; }
    public DateTime? VisitDate { get; set; }


    public PlaceViewModel Place { get; set; }

    public ICollection<Guest> Guests { get; set; }
}