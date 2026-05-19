using JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;

namespace JacksonVeroneze.NET.GRPCServer.Application.Common.Models.Common.Response;

public abstract record DataResponse<TType> : IResponse
{
    public TType? Data { get; init; }
}
