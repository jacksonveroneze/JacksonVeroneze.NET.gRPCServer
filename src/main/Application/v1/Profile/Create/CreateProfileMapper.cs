using Mapster;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;

public class CreateProfileMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<Domain.Entities.Profile, CreateProfileResult>()
            .Map(dest => dest.Data, src => src);
    }
}
