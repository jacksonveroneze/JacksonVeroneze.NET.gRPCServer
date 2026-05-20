using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Models;
using JacksonVeroneze.NET.GRPCServer.Contracts.Profiles.v1;
using Mapster;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services.v1.Profiles.Common.Mappers;

public class ProfileMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<ProfileResult, Profile>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Birthday, src => src.Birthday)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Cpf, src => src.Cpf)
            .Map(dest => dest.Status, src => src.Status);
    }
}
