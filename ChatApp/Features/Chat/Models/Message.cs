using System.ComponentModel.DataAnnotations;
using ChatApp.Features.UserAuth.Models;

namespace ChatApp.Features.Chat.Models;

public class Message
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public User Sender { get; set; }
    public Guid ChatId { get; set; }
    public string Content { get; set; }

    public Message(Guid senderId, string content, Guid chatId)
    {
        Content = content;
        SenderId = senderId;
        ChatId = chatId;
    }
}