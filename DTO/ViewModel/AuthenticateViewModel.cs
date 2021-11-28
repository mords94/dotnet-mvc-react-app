

using dotnet.Models;

public class AuthenicateViewModel
{

    public User user;

    public string token;

    public AuthenicateViewModel(User user, string token)
    {
        this.user = user;
        this.token = token;
    }
}