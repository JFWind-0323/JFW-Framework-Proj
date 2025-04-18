using System;
using Framework.TextSystem;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace JFWindEditor
{
    public enum TextDataType
    {
        //线性
        Linear,

        //树状
        Tree,

        //任务
        Task,

        //物品描述
        ItemDiscription,
    }

    public class TextDataEditorWindow : OdinMenuEditorWindow
    {
        private TextDataCreator creator;
        [MenuItem("Tools / Text Data Editor")]
        public static void OpenWindow()
        {
            GetWindow<TextDataEditorWindow>().Show();
        }

        // 在开始绘制编辑器之前调用，添加工具栏按钮
        protected override void OnBeginDrawEditors()
        {
            OdinMenuTreeSelection selected = this.MenuTree.Selection;
            SirenixEditorGUI.BeginHorizontalToolbar();
            {
                string path = string.Empty;
                if (selected != null)
                {
                    var data = selected.SelectedValue as ScriptableObject;
                    path = AssetDatabase.GetAssetPath(data);
                }

                GUILayout.FlexibleSpace();
                if (SirenixEditorGUI.ToolbarButton("Delete Current Asset"))
                {
                    AssetDatabase.DeleteAsset(path);
                    AssetDatabase.SaveAssets();
                }
            }

            SirenixEditorGUI.EndHorizontalToolbar();
        }
        // 文件导入方法


        protected override OdinMenuTree BuildMenuTree()
        {
            var mainMenu = new OdinMenuTree();
            creator = new TextDataCreator();
            mainMenu.Add("TextDataCreator", creator);
            mainMenu.AddAllAssetsAtPath("Linear", "Assets/Resources/SO/TextData/Linear", typeof(TextDataSOLinear));
            mainMenu.AddAllAssetsAtPath("Tree", "Assets/Resources/SO/TextData/Tree", typeof(TextDataSOTree));
            mainMenu.AddAllAssetsAtPath("Task", "Assets/Resources/SO/TextData/Task", typeof(TextDataSOTask));
            mainMenu.AddAllAssetsAtPath("ItemDiscription", "Assets/Resources/SO/TextData/ItemDescription",
                typeof(TextDataSOItemDescription));

            return mainMenu;
        }
    }

    public class TextDataCreator
    {
        [EnumToggleButtons] [OnValueChanged("CreateNewTextDataAsset")]
        public TextDataType textDataType = TextDataType.Linear;

        [Header("数据名称")] public string dataName;

        [Header("数据内容")] [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden,Expanded = true)]
        public ScriptableObject data;

        public TextDataCreator()
        {
            CreateNewTextDataAsset();
        }

        public void CreateNewTextDataAsset()
        {
            data = textDataType switch
            {
                TextDataType.Linear => ScriptableObject.CreateInstance<TextDataSOLinear>(),
                TextDataType.Tree => ScriptableObject.CreateInstance<TextDataSOTree>(),
                TextDataType.Task => ScriptableObject.CreateInstance<TextDataSOTask>(),
                TextDataType.ItemDiscription => ScriptableObject.CreateInstance<TextDataSOItemDescription>(),
                _ => throw new InvalidOperationException($"不支持的数据种类: {textDataType}，请添加switch分支")
            };
        }

        [Button("Create New TextData Asset")]
        private void CreateAsset()
        {
            AssetDatabase.CreateAsset(data, $"Assets/Resources/SO/TextData/{textDataType.ToString()}/{dataName}.asset");
            AssetDatabase.SaveAssets();
        }
    }
}