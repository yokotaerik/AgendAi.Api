namespace MarcAI.Application.Dtos.Messages
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public required Guid SenderId { get; set; }
        public required Guid ReceiverId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
