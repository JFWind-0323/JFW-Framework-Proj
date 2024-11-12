using UnityEngine;

namespace UI.Base
{
    public class PanelBase : MonoBehaviour
    {
        public UILayer uiLayer;

        public virtual void OnEnter()
        {
            transform.SetParent(UIMgr.Instance.dicLayer[uiLayer]);
            gameObject.SetActive(true);
        }

        public virtual void OnPause()
        {
        }

        public virtual void OnResume()
        {
        }

        public virtual void OnExit()
        {
            gameObject.SetActive(false);
        }
    }
}