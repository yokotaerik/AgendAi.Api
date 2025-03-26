namespace MarcAI.Application.Dtos.Messages;

public class ChatDto
{
    /// <summary>
    /// User Id of reciever
    /// </summary>
    public Guid ReceiverId { get; set; }
    public required string ReceiverName { get; set; }
    public required string LastMessage { get; set; }
}
