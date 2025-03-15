using MarcAI.Application.Dtos.Messages;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;

namespace MarcAI.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(
            IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<IEnumerable<MessageDto>> GetMessagesBetweenUsersAsync(string companyId, string customerId)
        {
            var messages = await _messageRepository.GetMessagesBetweenUsersAsync(companyId, customerId);
            return messages.Select(MapToDto);
        }

        public async Task<MessageDto> SendMessageAsync(string content, string senderId, string receiverId)
        {
            var senderGuid = new Guid(senderId);

            var receiverGuid = new Guid(receiverId);

            var message = Message.Create(content, senderGuid, receiverGuid);

            await _messageRepository.AddAsync(message);

            var messageDto = MapToDto(message);

            var sucess = await _messageRepository.Commit(); 

            if(!sucess) throw new Exception("Persistence error");

            return messageDto;
        }

        private MessageDto MapToDto(Message message)
        {
            return new MessageDto
            {
                Id = message.Id,
                Content = message.Content,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                CreatedAt = message.CreatedAt,
            };
        }
    }
}
