using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using Coffee.UIExtensions;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.Wallet
{
    [RequireComponent(typeof(Button))]
    //[RequireComponent(typeof(ParticleSystem))]
    //[RequireComponent(typeof(UIParticle))]
    public class CurrencyButtonView : MonoBehaviour, IView
    {
        public event Action Clicked;

        //[SerializeField] private CurrencyTypes _currency;

        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private Image _image;

        //[field: SerializeField] public ParticleSystem _emissionParticleSystem;

        private Button _button;

        public void SetTitle(string text) => _title.text = text;
        public void SetSprite(Sprite sprite) => _image.sprite = sprite;
        public void SetColor(Color color) => _image.color = color;


        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick() => Clicked?.Invoke();

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }

}