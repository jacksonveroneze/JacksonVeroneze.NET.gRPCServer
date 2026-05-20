namespace JacksonVeroneze.NET.GRPCServer.Application.Common.Models.Common.Response;

public sealed record PageInfoResponse
{
    public bool? HasMore { get; init; }

    public string? NextCursor { get; init; }

    public string? PreviousCursor { get; init; }
}
