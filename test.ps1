# dotnet tool install -g dotnet-reportgenerator-globaltool

Param([switch]$noReport = $false)

dotnet test ./test/Nut.Results.Test/Nut.Results.Test.csproj `
    /p:CollectCoverage=true `
    /p:CoverletOutputFormat=cobertura `
    /p:CoverletOutput=../../nut.results.coverage.xml `
    /p:Exclude="[Nut.Results.FluentAssertions*]*" `
    /p:ExcludeByFile="**/*.g.cs"

dotnet test ./test/Nut.Results.FluentAssertions.Test/Nut.Results.FluentAssertions.Test.csproj `
    /p:CollectCoverage=true `
    /p:CoverletOutputFormat=cobertura `
    /p:CoverletOutput=../../nut.results.fluentassertions.coverage.xml `
    /p:Include="[Nut.Results.FluentAssertions*]*" `
    /p:ExcludeByFile="**/*.g.cs"

if(!$noReport) {
    dotnet tool run reportgenerator "-reports:./nut.results.coverage.xml;./nut.results.fluentassertions.coverage.xml" `
        -targetdir:coveragereport `
        -reporttypes:Html
}
