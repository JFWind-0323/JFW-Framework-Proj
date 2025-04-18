using System;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace JFWindEditor
{
    public enum StateType
    {
        PlayerState,
        GameState
    }

     public class StateMachineEditorWindow : OdinMenuEditorWindow
    {
        /*
         * 请注意文件夹结构
         * Sample
         *     - StateMachine
         *         - GameState
         *             - GameState1
         *             - GameState2
         *         - OtherState
         *             - ...
         *         - ...
         * Resources
         *     - SO
         *         - GameState
         *             - GameState1.asset
         *             - GameState2.asset
         *         - OtherState
         *             - ...
         *         - ...
         */
        [MenuItem("Tools/State Machine Editor")]
        public static void OpenWindow()
        {
            GetWindow<StateMachineEditorWindow>().Show();
        }


        protected override void OnDestroy()
        {
        }

        protected override void OnBeginDrawEditors()
        {
            OdinMenuTreeSelection selected = this.MenuTree.Selection;
            SirenixEditorGUI.BeginHorizontalToolbar();
            {
                GUILayout.FlexibleSpace();
                if (SirenixEditorGUI.ToolbarButton("Delete Current Asset"))
                {
                    ScriptableObject selectedState = selected.SelectedValue as ScriptableObject;
                    string path = AssetDatabase.GetAssetPath(selectedState);
                    AssetDatabase.DeleteAsset(path);
                    AssetDatabase.SaveAssets();
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        private StateMachineEditor stateMachineEditor;
        public string nameSpace => " Sample.StateMachine";

        protected override OdinMenuTree BuildMenuTree()
        {
            var mainMenu = new OdinMenuTree();
            Assembly assembly = Assembly.Load("Assembly-CSharp");
            stateMachineEditor = new StateMachineEditor(nameSpace, assembly);
            mainMenu.Add("State Machine", stateMachineEditor);

            foreach (var stateType in Enum.GetValues(typeof(StateType)))
            {
                Type screenType = assembly.GetType($"{nameSpace}.{stateType}.{stateType}");
                Type[] allSubclasses = screenType.GetAllDerivedTypes().ToArray();
                //allSubclasses.Print();
                foreach (var subclass in allSubclasses)
                {
                    var assets = mainMenu.AddAllAssetsAtPath($"States/{screenType.Name}",
                        $"Assets/Resources/SO/{screenType.Name}", subclass);
                    assets.SortMenuItemsByName();
                }
            }

            return mainMenu;
        }
    }

    public class StateMachineEditor
    {
        #region 程序集与命名空间配置

        private string nameSpace;
        private Assembly assembly;

        #endregion

        #region 状态选择

        [EnumToggleButtons] [OnValueChanged("CreateNewState")]
        public StateType stateType = StateType.GameState;

        Type screenType => assembly.GetType($"{nameSpace}.{stateType}.{stateType}");
        Type[] allSubclasses => screenType.GetAllDerivedTypes().ToArray();

        [ShowInInspector] [ValueDropdown("allSubclasses"),OnValueChanged("CreateNewState")]
        public Type selectedSubclass;

        #endregion

        [Header("文件名称")] public string fileName;

        [Header("状态数据")] [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Boxed)] [ShowInInspector]
        ScriptableObject currentState;


        public StateMachineEditor(string nameSpace, Assembly assembly)
        {
            this.nameSpace = nameSpace;
            this.assembly = assembly;
            selectedSubclass = allSubclasses[0];
            CreateNewState();
        }


        public void CreateNewState()
        {
            currentState = ScriptableObject.CreateInstance(selectedSubclass);
        }

        [Button("Create New State Asset")]
        private void CreateAsset()
        {
            AssetDatabase.CreateAsset(currentState, $"Assets/Resources/SO/{fileName}.asset");
            AssetDatabase.SaveAssets();
        }
    }
}