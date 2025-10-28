using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    public class CurrencyButtonPresenter : IPresenter
    {
        private CurrencyButtonView _view;
        private readonly WalletService _walletService;
        private CurrencyTypes _currencyType;

        private CurrencyRandomizer _currencyRandomizer;
        private CurrencyButtonsConfig _buttonsConfig;

        public CurrencyButtonPresenter(
            CurrencyButtonView view, 
            WalletService walletService,
            ConfigsProviderService configsProviderService, 
            CurrencyTypes currencyType)
        {
            _view = view;
            _walletService = walletService;
            _currencyType = currencyType;

            _currencyRandomizer = new(configsProviderService.GetConfig<CurrencyRangeConfig>());
            _buttonsConfig = configsProviderService.GetConfig<CurrencyButtonsConfig>();
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
        }

        private void OnButtonClicked()
        {
            _walletService.Add(_currencyType, _currencyRandomizer.GetFor(_currencyType));
        }
    }
}