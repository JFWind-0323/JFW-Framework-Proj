using Sirenix.OdinInspector;
using UnityEngine;

namespace TextSystem
{
    [CreateAssetMenu(fileName = "TextDataSOItemDescription", menuName = "SO/TextData/ItemDescription", order = 0)]
    public class TextDataSOItemDescription : TextDataSOBase<LineItemDescription>
    {
        [OnValueChanged("UpdateIcons")] [SuffixLabel("如果勾选，所有物品都将使用自定义的相同图标,常用于同类物品的种类标识")]
        public bool sameIcon;

        private bool notSameIcon => !sameIcon;

        [OnValueChanged("UpdateIcons")] [HideIf("notSameIcon")]
        public Sprite defaultIcon;

        [HideIf("sameIcon")] [ListDrawerSettings(ShowIndexLabels = true)]
        public Sprite[] icons;


        protected override void SplitLine(string content)
        {
            var split = content.Split("\n");
            for (int i = 1; i < split.Length; i++)
            {
                if (string.IsNullOrEmpty(split[i]))
                {
                }
                else
                {
                    var line = split[i].Split(",");
                    lines.Add(new LineItemDescription(line));
                }

                UpdateIcons();
            }
        }

        [Button("Only Update Icons")]
        private void UpdateIcons()
        {
            if (sameIcon)
            {
                foreach (var line in lines)
                {
                    line.SetIcon(defaultIcon);
                }
            }
            else
            {
                foreach (var line in lines)
                {
                    line.SetIcon(icons);
                }
            }
        }
    }
}