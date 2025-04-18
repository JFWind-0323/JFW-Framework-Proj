using TextSystem;
using UnityEngine;

namespace Framework.TextSystem
{
    [CreateAssetMenu(fileName = "TextDataSOItemDescription", menuName = "SO/TextData/ItemDescription", order = 0)]
    public class TextDataSOItemDescription : TextDataSOBase<LineItemDescription>
    {
        protected override void SplitLine(string content)
        {
            
        }
    }
}