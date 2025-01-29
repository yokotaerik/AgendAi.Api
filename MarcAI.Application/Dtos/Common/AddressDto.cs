namespace MarcAI.Application.Dtos.Common;

public record AddressDto(
  string Street,
  string Number,
  string? Complement,
  string? Neighborhood,
  string City,
  string State,
  string ZipCode
);
