using MarcAI.Domain.Models.Common;

namespace MarcAI.Domain.Models.Entities;

public class Message : BaseEntity
{
    public string Content { get; private set; }
    public Guid SenderId { get; private set; }
    public string SenderName { get; private set; }
    public Guid ReceiverId { get; private set; }
    public string ReceiverName { get; private set; }

    private Message(string content, Guid senderId, string senderName, Guid receiverId, string receiverName) : base()
    {
        Content = content;
        SenderId = senderId;
        SenderName = senderName;
        ReceiverId = receiverId;
        ReceiverName = receiverName;
    }

    public static Message Create(string content, Guid senderId, string senderName, Guid receiverId, string receiverName)
    {
        return new Message(content, senderId, senderName, receiverId, receiverName);
    }
}
