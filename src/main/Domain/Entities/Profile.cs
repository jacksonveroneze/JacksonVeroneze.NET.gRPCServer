using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.GRPCServer.Domain.Enums;
using JacksonVeroneze.NET.GRPCServer.Domain.Errors;
using JacksonVeroneze.NET.GRPCServer.Domain.Utils;

namespace JacksonVeroneze.NET.GRPCServer.Domain.Entities;

public sealed class Profile : Entity
{
    public Guid Id { get; }

    public string Name { get; private set; } = null!;

    public DateOnly Birthday { get; private set; }

    public Gender Gender { get; private set; }

    public string Cpf { get; private set; } = null!;

    public ProfileStatus Status { get; private set; }

    public DateTimeOffset? ActivedOnUtc { get; private set; }

    public DateTimeOffset? InactivedOnUtc { get; private set; }

    #region ctor

    private Profile()
    {
    }

    public Profile(string name, 
        DateOnly birthday, Gender gender, string cpf)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(cpf);

        Id = GuidGenerator.Generate();
        Name = name;
        Birthday = birthday;
        Gender = gender;
        Cpf = cpf;

        Status = ProfileStatus.PendingActivation;
    }

    #endregion

    #region Status

    public Result.Result Activate(DateTimeOffset utcNow)
    {
        if (IsActive)
        {
            return Result.Result.FromInvalid(
                DomainErrors.ProfileError.AlreadyActivated);
        }

        Status = ProfileStatus.Active;

        ActivedOnUtc = utcNow;

        return Result.Result.WithSuccess();
    }

    public Result.Result Inactivate(DateTimeOffset utcNow)
    {
        if (IsInactive)
        {
            return Result.Result.FromInvalid(
                DomainErrors.ProfileError.AlreadyInactivated);
        }

        Status = ProfileStatus.Inactive;

        InactivedOnUtc = utcNow;

        return Result.Result.WithSuccess();
    }

    public bool IsActive => Status is ProfileStatus.Active;
    public bool IsInactive => Status is ProfileStatus.Inactive;
    public bool IsPendingActivation => Status is ProfileStatus.PendingActivation;

    #endregion
}
