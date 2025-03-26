using MarcAI.Application.Dtos.Messages;
using MarcAI.Application.Interfaces;
using MarcAI.Domain.Interfaces.Repositories;
using MarcAI.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarcAI.Application.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public MessageService(
        IMessageRepository messageRepository, IUserRepository userRepository, IUserService userService)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<IEnumerable<ChatDto>> GetChats()
    {
        var query = _messageRepository.GetQueryable();

        var userContext = await _userService.GetCurrentUserAsync();

        var chats = await query
            .Where(m => m.ReceiverId == userContext.Id)
            .GroupBy(m => m.SenderId)
            .Select(g => new ChatDto
            {
                ReceiverId = g.Key,
                ReceiverName = g.OrderByDescending(m => m.CreatedAt).FirstOrDefault()!.SenderName, 
                LastMessage = g.OrderByDescending(m => m.CreatedAt)!.FirstOrDefault()!.Content 
            })
            .ToListAsync();

        return chats;
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

        var receiverName = await _userRepository.GetSenderName(receiverGuid);

        var senderName = await _userRepository.GetSenderName(senderGuid);

        var message = Message.Create(content, senderGuid, senderName ?? "", receiverGuid, receiverName?? "");

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
            ReceiverName = message.ReceiverName,
            ReceiverId = message.ReceiverId,
            SenderName = message.SenderName,
            CreatedAt = message.CreatedAt,
        };
    }


}
