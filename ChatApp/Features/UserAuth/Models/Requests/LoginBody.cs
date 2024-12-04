namespace ChatApp.Features.UserAuth.Models.Requests;

public class LoginBody
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}