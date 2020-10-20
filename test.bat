REM dotnet tool install -g dotnet-reportgenerator-globaltool

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
reportgenerator "-reports:test\Nut.Results.Test\coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html
