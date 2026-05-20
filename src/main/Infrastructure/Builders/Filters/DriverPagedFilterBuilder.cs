using System.Linq.Expressions;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Filters;
using JacksonVeroneze.NET.GRPCServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Builders.Filters;

public class ProfilePagedFilterBuilder(
    ProfilePagedFilter filter)
{
    public static ProfilePagedFilterBuilder Create(
        ProfilePagedFilter filter)
    {
        return new ProfilePagedFilterBuilder(filter);
    }

    public Expression<Func<Profile, bool>> Build()
    {
        FilterBuilder<Profile> builder = new();

        if (!string.IsNullOrWhiteSpace(filter.Cursor)
            && Guid.TryParse(filter.Cursor, out Guid cursor))
        {
            bool? isNext = filter.Pagination?.PaginationNext;

            if (isNext is true)
            {
                builder.And(d => d.Id > cursor);
            }
            else
            {
                builder.And(d => d.Id < cursor);
            }
        }

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            builder.And(d => EF.Functions
                .ILike(d.Name!, $"%{filter.Name}%"));
        }

        return builder.Build();
    }
}
