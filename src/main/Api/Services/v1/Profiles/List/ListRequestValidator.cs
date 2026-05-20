using FluentValidation;
using JacksonVeroneze.NET.GRPCServer.Contracts.Profiles.v1;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services.v1.Profiles.List;

public sealed class ListRequestValidator
    : AbstractValidator<ListProfilesRequest>
{
    public ListRequestValidator()
    {
        RuleFor(x => x.PageDirection)
            .NotEmpty();
    }
}
