using MarcAI.Domain.Models.Common;

namespace MarcAI.Domain.Models.Entities;

public class Message : BaseEntity
{
    public string Content { get; private set; }
    public Guid SenderId { get; private set; }
    public Guid ReceiverId { get; private set; }

    private Message(string content, Guid senderId, Guid receiverId) : base()
    {
        Content = content;
        SenderId = senderId;
        ReceiverId = receiverId;
    }

    public static Message Create(string content, Guid senderId, Guid receiverId)
    {
        return new Message(content, senderId, receiverId);
    }
}
