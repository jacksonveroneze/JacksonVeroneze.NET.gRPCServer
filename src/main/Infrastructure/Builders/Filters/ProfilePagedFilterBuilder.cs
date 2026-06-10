using System.Linq.Expressions;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Filters;
using JacksonVeroneze.NET.GRPCServer.Domain.Entities;
using JacksonVeroneze.NET.GRPCServer.Domain.Enums;
using JacksonVeroneze.NET.GRPCServer.Infrastructure.Builders.Util;
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

        if (!string.IsNullOrWhiteSpace(filter.FullName))
        {
            builder.And(d => EF.Functions
                .ILike(d.FullName, $"%{filter.FullName}%"));
        }

        if (filter.Gender.HasValue && filter.Gender != Gender.None)
        {
            builder.And(d => d.Gender == filter.Gender);
        }

        if (!string.IsNullOrEmpty(filter.Cpf))
        {
            builder.And(d => d.Cpf == filter.Cpf);
        }

        if (filter.Status.HasValue && filter.Status != ProfileStatus.None)
        {
            builder.And(d => d.Status == filter.Status);
        }

        return builder.Build();
    }
}
