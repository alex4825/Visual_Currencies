using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using R3;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    public class CurrencySliderPresenter : IPresenter
    {
        private const float MaxKoef = 3f;
        private const float InitialKoef = 1f;

        private CurrencySliderView _view;
        private Color _color;

        public CurrencySliderPresenter(CurrencySliderView view, CurrencyTypes currencyType, Color color)
        {
            _view = view;
            CurrencyType = currencyType;
            _color = color;
        }

        public CurrencyTypes CurrencyType { get; private set; }
        public ReactiveProperty<float> TimeScaler { get; private set; }

        public void Initialize()
        {
            _view.ValueChanged += OnValueChanged;

            _view.SetMaxKoefValue(MaxKoef);
            _view.SetColor(_color);

            TimeScaler = new(InitialKoef);
            OnValueChanged(InitialKoef);
        }

        public void Dispose()
        {
            _view.ValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(float newValue)
        {
            TimeScaler.Value = newValue;

            _view.SetKoefText(TimeScaler.Value);
        }
    }
}