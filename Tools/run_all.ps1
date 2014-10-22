param(
    [string]$ApiKey,
    [switch]$TestMode = $false
)

$ErrorActionPreference = "Stop"

.\build.ps1
.\package.ps1

$publishParams = "-ApiKey", $ApiKey
if ($TestMode)
{
    $publishParams += "-TestMode"
}
powershell -Command .\publish.ps1 -args $publishParams