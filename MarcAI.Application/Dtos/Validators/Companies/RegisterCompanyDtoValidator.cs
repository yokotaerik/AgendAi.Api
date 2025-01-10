using FluentValidation;
using MarcAI.Application.Dtos.Companies;

namespace MarcAI.Application.Dtos.Validators.Companies;

internal class RegisterCompanyDtoValidator : AbstractValidator<RegisterCompanyDto>
{
    public RegisterCompanyDtoValidator()
    {
        RuleFor(x => x.CorporateName)
            .NotEmpty()
            .WithMessage("CorporateName is required");

        RuleFor(x => x.FantasyName)
            .NotEmpty()
            .WithMessage("FantasyName is required");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address is required");

        RuleFor(x => x.Cnpj)
            .NotEmpty()
            .WithMessage("Cnpj is required");
    }
}
