using TextSystem;
using UnityEngine;

namespace Framework.TextSystem
{
    [CreateAssetMenu(fileName = "TextDataSOTree", menuName = "SO/TextData/Tree", order = 0)]
    public class TextDataSOTree : TextDataSOBase<LineTree>
    {
        protected override void SplitLine(string content)
        {
        }
    }
}