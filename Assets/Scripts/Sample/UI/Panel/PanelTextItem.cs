using System.Collections;
using System.Collections.Generic;
using Framework.UI.Base;
using TextData;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PanelTextItem : PanelBase
{ 
    
    public TextDataSOItemDescription[] itemDescriptionDatas;
    public TMP_Text itemNameText;
    public TMP_Text text;
    public GameObject itemsParent;
    public int itemDescriptionId;
    private TextDataSOItemDescription currentData=> itemDescriptionDatas[itemDescriptionId];

    void Awake()
    {
        itemDescriptionDatas = Resources.LoadAll<TextDataSOItemDescription>("SO/TextData/ItemDescription");
    }

    protected override void Enter()
    {
        var btns = itemsParent.GetComponentsInChildren<Button>();
        var images = itemsParent.GetComponentsInChildren<Image>();
        for (var index = 1; index < images.Length; index++)
        {
            var image = images[index];


            image.sprite = currentData.GetLineByIndex(index - 1)?.icon;
        }

        for (var index = 0; index < btns.Length; index++)
        {
            var btn = btns[index];
            int itemId = index;
            btn.onClick.AddListener(() => UpdateText(itemId));
        }
    }

    void UpdateText(int itemIndex)
    {
        Debug.Log(itemIndex);
        itemNameText.text = currentData.GetLineByIndex(itemIndex)?.content;
        text.text = currentData.GetLineByIndex(itemIndex)?.description;
    }
}