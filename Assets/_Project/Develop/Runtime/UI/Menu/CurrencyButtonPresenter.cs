using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.UI.Wallet.Animation;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Coffee.UIExtensions;
using R3;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    public class CurrencyButtonPresenter : IPresenter
    {
        private readonly CurrencyButtonView _view;
        private readonly WalletService _originalWalletService;
        private readonly WalletService _visualWalletService;
        private CurrencyTypes _currencyType;
        private Transform _currencyView;
        private Transform _vfxLayer;
        private ReactiveProperty<float> _timeScale;


        private CurrencyRandomizer _currencyRandomizer;
        private CurrencyButtonsConfig _buttonsConfig;

        private CurrencyEffectVisualizer _currencyEffectVisualizer;

        public CurrencyButtonPresenter(
            CurrencyButtonView view,
            WalletService originalWalletService,
            WalletService visualWalletService,
            ConfigsProviderService configsProviderService,
            CurrencyTypes currencyType,
            Transform currencyView,
            Transform vfxLayer,
            ReactiveProperty<float> timeScale)
        {
            _view = view;
            _originalWalletService = originalWalletService;
            _visualWalletService = visualWalletService;
            _currencyType = currencyType;
            _currencyView = currencyView;
            _vfxLayer = vfxLayer;
            _timeScale = timeScale;

            _currencyRandomizer = new(configsProviderService.GetConfig<CurrencyRangeConfig>());
            _buttonsConfig = configsProviderService.GetConfig<CurrencyButtonsConfig>();

            _currencyEffectVisualizer = new(
                _currencyType,
                _buttonsConfig.GetEffectFor(_currencyType),
                _timeScale,
                _view.transform,
                _currencyView.GetComponentInChildren<ParticleAttractor>().transform,
                _visualWalletService,
                _vfxLayer);
        }

        public void Initialize()
        {
            _view.SetTitle(_buttonsConfig.GetNameFor(_currencyType));
            _view.SetSprite(_buttonsConfig.GetSpriteFor(_currencyType));
            _view.SetColor(_buttonsConfig.GetColorFor(_currencyType));

            _view.Clicked += OnButtonClicked;
        }

        public void Dispose()
        {
            _view.Clicked -= OnButtonClicked;
            _currencyEffectVisualizer?.Dispose();
        }

        private void OnButtonClicked()
        {
            int currencyCount = _currencyRandomizer.GetFor(_currencyType);

            _originalWalletService.Add(_currencyType, currencyCount);

            _currencyEffectVisualizer.ShowEffect(currencyCount);
        }
    }
}