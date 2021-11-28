using AutoMapper;
using dotnet.Models;

public class DefaultProfile : Profile
{
    public DefaultProfile()
    {
        CreateMap<Visit, VisitViewModel>();
        CreateMap<Guest, GuestViewModel>();
        CreateMap<GuestDto, Guest>();
        CreateMap<PlaceDto, Place>();
        CreateMap<Place, PlaceViewModel>();
    }
}