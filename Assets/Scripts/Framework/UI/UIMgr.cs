using System.Collections.Generic;
using Framework.Factory;
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

        public Dictionary<UILayer, Transform> dicLayer;

        private Dictionary<UILayer, string> dicLayerName;


        private Stack<PanelBase> stackPanel;

        protected override void WhenInit()

        {
            //设置好MainCanvas
            canvasTf = GameObject.FindWithTag("MainCanvas").transform;
            DontDestroyOnLoad(canvasTf.gameObject);
            InitUILayer();
            LoadLayer();
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
            var panel = PrefabFactory.Instance.Create(panelType, true, canvasTf).GetComponent<PanelBase>();
            if (panel == null)
            {
                Debug.LogWarning("面板预制体加载失败,请检查是否忘记挂载PanelBase及其派生类的脚本");
            }

            return panel;
        }

        /// <summary>
        /// 加载面板，不入栈
        /// </summary>
        /// param dataName="panelType"> 面板预制体名称 /param>
        public void LoadPanel(string panelType)
        {
            //无返回值的重载
            var panel = GetPanel(panelType);
            panel.OnEnter();
        }

        public void LoadPanel(string panelType, out PanelBase panel)
        {
            //有返回值的重载
            panel = GetPanel(panelType);
            panel.OnEnter();
        }

        /// <summary>
        ///     打开面板，入栈
        /// </summary>
        ///param dataName="panelType"> 面板预制体名称 /param>
        public void PushPanel(string panelType)
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
        /// 关闭最上层的面板
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
        /// 获取最上层的面板
        /// </summary>
        /// <returns> 面板的基类 </returns>
        public PanelBase GetTopPanel()
        {
            return stackPanel.Count > 0 ? stackPanel.Peek() : null;
        }
    }
}