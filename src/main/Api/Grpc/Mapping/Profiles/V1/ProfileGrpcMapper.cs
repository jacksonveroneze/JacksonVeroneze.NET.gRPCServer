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
            .Map(dest => dest.ProfileId, src => src.Id)
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.BirthDate, src => src.BirthDate)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Cpf, src => src.Cpf)
            .Map(dest => dest.Status, src => src.Status);

        #endregion

        #region command

        config.NewConfig<CreateProfileRequest, ContractsApp.Create.CreateProfileRequest>()
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.BirthDate, src => src.BirthDate)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Cpf, src => src.Cpf);

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
