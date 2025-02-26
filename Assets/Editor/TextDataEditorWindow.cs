using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using TextSystem;
using UnityEditor;
using UnityEngine;

namespace Editor
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
                ScriptableObject data;
                string path = string.Empty;
                if (selected != null)
                {
                    data = selected.SelectedValue as ScriptableObject;
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
            foreach (TextDataType type in Enum.GetValues(typeof(TextDataType)))
            {
                if (type == TextDataType.Linear)
                {
                    mainMenu.Add("Linear", ScriptableObject.CreateInstance<TextDataLinearSO>());
                }
                else if (type == TextDataType.Tree)
                {
                    mainMenu.Add("Tree", ScriptableObject.CreateInstance<TextDataTreeSO>());
                }
                else if (type == TextDataType.Task)
                {
                    mainMenu.Add("Task", ScriptableObject.CreateInstance<TextDataTaskSO>());
                }
                else if (type == TextDataType.ItemDiscription)
                {
                    mainMenu.Add("ItemDiscription", ScriptableObject.CreateInstance<TextDataItemDiscriptionSO>());
                }
            }

            mainMenu.AddAllAssetsAtPath("Linear", "Assets/Resources/SO/TextData", typeof(TextDataLinearSO));
            mainMenu.AddAllAssetsAtPath("Tree", "Assets/Resources/SO/TextData", typeof(TextDataTreeSO));
            mainMenu.AddAllAssetsAtPath("Task", "Assets/Resources/SO/TextData", typeof(TextDataTaskSO));
            mainMenu.AddAllAssetsAtPath("ItemDiscription", "Assets/Resources/SO/TextData", typeof(TextDataItemDiscriptionSO));

            return mainMenu;
        }

     
    }
}