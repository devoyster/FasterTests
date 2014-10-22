$ErrorActionPreference = "Stop"

$rootFolder = ".."
$sourceFolder = "$rootFolder\Source"
$buildFolder = "$rootFolder\build"

function imports()
{
    if ((Get-ExecutionPolicy) -ne 'Bypass')
    {
        Set-ExecutionPolicy Bypass
    }
    
    Import-Module ".\Invoke-MsBuild.psm1"
}

function cleanup_build_folder()
{
    if (Test-Path $buildFolder)
    {
        Remove-Item $buildFolder -Recurse -Force
    }
    New-Item $buildFolder -Type Directory | Out-Null
}

function build_for_anycpu()
{
    Write-Host "Building AnyCpu version..."

    $buildSucceeded = Invoke-MsBuild -Path "$sourceFolder\FasterTests.sln" -MsBuildParameters "/target:Clean;Build /property:Configuration=Release /verbosity:Quiet"

    if ($buildSucceeded)
    {
        Copy-Item "$sourceFolder\ConsoleRunner\bin\Release\*" $buildFolder -Include *.dll,FasterTests-Run.exe
    }
    
    $buildSucceeded
}

function build_for_x86()
{
    Write-Host "Building x86 version..."

    $buildSucceeded = Invoke-MsBuild -Path "$sourceFolder\ConsoleRunner\ConsoleRunner.csproj" -MsBuildParameters "/target:Build /property:Configuration=Release;Platform=x86 /verbosity:Quiet"

    if ($buildSucceeded)
    {
        Copy-Item "$sourceFolder\ConsoleRunner\bin\x86\Release\FasterTests-Run.exe" "$buildFolder\FasterTests-Run-x86.exe"
    }
    
    $buildSucceeded
}

function copy_content()
{
    Copy-Item "$rootFolder\license.txt" $buildFolder
}

imports
cleanup_build_folder
if (-not (build_for_anycpu))
{
    return
}
if (-not (build_for_x86))
{
    return
}
copy_content

Write-Host "Build succeeded"