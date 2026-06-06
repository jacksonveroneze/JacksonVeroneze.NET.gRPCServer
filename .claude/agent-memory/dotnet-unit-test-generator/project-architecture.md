---
name: project-architecture
description: Key architectural patterns in JacksonVeroneze.NET.gRPCServer — Result library API, MCP tool structure, Central Package Management setup, and analyzer strictness.
metadata:
  type: project
---

This is a .NET 10 / C# 14 gRPC+REST+MCP server project.

**Why:** Understanding these patterns is essential for generating compilable, correct tests without trial-and-error.

**How to apply:** Always verify the Result library factory methods, respect CPM rules, and add `using Xunit;` explicitly in test files.

## Result Library (`JacksonVeroneze.NET.Result` v1.0.0)

- `Result.Result` (non-generic): `WithSuccess()`, `FromNotFound(Error)`, `FromInvalid(Error)`, `FromConflict(Error)`, `FromRuleViolation(Error)`, `WithError(Error)`
- `Result<T>` (generic): same static factories but typed — `WithSuccess(T value)`, `FromNotFound(Error)`, `FromConflict(Error)`, etc.
- Key properties: `IsSuccess`, `IsFailure`, `FirstError` (returns `Error`), `Errors`, `Value` (generic only)
- `Error` type: `Error.Create(code, message, target?)` — properties `Code`, `Message`, `Target`

## Domain Errors (`DomainErrors.ProfileError`)
- `NotFound` — "The profile with the specified identifier was not found."
- `Duplicated` — "The specified cpf is already in use."
- `AlreadyActivated` — "The profile has already been activated."
- `AlreadyInactivated` — "The profile has already been inactivated."

## MCP Tool Pattern

`ProfileTools` is a `[McpServerToolType]` class injected with `IMapper` via constructor.
Each method receives use case + optional validator via `[FromServices]` parameters — these must be passed directly in tests (no DI container needed).

`CallToolResult` structure:
- `IsError` (bool)
- `Content` (list of `ContentBlock` — typically `TextContentBlock { Text = message }`)
- `StructuredContent` (JsonElement — serialized structured data)

Success path calls `result.ToCallToolResultSuccess()` (extension method, `[ExcludeFromCodeCoverage]`).
Error path calls `result.ToCallToolResultError()` or `validationResult.ToCallToolResultError()`.

## Central Package Management (CPM)

All package versions are centrally managed in `src/Directory.Packages.props`.
Test packages must be added there first; `<PackageReference>` in `.csproj` must NOT include `Version="..."`.
`Directory.Build.props` sets `TreatWarningsAsErrors=true` and enforces Meziantou + SonarAnalyzer + BannedApiAnalyzers.

## Strict Analyzer Rules That Affect Tests

- `MA0011` / `S6580`: `DateOnly.Parse(str)` requires `CultureInfo.InvariantCulture` — use `DateOnly.Parse(str, CultureInfo.InvariantCulture)`.
- `using Xunit;` must be explicit — ImplicitUsings does NOT include xUnit namespaces.
- `using System.Globalization;` needed when using `CultureInfo`.

## Test Project Location

`src/tests/unit/Api.UnitTests/` — references `src/main/Api/Api.csproj` with relative path `..\..\..\..\src\main\Api\Api.csproj` (note: CPM props are at `src/` level, so test project in `src/tests/` is within the same CPM scope).
