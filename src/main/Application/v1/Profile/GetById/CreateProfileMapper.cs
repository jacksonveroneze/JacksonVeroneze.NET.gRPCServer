using Mapster;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetById;

public class GetByIdProfileMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<Domain.Entities.Profile, GetByIdProfileResponse>()
            .Map(dest => dest.Data, src => src);
    }
}
