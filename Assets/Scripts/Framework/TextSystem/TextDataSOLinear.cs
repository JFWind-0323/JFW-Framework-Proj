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
            for (var index = 0; index < split.Length; index++)
            {
                if (string.IsNullOrEmpty(split[index])) continue;
                if (index == 0)
                {
                }
                else
                {
                    var line = split[index];
                    lines.Add(new LineLinear(line.Trim().Split(",")));
                }
            }
        }
    }
}