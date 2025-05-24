using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TextData
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
        [TableColumnWidth(60, Resizable = false)]
        public int id;

         [TableColumnWidth(100)] public string content;


        [TableColumnWidth(100, Resizable = false)]
        public LineType type = LineType.Default;

        protected LineBase()
        {
        }

        public LineBase(string[] lineFromFile)
        {
        }
    }

    [Serializable]
    public class LineLinear : LineBase
    {
        public LineLinear(int id, string content)
        {
            this.id = id;
            this.content = content;
        }
    }

    [Serializable]
    public class LineTree : LineBase
    {
        [TableColumnWidth(60, Resizable = false)]
        public string character;

        [TableColumnWidth(60, Resizable = false)]
        public int next;

        [TableColumnWidth(60, Resizable = false)]
        public string position;

        public LineTree(string[] lineFromFile, string[] characters, string[] positions) : base(lineFromFile)
        {
            id = Int32.TryParse(lineFromFile[0].Trim(), out id) ? id : 0;
            content = lineFromFile[1];
            int characterId = Int32.TryParse(lineFromFile[2].Trim(), out characterId) ? characterId : 0;
            character = characters[characterId];
            int positionId = Int32.TryParse(lineFromFile[3].Trim(), out positionId) ? positionId : 0;
            position = positions[positionId];
            type = lineFromFile[4] switch
            {
                "D" => LineType.Default,
                "Q" => LineType.Question,
                "O" => LineType.Option,
                "E" => LineType.End,
                _ => LineType.Default
            };
            next = Int32.TryParse(lineFromFile[5].Trim(), out next) ? next : 0;
        }
    }

    [Serializable]
    public class LineTask : LineBase
    {
        public LineTask(string[] lineFromFile) : base(lineFromFile)
        {
        }
    }

    [Serializable, TableList]
    public class LineItemDescription : LineBase
    {
        [TableColumnWidth(100), TextArea(4, 20)]
        public string description;

        [TableColumnWidth(60, Resizable = false)] [PreviewField(Height = 60, Alignment = ObjectFieldAlignment.Center)]
        public Sprite icon;


        public LineItemDescription(string[] lineFromFile) : base(lineFromFile)
        {
            id = Int32.TryParse(lineFromFile[0].Trim(), out id) ? id : 0;
            content = lineFromFile[1];
            type = LineType.Default;
            description = lineFromFile[2];
        }

        public void SetIcon(Sprite icon)
        {
            this.icon = icon;
        }

        public void SetIcon(Sprite[] icons)
        {
            if (icons.Length <= id) return;
            icon = icons[id];
        }
    }
}