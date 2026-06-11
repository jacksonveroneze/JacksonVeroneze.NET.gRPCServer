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
            .ConstructUsing(src => new Domain.Entities.Profile(
                src.FullName,
                src.BirthDate,
                src.Gender,
                src.Cpf,
                src.Email,
                src.PhoneNumber,
                src.MotherName,
                src.FatherName,
                src.Nationality,
                src.BirthCity,
                src.BirthState,
                src.Street,
                src.AddressNumber,
                src.Complement,
                src.Neighborhood,
                src.City,
                src.State,
                src.ZipCode,
                src.Country))
            .Ignore(dest => dest.Id!)
            .Ignore(dest => dest.Status!)
            .Ignore(dest => dest.ActivatedOnUtc!)
            .Ignore(dest => dest.InactivatedOnUtc!);
    }
}
