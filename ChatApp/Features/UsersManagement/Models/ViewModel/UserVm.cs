namespace ChatApp.Features.UsersManagement.Models.ViewModel;

public class UserVm
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
}