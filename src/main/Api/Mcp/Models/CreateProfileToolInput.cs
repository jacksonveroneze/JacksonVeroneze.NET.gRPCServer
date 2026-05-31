using System.ComponentModel;
using JacksonVeroneze.NET.GRPCServer.Domain.Enums;

namespace JacksonVeroneze.NET.GRPCServer.Api.Mcp.Models;

public sealed record CreateProfileToolInput
{
    [Description("Full name of the person. Example: Joana da Silva.")]
    public string? FullName { get; init; }

    [Description("Birth date in yyyy-MM-dd format. Example: 1995-05-25.")]
    public string? BirthDate { get; init; }

    [Description("Gender. Allowed values: Male, Female, Other, NotInformed.")]
    public Gender Gender { get; init; }

    [Description("Brazilian CPF with 11 digits, only numbers. Example: 11122233344.")]
    public string? Cpf { get; init; }
}
