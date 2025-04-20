using System.Collections;
using System.Collections.Generic;
using Framework.UI.Base;
using TextSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelTextItem : PanelBase
{
    public TextDataSOItemDescription dataItem;
    public TMP_Text itemName;
    public TMP_Text text;
    public GameObject itemsParent;

    protected override void Enter()
    {
        var btns = itemsParent.GetComponentsInChildren<Button>();
        var images = itemsParent.GetComponentsInChildren<Image>();
        for (var index = 1; index < images.Length; index++)
        {
            var image = images[index];


            image.sprite = dataItem.GetLineByIndex(index - 1)?.icon;
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
        itemName.text = dataItem.GetLineByIndex(itemIndex)?.text;
        text.text = dataItem.GetLineByIndex(itemIndex)?.description;
    }
}