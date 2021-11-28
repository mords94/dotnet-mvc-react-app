using System.ComponentModel.DataAnnotations;

public class AuthenticateRequestDto
{
    [Required(AllowEmptyStrings = false)]
    public string email;

    [Required(AllowEmptyStrings = false)]
    public string password;
}