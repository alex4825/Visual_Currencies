using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Menu;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using R3;
using System;
using UnityEngine;
using VContainer;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public class PresentersFactory
    {
        [Inject]
        private readonly WalletService _walletService;

        [Inject]
        private readonly ViewsFactory _viewsFactory;

        public CurrencyPresenter CreateCurrencyPresenter(
            IconTextView view,
            ReactiveProperty<int> currency,
            CurrencyTypes currencyType)
        {
            if (AssetsResourcesPaths.TryGetBy(AssetsIDs.CurrencyIconsConfig, out string configPath) == false)
                throw new Exception($"Config {configPath} doesn't exist");

            return new CurrencyPresenter(
                currency,
                currencyType,
                Resources.Load<CurrencyIconsConfig>(configPath),
                view);
        }

        public WalletPresenter CreateWalletPresenter(IconTextListView view)
        {
            return new WalletPresenter(_walletService, this, _viewsFactory, view);
        }
    }
}