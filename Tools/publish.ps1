param(
    [string]$ApiKey,
    [switch]$TestMode = $false
)

$ErrorActionPreference = "Stop"

$rootFolder = ".."
$sourceFolder = "$rootFolder\Source"
$packageFolder = "$rootFolder\package"

$nuget = "$sourceFolder\.nuget\nuget.exe"

$params = "push", "$packageFolder\*.nupkg", "-ApiKey", $ApiKey
if ($TestMode)
{
    $params += "-Source", "https://staging.nuget.org/"
}

&$nuget $params