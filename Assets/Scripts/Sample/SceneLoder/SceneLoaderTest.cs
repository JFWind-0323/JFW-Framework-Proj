using Framework.Singleton.SceneLoader;
using UnityEngine;
using DG.Tweening;

namespace Sample.SceneLoder
{
    public class SceneLoaderTest : SceneLoaderBase
    {
        public RectTransform upScreen;
        public RectTransform downScreen;
        private float defaultUp;
        private float defaultDown;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this.gameObject);
        }

        protected override void FadeIn()
        {
            UpAndDownFadeIn();
        }

        protected override void FadeOut()
        {
            UpAndDownFadeOut();
        }

        void UpAndDownFadeIn()
        {
            upScreen.gameObject.SetActive(true);
            downScreen.gameObject.SetActive(true);
            defaultUp = upScreen.position.y;
            defaultDown = downScreen.position.y;
            upScreen.DOLocalMoveY(0f,1f);
            downScreen.DOLocalMoveY(0, 1f);
        }

        void UpAndDownFadeOut()
        {
            upScreen.DOMoveY(defaultUp, 1f).OnComplete(() => upScreen.gameObject.SetActive(false));
            downScreen.DOMoveY(defaultDown, 1f).OnComplete(() => downScreen.gameObject.SetActive(false));
        }

        void LeftAndRightFadeIn()
        {
        }

        void LeftAndRightFadeOut()
        {
            
        }
    }
}