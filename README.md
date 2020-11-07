<img src="./assets/logo/logo.svg" alt="logo" height="192px" style="margin-bottom:2rem;" />

[![CI](https://github.com/Archway-SharedLib/Nut.Results/workflows/CI/badge.svg)](https://github.com/Archway-SharedLib/Nut.Results/actions)
[![codecov](https://codecov.io/gh/Archway-SharedLib/Nut.Results/branch/main/graph/badge.svg?token=C3XTN4VG2X)](https://codecov.io/gh/Archway-SharedLib/Nut.Results)


# Nut.Results

[![NuGet](https://img.shields.io/nuget/vpre/Nut.Results.svg)](https://www.nuget.org/packages/Nut.Results) 
[![NuGet](https://img.shields.io/nuget/dt/Nut.Results.svg)](https://www.nuget.org/packages/Nut.Results)

Nut.Resultsはシンプルな処理結果を表す.NETのオブジェクトを提供します。Nut.Resultsは処理の成功と失敗を表すことができます。

## Nut.Resultsが解決すること

処理の失敗は通常、例外もしくは真偽値、ときにはnullで表せられます。このような処理結果の返し方は次のような問題をはらんでいます。

- 例外を利用した場合は、処理が非線形な流れになり、コードの解析性と可読性が低下し、保守の難易度を上げてしまいます。
- 真偽値は処理結果の詳細が失われてしまいます。結果の詳細を失わないためにオブジェクトのプロパティなどに結果を保存しておくこともありますが、これは状態の管理をさらに複雑にしてしまう悪手です。
- nullでは、nullという値が失敗を表しているのか、結果が単純にないのか(例えばデータ取得処理でデータが無かっただけなのか)が表せません。処理ごとに開発者に意識させる必要がでてしまい、不具合の発生原因になります。

処理結果にNut.Resultsを利用し、各メソッドを適切に利用することで上記のような問題を解決します。

```cs
var okResult = Result.Ok("The process was successful!");
```

詳細な使い方は[ドキュメント](./docs/result/howtouse.md)を参照してください。


# Nut.Results.FluentAssertions

[![NuGet](https://img.shields.io/nuget/vpre/Nut.Results.FluentAssertions.svg)](https://www.nuget.org/packages/Nut.Results.FluentAssertions)
[![NuGet](https://img.shields.io/nuget/dt/Nut.Results.FluentAssertions.svg)](https://www.nuget.org/packages/Nut.Results.FluentAssertions)

Nut.Results.FluentAssertionsは、検証用ライブラリである[FluentAssertions](https://fluentassertions.com/)のNut.Results用の拡張です。
`Result`オブジェクトを検証するためのメソッドが提供されています。

```cs
var result = Result.Ok();
result.Should().BeOk();
```

詳細な使い方は[ドキュメント](./docs/fluentassertions/howtouse.md)を参照してください。