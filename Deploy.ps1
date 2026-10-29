[CmdletBinding(SupportsShouldProcess = $true)]
param(
    [Parameter(Mandatory = $true)]
    [string]$GameModsPath,
    [string]$DuckovPath,
    [string]$DuckovManagedPath,
    [switch]$SkipBuild,
    [switch]$UpdateInfoIni,
    [switch]$SkipBackup
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

# Resolve relative arguments against the caller's directory, not the script directory.
$modsRoot = $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($GameModsPath)
$modDirectory = Join-Path $modsRoot 'DisplayItemDurability'
$packageDir = Join-Path $PSScriptRoot 'artifacts/DisplayItemDurability'
$packageDll = Join-Path $packageDir 'DisplayItemDurability.dll'
$packageInfo = Join-Path $packageDir 'info.ini'

if (-not $PSCmdlet.ShouldProcess($modDirectory, 'Build and deploy DisplayItemDurability (back up an existing installation)')) {
    return
}

if (-not $SkipBuild) {
    $buildParameters = @{}
    if ($DuckovPath) { $buildParameters.DuckovPath = $DuckovPath }
    if ($DuckovManagedPath) { $buildParameters.DuckovManagedPath = $DuckovManagedPath }
    & (Join-Path $PSScriptRoot 'Build.ps1') @buildParameters
}

foreach ($requiredFile in @($packageDll, $packageInfo)) {
    if (-not (Test-Path -LiteralPath $requiredFile -PathType Leaf)) {
        throw "Missing package file: $requiredFile. Run Build.ps1 first."
    }
}

if (Test-Path -LiteralPath $modDirectory -PathType Leaf) {
    throw "The target is a file, not a directory: $modDirectory"
}
if ((Test-Path -LiteralPath $modDirectory -PathType Container) -and -not $SkipBackup) {
    $backupRoot = Join-Path $PSScriptRoot 'artifacts/backups'
    New-Item -ItemType Directory -Path $backupRoot -Force | Out-Null
    $backupName = 'DisplayItemDurability_{0}_{1}' -f (Get-Date -Format 'yyyyMMdd_HHmmss'), ([Guid]::NewGuid().ToString('N').Substring(0, 8))
    $backupPath = Join-Path $backupRoot $backupName
    Copy-Item -LiteralPath $modDirectory -Destination $backupPath -Recurse
    Write-Host "Existing mod backed up to: $backupPath"
}

New-Item -ItemType Directory -Path $modDirectory -Force | Out-Null
Copy-Item -LiteralPath $packageDll -Destination (Join-Path $modDirectory 'DisplayItemDurability.dll') -Force
$targetInfo = Join-Path $modDirectory 'info.ini'
if ($UpdateInfoIni -or -not (Test-Path -LiteralPath $targetInfo)) {
    Copy-Item -LiteralPath $packageInfo -Destination $targetInfo -Force
}
else {
    Write-Host 'Preserving existing info.ini (use -UpdateInfoIni to replace it).'
}
Write-Host "Deployed to: $modDirectory"
