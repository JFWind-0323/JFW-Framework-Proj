﻿using System.Collections;
using Framework.EDA;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Framework.Singleton.SceneLoader
{
    public class SceneLoaderBase : MonoSingle<SceneLoaderBase>
    {
        [Header("渐入时长")]
        public float fadeInDuration = 0.5f;

        protected virtual void FadeIn()
        {
            //TODO:渐入写在这里
            //DOTween 或者其他动画都可以
        }

        protected virtual void FadeOut()
        {
            //TODO:渐出写在这里
        }

        private IEnumerator LoadScene(int buildIndex, float duration)
        {
            FadeIn();
            yield return new WaitForSeconds(duration);
            var async = SceneManager.LoadSceneAsync(buildIndex);
            if (async != null) async.completed += OnSceneLoaded;
        }

        private void OnSceneLoaded(AsyncOperation asyncOperation)
        {
            FadeOut();
            EventCenter.Instance.Invoke(EventEnum.OnSceneLoad, SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadScene(int buildIndex)
        {
            StopCoroutine(LoadScene(buildIndex, fadeInDuration));
            StartCoroutine(LoadScene(buildIndex, fadeInDuration));
            
        }
    }
}