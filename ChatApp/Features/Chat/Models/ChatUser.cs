using ChatApp.Features.UserAuth.Models;

namespace ChatApp.Features.Chat.Models;

public class ChatUser
{
    public Guid ChatId { get; set; }
    public IList<Chat> Chats { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}