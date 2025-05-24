using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TextData
{
    public class TextDataSOBase<T> : ScriptableObject where T : LineBase
    {
        [Required] public TextAsset textAsset;
        [TableList] public List<T> lines = new();

        protected int CurrentIndex { get; set; }

        void OnEnable()
        {
            CurrentIndex = 0;
        }
        

        #region 内部处理数据和调试的方法

        [GUIColor(0, 1, 0)]
        [Button("Process Text Data")]
        protected void ProcessTextData()
        {
            lines.Clear();
            SplitLine(textAsset.text);
        }

        [GUIColor(0, 0.7f, 0.7f)]
        [Button("Print")]
        protected virtual void Print()
        {
        }

        protected virtual void SplitLine(string content)
        {
        }

        #endregion

        #region 外部操作数据的方法

        public virtual T GetCurrentLine()
        {
            if (CurrentIndex < lines.Count)
                return lines[CurrentIndex];
            else
                Debug.LogWarning("当前索引超出范围！");
            return null;
        }

        public T GetLineByIndex(int index)
        {
            return lines[index];
        }

        public virtual int GetCurrentIndex()
        {
            return CurrentIndex;
        }

        public virtual void UpdateCurrentIndex()
        {
        }

        public virtual void SetCurrentIndex(int index)
        {
            CurrentIndex = index;
        }

        #endregion
    }
}