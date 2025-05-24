using Framework.UI;
using TextData;
using UnityEngine;

namespace Sample.TextSystem
{
    public class TextPlayerManager : MonoBehaviour
    {
      
        public TextDataSOItemDescription[] itemDescriptionData;
        public TextDataSOTree[] treeData;


        public int itemId;
        public int treeId;

        void Awake()
        {
           
            itemDescriptionData = Resources.LoadAll<TextDataSOItemDescription>("SO/TextData/ItemDescription");
            treeData = Resources.LoadAll<TextDataSOTree>("SO/TextData/Tree");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UIMgr.Instance.PushPanel("PanelTextLinear");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UIMgr.Instance.PushPanel("PanelTextItem");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UIMgr.Instance.PushPanel("PanelTextTree");
            }
        }
    }
}
