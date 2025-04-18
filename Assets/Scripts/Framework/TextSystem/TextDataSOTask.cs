using TextSystem;
using UnityEngine;

namespace Framework.TextSystem
{
    [CreateAssetMenu(fileName = "TextDataSOTask", menuName = "SO/TextData/Task", order = 0)]
    public class TextDataSOTask : TextDataSOBase<LineTask>
    {
        protected override void SplitLine(string content)
        {
            
        }
    }
}