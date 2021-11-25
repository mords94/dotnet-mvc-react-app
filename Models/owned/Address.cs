using Microsoft.EntityFrameworkCore;


namespace dotnet.Models.Owned
{
[Owned]
public class Address {
    public string city { get; set; }
    public string addressLine { get; set; }
}
}