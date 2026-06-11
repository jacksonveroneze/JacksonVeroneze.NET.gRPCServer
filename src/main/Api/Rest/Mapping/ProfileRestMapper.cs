using JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Profiles.v1.Models;
using JacksonVeroneze.NET.GRPCServer.Application.Common.Models.Common.Response;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Models;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetPaged;
using Mapster;

namespace JacksonVeroneze.NET.GRPCServer.Api.Rest.Mapping;

public sealed class ProfileRestMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<GetPagedProfilesRestRequest, GetPagedProfilesRequest>()
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Cpf, src => src.Cpf)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.Page, src => src.Page)
            .Map(dest => dest.PageSize, src => src.PageSize)
            .Map(dest => dest.OrderBy, src => src.OrderBy)
            .Map(dest => dest.Order, src => src.Order);

        config.NewConfig<GetPagedProfilesResponse, GetPagedProfilesRestResponse>()
            .Map(dest => dest.Data, src => src.Data)
            .Map(dest => dest.Pagination, src => src.Pagination);

        config.NewConfig<ProfileResponse, ProfileRestResponse>()
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
            .Map(dest => dest.BirthCity, src => src.BirthCity)
            .Map(dest => dest.BirthState, src => src.BirthState)
            .Map(dest => dest.Street, src => src.Street)
            .Map(dest => dest.AddressNumber, src => src.AddressNumber)
            .Map(dest => dest.Complement, src => src.Complement)
            .Map(dest => dest.Neighborhood, src => src.Neighborhood)
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.State, src => src.State)
            .Map(dest => dest.ZipCode, src => src.ZipCode)
            .Map(dest => dest.Country, src => src.Country)
            .Map(dest => dest.ActivedOnUtc, src => src.ActivedOnUtc)
            .Map(dest => dest.InactivedOnUtc, src => src.InactivedOnUtc);

        config.NewConfig<PageInfoResponse, PageInfoRestResponse>()
            .Map(dest => dest.Page, src => src.Page)
            .Map(dest => dest.PageSize, src => src.PageSize)
            .Map(dest => dest.TotalPages, src => src.TotalPages)
            .Map(dest => dest.TotalElements, src => src.TotalElements)
            .Map(dest => dest.IsFirstPage, src => src.IsFirstPage)
            .Map(dest => dest.IsLastPage, src => src.IsLastPage)
            .Map(dest => dest.HasNextPage, src => src.HasNextPage)
            .Map(dest => dest.HasBackPage, src => src.HasBackPage)
            .Map(dest => dest.NextPage, src => src.NextPage)
            .Map(dest => dest.BackPage, src => src.BackPage);
    }
}
