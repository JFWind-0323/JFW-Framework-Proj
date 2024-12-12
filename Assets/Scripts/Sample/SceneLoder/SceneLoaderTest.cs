using System;
using Framework.Singleton.SceneLoader;
using UnityEngine;
using DG.Tweening;

namespace Sample.SceneLoder
{
    public enum ScnenTransitionType
    {
        UpAndDown,
        LeftAndRight
    }

    public class SceneLoaderTest : SceneLoaderBase
    {
        public ScnenTransitionType transitionType = ScnenTransitionType.UpAndDown;
        public RectTransform upScreen;
        public RectTransform downScreen;
        public RectTransform leftScreen;
        public RectTransform rightScreen;
        private float defaultUp;
        private float defaultDown;
        private float defaultLeft;
        private float defaultRight;
        

        protected override void WhenInit()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        protected override void FadeIn()
        {
            switch (transitionType)
            {
                case ScnenTransitionType.UpAndDown:
                    UpAndDownFadeIn();
                    break;
                case ScnenTransitionType.LeftAndRight:
                    LeftAndRightFadeIn();
                    break;
            }
        }

        protected override void FadeOut()
        {
            switch (transitionType)
            {
                case ScnenTransitionType.UpAndDown:
                    UpAndDownFadeOut();
                    break;
                case ScnenTransitionType.LeftAndRight:
                    LeftAndRightFadeOut();
                    break;

            }
        }

        void UpAndDownFadeIn()
        {
            upScreen.gameObject.SetActive(true);
            downScreen.gameObject.SetActive(true);
            defaultUp = upScreen.position.y;
            defaultDown = downScreen.position.y;
            upScreen.DOLocalMoveY(0f, 1f);
            downScreen.DOLocalMoveY(0, 1f);
        }

        void UpAndDownFadeOut()
        {
            upScreen.DOMoveY(defaultUp, 1f).OnComplete(() => upScreen.gameObject.SetActive(false));
            downScreen.DOMoveY(defaultDown, 1f).OnComplete(() => downScreen.gameObject.SetActive(false));
        }

        void LeftAndRightFadeIn()
        {
            leftScreen.gameObject.SetActive(true);
            rightScreen.gameObject.SetActive(true);
            defaultLeft = leftScreen.position.x;
            defaultRight = rightScreen.position.x;
            leftScreen.DOLocalMoveX(0f, 1f);
            rightScreen.DOLocalMoveX(0f, 1f);
        }

        void LeftAndRightFadeOut()
        {
            leftScreen.DOMoveX(defaultLeft, 1f).OnComplete(() => leftScreen.gameObject.SetActive(false));
            rightScreen.DOMoveX(defaultRight, 1f).OnComplete(() => rightScreen.gameObject.SetActive(false));
        }
    }
}