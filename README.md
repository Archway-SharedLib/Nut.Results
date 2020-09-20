# Results

## Test

Execute following command for install coverage tools before run test.

```
dotnet tool install -g dotnet-reportgenerator-globaltool
```

Run test with code coverage

```
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
```

Generate coverage report

```
reportgenerator "-reports:test\Results.Test\coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html
```