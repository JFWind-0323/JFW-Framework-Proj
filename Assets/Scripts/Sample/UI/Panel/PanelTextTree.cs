using Framework.PoolFactory;
using Framework.UI.Base;
using TextSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sample.UI.Panel
{
    public class PanelTextTree : PanelBase
    {
        public TextDataSOTree[] textDataSOTree;
        public int treeId;
        private TextDataSOTree currentData => textDataSOTree[treeId];
        
        
        
        public TMP_Text characterNameText;
        public TMP_Text contentText;
        public Transform optionParent;
        
        Button[] btns = new Button[4];
        LineTree currentLine;
        int childCount;
        bool isChoosing = false;

        void Awake()
        {
            textDataSOTree = Resources.LoadAll<TextDataSOTree>("SO/TextData/Tree");
        }

        void Start()
        {
            UpdateText();
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !isChoosing)
            {
                UpdateText();
            }
        }
        void ToNextLine()
        {
            Debug.Log(currentLine.type);
            if (currentLine.type == LineType.Question)
            {
                isChoosing = true;
                for (int i = 0; i < childCount; i++)
                {
                    btns[i] = PrefabFactoryPool.Instance.Get("OptionButton", true, optionParent).GetComponent<Button>();
                }

                for (int i = 0; i < childCount; i++)
                {
                    //处理选项
                    int index = i;
                    LineTree nextLine;
                    nextLine = currentData.GetNextLine(i);
                    btns[index].GetComponentInChildren<TMP_Text>().text = nextLine.content;
                    btns[index].onClick.AddListener(() =>
                    {
                        currentData.UpdateCurrentLine(nextLine);
                        UpdateText();
                    });
                }
            }
            else if (currentLine.type == LineType.Default)
            {
                var nextLine = currentData.GetNextLine();
                currentData.UpdateCurrentLine(nextLine);
            }
            else if (currentLine.type == LineType.Option)
            {
                var nextLine = currentData.GetNextLine();
                currentData.UpdateCurrentLine(nextLine);

                for (var i = 0; i < btns.Length; i++)
                {
                    var btn = btns[i];
                    if (btn == null) continue;
                    btn.onClick.RemoveAllListeners();
                    PrefabFactoryPool.Instance.Return(btn.gameObject);
                    btns[i] = null;
                }

                isChoosing = false;
                UpdateText();
            }
            else if (currentLine.type == LineType.End)
            {
                Debug.Log("End");
            }

           
        }

        void UpdateText()
        {
            currentLine = currentData.GetCurrentLine(out childCount);
            characterNameText.text = currentLine.character;
            contentText.text = currentLine.content;
            ToNextLine();
        }
    }
}