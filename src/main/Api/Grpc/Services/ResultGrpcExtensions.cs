using Grpc.Core;
using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.NET.GRPCServer.Api.Grpc.Services;

public static class ResultGrpcExtensions
{
    public static void ThrowRpcExceptionIfFailure(
        this Result.Result result)
    {
        ArgumentNullException.ThrowIfNull(result);
        
        if (result.IsSuccess)
        {
            return;
        }

        throw result.ToRpcException();
    }

    public static TValue ValueOrThrowRpcException<TValue>(
        this Result<TValue> result)
    {
        ArgumentNullException.ThrowIfNull(result);
        
        return result.IsSuccess 
            ? result.Value! 
            : throw result.ToRpcException();
    }

    private static RpcException ToRpcException(
        this Result.Result result)
    {
        var error = result.FirstError;

        if (error is null)
        {
            return new RpcException(new Status(
                StatusCode.Unknown,
                "An unknown error occurred."));
        }

        return new RpcException(new Status(
            result.Type.ToGrpcStatusCode(),
            error.Message));
    }

    private static RpcException ToRpcException<TValue>(
        this Result<TValue> result)
    {
        var error = result.FirstError;

        if (error is null)
        {
            return new RpcException(new Status(
                StatusCode.Unknown,
                "An unknown error occurred."));
        }

        return new RpcException(new Status(
            result.Type.ToGrpcStatusCode(),
            error.Message));
    }
}
