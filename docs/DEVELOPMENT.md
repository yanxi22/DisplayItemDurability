# 开发说明

## 工程与依赖

- 工程目标框架：`netstandard2.1`。
- 只编译 `src/**/*.cs`，不将临时文件、示例代码或本地参考资料纳入编译。
- 游戏入口：`DisplayItemDurability.ModBehaviour`，继承 `Duckov.Modding.ModBehaviour`。
- `mod/info.ini` 的 `name`、程序集名称、入口命名空间须一致。
- 本地编译引用 `Assembly-CSharp.dll`、`ItemStatsSystem.dll`、`TeamSoda.*.dll`、`Unity*.dll`；引用设置 `Private=false`，不复制游戏程序集。
- ModConfig 通过反射接入，不需要引用其 DLL。封装检查的 API 版本为整数 `1`。

## 本机路径配置

任选一种方式：

1. 命令行传入 `-p:DuckovPath=...`，或构建脚本参数 `-DuckovPath`。
2. 设置环境变量 `DUCKOV_PATH`。
3. 将根目录 `Duckov.local.props.example` 复制为 `Duckov.local.props`，在其中填写路径。

Windows 的 `DuckovPath` 为包含游戏可执行文件的目录，自动寻找其 `Duckov_Data/Managed/`；macOS 为包含 `Duckov.app` 的父目录，自动寻找 `Duckov.app/Contents/Resources/Data/Managed/`。

也可直接指定程序集目录：

```powershell
.\Build.ps1 -DuckovManagedPath 'D:\Games\Duckov\Duckov_Data\Managed'
dotnet build -c Release '-p:DuckovManagedPath=D:\Games\Duckov\Duckov_Data\Managed'
```

命令行 MSBuild 属性优先于本地配置。本地配置可覆盖从环境变量取得的路径。不设置任何路径，或缺少关键程序集时，工程会显示明确的错误说明。

请从自己的游戏安装取得依赖。仓库不包含游戏 DLL、反编译源码或 ModConfig 本体。若游戏更新导致接口变化，应使用对应版本的程序集重新构建并回归相关功能。

## 显示规则

有效最大耐久度在 `DurabilityLoss > 0` 时为 `MaxDurability × (1 - DurabilityLoss)`，否则使用 `MaxDurability`。

图标显示要求总开关开启、物品满足耐久条件且不是可堆叠物品。装备识别沿用游戏标签 `Weapon`、`Armor`、`Helmat`（最后一项拼写与当前代码一致）。

| 图标耐久比例 | 颜色 |
| --- | --- |
| > 80% | 白色 |
| > 60%，≤ 80% | 淡绿色 |
| > 40%，≤ 60% | 黄色 |
| > 20%，≤ 40% | 橙色 |
| ≤ 20% | 红色 |

零耐久使用纯红色、加粗与删除线。悬停详情另用 75%、50%、25% 三个阈值划分绿色、黄色、橙色、红色，不与图标颜色规则混同。

修复预览要求物品使用耐久度、含 `Repairable` 标签且不可堆叠。修复损耗比例来自 `GetRepairLossRatio()`，预览不执行实际修复。

## 更新与缓存

- 在 `OnEnable` 订阅物品悬停、活动 View、场景加载及翻页事件。
- 背包、战利品和储物箱 View 打开后，等待 UI 初始化再创建或刷新数字。
- 通过 `Item.onDurabilityChanged` 更新已缓存的物品显示。
- 配置变更后重新加载选项并刷新显示。
- 正常路径优先扫描活动 View 的子节点；没有活动 View 时，部分方法回退到场景查找。
- 缓存管理使用 500 个显示项作为清理阈值；清理和事件取消订阅由现有生命周期逻辑处理。

主要实现集中在 `src/ModBehaviour.cs`，本地化文本也在该文件中。此次仓库整理保留原有运行源码。

## 构建与发布边界

`Build.ps1` 仅将以下文件加入安装 ZIP：

```text
DisplayItemDurability/DisplayItemDurability.dll
DisplayItemDurability/info.ini
DisplayItemDurability/LICENSE
DisplayItemDurability/THIRD_PARTY_NOTICES.md
DisplayItemDurability/licenses/ModConfig-Unlicense.txt
```

`bin/`、`obj/`、`artifacts/`、本机配置和日志均被忽略。GitHub 公共构建环境默认没有游戏程序集；若配置 CI，应由维护者提供私有构建环境或依赖路径，不把游戏 DLL 提交到公共仓库。
