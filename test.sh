# dotnet tool install -g dotnet-reportgenerator-globaltool

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
~/.dotnet/tools/reportgenerator "-reports:test/Results.Test/coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html
