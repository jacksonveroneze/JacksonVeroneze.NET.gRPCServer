using Grpc.Core;
using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services;

public static class ResultTypeGrpcStatusCodeMapper
{
    public static StatusCode ToGrpcStatusCode(
        this ResultType resultType)
    {
        return resultType switch
        {
            ResultType.Success => StatusCode.OK,
            ResultType.Invalid => StatusCode.InvalidArgument,
            ResultType.Conflict => StatusCode.AlreadyExists,
            ResultType.NotFound => StatusCode.NotFound,
            ResultType.RuleViolation => StatusCode.FailedPrecondition,
            ResultType.Error => StatusCode.Internal,
            _ => StatusCode.Unknown,
        };
    }
}
