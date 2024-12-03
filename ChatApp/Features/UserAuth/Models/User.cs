using System.ComponentModel.DataAnnotations;
using ChatApp.Features.Chat.Models;

namespace ChatApp.Features.UserAuth.Models;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string HashedPassword { get; private set; }
    
    
    public User(
        string username,
        string email,
        string hashedPassword)
    {
        Username = username;
        Email = email;
        HashedPassword = hashedPassword;
    }

    public void Update(string username, string lastName, string email)
    {
        Username = username;
        Email = email;
    }

    public void UpdatePassword(string hashedPassword)
    {
        HashedPassword = hashedPassword;
    }
}