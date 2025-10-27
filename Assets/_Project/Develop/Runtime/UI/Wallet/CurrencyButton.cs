using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.Wallet
{
    [RequireComponent(typeof(Button))]
    public class CurrencyButton : MonoBehaviour
    {
        public event Action<CurrencyTypes> Clicked;

        [SerializeField] private CurrencyTypes _currency;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick() => Clicked?.Invoke(_currency);

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}