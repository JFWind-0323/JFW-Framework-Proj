using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace TextSystem
{
       public class TextDataSOBase<T> : ScriptableObject where T : LineBase
        {
            [Sirenix.OdinInspector.FilePath, HorizontalGroup("File Info"), LabelWidth(50)]
            public string filePath;

            [HorizontalGroup("File Info"), LabelWidth(60)]
            public string playbookName = "New Playbook";

            protected string content;

            public List<T> lines = new List<T>();


            [Button("Process Playbook")]
            private void ProcessPlaybook()
            {
                lines.Clear();
                content = File.ReadAllText(filePath);
                if (string.IsNullOrEmpty(filePath))
                {
                    EditorUtility.DisplayDialog("Error", "Please select a playbook file first", "OK");
                    return;
                }

                SplitLine();
            }

            protected virtual void SplitLine()
            {
            }

            [Button("Save")]
            public void Save()
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    EditorUtility.DisplayDialog("Error", "Please select a playbook file first", "OK");
                    return;
                }

                string targetPath =$"Assets/Resources/SO//TextData/{playbookName}.asset";
                if (AssetDatabase.LoadAssetAtPath<TextDataSOBase<T>>(targetPath) == null)
                {
                    AssetDatabase.CreateAsset(this, targetPath);
                }
                AssetDatabase.SaveAssets();
            }
        }

        public class TextDataLinearSO : TextDataSOBase<LineLinear>
        {
            protected override void SplitLine()
            {
                foreach (var line in content.Split("\n"))
                {
                    lines.Add(new LineLinear(lines.Count, lines.Count + 1, line));
                    Debug.Log(line);
                }
            }
        }

        public class TextDataTreeSO : TextDataSOBase<LineTree>
        {
            protected override void SplitLine()
            {
            }
        }

        public class TextDataTaskSO : TextDataSOBase<LineTask>
        {
            protected override void SplitLine()
            {
            }
        }

        public class TextDataItemDiscriptionSO : TextDataSOBase<LineItemDiscription>
        {
            protected override void SplitLine()
            {
            }
        }
}