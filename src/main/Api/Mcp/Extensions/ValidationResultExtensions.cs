using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;
using JacksonVeroneze.NET.GRPCServer.Api.Mcp.Util;
using ModelContextProtocol.Protocol;

namespace JacksonVeroneze.NET.GRPCServer.Api.Mcp.Extensions;

[ExcludeFromCodeCoverage]
public static class ValidationResultExtensions
{
    public static CallToolResult ToCallToolResultError(
        this ValidationResult validationResult,
        string? message = null)
    {
        ArgumentNullException.ThrowIfNull(validationResult);
        
        var erros = validationResult.ToDictionary();

        return McpToolResult.Error(
            code: "VALIDATION_ERROR",
            message: message ?? "Invalid input.",
            details: erros);
    }
}
