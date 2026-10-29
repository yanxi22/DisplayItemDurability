# DisplayItemDurability · 显示物品耐久度

《逃离鸭科夫》（Escape from Duckov）MOD：在物品悬停详情中显示耐久度与修复预览，也可通过 ModConfig 开启物品图标右上角的耐久度数字。

中文 | [English](README.md)

## 功能

- **悬停详情**：显示当前耐久度、有效最大耐久度和百分比。
- **修复预览**：对带有 `Repairable` 标签的非堆叠物品，显示修复后的最大耐久度；已满耐久时显示当前最大耐久度。
- **低耐久提示**：武器修复后或当前最大耐久度低于原始上限的 50% 时，显示警告。
- **图标数字**：在右上角显示耐久度，附半透明背景；武器、护甲、头盔显示百分比，其他符合条件的非堆叠物品显示数值，耐久低于有效上限的 90% 时显示 `当前值/上限`。
- **颜色提示**：随耐久度比例变色，零耐久图标显示红色加删除线。
- **可选设置**：支持热切换图标显示、单独隐藏武器及装备的图标数字。
- **本地化**：提供简体中文、繁体中文、英语、日语、韩语、俄语、德语、葡萄牙语、西班牙语和法语文本。

图标数字默认关闭；悬停详情默认启用。MOD 以显示信息为主，不修改物品耐久度。

## 安装

需要《逃离鸭科夫》本体。若要调整图标显示选项，还需安装兼容的 [ModConfig](https://github.com/FrozenFish259/duckov_mod_config)（本项目封装使用 API v1）。不安装 ModConfig 时，保留悬停详情，图标数字保持默认关闭。

1. 从仓库的 Releases 下载安装包，或按下文自行构建。
2. 解压，将 `DisplayItemDurability` 文件夹放到游戏的 MOD 目录。
3. 启动游戏，在 Mods 菜单启用此 MOD。
4. 使用图标数字时，启用 ModConfig，进入游戏场景后打开设置中的 **Mod Settings**，找到本 MOD 并开启相应选项。

Windows 目录结构：

```text
Escape from Duckov/
└── Duckov_Data/
    └── Mods/
        └── DisplayItemDurability/
            ├── DisplayItemDurability.dll
            └── info.ini
```

macOS 的 MOD 目录为 `Duckov.app/Contents/Mods/`。安装包中的许可文件可以一并保留。更新 MOD 前请关闭游戏。

### 设置项

| 配置键 | 游戏内选项 | 默认值 |
| --- | --- | --- |
| `ShowIconDurability` | 在物品图标上显示耐久度 | 关闭 |
| `HideEquipmentIconDurability` | 仅隐藏武器装备图标上显示耐久度 | 关闭 |

第二项在图标显示开启时生效，影响代码按 `Weapon`、`Armor`、`Helmat` 标签识别的装备；不影响悬停详情。

## 构建

准备支持 `netstandard2.1` 的 .NET SDK，以及本机游戏的 `Managed` 程序集。以下示例使用 PowerShell；路径需替换为自己的游戏安装位置。

```powershell
# 编译并生成可安装 ZIP
.\Build.ps1 -DuckovPath 'D:\Games\Steam\steamapps\common\Escape from Duckov'
```

生成结果：

```text
bin/Release/netstandard2.1/DisplayItemDurability.dll
artifacts/DisplayItemDurability/
artifacts/DisplayItemDurability.zip
```

也可以只编译：

```powershell
dotnet build DisplayItemDurability.csproj -c Release '-p:DuckovPath=D:\Games\Steam\steamapps\common\Escape from Duckov'
```

若要在 IDE 中构建，将 `Duckov.local.props.example` 复制为 `Duckov.local.props`，在其中设置路径。此本机配置文件已加入 `.gitignore`。还支持环境变量 `DUCKOV_PATH` 和显式参数 `DuckovManagedPath`，详见 [开发说明](docs/DEVELOPMENT.md)。

游戏和 Unity 的 DLL 仅作为本机编译引用，不会被复制进新构建的输出或安装 ZIP。

## 本地部署

先构建安装包，再指定游戏的 Mods 目录：

```powershell
# 预览目标，不写入游戏目录
.\Deploy.ps1 -GameModsPath 'D:\Games\Steam\steamapps\common\Escape from Duckov\Duckov_Data\Mods' -SkipBuild -WhatIf

# 部署；已有 MOD 默认先备份，已有 info.ini 默认保留
.\Deploy.ps1 -GameModsPath 'D:\Games\Steam\steamapps\common\Escape from Duckov\Duckov_Data\Mods' -SkipBuild
```

省略 `-SkipBuild` 时，脚本会先执行 Release 构建，可同时传入 `-DuckovPath` 或 `-DuckovManagedPath`。用 `-UpdateInfoIni` 替换目标元数据；用 `-SkipBackup` 跳过备份。备份位于本项目的 `artifacts/backups/`。

源码内的 `mod/info.ini` 未绑定 Steam Workshop 物品 ID。部署默认保留已有 `info.ini`，便于维护自己已发布的条目。

## 源码结构

```text
src/ModBehaviour.cs           MOD 生命周期、文本、本地化和 UI 更新
src/ModConfigApi.cs           ModConfig 反射接口封装
mod/info.ini                 游戏加载元数据
Build.ps1                    构建并打包 MOD
Deploy.ps1                   本地部署与备份
Duckov.local.props.example   本机路径配置示例
docs/                        开发、发布与回归检查说明
licenses/                    第三方许可原文
```

显示更新主要由界面切换、翻页和物品耐久度事件触发，没有持续定时扫描循环；在没有活动 View 的部分路径中仍会查找场景 UI。实现细节见 [开发说明](docs/DEVELOPMENT.md)。

## 反馈与贡献

欢迎通过 Issues 报告问题、通过 Pull Requests 改进代码。请提供游戏版本、MOD 构建来源、ModConfig 版本（如使用）、复现步骤与相关日志片段。提交前移除日志中的个人路径或账号信息。

发布步骤与手动回归项目见 [发布说明](docs/RELEASING.md)。

## 许可

本项目代码采用 [MIT License](LICENSE)。`src/ModConfigApi.cs` 改编自 FrozenFish259 的 ModConfig API，保留其 Unlicense，详见 [第三方声明](THIRD_PARTY_NOTICES.md)。
