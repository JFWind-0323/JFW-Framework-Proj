using System.Collections.Generic;
using Sirenix.OdinInspector;
using TextSystem;
using UnityEngine;

namespace Framework.TextSystem
{
    public class TextDataSOBase<T> : ScriptableObject where T : LineBase
    {
        [HorizontalGroup("File Info")] public TextAsset textAsset;
        [TableList] public List<T> lines = new();
        
        protected int currentIndex = 0;


        [Button("Process Playbook")]
        protected void ProcessPlaybook()
        {
            lines.Clear();
            SplitLine(textAsset.text);
        }

        protected virtual void SplitLine(string content)
        {
        }

        public T GetCurrentLine()
        {
            return lines[currentIndex];
        }

        public T GetLineByIndex(int index)
        {
            return lines[index];
        }

        public virtual void UpdateCurrentIndex()
        {
            
        }
    }
}