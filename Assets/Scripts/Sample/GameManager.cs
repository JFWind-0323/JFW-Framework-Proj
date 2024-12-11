using Framework.UI;
using Framework.UI.Enum;
using Sample.SceneLoder;
using Sample.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sample
{
    public class GameManager : MonoBehaviour
    {
        public PlayerStateMachine playerStateMachine;

        private UIMgr uiMgr;
        private bool IsTabAvtive;


        void UIUpdater()
        {
            if (!Input.GetKeyDown(KeyCode.Tab)) return;
            if (IsTabAvtive)
            {
                uiMgr.PopAll();
            }
            else
            {
                uiMgr.PushPanel(UIType.PanelData);
            }

            IsTabAvtive = !IsTabAvtive;
        }

        void StateMachineUpdater()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                playerStateMachine.SwitchState(typeof(PlayerStateTest));
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                playerStateMachine.SwitchState(typeof(PlayerStateIdle));
            }
        }

        void SceneLoaderUpdater()

        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneLoaderTest.Instance.LoadScene((SceneManager.GetActiveScene().buildIndex +
                                                    1) % SceneManager.sceneCountInBuildSettings);
            }
        }

        void Start()
        {
            uiMgr = UIMgr.Instance;
            uiMgr.LoadPanel(UIType.PanelInfo);
        }

        void Update()
        {
            UIUpdater();
            StateMachineUpdater();
            SceneLoaderUpdater();
        }
    }
}