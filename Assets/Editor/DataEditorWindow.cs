using Framework.DataPersistence;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using System;

using System.Reflection;
namespace Editor
{
    // 定义数据类型枚举
    public enum DataType
    {
        GameData = 0,
        PlayerData = 1,
        LevelData = 2
        
    }

    // 数据编辑器窗口类，继承自OdinMenuEditorWindow
    public class DataEditorWindow : OdinMenuEditorWindow
    {
        // 在菜单栏中添加“Tools / Data Editor”选项以打开窗口
        [MenuItem("Tools / Data Editor")]
        public static void OpenWindow()
        {
            GetWindow<DataEditorWindow>().Show();
        }

        // 创建数据资产的实例
        private CreateDataAsset createDataAsset;

        // 当编辑器窗口销毁时，销毁数据资产
        protected override void OnDestroy()
        {
            if (createDataAsset != null)
                DestroyImmediate(createDataAsset.data);
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
                    GameData data = selected.SelectedValue as GameData;
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
            createDataAsset = new CreateDataAsset();
            mainMenu.Add("CreateDataAsset", createDataAsset);
            Assembly assembly = Assembly.Load("Assembly-CSharp");
            foreach (DataType dataType in Enum.GetValues(typeof(DataType)))
            {
                // 获取对应的数据类型
                Type screenType = assembly.GetType($"Framework.DataPersistence.{dataType.ToString()}") ; 
                // 添加所有路径下的数据资产，并按名称排序
               var assets= mainMenu.AddAllAssetsAtPath($"Data/{screenType.Name}", "Assets/Resources/SO", screenType);
               assets.SortMenuItemsByName();
            }
            return mainMenu;
        }
    }

    // 创建数据资产的辅助类
    public class CreateDataAsset
    {
        // 使用枚举按钮选择数据类型，并在值改变时调用CreateNewDataAsset方法
        [EnumToggleButtons] [OnValueChanged("CreateNewDataAsset")]
        public DataType dataType;

        // 选择的数据类型和数据资产名称
        public Type selectedType;
        public string dataName;

        // 内联编辑器显示数据资产，隐藏对象字段
        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public ScriptableObject data;

        // 构造函数，初始化数据资产
        public CreateDataAsset()
        {
            CreateNewDataAsset();
        }

        // 根据选择的数据类型创建新的数据资产实例
        public void CreateNewDataAsset()
        {
            data = dataType switch
            {
                DataType.GameData => ScriptableObject.CreateInstance<GameData>(),
                DataType.PlayerData => ScriptableObject.CreateInstance<PlayerData>(),
                DataType.LevelData => ScriptableObject.CreateInstance<LevelData>(),
                _ => throw new InvalidOperationException($"不支持的数据种类: {dataType}，请添加switch分支")
            };
            dataName = "New " + dataType;
        }

        // 创建新的游戏数据资产并保存到指定路径
        [Button("Create New Game Data")]
        private void CreateAsset()
        {
            AssetDatabase.CreateAsset(data, $"Assets/Resources/SO/{dataName}.asset");
            AssetDatabase.SaveAssets();
        }
    }
}
