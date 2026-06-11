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
            .Map(dest => dest.Cpf, src => src.Cpf)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.MotherName, src => src.MotherName)
            .Map(dest => dest.FatherName, src => src.FatherName)
            .Map(dest => dest.Nationality, src => src.Nationality)
            .Map(dest => dest.BirthCity, src => src.BirthCity)
            .Map(dest => dest.BirthState, src => src.BirthState)
            .Map(dest => dest.Street, src => src.Street)
            .Map(dest => dest.AddressNumber, src => src.AddressNumber)
            .Map(dest => dest.Complement, src => src.Complement)
            .Map(dest => dest.Neighborhood, src => src.Neighborhood)
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.State, src => src.State)
            .Map(dest => dest.ZipCode, src => src.ZipCode)
            .Map(dest => dest.Country, src => src.Country);
    }
}
