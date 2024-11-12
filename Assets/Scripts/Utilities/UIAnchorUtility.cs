using UnityEngine;

namespace Utilities
{
    public static class UIAnchorUtility
    {
        public static void FillTheCanvas(RectTransform tf)
        {
            tf.anchorMin = Vector2.zero;
            tf.anchorMax = new Vector2(1, 1);
            tf.offsetMin = Vector2.zero;
            tf.offsetMax = Vector2.zero;
        }
        //还可以扩展
    }
}