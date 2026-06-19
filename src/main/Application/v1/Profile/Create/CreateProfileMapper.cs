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
                fullName: src.FullName,
                birthDate: src.BirthDate,
                gender: src.Gender,
                cpf: src.Cpf,
                email: src.Email,
                phoneNumber: src.PhoneNumber,
                motherName: src.MotherName,
                fatherName: src.FatherName,
                nationality: src.Nationality,
                street: src.Street,
                addressNumber: src.AddressNumber,
                neighborhood: src.Neighborhood,
                city: src.City,
                state: src.State,
                zipCode: src.ZipCode,
                country: src.Country,
                dependentsCount: src.DependentsCount,
                isPoliticallyExposed: src.IsPoliticallyExposed,
                isEmailVerified: src.IsEmailVerified,
                riskLevel: src.RiskLevel,
                latitude: src.Latitude,
                longitude: src.Longitude))
            .Ignore(dest => dest.Id!)
            .Ignore(dest => dest.Status!)
            .Ignore(dest => dest.ActivatedOnUtc!)
            .Ignore(dest => dest.InactivatedOnUtc!);
    }
}
