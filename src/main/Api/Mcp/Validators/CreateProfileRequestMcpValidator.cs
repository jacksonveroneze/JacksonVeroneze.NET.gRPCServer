using FluentValidation;
using JacksonVeroneze.NET.GRPCServer.Api.Mcp.Models;
using JacksonVeroneze.NET.GRPCServer.Domain.Enums;

namespace JacksonVeroneze.NET.GRPCServer.Api.Mcp.Validators;

public sealed class CreateProfileToolInputValidator
    : AbstractValidator<CreateProfileToolInput>
{
    public CreateProfileToolInputValidator()
    {
        RuleFor(exp => exp.FullName)
            .NotNull()
            .NotEmpty();

        RuleFor(exp => exp.Cpf)
            .NotNull()
            .NotEmpty();
        
        RuleFor(exp => exp.BirthDate)
            .NotEmpty();

        RuleFor(exp => exp.Gender)
            .IsInEnum()
            .NotEqual(Gender.None);
    }
}
