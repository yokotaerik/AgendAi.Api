using Microsoft.AspNetCore.SignalR;

namespace MarcAI.Infrastructure.SignalR
{
    public class SignalRHub : Hub
    {
        // A API será responsável por conectar este hub aos serviços da aplicação

        // Armazenamos apenas o ID do usuário que será usado para enviar mensagens
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            // Normalmente obteríamos o userId do token de autenticação
            //var userId = Context.UserIdentifier;
        }

        // Os métodos que serão chamados pelo cliente serão definidos aqui
        // A implementação real estará na API
    }
}
