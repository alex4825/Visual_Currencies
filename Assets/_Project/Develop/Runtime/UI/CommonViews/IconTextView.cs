using Assets._Project.Develop.Runtime.UI.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.CommonViews
{
    public class IconTextView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _icon;

        public void SetText(string text) => _text.text = text;
        public void SetIcon(Sprite icon) => _icon.sprite = icon;
        public void Shake()
        {
            _icon.transform
                .DOShakeScale(0.3f, 0.2f, 1)
                .OnComplete(() => _icon.transform.DOScale(Vector3.one, 0.3f));
            
            transform
                .DOShakeScale(0.3f, 0.15f, 1)
                .OnComplete(() => transform.DOScale(Vector3.one, 0.3f));
        }
    }
}
