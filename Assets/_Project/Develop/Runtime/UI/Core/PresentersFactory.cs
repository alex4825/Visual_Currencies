using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Menu;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using R3;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public class PresentersFactory
    {
        private readonly WalletService _walletService;
        private readonly ViewsFactory _viewsFactory;
        private readonly ConfigsProviderService _configsProviderService;

        public PresentersFactory(
            WalletService walletService, 
            ViewsFactory viewsFactory, 
            ConfigsProviderService configsProviderService)
        {
            _walletService = walletService;
            _viewsFactory = viewsFactory;
            _configsProviderService = configsProviderService;
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

        public CurrencyButtonPresenter CreateCurrencyButtonPresenter(CurrencyPresenter currencyPresenter, CurrencyTypes currencyType, Transform container)
        {
            CurrencyButtonView view = _viewsFactory.Create<CurrencyButtonView>(ViewIDs.CurrencyButtonView, container);

            return new CurrencyButtonPresenter(view, _walletService, _configsProviderService, currencyType, currencyPresenter);
        }
    }
}