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

        void Awake()
        {
            sceneNameText = transform.GetChild(0).GetComponent<TMP_Text>();
            playerStateText = transform.GetChild(1).GetComponent<TMP_Text>();
            EventCenter.Instance.Subscribe<int>(EventEnum.OnSceneLoad, UpdateSceneNameText);
            EventCenter.Instance.Subscribe<string>(EventEnum.OnPlayerStateChanged, UpdatePlayerStateText);
        }

        void Start()
        {
            
            UpdateSceneNameText(SceneManager.GetActiveScene().buildIndex);
            UpdatePlayerStateText("Idle");
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
            EventCenter.Instance.Unsubscribe<int>(EventEnum.OnSceneLoad, UpdateSceneNameText);
            EventCenter.Instance.Unsubscribe<string>(EventEnum.OnPlayerStateChanged, UpdatePlayerStateText);
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