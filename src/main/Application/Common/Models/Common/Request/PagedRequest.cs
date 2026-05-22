using JacksonVeroneze.NET.Pagination.Enums;

namespace JacksonVeroneze.NET.GRPCServer.Application.Common.Models.Common.Request;

public abstract record PagedRequest
{
    private const int DefaulPageSize = 20;

    protected PagedRequest(string defaultOrderBy, SortDirection defaultOrderDirection)
    {
        ArgumentException.ThrowIfNullOrEmpty(defaultOrderBy);

        _orderBy = defaultOrderBy;
        _orderDirection = defaultOrderDirection;

        _pageSize = DefaulPageSize;
    }

    private readonly int _pageSize;
    private readonly string? _orderBy;
    private readonly SortDirection? _orderDirection;

    public string? Cursor { get; init; }

    public int? PageSize
    {
        get => _pageSize;
        init => _pageSize = value ?? DefaulPageSize;
    }

    public string? OrderBy
    {
        get => _orderBy;
        init => _orderBy = value ?? _orderBy;
    }

    public SortDirection? OrderDirection
    {
        get => _orderDirection;
        init => _orderDirection = value ?? _orderDirection;
    }
}
