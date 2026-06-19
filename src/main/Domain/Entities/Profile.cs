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

    public long PhoneNumber { get; private set; }

    public string? MotherName { get; private set; }

    public string? FatherName { get; private set; }

    public string? Nationality { get; private set; }

    public string? Street { get; private set; }

    public int AddressNumber { get; private set; }

    public string? Neighborhood { get; private set; }

    public string? City { get; private set; }

    public string? State { get; private set; }

    public string? ZipCode { get; private set; }

    public string? Country { get; private set; }

    public int DependentsCount { get; private set; }

    public bool IsPoliticallyExposed { get; private set; }

    public bool IsEmailVerified { get; private set; }

    public ProfileRiskLevel RiskLevel { get; private set; }

    public double Latitude { get; private set; }

    public double Longitude { get; private set; }

    public DateTimeOffset? ActivatedOnUtc { get; private set; }

    public DateTimeOffset? InactivatedOnUtc { get; private set; }

    #region Ctor

    private Profile()
    {
    }

    public Profile(
        string fullName,
        DateOnly birthDate,
        Gender gender,
        string cpf,
        string? email,
        long phoneNumber,
        string? motherName,
        string? fatherName,
        string? nationality,
        string? street,
        int addressNumber,
        string? neighborhood,
        string? city,
        string? state,
        string? zipCode,
        string? country,
        int dependentsCount,
        bool isPoliticallyExposed,
        bool isEmailVerified,
        ProfileRiskLevel riskLevel,
        double latitude,
        double longitude)
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
        Street = street;
        AddressNumber = addressNumber;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;

        DependentsCount = dependentsCount;
        IsPoliticallyExposed = isPoliticallyExposed;
        IsEmailVerified = isEmailVerified;
        RiskLevel = riskLevel;
        Latitude = latitude;
        Longitude = longitude;

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
