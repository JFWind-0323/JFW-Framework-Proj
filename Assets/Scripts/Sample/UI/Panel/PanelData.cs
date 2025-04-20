using System;
using System.Collections.Generic;
using Framework.UI.Base;
using System.Reflection;
using UnityEngine;
using TMPro;
using Utilities;

namespace Sample.UI.Panel
{
    public class PanelData : PanelBase
    {
        private TMP_Dropdown dropdown;
        private List<TMP_Dropdown.OptionData> options;
        private Dictionary<int, ScriptableObject> optionsSODict;
        public TMP_Text text;
        public Transform content;

        private void Start()
        {
            Init();
        }

        protected override void Enter()
        {
        }

        void Init()
        {
            if (dropdown == null)
                dropdown = GetComponentInChildren<TMP_Dropdown>();
            options = dropdown.options;
            options.Clear();
            if (optionsSODict == null)
                optionsSODict = new Dictionary<int, ScriptableObject>();
            LoadAllData();
            SelectItem(0);
            dropdown.onValueChanged.AddListener(SelectItem);
        }

        private void LoadAllData()
        {
            var SOArray = Resources.LoadAll<ScriptableObject>("SO");
            //Debug.Log(SOArray.Length);
            foreach (var SO in SOArray)
            {
                options.Add(new TMP_Dropdown.OptionData(SO.name));
            }

            LinearTablesTool.InitDictFromArray(SOArray, out optionsSODict);
        }

        void SelectItem(int index)
        {
            var so = optionsSODict[index];
            Type soType = so.GetType();
            FieldInfo[] fields = soType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            string textValue = string.Empty;
            foreach (var field in fields)
            {
                object value = field.GetValue(so);
                if (value == null)
                {
                    textValue = string.Empty;
                }

                // 检查是否是数组
                else if (value.GetType().IsArray)
                {
                    Array array = (Array)value;
                    textValue += $"{field.Name} (Array): ";
                    foreach (var item in array)
                    {
                        textValue += item.ToString() + ", ";
                    }

                    textValue = textValue.TrimEnd(',', ' ') + "\n"; // 去掉最后的逗号
                }
                // 检查是否是列表或集合
                else if (value is System.Collections.IEnumerable enumerable)
                {
                    if (value is string)
                    {
                        textValue += $"{field.Name}: {value} \n";
                    }
                    else
                    {
                        textValue += $" \n{field.Name}:\n";
                        foreach (var item in enumerable)
                        {
                            textValue += item + "\n";
                        }

                        textValue = textValue.TrimEnd(',', ' '); // 去掉最后的逗号
                    }
                }
                else
                {
                    textValue += $"{field.Name}: {value}\n"; // 普通字段
                }

                //Debug.Log(field.Name + " : " + value);
            }

            text.text = textValue; // 更新文本
        }
    }
}