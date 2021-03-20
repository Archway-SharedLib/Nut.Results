<img src="./assets/logo/logo.svg" alt="logo" height="192px" style="margin-bottom:2rem;" />

[![CI](https://github.com/Archway-SharedLib/Nut.Results/workflows/CI/badge.svg)](https://github.com/Archway-SharedLib/Nut.Results/actions)
[![codecov](https://codecov.io/gh/Archway-SharedLib/Nut.Results/branch/main/graph/badge.svg?token=C3XTN4VG2X)](https://codecov.io/gh/Archway-SharedLib/Nut.Results)


# Nut.Results

[![NuGet](https://img.shields.io/nuget/vpre/Nut.Results.svg)](https://www.nuget.org/packages/Nut.Results) 
[![NuGet](https://img.shields.io/nuget/dt/Nut.Results.svg)](https://www.nuget.org/packages/Nut.Results)

<!--
Nut.Resultsはシンプルな処理結果を表す.NETのオブジェクトを提供します。Nut.Resultsは処理の成功と失敗を表すことができます。
-->

Nut.Results provides an object in .NET that represents the result of a simple process, and can represent the success or failure of the process.

<!-- ## Nut.Resultsが解決すること -->
## What does it solve

<!-- 処理の失敗は通常、例外もしくは真偽値、ときにはnullで表せられます。このような処理結果の返し方は次のような問題をはらんでいます。 -->
Processing failures are usually represented by exceptions or boolean values, sometimes null. This way of returning the result of processing has the following problems.

<!-- - 例外を利用した場合は、処理が非線形な流れになり、コードの解析性と可読性が低下し、保守の難易度を上げてしまいます。 -->
- The use of exceptions results in a non-linear flow of processing, which reduces the parsability and readability of the code, and increases the difficulty of maintenance.
<!-- - 真偽値は処理結果の詳細が失われてしまいます。 -->
- When using boolean values, the details of the processing result will be lost. 
<!-- - nullでは、nullという値が失敗を表しているのか、結果が単純にないのか(例えばデータ取得処理でデータが無かっただけなのか)が表せません。処理ごとに開発者に意識させる必要がでてしまい、不具合の発生原因になります。 -->
- When null is used, it is not possible to indicate whether the value null represents a failure or simply the absence of a result (for example, there was no data in the data acquisition process). The developer needs to be aware of this for each process, which can cause problems.

<!-- 処理結果にNut.Resultsを利用し、各メソッドを適切に利用することで上記のような問題を解決します。 -->
Using Nut.Results for processing results solves the above problem.

```cs
var okResult = Result.Ok("The process was successful!");
```

<!-- 詳細な使い方は[ドキュメント](./docs/result/refactoring_step.md)を参照してください。
リファクタリングを通して使い方を確認できます。
用意されているAPIについては[APIドキュメント](./docs/result/api.md)を参照してください。 -->
For detailed instructions, see [documentation](./docs/result/refactoring_step.md).
You can check how to use it through refactoring.
For the API provided, see [API documentation](./docs/result/api.md).

# Nut.Results.FluentAssertions

[![NuGet](https://img.shields.io/nuget/vpre/Nut.Results.FluentAssertions.svg)](https://www.nuget.org/packages/Nut.Results.FluentAssertions)
[![NuGet](https://img.shields.io/nuget/dt/Nut.Results.FluentAssertions.svg)](https://www.nuget.org/packages/Nut.Results.FluentAssertions)

<!-- Nut.Results.FluentAssertionsは、検証用ライブラリである[FluentAssertions](https://fluentassertions.com/)のNut.Results用の拡張です。 -->
Nut.Results.FluentAssertions is an extension library of [FluentAssertions](https://fluentassertions.com/).
<!-- `Result`オブジェクトを検証するためのメソッドが提供されています。 -->
It provides methods for validating `Result` objects.

```cs
var result = Result.Ok();
result.Should().BeOk();
```

<!-- 詳細な使い方は[ドキュメント](./docs/fluentassertions/howtouse.md)を参照してください。 -->
For detailed instructions, see [documentation](./docs/fluentassertions/howtouse.md).
