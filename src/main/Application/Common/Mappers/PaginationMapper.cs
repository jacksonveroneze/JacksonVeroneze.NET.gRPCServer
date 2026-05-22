using JacksonVeroneze.NET.GRPCServer.Application.Common.Models.Common.Request;
using JacksonVeroneze.NET.GRPCServer.Application.Common.Models.Common.Response;
using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Pagination.Enums;
using Mapster;

namespace JacksonVeroneze.NET.GRPCServer.Application.Common.Mappers;

public class PaginationMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<PagedRequest, PaginationParameters>()
            .ConstructUsing(src =>
                new PaginationParameters(src.PageSize!.Value,
                    src.Cursor, PaginationDirection.Next,
                    src.OrderBy, src.OrderDirection));

        config.NewConfig<PageInfo, PageInfoResponse>()
            .Map(dest => dest.HasMore, src => src.HasMore)
            .Map(dest => dest.NextCursor, src => src.NextCursor)
            .Map(dest => dest.PreviousCursor, src => src.PreviousCursor);
    }
}
