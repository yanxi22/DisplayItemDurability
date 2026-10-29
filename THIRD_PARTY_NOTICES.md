# 第三方来源与许可

## ModConfig API 封装

- 文件：`src/ModConfigApi.cs`。
- 来源：[FrozenFish259/duckov_mod_config](https://github.com/FrozenFish259/duckov_mod_config)，原文件为 `Api/ModConfigApi.cs`。
- 本项目使用其改编版本，包括本 MOD 的命名空间、反射方法缓存及初始化失败状态等调整。
- 上游许可：Unlicense。完整文本保留于 [licenses/ModConfig-Unlicense.txt](licenses/ModConfig-Unlicense.txt)。
- 以上来源及许可依据整理时的本地上游副本；不将上游 API 封装重新声明为本项目原创代码。

## 游戏与 Unity 程序集

《逃离鸭科夫》、Unity 及其第三方程序集由各自权利人提供。本仓库仅在本机编译时引用这些文件，不分发游戏 DLL、游戏资源或反编译源码。本项目的 MIT 许可不适用于这些外部文件。

ModConfig 本体为可选运行依赖，不包含在本仓库或本项目生成的发布包中。
