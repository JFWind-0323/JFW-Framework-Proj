using System.Collections.Generic;
using Framework.Singleton;
using Framework.UI.Base;
using Framework.UI.Enum;
using UnityEngine;
using Utilities;

namespace Framework.UI
{
    public partial class UIMgr : MonoSingle<UIMgr>
    {
        /*
         * UI管理器，请务必注意UI层级关系
         * 最适合该框架的关系如下
         * MainPanel
         * - Panel1
         *  - - Panel1-1
         * - Panel2
         *  - - Panel2-1
         *  - - Panel2-2
         *  - - Panel2-3
         * - Panel3
         *  - -etc
         * 举例：
         * 点击ESC->打开菜单栏
         * 菜单栏
         *   - 设置
         *      - - 声音
         *      - - 画面
         *      - - 通用
         *   - 关于
         *   - 退出游戏
         * 注意文件夹结构，UI预制体放在/Assets/Resources/Prefabs/UIPrefab下，有特殊需求自行修改
         */
        
        //Canvas
        public Transform canvasTf;
        private readonly string uiPrefabPath = "Prefabs/UIPrefab";

        public Dictionary<UILayer, Transform> dicLayer;

        private Dictionary<UILayer, string> dicLayerName;

        private Dictionary<string, PanelBase> dicPanel;

        //所有UI prefab在Resource下的路径
        private Dictionary<string, string> dicPath;

        private Stack<PanelBase> stackPanel;

        protected override void WhenInit()

        {
            //设置好MainCanvas
            canvasTf = GameObject.FindWithTag("MainCanvas").transform;
            DontDestroyOnLoad(canvasTf.gameObject);
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
                //Debug.Log(item.dataName);
            }
        }

        private void InitUILayer()
        {
            dicLayerName = new Dictionary<UILayer, string>();
            //遍历枚举，添加进UILayer字典
            foreach (UILayer item in System.Enum.GetValues(typeof(UILayer))) dicLayerName.Add(item, item.ToString());
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
                //Debug.Log(path);
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
        /// <param dataName="panelType"> 面板种类 </param>
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
        /// <param dataName="panelType"> 面板种类 </param>
        public void PushPanel(UIType panelType)
        {
            if (stackPanel == null)
                stackPanel = new Stack<PanelBase>();
            if (stackPanel.Count > 0)
            {
                var top = stackPanel.Peek();
                top.OnPause();
            }

            LoadPanel(panelType, out var panel);
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

            if (stackPanel.Count <= 0) return;
            var panel = stackPanel.Peek();
            panel.OnResume();
        }

        public void PopAll()
        {
            if (stackPanel == null)
                return;
            while (stackPanel.Count > 0)
            {
                PopPanel();
            }
        }

        /// <summary>
        ///     获取最上层的面板
        /// </summary>
        /// <returns> 面板的基类 </returns>
        public PanelBase GetTopPanel()
        {
            return stackPanel.Count > 0 ? stackPanel.Peek() : null;
        }
    }
}