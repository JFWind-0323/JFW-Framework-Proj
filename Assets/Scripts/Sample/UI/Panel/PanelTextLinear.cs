using Framework.UI.Base;
using TextSystem;
using TMPro;
using UnityEngine;

namespace Sample.UI.Panel
{
    public class PanelTextLinear : PanelBase
    {
        public TMP_Text text;
        public TMP_Text idText;
        public TextDataSOLinear[] linearData;

        public int linearId;

        private TextDataSOLinear currentData => linearData[linearId];

        protected override void Enter()
        {
            UpdateText();
        }

        void Awake()
        {
            linearData = Resources.LoadAll<TextDataSOLinear>("SO/TextData/Linear");
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
               UpdateText();
            }
        }

        void UpdateText()
        {
            text.text= currentData.GetCurrentLine()?.content;
            idText.text = currentData.GetCurrentIndex().ToString();
            currentData.UpdateCurrentIndex();
        }
    }
}