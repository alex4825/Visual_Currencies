using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Project.Develop.Runtime.UI.Wallet.Animation
{
    public class PointerVisual : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Ease _easeTypeHover = Ease.OutQuad;
        [SerializeField] private Ease _easeTypeClick = Ease.OutQuad;
        [SerializeField] private float _scaleTime = 0.05f;
        [SerializeField] private float _maxScale = 1.1f;

        public void OnPointerEnter(PointerEventData eventData)
        {
#if !UNITY_ANDROID && !UNITY_IOS
            _transform
                .DOScale(transform.localScale * _maxScale, _scaleTime).From(Vector3.one)
                .SetEase(_easeTypeHover);
#endif
        }

        public void OnPointerClick(PointerEventData eventData)
        {
#if UNITY_ANDROID || UNITY_IOS
            _transform
                .DOScale(Vector3.one * _maxScale, _scaleTime / 2)
                .SetEase(_easeTypeClick)
                .OnComplete(() =>
                {
                    _transform
                        .DOScale(Vector3.one, _scaleTime / 2)
                        .SetEase(_easeTypeClick);
                });
#endif
        }

        public void OnPointerExit(PointerEventData eventData)
        {
#if !UNITY_ANDROID && !UNITY_IOS
            _transform
                .DOScale(Vector3.one, _scaleTime)
                .SetEase(_easeTypeHover);
#endif
        }
    }
}