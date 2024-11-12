using System;
using System.Collections.Generic;
using Singleton;
using UI.Base;
using UnityEngine;
using Utilities;

namespace UI
{
    public partial class UIMgr : MonoSingle<UIMgr>
    {
        //Canvas
        public Transform canvasTf;
        private readonly string uiPrefabPath = "Prefabs/UIPrefab";

        public Dictionary<UILayer, Transform> dicLayer;

        private Dictionary<UILayer, string> dicLayerName;

        private Dictionary<string, PanelBase> dicPanel;

        //所有UI prefab在Resource下的路径
        private Dictionary<string, string> dicPath;

        private Stack<PanelBase> stackPanel;


        private void Awake()
        {
            //设置好MainCanvas
            canvasTf = GameObject.FindWithTag("MainCanvas").transform;

            InitPath();
            InitUILayer();
            LoadLayer();
        }

        private void InitPath()
        {
            //从UIPrefab文件夹中找到所有UI的预制体
            //将他们的名称和路径键值对添加到dicPath中
            var prefabs = Resources.LoadAll<GameObject>(uiPrefabPath);
            dicPath = new Dictionary<string, string>();
            foreach (var item in prefabs)
            {
                if (dicPath.ContainsKey(item.name))
                    dicPath[item.name] = $"Prefabs/UIPrefab/{item.name}";
                else
                    dicPath.Add(item.name, $"Prefabs/UIPrefab/{item.name}");
                Debug.Log(item.name);
            }
        }

        private void InitUILayer()
        {
            dicLayerName = new Dictionary<UILayer, string>();
            //遍历枚举，添加进UILayer字典
            foreach (UILayer item in Enum.GetValues(typeof(UILayer))) dicLayerName.Add(item, item.ToString());
            // Debug.Log(item.ToString());
        }

        private void LoadLayer()
        {
            dicLayer = new Dictionary<UILayer, Transform>();
            //创建空物体作为Layer
            foreach (var item in dicLayerName.Keys)
            {
                var layer = new GameObject(dicLayerName[item]);
                var tf = layer.AddComponent<RectTransform>();
                tf.SetParent(canvasTf);
                UIAnchorUtility.FillTheCanvas(tf);
                dicLayer.Add(item, layer.transform);
            }
        }
    }

    public partial class UIMgr
    {
        //获取dicPanel中存储的基层BasePanel的类，如果为空，先加载添加进去
        private PanelBase GetPanel(string panelType)
        {
            if (dicPanel == null) dicPanel = new Dictionary<string, PanelBase>();
            if (dicPanel.TryGetValue(panelType, out var panel1)) return panel1;

            var path = string.Empty;
            if (dicPath.TryGetValue(panelType, out var value))
            {
                path = value;
                Debug.Log(path);
            }
            else
            {
                Debug.LogWarning("没有该面板的预制体，或检查路径下预制体的名称与UIType中的枚举是否不一致");
                return null;
            }


            var go = Resources.Load<GameObject>(path);
            var goPanel = Instantiate(go, canvasTf, false);
            var panel = goPanel.GetComponent<PanelBase>();
            if (panel == null) panel = goPanel.AddComponent<PanelBase>();
            dicPanel.Add(panelType, panel);
            return panel;
        }

        /// <summary>
        ///     加载面板，不入栈
        /// </summary>
        /// <param name="panelType"> 面板种类 </param>
        public void LoadPanel(UIType panelType)
        {
            var type = panelType.ToString();
            //无返回值的重载
            var panel = GetPanel(type);
            panel.OnEnter();
        }

        public void LoadPanel(UIType panelType, out PanelBase panel)
        {
            //有返回值的重载
            var type = panelType.ToString();
            panel = GetPanel(type);
            panel.OnEnter();
        }

        /// <summary>
        ///     打开面板，入栈
        /// </summary>
        /// <param name="panelType"> 面板种类 </param>
        public void PushPanel(UIType panelType)
        {
            if (stackPanel == null)
                stackPanel = new Stack<PanelBase>();
            if (stackPanel.Count > 0)
            {
                var top = stackPanel.Peek();
                top.OnPause();
            }

            PanelBase panel;
            LoadPanel(panelType, out panel);
            stackPanel.Push(panel);
        }

        /// <summary>
        ///     关闭最上层的面板
        /// </summary>
        public void PopPanel()
        {
            if (stackPanel == null) stackPanel = new Stack<PanelBase>();

            if (stackPanel.Count <= 0) return;

            var top = stackPanel.Pop();
            top.OnExit();

            if (stackPanel.Count > 0)
            {
                var panel = stackPanel.Peek();
                panel.OnResume();
            }
        }

        /// <summary>
        ///     获取最上层的面板
        /// </summary>
        /// <returns> 面板的基类 </returns>
        public PanelBase GetTopPanel()
        {
            if (stackPanel.Count > 0)
                return stackPanel.Peek();
            return null;
        }
    }
}