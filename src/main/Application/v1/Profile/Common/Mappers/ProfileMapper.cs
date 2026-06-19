using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Models;
using Mapster;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Mappers;

public class ProfileMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<Domain.Entities.Profile, ProfileResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.BirthDate, src => src.BirthDate)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Cpf, src => src.Cpf)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.MotherName, src => src.MotherName)
            .Map(dest => dest.FatherName, src => src.FatherName)
            .Map(dest => dest.Nationality, src => src.Nationality)
            .Map(dest => dest.Street, src => src.Street)
            .Map(dest => dest.AddressNumber, src => src.AddressNumber)
            .Map(dest => dest.Neighborhood, src => src.Neighborhood)
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.State, src => src.State)
            .Map(dest => dest.ZipCode, src => src.ZipCode)
            .Map(dest => dest.Country, src => src.Country)
            .Map(dest => dest.DependentsCount, src => src.DependentsCount)
            .Map(dest => dest.IsPoliticallyExposed, src => src.IsPoliticallyExposed)
            .Map(dest => dest.IsEmailVerified, src => src.IsEmailVerified)
            .Map(dest => dest.RiskLevel, src => src.RiskLevel)
            .Map(dest => dest.Latitude, src => src.Latitude)
            .Map(dest => dest.Longitude, src => src.Longitude)
            .Map(dest => dest.ActivedOnUtc, src => src.ActivatedOnUtc)
            .Map(dest => dest.InactivedOnUtc, src => src.InactivatedOnUtc);
    }
}
