using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using TextSystem;
using UnityEngine;

namespace Framework.TextSystem
{
    public class TextDataSOBase<T> : ScriptableObject where T : LineBase
    {
        [HorizontalGroup("File Info")] public TextAsset textAsset;
        [TableList] public List<T> lines = new();


        [Button("Process Playbook")]
        private void ProcessPlaybook()
        {
            lines.Clear();
            SplitLine(textAsset.text);
        }

        protected virtual void SplitLine(string content)
        {
        }

        public T GetLine(int index)
        {
            return lines[index];
        }
    }
}