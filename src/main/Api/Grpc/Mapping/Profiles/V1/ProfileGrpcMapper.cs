using System.Globalization;
using Google.Protobuf.Collections;
using JacksonVeroneze.GrpcServer.Contracts.Profiles.V1;
using JacksonVeroneze.NET.GRPCServer.Application.Common.Models.Common.Response;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Models;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetById;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetPaged;
using Mapster;
using ContractsApp = JacksonVeroneze.NET.GRPCServer.Application.v1.Profile;

namespace JacksonVeroneze.NET.GRPCServer.Api.Grpc.Mapping.Profiles.V1;

public class ProfileGrpcMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        #region common

        config.NewConfig<ProfileResponse, Profile>()
            .Map(dest => dest.ProfileId, src => src.Id.ToString())
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.BirthDate, src => src.BirthDate.HasValue
                ? src.BirthDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                : string.Empty)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Cpf, src => src.Cpf ?? string.Empty)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.ActivedOnUtc, src => src.ActivedOnUtc.HasValue
                ? src.ActivedOnUtc.Value.ToString("O", CultureInfo.InvariantCulture)
                : string.Empty)
            .Map(dest => dest.InactivedOnUtc, src => src.InactivedOnUtc.HasValue
                ? src.InactivedOnUtc.Value.ToString("O", CultureInfo.InvariantCulture)
                : string.Empty)
            .Map(dest => dest.Email, src => src.Email ?? string.Empty)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber ?? string.Empty)
            .Map(dest => dest.MotherName, src => src.MotherName ?? string.Empty)
            .Map(dest => dest.FatherName, src => src.FatherName ?? string.Empty)
            .Map(dest => dest.Nationality, src => src.Nationality ?? string.Empty)
            .Map(dest => dest.BirthCity, src => src.BirthCity ?? string.Empty)
            .Map(dest => dest.BirthState, src => src.BirthState ?? string.Empty)
            .Map(dest => dest.Street, src => src.Street ?? string.Empty)
            .Map(dest => dest.AddressNumber, src => src.AddressNumber ?? string.Empty)
            .Map(dest => dest.Complement, src => src.Complement ?? string.Empty)
            .Map(dest => dest.Neighborhood, src => src.Neighborhood ?? string.Empty)
            .Map(dest => dest.City, src => src.City ?? string.Empty)
            .Map(dest => dest.State, src => src.State ?? string.Empty)
            .Map(dest => dest.ZipCode, src => src.ZipCode ?? string.Empty)
            .Map(dest => dest.Country, src => src.Country ?? string.Empty);

        #endregion

        #region command

        config.NewConfig<CreateProfileRequest, ContractsApp.Create.CreateProfileRequest>()
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

        config.NewConfig<ContractsApp.Create.CreateProfileResponse, CreateProfileResponse>()
            .Map(dest => dest.Profile, src => src.Data);

        config.NewConfig<ActivateProfileRequest, ContractsApp.Activate.ActivateProfileRequest>()
            .Map(dest => dest.Id, src => src.ProfileId);

        config.NewConfig<InactivateProfileRequest, ContractsApp.Inactivate.InactivateProfileRequest>()
            .Map(dest => dest.Id, src => src.ProfileId);

        #endregion

        #region query

        config.NewConfig<ListProfilesRequest, GetPagedProfilesRequest>()
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Cpf, src => src.Cpf)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.Page, src => src.Pagination.Page)
            .Map(dest => dest.PageSize, src => src.Pagination.PageSize)
            .Map(dest => dest.OrderBy, src => src.Pagination.OrderBy)
            .Map(dest => dest.Order, src => src.Pagination.Order);

        config.NewConfig<GetPagedProfilesResponse, ListProfilesResponse>()
            .Map(dest => dest.Data, src => src.Data)
            .Map(dest => dest.Pagination, src => src.Pagination)
            .UseDestinationValue(member =>
                member.SetterModifier == AccessModifier.None
            );

        config.NewConfig<List<ProfileResponse>, RepeatedField<Profile>>()
            .Map(dest => dest, src => src);

        config.NewConfig<PageInfoResponse, PagedResponse>()
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

        config.NewConfig<GetProfileRequest, GetByIdProfileRequest>()
            .Map(dest => dest.Id, src => src.ProfileId);

        config.NewConfig<GetByIdProfileResponse, GetProfileResponse>()
            .Map(dest => dest.Data, src => src.Data);

        #endregion
    }
}
