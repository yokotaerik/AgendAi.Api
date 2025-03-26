using MarcAI.Application.Dtos.Messages;

namespace MarcAI.Application.Interfaces;

public interface IMessageService
{
    Task<IEnumerable<ChatDto>> GetChats();
    Task<IEnumerable<MessageDto>> GetMessagesBetweenUsersAsync(string companyId, string customerId);
    Task<MessageDto> SendMessageAsync(string content, string senderId, string receiverId);
}
