<img src="./assets/logo/logo.svg" alt="logo" height="192px" style="margin-bottom:2rem;" />

[![CI](https://github.com/Archway-SharedLib/Nut.Results/workflows/CI/badge.svg)](https://github.com/Archway-SharedLib/Nut.Results/actions)
[![codecov](https://codecov.io/gh/Archway-SharedLib/Nut.Results/branch/main/graph/badge.svg?token=C3XTN4VG2X)](https://codecov.io/gh/Archway-SharedLib/Nut.Results)


# Nut.Results

[![NuGet](https://img.shields.io/nuget/vpre/Nut.Results.svg)](https://www.nuget.org/packages/Nut.Results) 
[![NuGet](https://img.shields.io/nuget/dt/Nut.Results.svg)](https://www.nuget.org/packages/Nut.Results)

Nut.Results provides an object in .NET that represents the result of a simple process, and can represent the success or failure of the process.

## What does it solve

Processing failures are usually represented by exceptions or boolean values, sometimes null. This way of returning the result of processing has the following problems.

- The use of exceptions results in a non-linear flow of processing, which reduces the parsability and readability of the code, and increases the difficulty of maintenance.
- When using boolean values, the details of the processing result will be lost. 
- When null is used, it is not possible to indicate whether the value null represents a failure or simply the absence of a result (for example, there was no data in the data acquisition process). The developer needs to be aware of this for each process, which can cause problems.

Using Nut.Results for processing results solves the above problem.

```cs
var okResult = Result.Ok("The process was successful!");
```

For detailed instructions, see [documentation](./docs/result/refactoring_step.md).
You can check how to use it through refactoring.
For the API provided, see [API documentation](./docs/result/api.md).

# Nut.Results.FluentAssertions

[![NuGet](https://img.shields.io/nuget/vpre/Nut.Results.FluentAssertions.svg)](https://www.nuget.org/packages/Nut.Results.FluentAssertions)
[![NuGet](https://img.shields.io/nuget/dt/Nut.Results.FluentAssertions.svg)](https://www.nuget.org/packages/Nut.Results.FluentAssertions)

Nut.Results.FluentAssertions is an extension library of [FluentAssertions](https://fluentassertions.com/).
It provides methods for validating `Result` objects.

```cs
var result = Result.Ok();
result.Should().BeOk();
```

For detailed instructions, see [documentation](./docs/fluentassertions/howtouse.md).
