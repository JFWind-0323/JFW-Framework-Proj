using TextSystem;
using UnityEngine;

namespace Framework.TextSystem
{
    [CreateAssetMenu(fileName = "TextDataSOLinear", menuName = "SO/TextData/Linear", order = 0)]
    public class TextDataSOLinear : TextDataSOBase<LineLinear>
    {
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
                    lines.Add(new LineLinear(line));
                }
            }
        }

        public override void UpdateCurrentIndex()
        {
            currentIndex++;
        }
    }
}