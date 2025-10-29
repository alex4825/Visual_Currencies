using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using R3;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    public class CurrencySliderPresenter : IPresenter
    {
        private const float MaxKoef = 3f;

        private CurrencySliderView _view;

        public CurrencySliderPresenter(CurrencySliderView view, CurrencyTypes currencyType)
        {
            _view = view;
            CurrencyType = currencyType;
            TimeScaler = new(1);
        }

        public CurrencyTypes CurrencyType { get; private set; }
        public ReactiveProperty<float> TimeScaler { get; private set; }

        public void Initialize()
        {
            _view.ValueChanged += OnValueChanged;
        }

        public void Dispose()
        {
            _view.ValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(float newValue)
        {
            TimeScaler.Value = newValue * MaxKoef;

            _view.SetKoefText(TimeScaler.Value);
        }
    }
}