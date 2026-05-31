using System.ComponentModel;
using FluentValidation;
using JacksonVeroneze.NET.GRPCServer.Api.Mcp.Extensions;
using JacksonVeroneze.NET.GRPCServer.Api.Mcp.Models;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Activate;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetById;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Inactivate;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;

namespace JacksonVeroneze.NET.GRPCServer.Api.Mcp.Tools;

[McpServerToolType]
public sealed class ProfileTools(IMapper mapper)
{
    #region constants

    private const string CreateProfileToolName = "create_profile";
    private const string CreateProfileToolTitle = "create profile";
    private const string CreateProfileToolDesc = "Creates a new person profile with status PendingActivation.";

    private const string ActivateProfileToolName = "activate_profile";
    private const string ActivateProfileToolTitle = "activate profile";
    private const string ActivateProfileToolDesc = "Activates a person profile.";

    private const string InactivateProfileToolName = "inactivate_profile";
    private const string InactivateProfileToolTitle = "inactivate profile";
    private const string InactivateProfileToolDesc = "Inactivates a person profile.";

    private const string GetByIdProfileToolName = "get_profile";
    private const string GetByIdProfileToolTitle = "get profile";
    private const string GetByIdProfileToolDesc = "Retrieves a person profile by its identifier.";

    #endregion

    [McpServerTool(
        Name = CreateProfileToolName,
        Title = CreateProfileToolTitle
    )]
    [Description(CreateProfileToolDesc)]
    public async Task<CallToolResult> CreateAsync(
        [FromServices] ICreateProfileUseCase createProfileUseCase,
        [FromServices] IValidator<CreateProfileToolInput> validator,
        CreateProfileToolInput input,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator
            .ValidateAsync(input, cancellationToken);

        if (!validationResult.IsValid)
        {
            var resultError = validationResult
                .ToCallToolResultError();

            return resultError;
        }

        var request = mapper.Map<CreateProfileToolInput,
            CreateProfileRequest>(input);

        var result = await createProfileUseCase
            .ExecuteAsync(request, cancellationToken);

        return result.IsSuccess
            ? result.ToCallToolResultSuccess()
            : result.ToCallToolResultError();
    }

    [McpServerTool(
        Name = ActivateProfileToolName,
        Title = InactivateProfileToolTitle
    )]
    [Description(ActivateProfileToolDesc)]
    public async Task<CallToolResult> ActivateAsync(
        [FromServices] IActivateProfileUseCase activateProfileUseCase,
        Guid id,
        CancellationToken cancellationToken)
    {
        var request = new ActivateProfileRequest(id);

        var result = await activateProfileUseCase
            .ExecuteAsync(request, cancellationToken);
        
        return result.IsSuccess
            ? result.ToCallToolResultSuccess()
            : result.ToCallToolResultError();
    }

    [McpServerTool(
        Name = InactivateProfileToolName,
        Title = ActivateProfileToolTitle
    )]
    [Description(InactivateProfileToolDesc)]
    public async Task<CallToolResult> InactivateAsync(
        [FromServices] IInactivateProfileUseCase inactivateProfileUseCase,
        Guid id,
        CancellationToken cancellationToken)
    {
        var request = new InactivateProfileRequest(id);
        
        var result = await inactivateProfileUseCase
            .ExecuteAsync(request, cancellationToken);

        return result.IsSuccess
            ? result.ToCallToolResultSuccess()
            : result.ToCallToolResultError();
    }

    [McpServerTool(
        Name = GetByIdProfileToolName,
        Title = GetByIdProfileToolTitle
    )]
    [Description(GetByIdProfileToolDesc)]
    public async Task<CallToolResult> GetByIdAsync(
        [FromServices] IGetByIdProfileUseCase getByIdProfileUseCase,
        Guid id,
        CancellationToken cancellationToken)
    {
        var request = new GetByIdProfileRequest(id);
        
        var result = await getByIdProfileUseCase
            .ExecuteAsync(request, cancellationToken);

        return result.IsSuccess
            ? result.ToCallToolResultSuccess()
            : result.ToCallToolResultError();
    }
}
