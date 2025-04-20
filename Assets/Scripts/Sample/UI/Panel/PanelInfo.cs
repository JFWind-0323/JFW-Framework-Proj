using Framework.EDA;
using Framework.UI.Base;
using TMPro;
using UnityEngine.SceneManagement;

namespace Sample.UI.Panel
{
    public class PanelInfo : PanelBase
    {
        private TMP_Text sceneNameText;
        private TMP_Text playerStateText;
        
        private EDA_Event<int> onSceneLoaded;
        private EDA_Event<string> onPlayerStateChanged;

        void Awake()
        {
            sceneNameText = transform.GetChild(0).GetComponent<TMP_Text>();
            playerStateText = transform.GetChild(1).GetComponent<TMP_Text>();
            
            EventCenter.Instance.Register(EventEnum.SceneLoad, onSceneLoaded);
            EventCenter.Instance.Register(EventEnum.PlayerStateChanged, onPlayerStateChanged);
            
            EventCenter.Instance.AddListener<int>(EventEnum.SceneLoad, UpdateSceneNameText);
            EventCenter.Instance.AddListener<string>(EventEnum.PlayerStateChanged, UpdatePlayerStateText);
        }

        void Start()
        {
            
            UpdateSceneNameText(SceneManager.GetActiveScene().buildIndex);
            UpdatePlayerStateText("Idle");
        }

        protected override void Enter()
        {
        }

        protected override void Exit()
        {
            EventCenter.Instance.RemoveListener<int>(EventEnum.SceneLoad, UpdateSceneNameText);
            EventCenter.Instance.RemoveListener<string>(EventEnum.PlayerStateChanged, UpdatePlayerStateText);
        }


        void UpdateSceneNameText(int sceneIndex)
        {
            sceneNameText.text = "场景名称: " + SceneManager.GetSceneByBuildIndex(sceneIndex).name;
        }

        void UpdatePlayerStateText(string playerStateName)
        {
            playerStateText.text = "玩家状态: " + playerStateName;
        }
    }
}