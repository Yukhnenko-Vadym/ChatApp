namespace ChatApp.Features.UserManagement.Models.Requests;

public class UpdateUserBody
{
    public required string Username { get; init; }
    public required string Email { get; init; }
}