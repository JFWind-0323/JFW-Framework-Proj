using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Config;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace JFWindEditor
{
    // 定义数据类型枚举
    public enum DataType
    {
        GameConfig = 0,
        PlayerConfig = 1,
        LevelConfig = 2,

        AudioConfig = 3
        //切记在下方添加Switch进行筛选
    }

    // 数据编辑器窗口类，继承自OdinMenuEditorWindow
    public class ConfigEditorWindow : OdinMenuEditorWindow
    {
        // 在菜单栏中添加选项以打开窗口
        [MenuItem("Tools / Config Editor")]
        public static void OpenWindow()
        {
            GetWindow<ConfigEditorWindow>().Show();
        }

        // 创建数据资产的实例
        private ConfigAsset configAsset;

        // 当编辑器窗口销毁时，销毁数据资产
        protected override void OnDestroy()
        {
            if (configAsset != null)
                DestroyImmediate(configAsset.data);
        }

        // 在开始绘制编辑器之前调用，添加工具栏按钮
        protected override void OnBeginDrawEditors()
        {
            OdinMenuTreeSelection selected = this.MenuTree.Selection;
            SirenixEditorGUI.BeginHorizontalToolbar();
            {
                GUILayout.FlexibleSpace();
                if (SirenixEditorGUI.ToolbarButton("Delete Current Asset"))
                {
                    ScriptableObject data = selected.SelectedValue as ScriptableObject;
                    string path = AssetDatabase.GetAssetPath(data);
                    AssetDatabase.DeleteAsset(path);
                    AssetDatabase.SaveAssets();
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        // 选择的数据类型
        private Type selectedType;


        // 构建菜单树，添加不同类型的数据资产
        protected override OdinMenuTree BuildMenuTree()
        {
            var mainMenu = new OdinMenuTree();


            configAsset = new ConfigAsset();
            mainMenu.Add("Config Asset Editor", configAsset);

            Assembly assembly = Assembly.Load("Assembly-CSharp");
            if (configAsset.nameSpace == string.Empty) return mainMenu;
            foreach (DataType dataType in Enum.GetValues(typeof(DataType)))
            {
                // 获取对应的数据类型
                Type screenType = assembly.GetType($"{configAsset.nameSpace}.{dataType.ToString()}");
                if (screenType == null)
                {
                    Debug.LogError($"Can't find screen textDataType {configAsset.nameSpace}.{dataType.ToString()}");
                    break;
                }

                // 添加所有路径下的数据资产，并按名称排序
                var assets =
                    mainMenu.AddAllAssetsAtPath($"Data/{screenType.Name}", $"Assets/Resources/SO/{dataType.ToString()}", screenType);
                assets.SortMenuItemsByName();
            }

            return mainMenu;
        }
    }

// 创建数据资产的辅助类
    public class ConfigAsset
    {
        // 使用枚举按钮选择数据类型，并在值改变时调用CreateNewDataAsset方法
        [EnumToggleButtons] [OnValueChanged("CreateNewDataAsset")]
        public DataType dataType;

        // 选择的数据类型和数据资产名称
        public Type selectedType;
        [Header("文件名称")] public string dataName;
        [Header("命名空间")] public string nameSpace = "Config";

        [Header("数据内容")]
        // 内联编辑器显示数据资产，隐藏对象字段
        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public ScriptableObject data;

        // 构造函数，初始化数据资产
        public ConfigAsset()
        {
            CreateNewDataAsset();
        }

        // 根据选择的数据类型创建新的数据资产实例
        public void CreateNewDataAsset()
        {
            data = dataType switch
            {
                DataType.GameConfig => ScriptableObject.CreateInstance<GameConfig>(),
                DataType.PlayerConfig => ScriptableObject.CreateInstance<PlayerConfig>(),
                DataType.LevelConfig => ScriptableObject.CreateInstance<LevelConfig>(),
                DataType.AudioConfig => ScriptableObject.CreateInstance<AudioConfig>(),
                _ => throw new InvalidOperationException($"不支持的数据种类: {dataType}，请添加switch分支")
            };
            dataName = "New " + dataType;
        }

        // 创建新的游戏数据资产并保存到指定路径
        [Button("Create New Config Asset")]
        private void CreateAsset()
        {
            string folderPath = $"Assets/Resources/SO/{dataType}";
            string fullPath = $"{folderPath}/{dataName}.asset";

            try
            {
                // 新增路径清洗逻辑
                folderPath = folderPath.Replace("\\", "/").TrimEnd('/');
                string[] pathSegments = folderPath.Split('/');

                StringBuilder currentPath = new StringBuilder("Assets");
                foreach (var segment in pathSegments.Skip(1)) // 跳过初始的Assets
                {
                    if (string.IsNullOrWhiteSpace(segment))
                    {
                        Debug.LogError("包含空路径段");
                        return;
                    }

                    currentPath.Append($"/{segment}");

                    // 新增有效性验证
                    if (!IsValidFolderName(segment))
                    {
                        Debug.LogError($"非法文件夹名称: {segment}");
                        return;
                    }

                    // 改进创建逻辑
                    if (!AssetDatabase.IsValidFolder(currentPath.ToString()))
                    {
                        string guid = AssetDatabase.CreateFolder(
                            Path.GetDirectoryName(currentPath.ToString()),
                            Path.GetFileName(currentPath.ToString())
                        );

                        // 新增结果验证
                        if (string.IsNullOrEmpty(guid))
                        {
                            Debug.LogError($"创建失败: {currentPath}");
                            return;
                        }

                        // 新增延时刷新
                        System.Threading.Thread.Sleep(100);
                        AssetDatabase.Refresh();
                    }
                }

                AssetDatabase.CreateAsset(data, fullPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"创建失败: {e}");
            }
        }

// 新增文件夹名称验证
        private bool IsValidFolderName(string name)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            return !name.Any(c => invalidChars.Contains(c)) &&
                   !name.StartsWith(" ") &&
                   !name.EndsWith(".");
        }
    }
}