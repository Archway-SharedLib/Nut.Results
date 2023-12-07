$files = Get-ChildItem -Path "$PSScriptRoot/../src" -Filter *.tt -Recurse

foreach($file in $files)
{
    Push-Location -Path $file.Directory
    dotnet t4 $file.Name
    Pop-Location
}

$files = Get-ChildItem -Path "$PSScriptRoot/../test" -Filter *.tt -Recurse

foreach($file in $files)
{
    Push-Location -Path $file.Directory
    dotnet t4 $file.Name
    Pop-Location
}