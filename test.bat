dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
reportgenerator "-reports:test\Results.Test\coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html
