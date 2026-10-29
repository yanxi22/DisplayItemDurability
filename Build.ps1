[CmdletBinding()]
param(
    [string]$DuckovPath,
    [string]$DuckovManagedPath,
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Release'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$projectPath = Join-Path $PSScriptRoot 'DisplayItemDurability.csproj'
$artifactRoot = Join-Path $PSScriptRoot 'artifacts'
$packageDir = Join-Path $artifactRoot 'DisplayItemDurability'
$archivePath = Join-Path $artifactRoot 'DisplayItemDurability.zip'
$buildArguments = @('build', $projectPath, '--configuration', $Configuration, '--nologo')
if ($DuckovPath) { $buildArguments += "-p:DuckovPath=$DuckovPath" }
if ($DuckovManagedPath) { $buildArguments += "-p:DuckovManagedPath=$DuckovManagedPath" }

Push-Location $PSScriptRoot
try {
    & dotnet @buildArguments
    if ($LASTEXITCODE -ne 0) { throw "Build failed (exit code $LASTEXITCODE)." }

    $dllPath = Join-Path $PSScriptRoot "bin/$Configuration/netstandard2.1/DisplayItemDurability.dll"
    if (-not (Test-Path -LiteralPath $dllPath -PathType Leaf)) {
        throw "Compiled mod DLL was not found: $dllPath"
    }

    New-Item -ItemType Directory -Path $packageDir -Force | Out-Null
    $licenseDir = Join-Path $packageDir 'licenses'
    New-Item -ItemType Directory -Path $licenseDir -Force | Out-Null
    Copy-Item -LiteralPath $dllPath -Destination (Join-Path $packageDir 'DisplayItemDurability.dll') -Force
    Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'mod/info.ini') -Destination (Join-Path $packageDir 'info.ini') -Force
    Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'LICENSE') -Destination $packageDir -Force
    Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'THIRD_PARTY_NOTICES.md') -Destination $packageDir -Force
    Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'licenses/ModConfig-Unlicense.txt') -Destination $licenseDir -Force

    # Use an explicit file list so old output or game DLLs cannot enter the ZIP.
    $packageFiles = @(
        'DisplayItemDurability.dll',
        'info.ini',
        'LICENSE',
        'THIRD_PARTY_NOTICES.md',
        'licenses/ModConfig-Unlicense.txt'
    )
    Add-Type -AssemblyName System.IO.Compression
    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $stream = [System.IO.File]::Open($archivePath, [System.IO.FileMode]::Create)
    try {
        $archive = [System.IO.Compression.ZipArchive]::new($stream, [System.IO.Compression.ZipArchiveMode]::Create)
        try {
            foreach ($relativeFile in $packageFiles) {
                $filePath = Join-Path $packageDir $relativeFile
                $entryName = "DisplayItemDurability/$relativeFile"
                [System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile(
                    $archive, $filePath, $entryName, [System.IO.Compression.CompressionLevel]::Optimal
                ) | Out-Null
            }
        }
        finally { $archive.Dispose() }
    }
    finally { $stream.Dispose() }

    Write-Host "Package directory: $packageDir"
    Write-Host "Installable ZIP: $archivePath"
}
finally { Pop-Location }
