using Mapster;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;

public class CreateProfileMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<Domain.Entities.Profile, CreateProfileResponse>()
            .Map(dest => dest.Data, src => src);

        config.NewConfig<CreateProfileRequest, Domain.Entities.Profile>()
            .ConstructUsing(dest => new Domain.Entities.Profile(
                dest.FullName, dest.BirthDate, dest.Gender, dest.Cpf));
    }
}
