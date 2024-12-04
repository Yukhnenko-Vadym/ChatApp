namespace ChatApp.Features.UserAuth.Models.Requests;

public class RegisterBody
{
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}