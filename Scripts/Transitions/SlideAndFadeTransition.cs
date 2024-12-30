using DG.Tweening;
using JSD.MenuSystem;
using UnityEngine;

namespace JSD.MenuSystem
{
    public class SlideAndFadeTransition : IMenuTransition
    {
        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private float _fadeStart;

        public SlideAndFadeTransition(Vector2 startPosition, Vector2 endPosition, float fadeStart = 0f)
        {
            _startPosition = startPosition;
            _endPosition = endPosition;
            _fadeStart = fadeStart;
        }

        public Tween PlayShowTransition(RectTransform menuTransform, float duration)
        {
            menuTransform.anchoredPosition = _startPosition;
            var canvasGroup = menuTransform.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = menuTransform.gameObject.AddComponent<CanvasGroup>();
            }

            canvasGroup.alpha = _fadeStart;

            var moveTween = menuTransform.DOAnchorPos(_endPosition, duration).SetEase(Ease.OutCubic);
            var fadeTween = canvasGroup.DOFade(1f, duration);

            return DOTween.Sequence().Join(moveTween).Join(fadeTween);
        }

        public Tween PlayHideTransition(RectTransform menuTransform, float duration)
        {
            var canvasGroup = menuTransform.GetComponent<CanvasGroup>();
            if (canvasGroup == null) return null;

            var moveTween = menuTransform.DOAnchorPos(_startPosition, duration).SetEase(Ease.InCubic);
            var fadeTween = canvasGroup.DOFade(0f, duration);

            return DOTween.Sequence().Join(moveTween).Join(fadeTween);
        }
    }
}