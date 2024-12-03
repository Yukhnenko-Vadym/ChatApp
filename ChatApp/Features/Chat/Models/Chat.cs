using ChatApp.Features.UserAuth.Models;

namespace ChatApp.Features.Chat.Models;

public class Chat
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public User Creator { get; set; }
    public ChatUser ChatUser{ get; set; }
    public DateTime CreatedAt { get; set; }

    public Chat(Guid creatorId, DateTime createdAt)
    {
        CreatorId = creatorId;
        CreatedAt = createdAt;
    }
}