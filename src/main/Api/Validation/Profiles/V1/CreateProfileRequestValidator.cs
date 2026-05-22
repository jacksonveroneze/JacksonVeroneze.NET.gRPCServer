using FluentValidation;
using JacksonVeroneze.GrpcServer.Contracts.Profiles.V1;

namespace JacksonVeroneze.NET.GRPCServer.Api.Validation.Profiles.V1;

public sealed class CreateProfileRequestValidator
    : AbstractValidator<CreateProfileRequest>
{
    public CreateProfileRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Cpf)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.BirthDate)
            .NotEmpty();

        RuleFor(x => x.Gender)
            .IsInEnum();
    }
}
