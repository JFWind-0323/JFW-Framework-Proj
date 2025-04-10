﻿using Framework.EDA;
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
            EventCenter.Instance.AddListener<int>(EventEnum.SceneLoad, UpdateSceneNameText);
            EventCenter.Instance.AddListener<string>(EventEnum.PlayerStateChanged, UpdatePlayerStateText);
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