$ErrorActionPreference = "Stop"

$rootFolder = ".."
$sourceFolder = "$rootFolder\Source"
$buildFolder = "$rootFolder\build"
$packageFolder = "$rootFolder\package"

function cleanup_package_folder()
{
    if (Test-Path $packageFolder)
    {
        Remove-Item $packageFolder -Recurse -Force
    }
    New-Item $packageFolder -Type Directory | Out-Null
}

function zip($sourceDir, $outputFileName)
{
   Add-Type -Assembly System.IO.Compression.FileSystem | Out-Null
   
   $compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
   [System.IO.Compression.ZipFile]::CreateFromDirectory($sourceDir, $outputFileName, $compressionLevel, $false)
}

function pack_nuget()
{
    Write-Host "Preparing NuGet package..."
    
    $nuget = "$sourceFolder\.nuget\nuget.exe"
    &$nuget pack FasterTests.nuspec -BasePath $buildFolder -OutputDirectory $packageFolder -Verbosity quiet
}

function pack_zip()
{
    Write-Host "Preparing zip package..."
    
    zip $buildFolder "$packageFolder\latest.zip"
}

cleanup_package_folder
pack_nuget
pack_zip

Write-Host "Package succeeded"