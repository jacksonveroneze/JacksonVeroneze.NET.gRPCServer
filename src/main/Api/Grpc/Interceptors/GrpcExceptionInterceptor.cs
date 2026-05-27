using FluentValidation;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace JacksonVeroneze.NET.GRPCServer.Api.Grpc.Interceptors;

public sealed class GrpcExceptionInterceptor(
    ILogger<GrpcExceptionInterceptor> logger) : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        ArgumentNullException.ThrowIfNull(continuation);
        ArgumentNullException.ThrowIfNull(context);
        
        try
        {
            return await continuation(request, context);
        }
        catch (RpcException)
        {
            throw;
        }
        catch (ValidationException exception)
        {
            var message = string.Join(
                ';', exception.Errors.Select(error =>
                    $"{error.PropertyName}: {error.ErrorMessage}"));

            throw new RpcException(new Status(
                StatusCode.InvalidArgument,
                message));
        }
        catch (OperationCanceledException) when
            (context.CancellationToken.IsCancellationRequested)
        {
            throw new RpcException(new Status(
                StatusCode.Cancelled,
                "The operation was cancelled."));
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled gRPC exception.");

            throw new RpcException(new Status(
                StatusCode.Internal,
                "An unexpected error occurred."));
        }
    }
}
