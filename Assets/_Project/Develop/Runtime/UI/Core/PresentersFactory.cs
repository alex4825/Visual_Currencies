using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Menu;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using R3;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public class PresentersFactory
    {
        private readonly WalletService _walletService;
        private readonly ViewsFactory _viewsFactory;
        private readonly ConfigsProviderService _configsProviderService;
        private readonly CurrencyRandomizer _currencyRandomizer;

        public PresentersFactory(
            WalletService walletService, 
            ViewsFactory viewsFactory, 
            ConfigsProviderService configsProviderService, 
            CurrencyRandomizer currencyRandomizer)
        {
            _walletService = walletService;
            _viewsFactory = viewsFactory;
            _configsProviderService = configsProviderService;
            _currencyRandomizer = currencyRandomizer;
        }

        public CurrencyPresenter CreateCurrencyPresenter(
            IconTextView view,
            ReactiveProperty<int> currency,
            CurrencyTypes currencyType)
        {            
            return new CurrencyPresenter(
                currency,
                currencyType,
                _configsProviderService.GetConfig<CurrencyIconsConfig>(),
                view);
        }

        public WalletPresenter CreateWalletPresenter(IconTextListView view)
        {
            return new WalletPresenter(_walletService, this, _viewsFactory, view);
        }

        public MenuButtonsPresenter CreateMenuButtonsPresenter(IReadOnlyList<CurrencyButton> buttons)
        {
            return new MenuButtonsPresenter(buttons, _walletService, _currencyRandomizer);
        }
    }
}