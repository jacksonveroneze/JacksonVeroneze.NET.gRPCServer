using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Models;
using Mapster;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Mappers;

public class ProfileMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<Domain.Entities.Profile, ProfileResult>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Birthday, src => src.Birthday)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Cpf, src => src.Cpf)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.ActivedOnUtc, src => src.ActivedOnUtc)
            .Map(dest => dest.InactivedOnUtc, src => src.InactivedOnUtc);
    }
}
