using FluentValidation;
using JacksonVeroneze.GrpcServer.Contracts.Profiles.V1;

namespace JacksonVeroneze.NET.GRPCServer.Api.Grpc.Validation.Profiles.V1;

public sealed class GetProfileRequestValidator
    : AbstractValidator<GetProfileRequest>
{
    public GetProfileRequestValidator()
    {
        RuleFor(x => x.ProfileId)
            .NotEmpty();
    }
}
