# 发布说明

## 首次上传 GitHub

整理后的目录没有携带旧 `.git` 历史、日志、编译产物、反编译资料或编辑器配置。首次发布可以用 GitHub Desktop 的 **Add local repository / Create a repository** 选择项目，或在项目根目录运行：

```powershell
git init -b main
git add .
git diff --cached --stat
git commit -m "chore: prepare DisplayItemDurability for open source"
```

再在 GitHub 新建空仓库，按页面提示添加远程地址并推送。不要将整个游戏目录拖入仓库。

根目录的 MIT 许可适用于本项目代码，ModConfig API 的来源及 Unlicense 单独保留。版权行使用 `DisplayItemDurability contributors`；维护者可按实际署名信息调整。

## 生成安装包

```powershell
.\Build.ps1 -DuckovPath 'D:\Games\Steam\steamapps\common\Escape from Duckov'
```

将 `artifacts/DisplayItemDurability.zip` 作为 GitHub Release 附件。GitHub 自动生成的 **Source code** 下载用于开发；玩家安装应选择带本 MOD DLL 的安装包。安装包不携带旧部署记录和游戏 DLL。

发布说明中填写该次使用的游戏版本、变更和适用的 ModConfig API 信息。不要从旧日志或文件日期推断支持版本或性能数字。

## 手动回归项目

- [ ] 关闭 ModConfig 时，悬停耐久物品可显示详情，图标数字保持默认关闭。
- [ ] 开启 ModConfig 后，两个配置开关能够保存并即时应用。
- [ ] 武器、护甲、头盔显示百分比；其他适用物品显示数值；可堆叠物品不显示图标数字。
- [ ] 受损与满耐久可修复物品的预览、低耐久警告符合预期。
- [ ] 消耗、修复、切换装备和翻页后，检查数值及颜色更新。
- [ ] 进入场景、打开或关闭背包、战利品界面、储物箱后，检查显示刷新。
- [ ] 关闭再启用 MOD，检查是否有重复文本、重复回调或异常日志。
- [ ] 切换游戏语言，检查文本与配置描述。

## Steam Workshop 元数据

仓库中的 `mod/info.ini` 不预填 `publishedFileId`，便于其他开发者构建自己的副本。更新自己已有的 Workshop 条目时，应保留该条目的 ID；`Deploy.ps1` 默认保留目标目录已有 `info.ini`。只有确定要使用仓库元数据时才传 `-UpdateInfoIni`。

如需预览图，请添加自己有权使用的 `preview.png` 并放入最终 MOD 文件夹。目前的构建脚本只打包列出的源码产物与许可文件。
