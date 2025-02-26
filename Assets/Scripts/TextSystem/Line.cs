using System;
using Sirenix.OdinInspector;

namespace TextSystem
{
    public enum LineType
    {
        /*
         * 自行扩展
         */
        Default,
        Option,
    }

    [Serializable]
    public class LineBase
    {
        /*
         * 自行扩展
         */
        [HorizontalGroup("Line info"), LabelWidth(50)]
        public int id;

        [HorizontalGroup("Line info"), LabelWidth(50)]
        public int next;

        [HorizontalGroup("Line info"), LabelWidth(50)]
        public string text;

        [HorizontalGroup("Line Config"), LabelWidth(50)]
        public bool isDone;

        [HorizontalGroup("Line Config"), LabelWidth(50)]
        public LineType type = LineType.Default;

        public LineBase(int id, int next, string text)
        {
        }
    }

    [Serializable]
    public class LineLinear : LineBase
    {
        public LineLinear(int id, int next, string text) : base(id, next, text)
        {
            this.id = id;
            this.next = next;
            this.text = text;
        }
    }

    [Serializable]
    public class LineTree : LineBase
    {
        public LineTree(int id, int next, string text) : base(id, next, text)
        {
        }
    }

    [Serializable]
    public class LineTask : LineBase
    {
        public LineTask(int id, int next, string text) : base(id, next, text)
        {
        }
    }

    [Serializable]
    public class LineItemDiscription : LineBase
    {
        public LineItemDiscription(int id, int next, string text) : base(id, next, text)
        {
        }
    }
}