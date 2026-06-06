using System.Globalization;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using JacksonVeroneze.NET.GRPCServer.Api.Mcp.Models;
using JacksonVeroneze.NET.GRPCServer.Api.Mcp.Tools;
using Xunit;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Activate;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Common.Models;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Create;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.GetById;
using JacksonVeroneze.NET.GRPCServer.Application.v1.Profile.Inactivate;
using JacksonVeroneze.NET.GRPCServer.Domain.Enums;
using JacksonVeroneze.NET.GRPCServer.Domain.Errors;
using JacksonVeroneze.NET.Result;
using MapsterMapper;
using ModelContextProtocol.Protocol;
using Moq;

namespace JacksonVeroneze.NET.GRPCServer.Api.UnitTests.Mcp.Tools;

public sealed class ProfileToolsTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICreateProfileUseCase> _createUseCaseMock;
    private readonly Mock<IActivateProfileUseCase> _activateUseCaseMock;
    private readonly Mock<IInactivateProfileUseCase> _inactivateUseCaseMock;
    private readonly Mock<IGetByIdProfileUseCase> _getByIdUseCaseMock;
    private readonly Mock<IValidator<CreateProfileToolInput>> _validatorMock;
    private readonly ProfileTools _sut;

    public ProfileToolsTests()
    {
        _mapperMock = new Mock<IMapper>();
        _createUseCaseMock = new Mock<ICreateProfileUseCase>();
        _activateUseCaseMock = new Mock<IActivateProfileUseCase>();
        _inactivateUseCaseMock = new Mock<IInactivateProfileUseCase>();
        _getByIdUseCaseMock = new Mock<IGetByIdProfileUseCase>();
        _validatorMock = new Mock<IValidator<CreateProfileToolInput>>();

        _sut = new ProfileTools(_mapperMock.Object);
    }

    #region CreateAsync Tests

    [Fact]
    public async Task CreateAsync_Should_ReturnSuccess_WhenValidInputAndUseCaseSucceeds()
    {
        // Arrange
        var input = BuildValidCreateProfileToolInput();
        var request = BuildCreateProfileRequest();
        var profileResponse = BuildProfileResponse();
        var response = new CreateProfileResponse { Data = profileResponse };
        var useCaseResult = Result<CreateProfileResponse>.WithSuccess(response);

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mapperMock
            .Setup(m => m.Map<CreateProfileToolInput, CreateProfileRequest>(input))
            .Returns(request);

        _createUseCaseMock
            .Setup(uc => uc.ExecuteAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Content.Should().NotBeEmpty();
    }

    [Fact]
    public async Task CreateAsync_Should_ReturnError_WhenValidationFails()
    {
        // Arrange
        var input = new CreateProfileToolInput
        {
            FullName = null,
            BirthDate = null,
            Cpf = null,
            Gender = Gender.None,
        };

        var validationFailures = new List<ValidationFailure>
        {
            new("FullName", "'Full Name' must not be empty."),
            new("Cpf", "'Cpf' must not be empty."),
            new("BirthDate", "'Birth Date' must not be empty."),
            new("Gender", "'Gender' must not be equal to 'None'."),
        };

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(validationFailures));

        // Act
        var result = await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();

        _mapperMock.Verify(
            m => m.Map<CreateProfileToolInput, CreateProfileRequest>(It.IsAny<CreateProfileToolInput>()),
            Times.Never);

        _createUseCaseMock.Verify(
            uc => uc.ExecuteAsync(It.IsAny<CreateProfileRequest>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateAsync_Should_ReturnError_WhenValidationFailsWithNullFullName()
    {
        // Arrange
        var input = new CreateProfileToolInput
        {
            FullName = null,
            BirthDate = "1990-01-15",
            Cpf = "11122233344",
            Gender = Gender.Male,
        };

        var validationFailures = new List<ValidationFailure>
        {
            new("FullName", "'Full Name' must not be empty."),
        };

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(validationFailures));

        // Act
        var result = await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.Content.Should().ContainSingle();
    }

    [Fact]
    public async Task CreateAsync_Should_ReturnError_WhenValidationFailsWithNullCpf()
    {
        // Arrange
        var input = new CreateProfileToolInput
        {
            FullName = "Joana da Silva",
            BirthDate = "1995-05-25",
            Cpf = null,
            Gender = Gender.Female,
        };

        var validationFailures = new List<ValidationFailure>
        {
            new("Cpf", "'Cpf' must not be empty."),
        };

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(validationFailures));

        // Act
        var result = await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task CreateAsync_Should_ReturnError_WhenValidationFailsWithGenderNone()
    {
        // Arrange
        var input = new CreateProfileToolInput
        {
            FullName = "Joana da Silva",
            BirthDate = "1995-05-25",
            Cpf = "11122233344",
            Gender = Gender.None,
        };

        var validationFailures = new List<ValidationFailure>
        {
            new("Gender", "'Gender' must not be equal to 'None'."),
        };

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(validationFailures));

        // Act
        var result = await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task CreateAsync_Should_ReturnError_WhenUseCaseReturnsDuplicatedConflict()
    {
        // Arrange
        var input = BuildValidCreateProfileToolInput();
        var request = BuildCreateProfileRequest();
        var useCaseResult = Result<CreateProfileResponse>.FromConflict(
            DomainErrors.ProfileError.Duplicated);

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mapperMock
            .Setup(m => m.Map<CreateProfileToolInput, CreateProfileRequest>(input))
            .Returns(request);

        _createUseCaseMock
            .Setup(uc => uc.ExecuteAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.Content.Should().ContainSingle()
            .Which.As<TextContentBlock>()
            .Text.Should().Be(DomainErrors.ProfileError.Duplicated.Message);
    }

    [Fact]
    public async Task CreateAsync_Should_CallValidatorOnce_WhenInvoked()
    {
        // Arrange
        var input = BuildValidCreateProfileToolInput();
        var request = BuildCreateProfileRequest();
        var response = new CreateProfileResponse { Data = BuildProfileResponse() };
        var useCaseResult = Result<CreateProfileResponse>.WithSuccess(response);

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mapperMock
            .Setup(m => m.Map<CreateProfileToolInput, CreateProfileRequest>(input))
            .Returns(request);

        _createUseCaseMock
            .Setup(uc => uc.ExecuteAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        _validatorMock.Verify(
            v => v.ValidateAsync(input, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task CreateAsync_Should_CallMapperOnce_WhenValidationPasses()
    {
        // Arrange
        var input = BuildValidCreateProfileToolInput();
        var request = BuildCreateProfileRequest();
        var response = new CreateProfileResponse { Data = BuildProfileResponse() };
        var useCaseResult = Result<CreateProfileResponse>.WithSuccess(response);

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mapperMock
            .Setup(m => m.Map<CreateProfileToolInput, CreateProfileRequest>(input))
            .Returns(request);

        _createUseCaseMock
            .Setup(uc => uc.ExecuteAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        _mapperMock.Verify(
            m => m.Map<CreateProfileToolInput, CreateProfileRequest>(input),
            Times.Once);
    }

    [Fact]
    public async Task CreateAsync_Should_CallUseCaseOnce_WhenValidationAndMappingPass()
    {
        // Arrange
        var input = BuildValidCreateProfileToolInput();
        var request = BuildCreateProfileRequest();
        var response = new CreateProfileResponse { Data = BuildProfileResponse() };
        var useCaseResult = Result<CreateProfileResponse>.WithSuccess(response);

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mapperMock
            .Setup(m => m.Map<CreateProfileToolInput, CreateProfileRequest>(input))
            .Returns(request);

        _createUseCaseMock
            .Setup(uc => uc.ExecuteAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        _createUseCaseMock.Verify(
            uc => uc.ExecuteAsync(request, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task CreateAsync_Should_NotCallUseCase_WhenValidationFails()
    {
        // Arrange
        var input = BuildValidCreateProfileToolInput();

        var validationFailures = new List<ValidationFailure>
        {
            new("FullName", "FullName is required."),
        };

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(validationFailures));

        // Act
        await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        _createUseCaseMock.Verify(
            uc => uc.ExecuteAsync(It.IsAny<CreateProfileRequest>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateAsync_Should_ReturnSuccessWithContent_WhenUseCaseReturnsValidResponse()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var input = BuildValidCreateProfileToolInput();
        var request = BuildCreateProfileRequest();
        var profileResponse = BuildProfileResponse(profileId);
        var response = new CreateProfileResponse { Data = profileResponse };
        var useCaseResult = Result<CreateProfileResponse>.WithSuccess(response);

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mapperMock
            .Setup(m => m.Map<CreateProfileToolInput, CreateProfileRequest>(input))
            .Returns(request);

        _createUseCaseMock
            .Setup(uc => uc.ExecuteAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Content.Should().ContainSingle()
            .Which.As<TextContentBlock>()
            .Text.Should().NotBeNullOrEmpty();
        result.StructuredContent.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateAsync_Should_PropagatesCancellationToken_ToValidator()
    {
        // Arrange
        var input = BuildValidCreateProfileToolInput();
        var cancellationToken = new CancellationToken(canceled: false);

        _validatorMock
            .Setup(v => v.ValidateAsync(input, cancellationToken))
            .ReturnsAsync(new ValidationResult());

        _mapperMock
            .Setup(m => m.Map<CreateProfileToolInput, CreateProfileRequest>(input))
            .Returns(BuildCreateProfileRequest());

        _createUseCaseMock
            .Setup(uc => uc.ExecuteAsync(It.IsAny<CreateProfileRequest>(), cancellationToken))
            .ReturnsAsync(Result<CreateProfileResponse>.WithSuccess(
                new CreateProfileResponse { Data = BuildProfileResponse() }));

        // Act
        await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            cancellationToken);

        // Assert
        _validatorMock.Verify(
            v => v.ValidateAsync(input, cancellationToken),
            Times.Once);
    }

    #endregion

    #region ActivateAsync Tests

    [Fact]
    public async Task ActivateAsync_Should_ReturnSuccess_WhenUseCaseSucceeds()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var useCaseResult = Result.Result.WithSuccess();

        _activateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.Is<ActivateProfileRequest>(r => r.Id == profileId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.ActivateAsync(
            _activateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
    }

    [Fact]
    public async Task ActivateAsync_Should_ReturnError_WhenProfileNotFound()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var useCaseResult = Result.Result.FromNotFound(
            DomainErrors.ProfileError.NotFound);

        _activateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.Is<ActivateProfileRequest>(r => r.Id == profileId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.ActivateAsync(
            _activateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.Content.Should().ContainSingle()
            .Which.As<TextContentBlock>()
            .Text.Should().Be(DomainErrors.ProfileError.NotFound.Message);
    }

    [Fact]
    public async Task ActivateAsync_Should_ReturnError_WhenProfileAlreadyActivated()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var useCaseResult = Result.Result.FromInvalid(
            DomainErrors.ProfileError.AlreadyActivated);

        _activateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.Is<ActivateProfileRequest>(r => r.Id == profileId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.ActivateAsync(
            _activateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.Content.Should().ContainSingle()
            .Which.As<TextContentBlock>()
            .Text.Should().Be(DomainErrors.ProfileError.AlreadyActivated.Message);
    }

    [Fact]
    public async Task ActivateAsync_Should_CallUseCaseWithCorrectRequest()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        ActivateProfileRequest? capturedRequest = null;

        _activateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<ActivateProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .Callback<ActivateProfileRequest, CancellationToken>(
                (req, _) => capturedRequest = req)
            .ReturnsAsync(Result.Result.WithSuccess());

        // Act
        await _sut.ActivateAsync(
            _activateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        capturedRequest.Should().NotBeNull();
        capturedRequest!.Id.Should().Be(profileId);
    }

    [Fact]
    public async Task ActivateAsync_Should_CallUseCaseOnce_WhenInvoked()
    {
        // Arrange
        var profileId = Guid.NewGuid();

        _activateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<ActivateProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Result.WithSuccess());

        // Act
        await _sut.ActivateAsync(
            _activateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        _activateUseCaseMock.Verify(
            uc => uc.ExecuteAsync(
                It.Is<ActivateProfileRequest>(r => r.Id == profileId),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ActivateAsync_Should_ReturnSuccessWithContent_WhenUseCaseSucceeds()
    {
        // Arrange
        var profileId = Guid.NewGuid();

        _activateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<ActivateProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Result.WithSuccess());

        // Act
        var result = await _sut.ActivateAsync(
            _activateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Content.Should().ContainSingle();
        result.StructuredContent.Should().NotBeNull();
    }

    [Fact]
    public async Task ActivateAsync_Should_PropagatesCancellationToken_ToUseCase()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var cancellationToken = new CancellationToken(canceled: false);

        _activateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<ActivateProfileRequest>(),
                cancellationToken))
            .ReturnsAsync(Result.Result.WithSuccess());

        // Act
        await _sut.ActivateAsync(
            _activateUseCaseMock.Object,
            profileId,
            cancellationToken);

        // Assert
        _activateUseCaseMock.Verify(
            uc => uc.ExecuteAsync(
                It.IsAny<ActivateProfileRequest>(),
                cancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task ActivateAsync_Should_ReturnError_WhenIdIsEmptyGuid()
    {
        // Arrange
        var profileId = Guid.Empty;
        var useCaseResult = Result.Result.FromNotFound(
            DomainErrors.ProfileError.NotFound);

        _activateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.Is<ActivateProfileRequest>(r => r.Id == profileId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.ActivateAsync(
            _activateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
    }

    #endregion

    #region InactivateAsync Tests

    [Fact]
    public async Task InactivateAsync_Should_ReturnSuccess_WhenUseCaseSucceeds()
    {
        // Arrange
        var profileId = Guid.NewGuid();

        _inactivateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.Is<InactivateProfileRequest>(r => r.Id == profileId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Result.WithSuccess());

        // Act
        var result = await _sut.InactivateAsync(
            _inactivateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
    }

    [Fact]
    public async Task InactivateAsync_Should_ReturnError_WhenProfileNotFound()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var useCaseResult = Result.Result.FromNotFound(
            DomainErrors.ProfileError.NotFound);

        _inactivateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.Is<InactivateProfileRequest>(r => r.Id == profileId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.InactivateAsync(
            _inactivateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.Content.Should().ContainSingle()
            .Which.As<TextContentBlock>()
            .Text.Should().Be(DomainErrors.ProfileError.NotFound.Message);
    }

    [Fact]
    public async Task InactivateAsync_Should_ReturnError_WhenProfileAlreadyInactivated()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var useCaseResult = Result.Result.FromInvalid(
            DomainErrors.ProfileError.AlreadyInactivated);

        _inactivateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.Is<InactivateProfileRequest>(r => r.Id == profileId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.InactivateAsync(
            _inactivateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.Content.Should().ContainSingle()
            .Which.As<TextContentBlock>()
            .Text.Should().Be(DomainErrors.ProfileError.AlreadyInactivated.Message);
    }

    [Fact]
    public async Task InactivateAsync_Should_CallUseCaseWithCorrectRequest()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        InactivateProfileRequest? capturedRequest = null;

        _inactivateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<InactivateProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .Callback<InactivateProfileRequest, CancellationToken>(
                (req, _) => capturedRequest = req)
            .ReturnsAsync(Result.Result.WithSuccess());

        // Act
        await _sut.InactivateAsync(
            _inactivateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        capturedRequest.Should().NotBeNull();
        capturedRequest!.Id.Should().Be(profileId);
    }

    [Fact]
    public async Task InactivateAsync_Should_CallUseCaseOnce_WhenInvoked()
    {
        // Arrange
        var profileId = Guid.NewGuid();

        _inactivateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<InactivateProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Result.WithSuccess());

        // Act
        await _sut.InactivateAsync(
            _inactivateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        _inactivateUseCaseMock.Verify(
            uc => uc.ExecuteAsync(
                It.Is<InactivateProfileRequest>(r => r.Id == profileId),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task InactivateAsync_Should_ReturnSuccessWithContent_WhenUseCaseSucceeds()
    {
        // Arrange
        var profileId = Guid.NewGuid();

        _inactivateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<InactivateProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Result.WithSuccess());

        // Act
        var result = await _sut.InactivateAsync(
            _inactivateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Content.Should().ContainSingle();
        result.StructuredContent.Should().NotBeNull();
    }

    [Fact]
    public async Task InactivateAsync_Should_PropagatesCancellationToken_ToUseCase()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var cancellationToken = new CancellationToken(canceled: false);

        _inactivateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<InactivateProfileRequest>(),
                cancellationToken))
            .ReturnsAsync(Result.Result.WithSuccess());

        // Act
        await _sut.InactivateAsync(
            _inactivateUseCaseMock.Object,
            profileId,
            cancellationToken);

        // Assert
        _inactivateUseCaseMock.Verify(
            uc => uc.ExecuteAsync(
                It.IsAny<InactivateProfileRequest>(),
                cancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task InactivateAsync_Should_ReturnError_WhenIdIsEmptyGuid()
    {
        // Arrange
        var profileId = Guid.Empty;
        var useCaseResult = Result.Result.FromNotFound(
            DomainErrors.ProfileError.NotFound);

        _inactivateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.Is<InactivateProfileRequest>(r => r.Id == profileId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.InactivateAsync(
            _inactivateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
    }

    #endregion

    #region GetByIdAsync Tests

    [Fact]
    public async Task GetByIdAsync_Should_ReturnSuccess_WhenProfileExists()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var profileResponse = BuildProfileResponse(profileId);
        var response = new GetByIdProfileResponse { Data = profileResponse };
        var useCaseResult = Result<GetByIdProfileResponse>.WithSuccess(response);

        _getByIdUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.Is<GetByIdProfileRequest>(r => r.Id == profileId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.GetByIdAsync(
            _getByIdUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnError_WhenProfileNotFound()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var useCaseResult = Result<GetByIdProfileResponse>.FromNotFound(
            DomainErrors.ProfileError.NotFound);

        _getByIdUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.Is<GetByIdProfileRequest>(r => r.Id == profileId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.GetByIdAsync(
            _getByIdUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.Content.Should().ContainSingle()
            .Which.As<TextContentBlock>()
            .Text.Should().Be(DomainErrors.ProfileError.NotFound.Message);
    }

    [Fact]
    public async Task GetByIdAsync_Should_CallUseCaseWithCorrectRequest()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        GetByIdProfileRequest? capturedRequest = null;

        _getByIdUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<GetByIdProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .Callback<GetByIdProfileRequest, CancellationToken>(
                (req, _) => capturedRequest = req)
            .ReturnsAsync(Result<GetByIdProfileResponse>.WithSuccess(
                new GetByIdProfileResponse { Data = BuildProfileResponse(profileId) }));

        // Act
        await _sut.GetByIdAsync(
            _getByIdUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        capturedRequest.Should().NotBeNull();
        capturedRequest!.Id.Should().Be(profileId);
    }

    [Fact]
    public async Task GetByIdAsync_Should_CallUseCaseOnce_WhenInvoked()
    {
        // Arrange
        var profileId = Guid.NewGuid();

        _getByIdUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<GetByIdProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<GetByIdProfileResponse>.WithSuccess(
                new GetByIdProfileResponse { Data = BuildProfileResponse(profileId) }));

        // Act
        await _sut.GetByIdAsync(
            _getByIdUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        _getByIdUseCaseMock.Verify(
            uc => uc.ExecuteAsync(
                It.Is<GetByIdProfileRequest>(r => r.Id == profileId),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnSuccessWithContent_WhenProfileExists()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var profileResponse = BuildProfileResponse(profileId);
        var response = new GetByIdProfileResponse { Data = profileResponse };
        var useCaseResult = Result<GetByIdProfileResponse>.WithSuccess(response);

        _getByIdUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<GetByIdProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.GetByIdAsync(
            _getByIdUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Content.Should().ContainSingle();
        result.StructuredContent.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_Should_PropagatesCancellationToken_ToUseCase()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var cancellationToken = new CancellationToken(canceled: false);

        _getByIdUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<GetByIdProfileRequest>(),
                cancellationToken))
            .ReturnsAsync(Result<GetByIdProfileResponse>.WithSuccess(
                new GetByIdProfileResponse { Data = BuildProfileResponse(profileId) }));

        // Act
        await _sut.GetByIdAsync(
            _getByIdUseCaseMock.Object,
            profileId,
            cancellationToken);

        // Assert
        _getByIdUseCaseMock.Verify(
            uc => uc.ExecuteAsync(
                It.IsAny<GetByIdProfileRequest>(),
                cancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnError_WhenIdIsEmptyGuid()
    {
        // Arrange
        var profileId = Guid.Empty;
        var useCaseResult = Result<GetByIdProfileResponse>.FromNotFound(
            DomainErrors.ProfileError.NotFound);

        _getByIdUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.Is<GetByIdProfileRequest>(r => r.Id == profileId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.GetByIdAsync(
            _getByIdUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
    }

    #endregion

    #region CallToolResult Structure Tests

    [Fact]
    public async Task ActivateAsync_Should_ReturnErrorWithStructuredContent_WhenUseCaseFails()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var useCaseResult = Result.Result.FromNotFound(
            DomainErrors.ProfileError.NotFound);

        _activateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<ActivateProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.ActivateAsync(
            _activateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.StructuredContent.Should().NotBeNull();
    }

    [Fact]
    public async Task InactivateAsync_Should_ReturnErrorWithStructuredContent_WhenUseCaseFails()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var useCaseResult = Result.Result.FromNotFound(
            DomainErrors.ProfileError.NotFound);

        _inactivateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<InactivateProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.InactivateAsync(
            _inactivateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.StructuredContent.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnErrorWithStructuredContent_WhenUseCaseFails()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var useCaseResult = Result<GetByIdProfileResponse>.FromNotFound(
            DomainErrors.ProfileError.NotFound);

        _getByIdUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<GetByIdProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.GetByIdAsync(
            _getByIdUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.StructuredContent.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateAsync_Should_ReturnErrorWithStructuredContent_WhenValidationFails()
    {
        // Arrange
        var input = BuildValidCreateProfileToolInput();

        var validationFailures = new List<ValidationFailure>
        {
            new("FullName", "FullName is required."),
        };

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(validationFailures));

        // Act
        var result = await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.StructuredContent.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateAsync_Should_ReturnIsErrorFalse_WhenUseCaseSucceeds()
    {
        // Arrange
        var input = BuildValidCreateProfileToolInput();
        var request = BuildCreateProfileRequest();
        var response = new CreateProfileResponse { Data = BuildProfileResponse() };

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mapperMock
            .Setup(m => m.Map<CreateProfileToolInput, CreateProfileRequest>(input))
            .Returns(request);

        _createUseCaseMock
            .Setup(uc => uc.ExecuteAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<CreateProfileResponse>.WithSuccess(response));

        // Act
        var result = await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
    }

    #endregion

    #region Theory Tests

    [Theory]
    [InlineData("11122233344", "Joana da Silva", "1995-05-25")]
    [InlineData("99988877766", "Carlos Souza", "1980-12-01")]
    [InlineData("55566677788", "Maria Oliveira", "2000-03-20")]
    public async Task CreateAsync_Should_ReturnSuccess_ForVariousValidInputs(
        string cpf, string fullName, string birthDate)
    {
        // Arrange
        var input = new CreateProfileToolInput
        {
            FullName = fullName,
            BirthDate = birthDate,
            Cpf = cpf,
            Gender = Gender.Female,
        };

        var request = new CreateProfileRequest(
            fullName,
            DateOnly.Parse(birthDate, CultureInfo.InvariantCulture),
            Gender.Female,
            cpf);

        var response = new CreateProfileResponse { Data = BuildProfileResponse() };

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mapperMock
            .Setup(m => m.Map<CreateProfileToolInput, CreateProfileRequest>(input))
            .Returns(request);

        _createUseCaseMock
            .Setup(uc => uc.ExecuteAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<CreateProfileResponse>.WithSuccess(response));

        // Act
        var result = await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
    }

    [Theory]
    [InlineData(Gender.Male)]
    [InlineData(Gender.Female)]
    [InlineData(Gender.Other)]
    public async Task CreateAsync_Should_ReturnSuccess_ForAllValidGenderValues(Gender gender)
    {
        // Arrange
        var input = new CreateProfileToolInput
        {
            FullName = "Test User",
            BirthDate = "1990-06-15",
            Cpf = "11122233344",
            Gender = gender,
        };

        var request = new CreateProfileRequest(
            "Test User",
            new DateOnly(1990, 6, 15),
            gender,
            "11122233344");

        var response = new CreateProfileResponse { Data = BuildProfileResponse() };

        _validatorMock
            .Setup(v => v.ValidateAsync(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mapperMock
            .Setup(m => m.Map<CreateProfileToolInput, CreateProfileRequest>(input))
            .Returns(request);

        _createUseCaseMock
            .Setup(uc => uc.ExecuteAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<CreateProfileResponse>.WithSuccess(response));

        // Act
        var result = await _sut.CreateAsync(
            _createUseCaseMock.Object,
            _validatorMock.Object,
            input,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ActivateAsync_Should_ReturnResult_MatchingUseCaseIsSuccess(bool isSuccess)
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var useCaseResult = isSuccess
            ? Result.Result.WithSuccess()
            : Result.Result.FromNotFound(DomainErrors.ProfileError.NotFound);

        _activateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<ActivateProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.ActivateAsync(
            _activateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.IsError.Should().Be(!isSuccess);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task InactivateAsync_Should_ReturnResult_MatchingUseCaseIsSuccess(bool isSuccess)
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var useCaseResult = isSuccess
            ? Result.Result.WithSuccess()
            : Result.Result.FromNotFound(DomainErrors.ProfileError.NotFound);

        _inactivateUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<InactivateProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.InactivateAsync(
            _inactivateUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.IsError.Should().Be(!isSuccess);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task GetByIdAsync_Should_ReturnResult_MatchingUseCaseIsSuccess(bool isSuccess)
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var useCaseResult = isSuccess
            ? Result<GetByIdProfileResponse>.WithSuccess(
                new GetByIdProfileResponse { Data = BuildProfileResponse(profileId) })
            : Result<GetByIdProfileResponse>.FromNotFound(DomainErrors.ProfileError.NotFound);

        _getByIdUseCaseMock
            .Setup(uc => uc.ExecuteAsync(
                It.IsAny<GetByIdProfileRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(useCaseResult);

        // Act
        var result = await _sut.GetByIdAsync(
            _getByIdUseCaseMock.Object,
            profileId,
            CancellationToken.None);

        // Assert
        result.IsError.Should().Be(!isSuccess);
    }

    #endregion

    #region Test Data Builders

    private static CreateProfileToolInput BuildValidCreateProfileToolInput() =>
        new()
        {
            FullName = "Joana da Silva",
            BirthDate = "1995-05-25",
            Cpf = "11122233344",
            Gender = Gender.Female,
        };

    private static CreateProfileRequest BuildCreateProfileRequest() =>
        new(
            FullName: "Joana da Silva",
            BirthDate: new DateOnly(1995, 5, 25),
            Gender: Gender.Female,
            Cpf: "11122233344");

    private static ProfileResponse BuildProfileResponse(Guid? id = null) =>
        new(
            Id: id ?? Guid.NewGuid(),
            FullName: "Joana da Silva",
            BirthDate: new DateOnly(1995, 5, 25),
            Gender: Gender.Female,
            Cpf: "11122233344",
            Status: ProfileStatus.PendingActivation,
            ActivedOnUtc: null,
            InactivedOnUtc: null);

    #endregion
}
