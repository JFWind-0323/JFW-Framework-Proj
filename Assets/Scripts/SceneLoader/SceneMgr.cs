using System.Collections;
using Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoader
{
    public class SceneMgr : MonoSingle<SceneMgr>
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void FadeIn()
        {
            //TODO:渐入写在这里
            //DOTween 或者其他动画都可以
        }

        private void FadeOut()
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
        }

        public void LoadScene(int buildIndex)
        {
            StopCoroutine(LoadScene(buildIndex, 1f));
            StartCoroutine(LoadScene(buildIndex, 1f));
        }
    }
}