using TextSystem;
using UnityEngine;

namespace Framework.TextSystem
{
    [CreateAssetMenu(fileName = "TextDataSOTree", menuName = "SO/TextData/Tree", order = 0)]
    public class TextDataSOTree : TextDataSOBase<LineTree>
    {
        public string[] characters;
        public string[] positions;

        protected override void SplitLine(string content)
        {
            var split = content.Split("\n");
            for (var i = 1; i < split.Length; i++)
            {
                if (string.IsNullOrEmpty(split[i]))
                {
                }
                else
                {
                    var line = split[i].Trim().Split(",");
                    lines.Add(new LineTree(line, characters, positions));
                }
            }
        }
    }
}