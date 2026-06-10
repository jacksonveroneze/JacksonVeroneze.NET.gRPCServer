using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Filters;
using JacksonVeroneze.NET.Pagination.Offset;
using Mapster;

namespace JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetPaged;

public class GetPagedProfilesMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<GetPagedProfilesRequest, ProfilePagedFilter>()
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Cpf, src => src.Cpf)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.Pagination, src => src);

        config.NewConfig<GetPagedProfilesRequest, ProfilePagedFilter>()
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Cpf, src => src.Cpf)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.Pagination, src => src);
        
        config.NewConfig<GetPagedProfilesRequest, PaginationParameters>()
            .ConstructUsing(src =>
                new PaginationParameters(
                    src.Page!.Value,
                    src.PageSize!.Value,
                    src.OrderBy, 
                    src.Order));
        
        config.NewConfig<Page<Domain.Entities.Profile>, GetPagedProfilesResponse>()
            .Map(dest => dest.Data, src => src.Data)
            .Map(dest => dest.Pagination, src => src.PageInfo);
    }
}
