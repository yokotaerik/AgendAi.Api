namespace MarcAI.Application.Dtos.Messages
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public required Guid SenderId { get; set; }
        public required string SenderName { get; set; }
        public required Guid ReceiverId { get; set; }
        public required string ReceiverName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
