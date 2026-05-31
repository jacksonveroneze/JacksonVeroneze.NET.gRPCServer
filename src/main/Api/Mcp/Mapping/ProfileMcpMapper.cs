using JacksonVeroneze.NET.GRPCServer.Api.Mcp.Models;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;
using Mapster;

namespace JacksonVeroneze.NET.GRPCServer.Api.Mcp.Mapping;

public class ProfileMcpMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<CreateProfileToolInput, CreateProfileRequest>()
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.BirthDate, src => src.BirthDate)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Cpf, src => src.Cpf);
    }
}
