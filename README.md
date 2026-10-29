# DisplayItemDurability

A mod for **Escape from Duckov** that adds durability details and repair previews to item tooltips, with optional durability numbers in the top-right corner of item icons.

English | [简体中文](README.zh-CN.md)

## Features

- Tooltips show current durability, the effective maximum and a percentage.
- Repair previews apply to non-stackable items tagged `Repairable`; weapon warnings appear when the predicted or current maximum falls below 50% of the original maximum.
- Optional icon overlays show a percentage for weapons, armor and helmets, or numeric values for other eligible non-stackable items.
- Colors reflect durability; zero durability uses red text with a strikethrough.
- ModConfig settings can toggle icon overlays and hide equipment overlays separately.
- Text is provided for Simplified/Traditional Chinese, English, Japanese, Korean, Russian, German, Portuguese, Spanish and French.

Icon overlays are **off by default**. Tooltips remain available without ModConfig. This mod displays information and does not change item durability.

## Install

1. Download an installation ZIP from this repository's Releases, or build it yourself.
2. Extract the `DisplayItemDurability` folder into `Duckov_Data/Mods/` under your game installation. On macOS, use `Duckov.app/Contents/Mods/`.
3. Enable the mod in the game's Mods menu.
4. For icon overlays, install and enable compatible [ModConfig](https://github.com/FrozenFish259/duckov_mod_config), enter a game scene, then enable **Show durability on item icons** in **Mod Settings**. The bundled API wrapper targets ModConfig API v1.

The mod folder must contain `DisplayItemDurability.dll` and `info.ini`. Close the game before replacing files.

## Build

Install a .NET SDK supporting `netstandard2.1` and provide the game's local Managed assemblies. Replace the example path with your own installation:

```powershell
.\Build.ps1 -DuckovPath 'D:\Games\Steam\steamapps\common\Escape from Duckov'
```

The script produces `artifacts/DisplayItemDurability/` and an installable `artifacts/DisplayItemDurability.zip`. To compile without packaging:

```powershell
dotnet build DisplayItemDurability.csproj -c Release '-p:DuckovPath=D:\Games\Steam\steamapps\common\Escape from Duckov'
```

Alternatively, copy `Duckov.local.props.example` to `Duckov.local.props` and edit it, set the `DUCKOV_PATH` environment variable, or pass `-DuckovManagedPath` to the build script for an explicit Managed directory. On macOS, `DuckovPath` is the parent directory containing `Duckov.app`.

Game assemblies are local build references and are excluded from the generated installation ZIP.

## Deploy locally

```powershell
.\Deploy.ps1 -GameModsPath 'D:\Games\Steam\steamapps\common\Escape from Duckov\Duckov_Data\Mods' -SkipBuild -WhatIf
.\Deploy.ps1 -GameModsPath 'D:\Games\Steam\steamapps\common\Escape from Duckov\Duckov_Data\Mods' -SkipBuild
```

Existing installations are backed up under `artifacts/backups/`. Existing `info.ini` files are preserved unless `-UpdateInfoIni` is supplied. Omit `-SkipBuild` to build first; supply a game path through the same options used by `Build.ps1`. The source metadata has no Workshop item ID.

## Development and contributions

Source lives in `src/`; metadata lives in `mod/`. See [development notes](docs/DEVELOPMENT.md) and [release instructions](docs/RELEASING.md), both in Chinese. Issues should include the game version, mod build, ModConfig version if applicable, and reproduction steps. Remove personal details from log excerpts.

## License

[MIT](LICENSE) for project code. The adapted ModConfig API wrapper retains its upstream Unlicense; see [third-party notices](THIRD_PARTY_NOTICES.md).
