using MarcAI.Domain.Enums.User;

namespace MarcAI.Application.Dtos.Auth;

public record UserInfoDto
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public UserType Type { get; init; }
}
