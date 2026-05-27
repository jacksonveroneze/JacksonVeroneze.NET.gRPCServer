using FluentValidation;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace JacksonVeroneze.NET.GRPCServer.Api.Grpc.Interceptors;

public sealed class GrpcValidationInterceptor(
    IServiceProvider serviceProvider) : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(continuation);
        
        var validator = serviceProvider
            .GetService<IValidator<TRequest>>();

        if (validator is null)
        {
            return await continuation(request, context);
        }

        var validationResult = await validator.ValidateAsync(
            request,
            context.CancellationToken);

        if (validationResult.IsValid)
        {
            return await continuation(request, context);
        }

        var message = string.Join(
            "; ",
            validationResult.Errors.Select(error =>
                $"{error.PropertyName}: {error.ErrorMessage}"));

        throw new RpcException(new Status(
            StatusCode.InvalidArgument,
            message));

    }
}
