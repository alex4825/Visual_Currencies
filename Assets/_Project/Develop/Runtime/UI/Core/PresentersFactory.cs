using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Menu;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using R3;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public class PresentersFactory
    {
        private readonly WalletService _walletService;
        private readonly ViewsFactory _viewsFactory;
        private readonly ConfigsProviderService _configsProviderService;
        private readonly UIRoot _uIRoot;

        public PresentersFactory(
            WalletService walletService,
            ViewsFactory viewsFactory,
            ConfigsProviderService configsProviderService,
            UIRoot uIRoot)
        {
            _walletService = walletService;
            _viewsFactory = viewsFactory;
            _configsProviderService = configsProviderService;
            _uIRoot = uIRoot;
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

        public WalletPresenter CreateWalletPresenter(IconTextListView view, ref WalletService visualWalletService)
        {
            visualWalletService = new(_walletService);

            return new WalletPresenter(visualWalletService, this, _viewsFactory, view);
        }

        public CurrencyButtonPresenter CreateCurrencyButtonPresenter(
            WalletService visualWalletService,
            CurrencyTypes currencyType,
            Transform container,
            Transform currencyView)
        {
            CurrencyButtonView view = _viewsFactory.Create<CurrencyButtonView>(ViewIDs.CurrencyButtonView, container);

            return new CurrencyButtonPresenter(view, _walletService, visualWalletService, _configsProviderService, currencyType, currencyView, _uIRoot.VFXLayer);
        }
    }
}