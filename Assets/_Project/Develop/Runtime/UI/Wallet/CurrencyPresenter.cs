using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using R3;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Wallet
{
    public class CurrencyPresenter : IPresenter
    {
        private readonly ReactiveProperty<int> _currency;
        private readonly CurrencyTypes _currencyType;
        private readonly CurrencyIconsConfig _currencyIconsConfig;

        private IconTextView _view;

        private IDisposable _disposable;

        public CurrencyPresenter(
            ReactiveProperty<int> currency,
            CurrencyTypes currencyType,
            CurrencyIconsConfig currencyIconsConfig,
            IconTextView view)
        {
            _currency = currency;
            _currencyType = currencyType;
            _currencyIconsConfig = currencyIconsConfig;
            _view = view;
        }
        public CurrencyTypes CurrencyType => _currencyType;

        public IconTextView View => _view;

        public void Initialize()
        {
            UpdateValue(_currency.Value);
            _view.SetIcon(_currencyIconsConfig.GetSpriteFor(_currencyType));

            _disposable = _currency.Subscribe(OnCurrencyChanged);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnCurrencyChanged(int newValue)
        {
            UpdateValue(newValue);
            _view.Shake();
        }

        private void UpdateValue(int newValue) => _view.SetText(newValue.ToString());
    }
}
