$files = Get-ChildItem -Path ../src,../test -Filter *.tt -Recurse
foreach($file in $files)
{
    Push-Location -Path $file.Directory
    dotnet t4 $file.Name
    Pop-Location
}
