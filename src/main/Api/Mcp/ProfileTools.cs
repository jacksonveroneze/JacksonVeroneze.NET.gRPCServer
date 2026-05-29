using System.ComponentModel;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Activate;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetById;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Inactivate;
using JacksonVeroneze.NET.Result;
using ModelContextProtocol.Server;

namespace JacksonVeroneze.NET.GRPCServer.Api.Mcp;

[McpServerToolType]
public sealed class ProfileTools(
    ICreateProfileUseCase createProfileUseCase,
    IActivateProfileUseCase activateProfileUseCase,
    IInactivateProfileUseCase inactivateProfileUseCase,
    IGetByIdProfileUseCase getByIdProfileUseCase)
{
    [McpServerTool(
        Name = "create_profile",
        //Title = "Cria um novo profile",
        OutputSchemaType = typeof(Result<CreateProfileResponse>)
    )]
    [McpMeta("category", "profile")]
    [Description("Cria um novo profile")]
    public async Task<Result<CreateProfileResponse>> CreateAsync(
        CreateProfileRequest input,
        CancellationToken cancellationToken)
    {
        var output = await createProfileUseCase
            .ExecuteAsync(input, cancellationToken);

        return output;
    }
    
    [McpServerTool(
        Name = "activate_profile",
        Title = "Ativa um profile pelo identificador",
        OutputSchemaType = typeof(Result.Result)
    )]
    [Description("Ativa um profile pelo identificador")]
    public async Task<Result.Result> ActivateAsync(
        ActivateProfileRequest input,
        CancellationToken cancellationToken)
    {
        var output = await activateProfileUseCase
            .ExecuteAsync(input, cancellationToken);

        return output;
    }
    
    [McpServerTool(
        Name = "inactivate_profile",
        Title = "Inativa um profile pelo identificador",
        OutputSchemaType = typeof(Result.Result)
    )]
    [Description("Inativa um profile pelo identificador")]
    public async Task<Result.Result> InactivateAsync(
        InactivateProfileRequest input,
        CancellationToken cancellationToken)
    {
        var output = await inactivateProfileUseCase
            .ExecuteAsync(input, cancellationToken);

        return output;
    }
    
    [McpServerTool(
        Name = "get_profile",
        Title = "Busca um profile pelo identificador",
        OutputSchemaType = typeof(GetByIdProfileResponse)
    )]
    [Description("Busca um profile pelo identificador")]
    public async Task<Result<GetByIdProfileResponse>> GetByIdAsync(
        GetByIdProfileRequest input,
        CancellationToken cancellationToken)
    {
        var output = await getByIdProfileUseCase
            .ExecuteAsync(input, cancellationToken);

        return output;
    }
}
