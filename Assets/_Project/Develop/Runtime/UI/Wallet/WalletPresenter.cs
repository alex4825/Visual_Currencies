﻿using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using System.Collections.Generic;
using System.Linq;

namespace Assets._Project.Develop.Runtime.UI.Wallet
{
    public class WalletPresenter : IPresenter
    {
        private readonly WalletService _walletService;
        private readonly PresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;

        private readonly IconTextListView _view;
        private readonly List<CurrencyPresenter> _currencyPresenters = new();

        public WalletPresenter(WalletService walletService, PresentersFactory presentersFactory, ViewsFactory viewsFactory, IconTextListView view)
        {
            _walletService = walletService;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
        }

        public void Initialize()
        {
            foreach (CurrencyTypes currencyType in _walletService.AvailableCurrencies)
            {
                IconTextView currencyView = _viewsFactory.Create<IconTextView>(ViewIDs.CurrencyView);

                _view.Add(currencyView);

                CurrencyPresenter currencyPresenter = _presentersFactory.CreateCurrencyPresenter(
                    currencyView,
                    _walletService.GetCurrency(currencyType),
                    currencyType);

                currencyPresenter.Initialize();
                _currencyPresenters.Add(currencyPresenter);
            }
        }

        public CurrencyPresenter GetPresenterBy(CurrencyTypes type)
        {
            CurrencyPresenter presenter = _currencyPresenters.First(presenter => presenter.CurrencyType == type);

            return presenter;
        }

        public void Dispose()
        {
            foreach (var currencyPresenter in _currencyPresenters)
            {
                _view.Remove(currencyPresenter.View);
                _viewsFactory.Release(currencyPresenter.View);
                currencyPresenter.Dispose();
            }

            _currencyPresenters.Clear();
        }
    }
}
