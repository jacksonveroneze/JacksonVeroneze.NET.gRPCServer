using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.GRPCServer.Domain.Enums;
using JacksonVeroneze.NET.GRPCServer.Domain.Errors;
using JacksonVeroneze.NET.GRPCServer.Domain.Utils;

namespace JacksonVeroneze.NET.GRPCServer.Domain.Entities;

public sealed class Profile : Entity
{
    public Guid Id { get; }

    public string FullName { get; private set; } = null!;

    public DateOnly BirthDate { get; private set; }

    public Gender Gender { get; private set; }

    public string Cpf { get; private set; } = null!;

    public ProfileStatus Status { get; private set; }

    public string? Email { get; private set; }

    public string? PhoneNumber { get; private set; }

    public string? MotherName { get; private set; }

    public string? FatherName { get; private set; }

    public string? Nationality { get; private set; }

    public string? BirthCity { get; private set; }

    public string? BirthState { get; private set; }

    public string? Street { get; private set; }

    public string? AddressNumber { get; private set; }

    public string? Complement { get; private set; }

    public string? Neighborhood { get; private set; }

    public string? City { get; private set; }

    public string? State { get; private set; }

    public string? ZipCode { get; private set; }

    public string? Country { get; private set; }

    public DateTimeOffset? ActivatedOnUtc { get; private set; }

    public DateTimeOffset? InactivatedOnUtc { get; private set; }

    #region ctor

    private Profile()
    {
    }

    public Profile(string fullName,
        DateOnly birthDate,
        Gender gender,
        string cpf,
        string? email = null,
        string? phoneNumber = null,
        string? motherName = null,
        string? fatherName = null,
        string? nationality = null,
        string? birthCity = null,
        string? birthState = null,
        string? street = null,
        string? addressNumber = null,
        string? complement = null,
        string? neighborhood = null,
        string? city = null,
        string? state = null,
        string? zipCode = null,
        string? country = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(fullName);
        ArgumentException.ThrowIfNullOrEmpty(cpf);

        Id = GuidGenerator.Generate();
        FullName = fullName;
        BirthDate = birthDate;
        Gender = gender;
        Cpf = cpf;

        Email = email;
        PhoneNumber = phoneNumber;
        MotherName = motherName;
        FatherName = fatherName;
        Nationality = nationality;
        BirthCity = birthCity;
        BirthState = birthState;
        Street = street;
        AddressNumber = addressNumber;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;

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

        ActivatedOnUtc = utcNow;

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

        InactivatedOnUtc = utcNow;

        return Result.Result.WithSuccess();
    }

    public bool IsActive => Status is ProfileStatus.Active;
    public bool IsInactive => Status is ProfileStatus.Inactive;
    public bool IsPendingActivation => Status is ProfileStatus.PendingActivation;

    #endregion
}
