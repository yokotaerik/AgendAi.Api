namespace MarcAI.Application.Dtos.Common.User;

public class BasicInfoDto
{
    public BasicInfoDto(Guid id,string? imageUrl, string completeName)
    {
        Id = id;
        ImageUrl = imageUrl;
        CompleteName = completeName;
    }
    public Guid Id { get; set; }
    public string? ImageUrl { get; set; }
    public string CompleteName { get; set; }
}
