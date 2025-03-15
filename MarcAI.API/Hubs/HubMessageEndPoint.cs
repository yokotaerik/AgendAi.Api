using MarcAI.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace MarcAI.API.Hubs;

public class ChatHub : Hub
{
    private readonly IMessageService _messageService;
    private readonly ILogger<ChatHub> _logger;
    private static readonly Dictionary<string, string> _userConnections = new();

    public ChatHub(IMessageService messageService, ILogger<ChatHub> logger)
    {
        _messageService = messageService;
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
        _logger.LogInformation("Usuário conectado - UserId: {UserId}, ConnectionId: {ConnectionId}", userId, Context.ConnectionId);
        
        if (!string.IsNullOrEmpty(userId))
        {
            _userConnections[userId] = Context.ConnectionId;
        }
        
        await base.OnConnectedAsync(); 
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User?.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
        _logger.LogInformation("Usuário desconectado - UserId: {UserId}, ConnectionId: {ConnectionId}", userId, Context.ConnectionId);
        
        if (!string.IsNullOrEmpty(userId))
        {
            _userConnections.Remove(userId);
        }
        
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string content, string receiverId)
    {
        var senderId = (Context.User?.Claims.FirstOrDefault(c => c.Type == "Id")?.Value) ?? throw new InvalidOperationException("Sender ID not found.");
        _logger.LogInformation("Tentando enviar mensagem - De: {SenderId}, Para: {ReceiverId}, Conteúdo: {Content}", senderId, receiverId, content);

        var msgDto = await _messageService.SendMessageAsync(content, senderId, receiverId);

        // Verifica se os usuários estão conectados
        var senderConnected = _userConnections.TryGetValue(senderId, out var senderConnectionId);
        var receiverConnected = _userConnections.TryGetValue(receiverId, out var receiverConnectionId);

        _logger.LogInformation("Status das conexões - Sender: {SenderConnected} ({SenderConnectionId}), Receiver: {ReceiverConnected} ({ReceiverConnectionId})",
            senderConnected, senderConnectionId, receiverConnected, receiverConnectionId);

        // Envia para o receptor usando ConnectionId
        if (receiverConnected)
        {
            await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", msgDto, CancellationToken.None);
            _logger.LogInformation("Mensagem enviada para receptor via ConnectionId");
        }
        else
        {
            // Tenta enviar usando User como fallback
            await Clients.User(receiverId).SendAsync("ReceiveMessage", msgDto, CancellationToken.None);
            _logger.LogInformation("Tentativa de envio para receptor via User");
        }

        // Envia para o remetente usando ConnectionId
        if (senderConnected)
        {
            await Clients.Client(senderConnectionId).SendAsync("ReceiveMessage", msgDto, CancellationToken.None);
            _logger.LogInformation("Mensagem enviada para remetente via ConnectionId");
        }
        else
        {
            // Tenta enviar usando User como fallback
            await Clients.User(senderId).SendAsync("ReceiveMessage", msgDto, CancellationToken.None);
            _logger.LogInformation("Tentativa de envio para remetente via User");
        }
    }
}
