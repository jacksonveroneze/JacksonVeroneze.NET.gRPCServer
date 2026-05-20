using FluentValidation;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services.v1.Profiles.Inactivate;

public sealed class InactivateProfileRequestValidator
    : AbstractValidator<Contracts.Profiles.v1.InactivateProfileRequest>
{
    public InactivateProfileRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
