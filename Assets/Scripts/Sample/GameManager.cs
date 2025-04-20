using Config;
using Framework.Audio;
using Framework.EDA;
using Framework.Factory;
using Framework.PoolFactory;
using Framework.UI;
using Framework.UI.Enum;
using Sample.SceneLoder;
using Sample.StateMachine;
using Sample.StateMachine.PlayerState;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sample
{
    public class GameManager : MonoBehaviour
    {
        public PlayerStateMachine playerStateMachine;
        public AudioConfig audioConfig;
        private UIMgr uiMgr;
        private bool IsTabActive;
        
        private EDA_Event<int> onSceneLoad;


        #region Updaters
        void UIUpdater()
        {
            if (!Input.GetKeyDown(KeyCode.Tab)) return;
            if (IsTabActive)
            {
                uiMgr.PopAll();
            }
            else
            {
                uiMgr.PushPanel(UIType.PanelData.ToString());
            }

            IsTabActive = !IsTabActive;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIMgr.Instance.PopAll();
            }
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

        void PlayerBGMWhenSceneIsLoaded(int sceneID)
        {
            AudioMgr.Instance.StopAll();
            switch (sceneID)
            {
                case 0:
                    AudioMgr.Instance.PlayLoop(audioConfig.audioClips[AudioClipType.BGM_1]);
                    break;
                case 1:
                    AudioMgr.Instance.PlayLoop(audioConfig.audioClips[AudioClipType.BGM_2]);
                    break;
            }
        }


        void PoolFactoryUpdater()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                // MonoFactory<MonoProduct> factory = new MonoFactory<MonoProduct>();
                // MonoProduct product = FactoryManager.Instance.Create<MonoProduct>();
                // MonoPool<MonoProduct> monoProductPool = new MonoPool<MonoProduct>(product, null);
                // PoolManager.Instance.Get<MonoProduct>().DoSomething();
                MonoPoolFactory<MonoProduct> poolFactory = new MonoPoolFactory<MonoProduct>();
                poolFactory.Get().DoSomething();
            }
        }
        #endregion

        void Awake()
        {
            EventCenter.Instance.Register(EventEnum.SceneLoad,onSceneLoad);
        }

        void Start()
        {
            uiMgr = UIMgr.Instance;
            uiMgr.LoadPanel("PanelInfo");
            PlayerBGMWhenSceneIsLoaded(SceneManager.GetActiveScene().buildIndex);
            EventCenter.Instance.AddListener<int>(EventEnum.SceneLoad, PlayerBGMWhenSceneIsLoaded);
        }

        void Update()
        {
            UIUpdater();
            StateMachineUpdater();
            SceneLoaderUpdater();
            PoolFactoryUpdater();
        }
    }
}