using FluentValidation;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services.v1.Profiles.Activate;

public sealed class ActivateProfileRequestValidator
    : AbstractValidator<Contracts.Profiles.v1.ActivateProfileRequest>
{
    public ActivateProfileRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
