using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;
using JacksonVeroneze.NET.GRPCServer.Contracts.Profiles.v1;
using Mapster;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services.v1.Profiles.Create;

public class CreateProfileMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<CreateProfileRequest, CreateProfileCommand>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Birthday, src => src.Birthday)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Cpf, src => src.Cpf);

        config.NewConfig<CreateProfileResult, CreateProfileResponse>()
            .Map(dest => dest.Profile, src => src.Data);
    }
}
