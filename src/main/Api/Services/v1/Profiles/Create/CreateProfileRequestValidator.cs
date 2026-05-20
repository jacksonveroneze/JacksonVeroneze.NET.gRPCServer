using FluentValidation;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services.v1.Profiles.Create;

public sealed class CreateProfileRequestValidator
    : AbstractValidator<Contracts.Profiles.v1.CreateProfileRequest>
{
    public CreateProfileRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Cpf)
            .NotEmpty();
    }
}
