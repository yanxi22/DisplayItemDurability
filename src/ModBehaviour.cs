using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Duckov.UI;
using ItemStatsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DisplayItemDurability
{
    /// <summary>
    /// 本地化文本管理（通过反射获取游戏内语言设置）
    /// </summary>
    public static class LocalizedText
    {
        // 通过反射获取游戏的语言设置
        private static SystemLanguage? _cachedLanguage = null;
        private static bool _reflectionInitialized = false;
        private static System.Reflection.PropertyInfo? _currentLanguageProperty = null;

        private static SystemLanguage CurrentLanguage
        {
            get
            {
                if (_cachedLanguage.HasValue)
                    return _cachedLanguage.Value;

                if (!_reflectionInitialized)
                {
                    InitializeReflection();
                    _reflectionInitialized = true;
                }

                if (_currentLanguageProperty != null)
                {
                    try
                    {
                        var language = (SystemLanguage)_currentLanguageProperty.GetValue(null);
                        _cachedLanguage = language;
                        return language;
                    }
                    catch
                    {
                        // 获取失败，回退到系统语言
                    }
                }

                // 回退到系统语言
                return Application.systemLanguage;
            }
        }

        private static void InitializeReflection()
        {
            try
            {
                // 查找包含 LocalizationManager 的程序集
                var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

                foreach (var assembly in assemblies)
                {
                    try
                    {
                        var type = assembly.GetType("SodaCraft.Localizations.LocalizationManager");
                        if (type != null)
                        {
                            _currentLanguageProperty = type.GetProperty("CurrentLanguage",
                                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

                            if (_currentLanguageProperty != null)
                            {
                                return;
                            }
                        }
                    }
                    catch
                    {
                        // 忽略加载失败的程序集
                        continue;
                    }
                }
            }
            catch
            {
                // 反射初始化失败，将使用系统语言
            }
        }

        // 清除缓存，在语言切换时调用
        public static void ClearLanguageCache()
        {
            _cachedLanguage = null;
        }

        public static string GetDurabilityLabel()
        {
            switch (CurrentLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    return "耐久度";
                case SystemLanguage.ChineseTraditional:
                    return "耐久度";
                case SystemLanguage.Japanese:
                    return "耐久性";
                case SystemLanguage.Korean:
                    return "내구도";
                case SystemLanguage.Russian:
                    return "Прочность";
                case SystemLanguage.German:
                    return "Haltbarkeit";
                case SystemLanguage.Portuguese:
                    return "Durabilidade";
                case SystemLanguage.Spanish:
                    return "Durabilidad";
                case SystemLanguage.French:
                    return "Durabilité";
                default:
                    return "Durability";
            }
        }

        public static string GetAfterRepairLabel()
        {
            switch (CurrentLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    return "修复后";
                case SystemLanguage.ChineseTraditional:
                    return "修復後";
                case SystemLanguage.Japanese:
                    return "修理後";
                case SystemLanguage.Korean:
                    return "수리 후";
                case SystemLanguage.Russian:
                    return "После ремонта";
                case SystemLanguage.German:
                    return "Nach Reparatur";
                case SystemLanguage.Portuguese:
                    return "Após Reparo";
                case SystemLanguage.Spanish:
                    return "Después de Reparar";
                case SystemLanguage.French:
                    return "Après Réparation";
                default:
                    return "After Repair";
            }
        }

        public static string GetCurrentMaxLabel()
        {
            switch (CurrentLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    return "当前最大耐久";
                case SystemLanguage.ChineseTraditional:
                    return "當前最大耐久";
                case SystemLanguage.Japanese:
                    return "現在の最大耐久性";
                case SystemLanguage.Korean:
                    return "현재 최대 내구도";
                case SystemLanguage.Russian:
                    return "Текущая макс. прочность";
                case SystemLanguage.German:
                    return "Aktuelle Max. Haltbarkeit";
                case SystemLanguage.Portuguese:
                    return "Durabilidade Máxima Atual";
                case SystemLanguage.Spanish:
                    return "Durabilidad Máxima Actual";
                case SystemLanguage.French:
                    return "Durabilité Max Actuelle";
                default:
                    return "Current Max Durability";
            }
        }

        public static string GetLowDurabilityWarning()
        {
            switch (CurrentLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    return "⚠ 耐久低于50%";
                case SystemLanguage.ChineseTraditional:
                    return "⚠ 耐久低於50%";
                case SystemLanguage.Japanese:
                    return "⚠ 耐久性50%未満";
                case SystemLanguage.Korean:
                    return "⚠ 내구도 50% 미만";
                case SystemLanguage.Russian:
                    return "⚠ Прочность < 50%";
                case SystemLanguage.German:
                    return "⚠ Haltbarkeit < 50%";
                case SystemLanguage.Portuguese:
                    return "⚠ Durabilidade < 50%";
                case SystemLanguage.Spanish:
                    return "⚠ Durabilidad < 50%";
                case SystemLanguage.French:
                    return "⚠ Durabilité < 50%";
                default:
                    return "⚠ Durability < 50%";
            }
        }

        public static string GetAfterRepairWarning()
        {
            switch (CurrentLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    return "⚠ 修复后耐久低于50%";
                case SystemLanguage.ChineseTraditional:
                    return "⚠ 修復後耐久低於50%";
                case SystemLanguage.Japanese:
                    return "⚠ 修理後耐久性50%未満";
                case SystemLanguage.Korean:
                    return "⚠ 수리 후 내구도 50% 미만";
                case SystemLanguage.Russian:
                    return "⚠ Прочность после ремонта < 50%";
                case SystemLanguage.German:
                    return "⚠ Haltbarkeit nach Reparatur < 50%";
                case SystemLanguage.Portuguese:
                    return "⚠ Durabilidade após reparo < 50%";
                case SystemLanguage.Spanish:
                    return "⚠ Durabilidad después de reparar < 50%";
                case SystemLanguage.French:
                    return "⚠ Durabilité après réparation < 50%";
                default:
                    return "⚠ Durability after repair < 50%";
            }
        }

        /// <summary>
        /// 获取配置项描述：显示物品图标耐久度
        /// </summary>
        public static string GetConfigShowIconDurabilityDescription()
        {
            switch (CurrentLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    return "在物品图标上显示耐久度";
                case SystemLanguage.ChineseTraditional:
                    return "在物品圖標上顯示耐久度";
                case SystemLanguage.Japanese:
                    return "アイテムアイコンに耐久度を表示";
                case SystemLanguage.Korean:
                    return "아이템 아이콘에 내구도 표시";
                case SystemLanguage.Russian:
                    return "Показывать прочность на иконках предметов";
                case SystemLanguage.German:
                    return "Haltbarkeit auf Gegenstandssymbolen anzeigen";
                case SystemLanguage.Portuguese:
                    return "Mostrar durabilidade nos ícones de itens";
                case SystemLanguage.Spanish:
                    return "Mostrar durabilidad en iconos de objetos";
                case SystemLanguage.French:
                    return "Afficher la durabilité sur les icônes d'objets";
                default:
                    return "Show durability on item icons";
            }
        }

        /// <summary>
        /// 获取配置项描述：仅隐藏武器装备图标耐久度
        /// </summary>
        public static string GetConfigHideEquipmentDescription()
        {
            switch (CurrentLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    return "仅隐藏武器装备图标上显示耐久度";
                case SystemLanguage.ChineseTraditional:
                    return "僅隱藏武器裝備圖標上顯示耐久度";
                case SystemLanguage.Japanese:
                    return "武器装備アイコンの耐久度のみ非表示";
                case SystemLanguage.Korean:
                    return "무기 장비 아이콘의 내구도만 숨기기";
                case SystemLanguage.Russian:
                    return "Скрыть прочность только для оружия и снаряжения";
                case SystemLanguage.German:
                    return "Nur Haltbarkeit von Waffen und Ausrüstung ausblenden";
                case SystemLanguage.Portuguese:
                    return "Ocultar durabilidade apenas de armas e equipamentos";
                case SystemLanguage.Spanish:
                    return "Ocultar durabilidad solo de armas y equipos";
                case SystemLanguage.French:
                    return "Masquer la durabilité uniquement des armes et équipements";
                default:
                    return "Hide equipment durability on icons only";
            }
        }
    }

    /// <summary>
    /// 在物品图标上显示耐久度数字
    /// 同时在悬停详情界面也显示详细的耐久度信息
    /// </summary>
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        // ModConfig 配置相关
        private const string MOD_NAME = "DisplayItemDurability";
        private const string CONFIG_KEY_SHOW_ICON_DURABILITY = "ShowIconDurability";
        private const string CONFIG_KEY_HIDE_EQUIPMENT_DURABILITY = "HideEquipmentIconDurability";
        private bool showIconDurability = false; // 默认关闭
        private bool hideEquipmentIconDurability = false; // 默认不隐藏装备

        // 用于悬停详情界面的文本
        private TextMeshProUGUI? _hoverText = null;

        // 字典，存储每个ItemDisplay对应的耐久度文本
        private Dictionary<ItemDisplay, TextMeshProUGUI> durabilityTexts = new Dictionary<ItemDisplay, TextMeshProUGUI>();

        private const int MAX_DURABILITY_DISPLAYS = 500;

        private Dictionary<ItemDisplay, float> displayLastAccessTime = new Dictionary<ItemDisplay, float>();

        private ItemHoveringUI? _currentHoverUI = null;
        private Item? _currentHoverItem = null;

        // 翻页事件订阅跟踪
        private List<InventoryDisplay> subscribedInventoryDisplays = new List<InventoryDisplay>();

        // 语言切换事件委托（用于取消订阅）
        private System.Delegate? _languageChangedDelegate = null;

        // 协程去重引用
        private Coroutine? _delayedUpdateUICoroutine = null;
        private Coroutine? _delayedPageRefreshCoroutine = null;

        void Awake()
        {
            Debug.Log($"[{MOD_NAME}] Awake() 执行");

            // 订阅语言切换事件（通过反射）
            try
            {
                var assemblyCSharp = System.Reflection.Assembly.Load("Assembly-CSharp");
                var localizationManagerType = assemblyCSharp.GetType("SodaCraft.Localizations.LocalizationManager");

                if (localizationManagerType != null)
                {
                    var onSetLanguageEvent = localizationManagerType.GetEvent("OnSetLanguage",
                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

                    if (onSetLanguageEvent != null)
                    {
                        // 创建事件处理委托
                        _languageChangedDelegate = System.Delegate.CreateDelegate(
                            onSetLanguageEvent.EventHandlerType,
                            this,
                            GetType().GetMethod("OnLanguageChanged",
                                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        );

                        onSetLanguageEvent.AddEventHandler(null, _languageChangedDelegate);
                    }
                }
            }
            catch
            {
                // 语言切换事件订阅失败，将在重启游戏时生效
            }
        }

        // 语言切换时的回调
        private void OnLanguageChanged(SystemLanguage language)
        {
            LocalizedText.ClearLanguageCache();

            // 如果当前有悬停的物品，重新刷新显示
            if (_currentHoverUI != null && _currentHoverItem != null)
            {
                OnSetupItemHoveringUI(_currentHoverUI, _currentHoverItem);
            }
        }

        void Start()
        {
            Debug.Log($"[{MOD_NAME}] Start() 执行");
        }

        /// <summary>
        /// 延迟初始创建
        /// </summary>
        private System.Collections.IEnumerator DelayedInitialCreate()
        {
            yield return new WaitForSeconds(1f);
            Debug.Log($"[{MOD_NAME}] 延迟初始创建耐久度显示...");
            ForceCreateAllDurabilityDisplays();
        }

        void OnEnable()
        {
            Debug.Log($"[{MOD_NAME}] OnEnable() 执行");

            // 订阅悬停详情UI事件（在详情界面显示）
            ItemHoveringUI.onSetupItem += OnSetupItemHoveringUI;
            ItemHoveringUI.onSetupMeta += OnSetupMeta;

            // 订阅UI打开/关闭事件
            View.OnActiveViewChanged += OnViewChanged;

            // 订阅场景加载事件
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;

            // 不再启动任何定时循环！完全事件驱动

            // 订阅翻页事件
            SubscribeToPageEvents();

            // 延迟初始化 ModConfig（给 ModConfig 一些时间完成初始化）
            StartCoroutine(DelayedInitializeModConfig());
        }

        private System.Collections.IEnumerator DelayedInitializeModConfig()
        {
            // 等待 1 秒让 ModConfig 完成初始化
            yield return new WaitForSeconds(1f);

            Debug.Log($"[{MOD_NAME}] 开始延迟初始化 ModConfig...");
            InitializeModConfig();
        }

        void OnDisable()
        {
            // 不再需要取消任何定时调用，因为我们没有使用InvokeRepeating

            // 取消订阅翻页事件
            UnsubscribeFromPageEvents();

            // 取消订阅UI事件
            ItemHoveringUI.onSetupItem -= OnSetupItemHoveringUI;
            ItemHoveringUI.onSetupMeta -= OnSetupMeta;
            View.OnActiveViewChanged -= OnViewChanged;

            // 取消场景加载事件订阅
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;

            // 取消ModConfig配置变更事件订阅
            ModConfigAPI.SafeRemoveOnOptionsChangedDelegate(OnConfigChanged);

            // 取消所有耐久度变化事件订阅并清理UI
            foreach (var kvp in durabilityTexts)
            {
                if (kvp.Key != null && kvp.Key.Target != null)
                {
                    kvp.Key.Target.onDurabilityChanged -= OnItemDurabilityChanged;
                }

                if (kvp.Value != null)
                    Destroy(kvp.Value.transform.parent.gameObject);
            }
            durabilityTexts.Clear();
        }

        /// <summary>
        /// 场景加载完成回调
        /// </summary>
        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            Debug.Log($"[{MOD_NAME}] 场景加载完成: {scene.name} (模式: {mode})");

            // 延迟扫描，确保UI完全初始化
            StartCoroutine(DelayedScanAfterSceneLoad(2f));
        }

        /// <summary>
        /// 场景加载后延迟扫描
        /// </summary>
        private System.Collections.IEnumerator DelayedScanAfterSceneLoad(float delay)
        {
            yield return new WaitForSeconds(delay);

            Debug.Log($"[{MOD_NAME}] 场景加载后刷新缓存... showIconDurability={showIconDurability}");
            if (showIconDurability)
            {
                UpdateUIFromCache();
            }
        }

        /// <summary>
        /// UI视图变化回调
        /// 优化后：UI打开时延迟刷新并更新显示
        /// </summary>
        private void OnViewChanged()
        {
            if (View.ActiveView != null)
            {
                string viewName = View.ActiveView.GetType().Name;
                bool shouldUpdate = IsItemRelatedUI(viewName);

                if (shouldUpdate)
                {
                    Debug.Log($"[{MOD_NAME}] UI打开: {viewName}，准备更新显示");
                    if (_delayedUpdateUICoroutine != null)
                        StopCoroutine(_delayedUpdateUICoroutine);
                    _delayedUpdateUICoroutine = StartCoroutine(DelayedUpdateUI(viewName));
                }
            }
            else
            {
                Debug.Log($"[{MOD_NAME}] UI关闭");
            }
        }

        /// <summary>
        /// 延迟更新UI（等待UI完全初始化）
        /// </summary>
        private System.Collections.IEnumerator DelayedUpdateUI(string viewName)
        {
            yield return null;
            yield return null;

            RefreshPageEventSubscriptions();
            ForceCreateAllDurabilityDisplays();

            _delayedUpdateUICoroutine = null;
        }

        /// <summary>
        /// 判断是否为包含物品的UI
        /// </summary>
        private bool IsItemRelatedUI(string viewName)
        {
            // 需要扫描的UI类型（包含物品显示的UI）
            string[] itemRelatedViews = {
                "LootView",          // 战利品界面
                "InventoryView",     // 背包界面
                "StorageView",       // 储物箱界面
            };

            return System.Array.Exists(itemRelatedViews, view => viewName.Contains(view));
        }

        #region ModConfig Integration

        /// <summary>
        /// 初始化 ModConfig
        /// </summary>
        private void InitializeModConfig()
        {
            // 尝试初始化 ModConfig
            bool modConfigAvailable = ModConfigAPI.Initialize();

            if (modConfigAvailable)
            {
                Debug.Log($"[{MOD_NAME}] ModConfig 可用，正在注册配置项...");
                RegisterModConfig();
                LoadConfig();
            }
            else
            {
                Debug.Log($"[{MOD_NAME}] ModConfig 未安装或不可用，使用默认配置。推荐安装 ModConfig 以在游戏内调整设置。");
                // 使用默认值
                showIconDurability = false;
            }
        }

        /// <summary>
        /// 注册 ModConfig 配置项
        /// </summary>
        private void RegisterModConfig()
        {
            // 添加布尔开关：是否显示物品图标耐久度
            string description1 = LocalizedText.GetConfigShowIconDurabilityDescription();
            bool success1 = ModConfigAPI.SafeAddBoolDropdownList(
                MOD_NAME,
                CONFIG_KEY_SHOW_ICON_DURABILITY,
                description1,
                defaultValue: false
            );

            // 添加布尔开关：是否仅隐藏武器装备图标耐久度
            string description2 = LocalizedText.GetConfigHideEquipmentDescription();
            bool success2 = ModConfigAPI.SafeAddBoolDropdownList(
                MOD_NAME,
                CONFIG_KEY_HIDE_EQUIPMENT_DURABILITY,
                description2,
                defaultValue: false
            );

            if (success1 && success2)
            {
                Debug.Log($"[{MOD_NAME}] 成功注册所有配置项");
                // 订阅配置变更事件
                ModConfigAPI.SafeAddOnOptionsChangedDelegate(OnConfigChanged);
            }
            else
            {
                Debug.LogWarning($"[{MOD_NAME}] 注册配置项失败");
            }
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        private void LoadConfig()
        {
            showIconDurability = ModConfigAPI.SafeLoad(MOD_NAME, CONFIG_KEY_SHOW_ICON_DURABILITY, defaultValue: false);
            hideEquipmentIconDurability = ModConfigAPI.SafeLoad(MOD_NAME, CONFIG_KEY_HIDE_EQUIPMENT_DURABILITY, defaultValue: false);
            Debug.Log($"[{MOD_NAME}] 加载配置: 显示物品图标耐久度 = {showIconDurability}, 仅隐藏武器装备 = {hideEquipmentIconDurability}");

            // 配置加载完成后，强制创建所有显示
            Debug.Log($"[{MOD_NAME}] 配置加载完成，强制创建显示...");
            ForceCreateAllDurabilityDisplays();
        }

        /// <summary>
        /// 配置变更回调（支持游戏内热加载）
        /// </summary>
        private void OnConfigChanged(string key)
        {
            // 检查是否是我们的配置项（ModConfig 传递的 key 带有 modName 前缀）
            string prefixedKeyShowIcon = $"{MOD_NAME}_{CONFIG_KEY_SHOW_ICON_DURABILITY}";
            string prefixedKeyHideEquipment = $"{MOD_NAME}_{CONFIG_KEY_HIDE_EQUIPMENT_DURABILITY}";

            if (key == prefixedKeyShowIcon || key == prefixedKeyHideEquipment)
            {
                // 重新加载配置
                LoadConfig();

                // 立即更新所有物品图标的耐久度显示（应用新配置）
                RefreshAllDurabilityDisplays();

                Debug.Log($"[{MOD_NAME}] 配置已更新并应用");
            }
        }

        /// <summary>
        /// 刷新所有物品图标的耐久度显示（热加载支持）
        /// </summary>
        private void RefreshAllDurabilityDisplays()
        {
            foreach (var kvp in durabilityTexts)
            {
                if (kvp.Key != null && kvp.Value != null && kvp.Key.Target != null)
                {
                    if (ShouldShowDurabilityOnIcon(kvp.Key.Target))
                    {
                        kvp.Value.transform.parent.gameObject.SetActive(true);
                        UpdateDurabilityText(kvp.Key);
                    }
                    else
                    {
                        kvp.Value.transform.parent.gameObject.SetActive(false);
                    }
                }
            }
        }

        #endregion

        void OnDestroy()
        {
            // 取消语言切换事件订阅
            if (_languageChangedDelegate != null)
            {
                try
                {
                    var assemblyCSharp = System.Reflection.Assembly.Load("Assembly-CSharp");
                    var locType = assemblyCSharp.GetType("SodaCraft.Localizations.LocalizationManager");
                    if (locType != null)
                    {
                        var evt = locType.GetEvent("OnSetLanguage",
                            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                        if (evt != null)
                            evt.RemoveEventHandler(null, _languageChangedDelegate);
                    }
                }
                catch { }
            }

            if (_hoverText != null)
                Destroy(_hoverText.gameObject);

            // 取消所有耐久度变化事件订阅并清理UI
            foreach (var kvp in durabilityTexts)
            {
                if (kvp.Key != null && kvp.Key.Target != null)
                {
                    kvp.Key.Target.onDurabilityChanged -= OnItemDurabilityChanged;
                }

                if (kvp.Value != null)
                    Destroy(kvp.Value.transform.parent.gameObject);
            }
            durabilityTexts.Clear();
        }

        /// <summary>
        /// 在悬停详情界面显示耐久度详细信息
        /// </summary>
        private void OnSetupItemHoveringUI(ItemHoveringUI uiInstance, Item item)
        {
            if (item == null)
            {
                _currentHoverUI = null;
                _currentHoverItem = null;
                if (_hoverText != null)
                    _hoverText.gameObject.SetActive(false);
                return;
            }

            // 检查物品是否使用耐久度
            if (!item.UseDurability)
            {
                _currentHoverUI = null;
                _currentHoverItem = null;
                if (_hoverText != null)
                    _hoverText.gameObject.SetActive(false);
                return;
            }

            // 保存当前悬停的UI和物品，用于语言切换时刷新
            _currentHoverUI = uiInstance;
            _currentHoverItem = item;

            // 创建或获取文本
            if (_hoverText == null)
            {
                // 创建一个新的文本对象
                GameObject textObj = new GameObject("DurabilityHoverText");
                _hoverText = textObj.AddComponent<TextMeshProUGUI>();
                _hoverText.fontSize = 14;
                _hoverText.fontStyle = FontStyles.Bold;
            }

            _hoverText.gameObject.SetActive(true);
            _hoverText.transform.SetParent(uiInstance.LayoutParent);
            _hoverText.transform.localScale = Vector3.one;

            // 计算耐久度信息
            float current = item.Durability;
            float max = GetEffectiveMaxDurability(item);
            float percent = (current / max) * 100f;

            // 根据百分比设置颜色
            Color durabilityColor;
            if (percent > 75f)
                durabilityColor = new Color(0.2f, 1f, 0.2f); // 绿色
            else if (percent > 50f)
                durabilityColor = new Color(1f, 1f, 0.2f); // 黄色
            else if (percent > 25f)
                durabilityColor = new Color(1f, 0.6f, 0.2f); // 橙色
            else
                durabilityColor = new Color(1f, 0.2f, 0.2f); // 红色

            _hoverText.color = durabilityColor;

            // 构建基本耐久度信息（使用本地化文本）
            string displayText = $"{LocalizedText.GetDurabilityLabel()}: {current:F0}/{max:F0} ({percent:F1}%)";

            // 添加修复预览信息（如果可修复且不是食物类）
            if (ShouldShowRepairPreview(item))
            {
                float maxDurabilityWithLoss = item.MaxDurability * (1f - item.DurabilityLoss);
                bool needsRepair = item.Durability < maxDurabilityWithLoss;

                if (needsRepair)
                {
                    // 需要修复：显示修复后的耐久度
                    float afterRepairMaxDur = CalculateAfterRepairMaxDurability(item);
                    displayText += $"\n<color=#AAAAAA>{LocalizedText.GetAfterRepairLabel()}: {afterRepairMaxDur:F0}</color>";

                    // 如果是枪械且修复后耐久度百分比低于50%，显示警告
                    float afterRepairPercent = (afterRepairMaxDur / item.MaxDurability) * 100f;
                    if (IsGun(item) && afterRepairPercent < 50f)
                    {
                        displayText += $" <color=#FF4444>{LocalizedText.GetAfterRepairWarning()}</color>";
                    }
                }
                else
                {
                    // 已经是满耐久：显示当前最大耐久度
                    displayText += $"\n<color=#AAAAAA>{LocalizedText.GetCurrentMaxLabel()}: {maxDurabilityWithLoss:F0}</color>";

                    // 如果是枪械且当前最大耐久度百分比低于50%，显示警告
                    float currentMaxPercent = (maxDurabilityWithLoss / item.MaxDurability) * 100f;
                    if (IsGun(item) && currentMaxPercent < 50f)
                    {
                        displayText += $" <color=#FF4444>{LocalizedText.GetLowDurabilityWarning()}</color>";
                    }
                }
            }

            _hoverText.text = displayText;
            _hoverText.fontSize = 18f;
        }

        /// <summary>
        /// 判断是否应该显示修复预览
        /// </summary>
        private bool ShouldShowRepairPreview(Item item)
        {
            // 必须使用耐久度
            if (!item.UseDurability)
                return false;

            // 必须可修复
            if (!item.Tags.Contains("Repairable"))
                return false;

            // 排除堆叠物品（食物等消耗品）
            if (item.Stackable)
                return false;

            // 始终显示修复预览，让玩家知道修复后的状态
            // 即使当前已经是"满耐久"也显示
            return true;
        }

        /// <summary>
        /// 计算修复后的最大耐久度
        /// </summary>
        private float CalculateAfterRepairMaxDurability(Item item)
        {
            float maxDurability = item.MaxDurability;
            float durabilityLoss = item.DurabilityLoss;
            float maxDurabilityWithLoss = maxDurability * (1f - durabilityLoss);
            float currentDurability = item.Durability;
            float repairAmount = maxDurabilityWithLoss - currentDurability;

            float repairLossRatio = item.GetRepairLossRatio();
            float lostAmount = repairAmount * repairLossRatio;
            float lostPercentage = lostAmount / maxDurability;

            // 修复后的最大耐久度
            float afterRepairMaxDur = maxDurability * (1f - durabilityLoss - lostPercentage);

            return afterRepairMaxDur;
        }

        /// <summary>
        /// 判断是否为枪械类物品
        /// </summary>
        private bool IsGun(Item item)
        {
            // 通过标签名称字符串判断
            return item.Tags.Contains("Gun") || item.Tags.Contains("Weapon");
        }

        private void OnSetupMeta(ItemHoveringUI uI, ItemMetaData data)
        {
            if (_hoverText != null)
                _hoverText.gameObject.SetActive(false);
        }


        /// <summary>
        /// 创建耐久度文本UI（带半透明背景）
        /// </summary>
        private void CreateDurabilityText(ItemDisplay display)
        {
            Debug.Log($"[{MOD_NAME}] CreateDurabilityText() 为物品 {display.Target?.DisplayName} 创建耐久度显示");

            // 创建容器对象
            GameObject container = new GameObject("DurabilityContainer");
            container.transform.SetParent(display.transform, false);

            // 设置容器的RectTransform（右上角）
            RectTransform containerRect = container.AddComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(1, 1);
            containerRect.anchorMax = new Vector2(1, 1);
            containerRect.pivot = new Vector2(1, 1);
            containerRect.anchoredPosition = new Vector2(-2, -2);
            containerRect.sizeDelta = new Vector2(30, 16); // 背景大小

            // 添加半透明灰色背景
            UnityEngine.UI.Image background = container.AddComponent<UnityEngine.UI.Image>();
            background.color = new Color(0.1f, 0.1f, 0.1f, 0.7f); // 深灰色，70%不透明

            // 创建文本（作为容器的子对象）
            GameObject textObj = new GameObject("DurabilityText");
            textObj.transform.SetParent(container.transform, false);
            var text = textObj.AddComponent<TextMeshProUGUI>();

            // 设置文本的RectTransform（填充整个容器）
            RectTransform textRect = text.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.pivot = new Vector2(0.5f, 0.5f);
            textRect.anchoredPosition = Vector2.zero;
            textRect.sizeDelta = Vector2.zero; // 填充父对象

            // 设置文本样式
            text.fontSize = 12;
            text.alignment = TextAlignmentOptions.Center; // 居中对齐
            text.outlineWidth = 0.2f;
            text.outlineColor = Color.black;
            text.fontStyle = FontStyles.Bold;

            durabilityTexts[display] = text;

            // 给容器添加一个标识符，用于后续查找
            container.name = $"DurabilityContainer_{display.GetInstanceID()}";

            // 订阅耐久度变化事件，实现实时更新
            if (display.Target != null)
            {
                display.Target.onDurabilityChanged += OnItemDurabilityChanged;
            }
        }

        /// <summary>
        /// 当物品耐久度变化时的回调
        /// 优化后：更新缓存并更新对应的显示
        /// </summary>
        private void OnItemDurabilityChanged(Item item)
        {
            // 更新所有显示这个物品的UI
            foreach (var kvp in durabilityTexts)
            {
                if (kvp.Key != null && kvp.Key.Target == item)
                {
                    UpdateDurabilityText(kvp.Key);
                }
            }
        }

        /// <summary>
        /// 判断物品是否应该显示百分比（枪械、头盔、护甲）
        /// </summary>
        private bool IsEquipment(Item item)
        {
            if (item == null || item.Tags == null) return false;

            // 检查Tags是否包含武器、护甲、头盔标签（注意：游戏中头盔的标签是"Helmat"）
            return item.Tags.Contains("Weapon") ||
                   item.Tags.Contains("Armor") ||
                   item.Tags.Contains("Helmat");
        }

        /// <summary>
        /// 获取物品有效最大耐久度（考虑DurabilityLoss）
        /// </summary>
        private float GetEffectiveMaxDurability(Item item)
        {
            if (item == null) return 0f;
            return item.DurabilityLoss > 0
                ? item.MaxDurability * (1f - item.DurabilityLoss)
                : item.MaxDurability;
        }

        /// <summary>
        /// 统一的耐久度图标显示判断
        /// </summary>
        private bool ShouldShowDurabilityOnIcon(Item item)
        {
            if (item == null) return false;
            if (!showIconDurability) return false;
            if (!item.UseDurability && item.MaxDurability <= 0) return false;
            if (item.Stackable) return false;
            if (hideEquipmentIconDurability && IsEquipment(item)) return false;
            return true;
        }

        /// <summary>
        /// 更新耐久度文本内容和颜色
        /// </summary>
        private void UpdateDurabilityText(ItemDisplay display)
        {
            if (!durabilityTexts.TryGetValue(display, out var text) || text == null)
                return;

            Item item = display.Target;
            if (item == null)
            {
                // 如果目标物品为空，隐藏显示
                if (text != null && text.transform.parent != null)
                    text.transform.parent.gameObject.SetActive(false);
                return;
            }

            // 检查显示的物品是否已经变更（ItemDisplay被重用）
            string expectedName = $"DurabilityContainer_{display.GetInstanceID()}";
            if (text.transform.parent == null || text.transform.parent.name != expectedName)
            {
                // ItemDisplay被重用了，需要重新创建
                Debug.Log($"[{MOD_NAME}] 检测到ItemDisplay被重用，重新创建显示");
                if (durabilityTexts.TryGetValue(display, out var oldText) && oldText != null)
                {
                    if (oldText.transform.parent != null)
                        Destroy(oldText.transform.parent.gameObject);
                    durabilityTexts.Remove(display);
                }
                CreateDurabilityText(display);
                return;
            }
            // 直接获取实际值
            float current = item.Durability;
            float max = GetEffectiveMaxDurability(item);

            // 避免除零
            if (max <= 0) max = 1f;

            float percent = (current / max) * 100f;

            // 特殊处理：零耐久度显示
            if (current <= 0)
            {
                text.text = IsEquipment(item) ? "0%" : "0";
                text.color = new Color(1f, 0f, 0f, 1f);  // 纯红色
                text.fontStyle = FontStyles.Bold | FontStyles.Strikethrough;  // 加粗+删除线
            }
            else
            {
                text.fontStyle = FontStyles.Bold;  // 恢复正常样式

                // 根据物品类型决定显示格式
                if (IsEquipment(item))
                {
                    // 枪械、头盔、护甲显示百分比
                    text.text = $"{percent:F0}%";
                }
                else
                {
                    // 其他物品（药品、食物、钥匙等）显示数值/最大值
                    text.text = $"{current:F0}";

                    // 如果当前值和最大值差距很大，显示完整格式
                    if (current < max * 0.9f)
                    {
                        text.text = $"{current:F0}/{max:F0}";
                    }
                }

                // 使用更平滑的渐变颜色（参考游戏原生逻辑）
                if (percent > 80f)
                    text.color = Color.white;                          // 白色（完美）
                else if (percent > 60f)
                    text.color = new Color(0.5f, 1f, 0.5f);           // 淡绿色（良好）
                else if (percent > 40f)
                    text.color = new Color(1f, 1f, 0.2f);             // 黄色（一般）
                else if (percent > 20f)
                    text.color = new Color(1f, 0.6f, 0.2f);           // 橙色（警告）
                else
                    text.color = new Color(1f, 0.2f, 0.2f);           // 红色（危险）
            }

            // 确保容器和文本都处于激活状态
            text.transform.parent.gameObject.SetActive(true);
            text.gameObject.SetActive(true);

            // 更新访问时间（LRU缓存）
            displayLastAccessTime[display] = Time.time;
        }

        /// <summary>
        /// 强制创建所有耐久度显示
        /// </summary>
        private void ForceCreateAllDurabilityDisplays()
        {
            if (!showIconDurability)
                return;

            Debug.Log($"[{MOD_NAME}] 强制创建所有耐久度显示...");

            try
            {
                // 先清理无效的显示，但保留有效的
                CleanupInvalidDurabilityTexts();
            }
            catch (Exception e)
            {
                Debug.LogError($"[{MOD_NAME}] 清理无效显示时出错: {e.Message}");
            }

            // 仅在当前打开的View内搜索ItemDisplay，避免全场景扫描
            ItemDisplay[] displays;
            if (View.ActiveView != null)
            {
                displays = View.ActiveView.GetComponentsInChildren<ItemDisplay>(true);
                Debug.Log($"[{MOD_NAME}] 在View内找到 {displays.Length} 个ItemDisplay");
            }
            else
            {
                displays = FindObjectsOfType<ItemDisplay>();
                Debug.Log($"[{MOD_NAME}] 全局扫描找到 {displays.Length} 个ItemDisplay");
            }

            int createdCount = 0;
            int updatedCount = 0;
            int skippedCount = 0;

            foreach (var display in displays)
            {
                if (display == null || display.Target == null)
                {
                    skippedCount++;
                    continue;
                }

                if (!display.gameObject.activeInHierarchy)
                {
                    skippedCount++;
                    continue;
                }

                try
                {
                    if (durabilityTexts.Count >= MAX_DURABILITY_DISPLAYS && !durabilityTexts.ContainsKey(display))
                    {
                        if (!ShouldShowDurabilityOnIcon(display.Target))
                        {
                            skippedCount++;
                            continue;
                        }

                        // 如果缓存已满但当前物品有耐久度，移除一个低优先级的
                        RemoveLowestPriorityDisplay();

                        // 移除后再次检查是否有空间
                        if (durabilityTexts.Count >= MAX_DURABILITY_DISPLAYS)
                        {
                            Debug.LogWarning($"[{MOD_NAME}] 缓存池仍然已满！当前: {durabilityTexts.Count}/{MAX_DURABILITY_DISPLAYS}");
                        }
                    }

                    if (!ShouldShowDurabilityOnIcon(display.Target))
                    {
                        if (durabilityTexts.TryGetValue(display, out var hideText) && hideText != null)
                            hideText.transform.parent.gameObject.SetActive(false);
                        skippedCount++;
                        continue;
                    }

                    // 检查是否已有有效的耐久度显示
                    bool needCreate = true;
                    if (durabilityTexts.TryGetValue(display, out var existingText) && existingText != null)
                    {
                        string expectedName = $"DurabilityContainer_{display.GetInstanceID()}";
                        if (existingText.transform.parent != null &&
                            existingText.transform.parent.name == expectedName)
                        {
                            needCreate = false;
                        }
                        else
                        {
                            if (existingText.transform.parent != null)
                                Destroy(existingText.transform.parent.gameObject);
                            durabilityTexts.Remove(display);
                        }
                    }

                    // 创建或更新显示
                    if (needCreate)
                    {
                        CreateDurabilityText(display);
                        createdCount++;
                        displayLastAccessTime[display] = Time.time;
                    }

                    // 确保显示激活并更新
                    if (durabilityTexts.TryGetValue(display, out var activeText) && activeText != null)
                    {
                        activeText.transform.parent.gameObject.SetActive(true);
                        try
                        {
                            UpdateDurabilityText(display);
                            updatedCount++;
                            // 更新访问时间
                            displayLastAccessTime[display] = Time.time;
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"[{MOD_NAME}] 更新耐久度显示时出错: {e.Message}");
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"[{MOD_NAME}] 处理ItemDisplay时出错: {e.Message}");
                    skippedCount++;
                }
            }

            // 添加详细日志，便于调试
            Debug.Log($"[{MOD_NAME}] 强制创建完成 - 创建: {createdCount} 个，更新: {updatedCount} 个，跳过: {skippedCount} 个，缓存池: {durabilityTexts.Count}/{MAX_DURABILITY_DISPLAYS}");

            if (durabilityTexts.Count >= MAX_DURABILITY_DISPLAYS)
            {
                Debug.LogWarning($"[{MOD_NAME}] 缓存池已满！可能需要增加缓存大小或优化策略。");
            }
        }

        /// <summary>
        /// 移除最低优先级的显示（用于缓存池管理）
        /// 使用LRU策略：优先移除最久未访问的显示
        /// </summary>
        private void RemoveLowestPriorityDisplay()
        {
            try
            {
                ItemDisplay toRemove = null;
                float oldestAccessTime = float.MaxValue;

                foreach (var kvp in durabilityTexts)
                {
                    // 优先移除无效的
                    if (kvp.Key == null || kvp.Key.Target == null)
                    {
                        toRemove = kvp.Key;
                        break;
                    }

                    // 其次移除没有耐久度的
                    if (!kvp.Key.Target.UseDurability)
                    {
                        toRemove = kvp.Key;
                        break;
                    }

                    // 最后按LRU策略：移除最久未访问的
                    float lastAccess = displayLastAccessTime.TryGetValue(kvp.Key, out var accessTime) ? accessTime : 0f;

                    if (lastAccess < oldestAccessTime)
                    {
                        oldestAccessTime = lastAccess;
                        toRemove = kvp.Key;
                    }
                }

                if (toRemove != null && durabilityTexts.TryGetValue(toRemove, out var textToRemove))
                {
                    if (textToRemove != null && textToRemove.transform.parent != null)
                    {
                        Destroy(textToRemove.transform.parent.gameObject);
                    }
                    durabilityTexts.Remove(toRemove);
                    displayLastAccessTime.Remove(toRemove);

                    string itemName = (toRemove.Target != null) ? toRemove.Target.DisplayName : "未知";
                    Debug.Log($"[{MOD_NAME}] 移除最少使用的显示 ({itemName}) 以腾出缓存空间");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[{MOD_NAME}] 移除低优先级显示时出错: {e.Message}");
            }
        }

        /// <summary>
        /// 清理无效的耐久度文本
        /// </summary>
        private void CleanupInvalidDurabilityTexts()
        {
            var toRemove = new List<ItemDisplay>();
            bool hasNullKeys = false;
            foreach (var kvp in durabilityTexts)
            {
                if (kvp.Key == null)
                {
                    hasNullKeys = true;
                    if (kvp.Value != null && kvp.Value.transform.parent != null)
                        Destroy(kvp.Value.transform.parent.gameObject);
                }
                else if (kvp.Value == null)
                {
                    toRemove.Add(kvp.Key);
                }
            }

            foreach (var key in toRemove)
            {
                durabilityTexts.Remove(key);
                displayLastAccessTime.Remove(key);
            }

            if (hasNullKeys)
            {
                var validEntries = new Dictionary<ItemDisplay, TextMeshProUGUI>();
                foreach (var kvp in durabilityTexts)
                {
                    if (kvp.Key != null && kvp.Value != null)
                        validEntries[kvp.Key] = kvp.Value;
                }
                var oldCount = durabilityTexts.Count;
                durabilityTexts.Clear();
                foreach (var entry in validEntries)
                    durabilityTexts[entry.Key] = entry.Value;

                var nullAccessKeys = new List<ItemDisplay>();
                foreach (var kvp in displayLastAccessTime)
                {
                    if (kvp.Key == null || !durabilityTexts.ContainsKey(kvp.Key))
                        nullAccessKeys.Add(kvp.Key);
                }
                foreach (var k in nullAccessKeys)
                {
                    if (k != null)
                        displayLastAccessTime.Remove(k);
                }
                Debug.Log($"[{MOD_NAME}] 清理了 {oldCount - validEntries.Count} 个空键和 {toRemove.Count} 个无效显示");
            }
            else if (toRemove.Count > 0)
            {
                Debug.Log($"[{MOD_NAME}] 清理了 {toRemove.Count} 个无效的耐久度显示");
            }
        }

        /// <summary>
        /// 清理所有耐久度文本（用于翻页时）
        /// </summary>
        private void CleanupAllDurabilityTexts()
        {
            int count = durabilityTexts.Count;
            foreach (var kvp in durabilityTexts)
            {
                if (kvp.Value != null && kvp.Value.transform.parent != null)
                {
                    Destroy(kvp.Value.transform.parent.gameObject);
                }
            }
            durabilityTexts.Clear();
            displayLastAccessTime.Clear();  // 同时清理访问时间记录

            if (count > 0)
                Debug.Log($"[{MOD_NAME}] 清理了所有 {count} 个耐久度显示（翻页重建）");
        }

        /// <summary>
        /// 订阅翻页事件
        /// </summary>
        private void SubscribeToPageEvents()
        {
            // 订阅所有InventoryDisplay的翻页事件
            RefreshPageEventSubscriptions();

            // 也订阅全局UI输入事件（翻页按键）
            UIInputManager.OnNextPage += OnPageChanged;
            UIInputManager.OnPreviousPage += OnPageChanged;
        }

        /// <summary>
        /// 取消订阅翻页事件
        /// </summary>
        private void UnsubscribeFromPageEvents()
        {
            // 清理所有InventoryDisplay的事件订阅
            foreach (var display in subscribedInventoryDisplays)
            {
                if (display != null)
                {
                    display.onPageInfoRefreshed -= OnInventoryPageRefreshed;
                }
            }
            subscribedInventoryDisplays.Clear();

            // 取消全局UI输入事件
            UIInputManager.OnNextPage -= OnPageChanged;
            UIInputManager.OnPreviousPage -= OnPageChanged;
        }

        /// <summary>
        /// 刷新翻页事件订阅（查找新的InventoryDisplay）
        /// </summary>
        private void RefreshPageEventSubscriptions()
        {
            // 仅在当前打开的View内搜索InventoryDisplay，避免全场景扫描
            InventoryDisplay[] inventoryDisplays;
            if (View.ActiveView != null)
                inventoryDisplays = View.ActiveView.GetComponentsInChildren<InventoryDisplay>(true);
            else
                inventoryDisplays = FindObjectsOfType<InventoryDisplay>();
            foreach (var display in inventoryDisplays)
            {
                if (display != null && !subscribedInventoryDisplays.Contains(display))
                {
                    display.onPageInfoRefreshed += OnInventoryPageRefreshed;
                    subscribedInventoryDisplays.Add(display);
                    Debug.Log($"[{MOD_NAME}] 订阅了 InventoryDisplay 翻页事件");
                }
            }
        }

        /// <summary>
        /// 处理翻页事件（来自UI输入）
        /// </summary>
        private void OnPageChanged(UIInputEventData data)
        {
            Debug.Log($"[{MOD_NAME}] 检测到翻页按键，刷新显示");
            if (_delayedPageRefreshCoroutine != null)
                StopCoroutine(_delayedPageRefreshCoroutine);
            _delayedPageRefreshCoroutine = StartCoroutine(DelayedPageRefresh(true));
        }

        /// <summary>
        /// 处理InventoryDisplay翻页事件
        /// </summary>
        private void OnInventoryPageRefreshed()
        {
            Debug.Log($"[{MOD_NAME}] InventoryDisplay翻页事件触发，刷新显示");
            if (_delayedPageRefreshCoroutine != null)
                StopCoroutine(_delayedPageRefreshCoroutine);
            _delayedPageRefreshCoroutine = StartCoroutine(DelayedPageRefresh(true));
        }

        /// <summary>
        /// 延迟刷新页面（等待翻页动画完成）
        /// </summary>
        private System.Collections.IEnumerator DelayedPageRefresh(bool isPageChange = false)
        {
            yield return null;
            if (showIconDurability)
                ForceCreateAllDurabilityDisplays();
            _delayedPageRefreshCoroutine = null;
        }


        /// <summary>
        /// 从缓存更新UI显示
        /// </summary>
        private void UpdateUIFromCache()
        {
            Debug.Log($"[{MOD_NAME}] 从缓存更新UI显示...");

            // 仅在当前打开的View内搜索ItemDisplay，避免全场景扫描
            ItemDisplay[] displays;
            if (View.ActiveView != null)
            {
                displays = View.ActiveView.GetComponentsInChildren<ItemDisplay>(true);
                Debug.Log($"[{MOD_NAME}] 在View内找到 {displays.Length} 个ItemDisplay");
            }
            else
            {
                displays = FindObjectsOfType<ItemDisplay>();
                Debug.Log($"[{MOD_NAME}] 全局扫描找到 {displays.Length} 个ItemDisplay");
            }

            int displayedCount = 0;
            int hiddenCount = 0;
            int skippedCount = 0;

            foreach (var display in displays)
            {
                if (display == null || display.Target == null)
                {
                    skippedCount++;
                    continue;
                }

                if (!display.gameObject.activeInHierarchy)
                {
                    skippedCount++;
                    continue;
                }

                if (!ShouldShowDurabilityOnIcon(display.Target))
                {
                    if (durabilityTexts.TryGetValue(display, out var hideText) && hideText != null)
                    {
                        hideText.transform.parent.gameObject.SetActive(false);
                        hiddenCount++;
                    }
                    skippedCount++;
                    continue;
                }

                if (display.Target.NeedInspection &&
                    !display.Target.IsInPlayerCharacter() &&
                    !display.Target.IsInPlayerStorage())
                {
                    if (durabilityTexts.TryGetValue(display, out var unsearchedText) && unsearchedText != null)
                    {
                        unsearchedText.transform.parent.gameObject.SetActive(false);
                        hiddenCount++;
                    }
                    continue;
                }

                if (!durabilityTexts.TryGetValue(display, out var cachedText) || cachedText == null)
                {
                    CreateDurabilityText(display);
                }

                if (durabilityTexts.TryGetValue(display, out var activeText) && activeText != null)
                {
                    activeText.transform.parent.gameObject.SetActive(true);
                    UpdateDurabilityText(display);
                    displayedCount++;
                }
            }

            Debug.Log($"[{MOD_NAME}] UI更新完成 - 显示: {displayedCount} 个，隐藏: {hiddenCount} 个，跳过: {skippedCount} 个 (总计: {displays.Length} 个ItemDisplay)");
        }

        #region 辅助方法


        /// <summary>
        /// 判断是否为消耗品
        /// </summary>
        private bool IsConsumable(Item item)
        {
            // 检查是否含有消耗品相关标签
            // 注意：只检查有耐久度的消耗品（如药品、食物等）
            if (!item.UseDurability)
                return false;

            return item.Tags.Contains("Food") ||
                   item.Tags.Contains("Drink") ||
                   item.Tags.Contains("Medicine") ||
                   item.Tags.Contains("Consumable");
            // 移除 Stackable 判断，因为可堆叠物品通常没有耐久度
        }

        #endregion
    }
}

