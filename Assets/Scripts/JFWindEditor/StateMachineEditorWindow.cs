using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
                Type screenType = assembly.GetType($"{nameSpace}.{stateType}.{stateType}Base");
                Type[] allSubclasses = screenType.GetAllDerivedTypes().ToArray();
                foreach (var subclass in allSubclasses)
                {
                    //遍历子类，添加到菜单树
                    var assets = mainMenu.AddAllAssetsAtPath($"States/{stateType}",
                        $"Assets/Resources/SO/State/{stateType}", subclass);
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

        [EnumToggleButtons] [OnValueChanged("UpdateSubclasses")]
        public StateType stateType = StateType.GameState;

        Type screenType => assembly.GetType($"{nameSpace}.{stateType}.{stateType}Base");
        Type[] allSubclasses => screenType.GetAllDerivedTypes().ToArray();

        [ShowInInspector] [ValueDropdown("allSubclasses"), OnValueChanged("CreateNewState")]
        public Type selectedSubclass;

        #endregion

        [Header("状态数据文件名称")] public string stateFileName;

        [Header("状态数据内容")] [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Boxed)] 
        [ShowInInspector] ScriptableObject currentState;


        public StateMachineEditor(string nameSpace, Assembly assembly)
        {
            this.nameSpace = nameSpace;
            this.assembly = assembly;
            selectedSubclass = allSubclasses[0];
            CreateNewState();
        }


        private void CreateNewState()
        {
            currentState = ScriptableObject.CreateInstance(selectedSubclass);
        }

        private void UpdateSubclasses()
        {
            selectedSubclass = allSubclasses[0];
            CreateNewState();
        }

        [Button("Create New State Asset")]
        private void CreateAsset()
        {
            string folderPath = $"Assets/Resources/SO/State/{stateType}";
            string fullPath = $"{folderPath}/{stateFileName}.asset";

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

                AssetDatabase.CreateAsset(currentState, fullPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            catch (Exception e)
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