namespace JacksonVeroneze.NET.GRPCServer.Application.Abstractions.UseCases;

public interface IBaseRequest;

public interface IResponse;

public interface IUseCase<in TRequest, TResponse>
    where TRequest : IBaseRequest
{
    Task<Result.Result<TResponse>> ExecuteAsync(
        TRequest input,
        CancellationToken cancellationToken);
}
