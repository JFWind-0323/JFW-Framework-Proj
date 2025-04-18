using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TextSystem
{
    public enum LineType
    {
        /*
         * 自行扩展
         */
        Default,
        Question,
        Option,
        End,
    }

    [Serializable]
    public class LineBase
    {
        /*
         * 自行扩展
         */
        [TableColumnWidth(30)]
        public int id;
        
        [TableColumnWidth(30)]
        public string text;
        
        [TableColumnWidth(30)]
        public int next;

        [TableColumnWidth(30)]
        public LineType type = LineType.Default;

        public LineBase(string[] lineFromFile)
        {
            
        }
    }

    [Serializable]
    public class LineLinear : LineBase
    {
        public LineLinear(string[] lineFromFile) : base(lineFromFile)
        {
            id=Int32.TryParse(lineFromFile[0].Trim(),out id) ? id : 0;
            text = lineFromFile[1];
            next = Int32.TryParse(lineFromFile[2].Trim(),out next) ? next : 0;
            type = LineType.Default;
            Debug.Log(text);
        }
    }

    [Serializable]
    public class LineTree : LineBase
    {
        public LineTree(string[] lineFromFile) : base(lineFromFile)
        {
        }
    }

    [Serializable]
    public class LineTask : LineBase
    {
        public LineTask(string[] lineFromFile) : base(lineFromFile)
        {
        }
    }

    [Serializable,TableList]
    public class LineItemDescription : LineBase
    {
        public LineItemDescription(string[] lineFromFile) : base(lineFromFile)
        {
        }
    }
}