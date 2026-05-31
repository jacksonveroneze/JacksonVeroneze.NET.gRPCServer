using FluentValidation;
using JacksonVeroneze.GrpcServer.Contracts.Profiles.V1;

namespace JacksonVeroneze.NET.GRPCServer.Api.Grpc.Validation.Profiles.V1;

public sealed class ListProfilesRequestValidator
    : AbstractValidator<ListProfilesRequest>
{
    public ListProfilesRequestValidator()
    {
        RuleFor(x => x)
            .NotEmpty();
    }
}
