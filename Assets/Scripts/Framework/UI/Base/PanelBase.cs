using Framework.UI.Enum;
using UnityEngine;

namespace Framework.UI.Base
{
    public class PanelBase : MonoBehaviour
    {
        /*
         *每个面板的逻辑都需要继承自PanelBase并挂载到物体上
         * 如果没有逻辑，就可以直接挂PanelBase
         */
        public UILayer uiLayer;

        public void OnEnter()
        {
            transform.SetParent(UIMgr.Instance.dicLayer[uiLayer]);
            gameObject.SetActive(true);
            Enter();
        }

        public void OnPause()
        {
            Pause();
        }

        public void OnResume()
        {
            Resume();
        }

        public void OnExit()
        {
            Exit();
            gameObject.SetActive(false);
            
        }

        public virtual void Enter()
        {
            
        }

        public virtual void Pause()
        {
            
        }

        public virtual void Resume()
        {
            
        }

        public virtual void Exit()
        {
            
        }
    }
}