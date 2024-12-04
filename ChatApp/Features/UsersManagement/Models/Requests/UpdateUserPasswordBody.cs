namespace ChatApp.Features.UserManagement.Models.Requests;

public class UpdateUserPasswordBody
{
    public required string Password { get; init; }
}