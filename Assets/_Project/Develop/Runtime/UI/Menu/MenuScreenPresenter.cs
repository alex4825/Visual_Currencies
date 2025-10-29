using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Wallet;
using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    public class MenuScreenPresenter : IPresenter
    {
        private readonly MenuScreenView _screenView;
        private readonly PresentersFactory _presentersFactory;

        private List<IPresenter> _childPresenters = new();

        private WalletService _visualWalletService;
        private WalletPresenter _walletPresenter;

        public MenuScreenPresenter(
            MenuScreenView screenView,
            PresentersFactory presentersFactory)
        {
            _screenView = screenView;
            _presentersFactory = presentersFactory;
        }

        public void Initialize()
        {
            CreateWallet();
            CreateButtons();
            CreateSliders();

            /*foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Initialize();*/

        }

        public void Dispose()
        {
            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Dispose();

            _childPresenters.Clear();
        }

        private void CreateWallet()
        {
            _walletPresenter = _presentersFactory.CreateWalletPresenter(_screenView.WalletView, ref _visualWalletService);
            _walletPresenter.Initialize();
            _childPresenters.Add(_walletPresenter);
        }

        private void CreateButtons()
        {
            foreach (CurrencyTypes type in Enum.GetValues(typeof(CurrencyTypes)))
            {
                CurrencyButtonPresenter buttonPresenter 
                    = _presentersFactory
                        .CreateCurrencyButtonPresenter(_visualWalletService, type, _screenView.ButtonsContainer, _walletPresenter.GetPresenterBy(type).View.transform);

                buttonPresenter.Initialize();
                _childPresenters.Add(buttonPresenter);
            }
        }

        private void CreateSliders()
        {
            foreach (CurrencyTypes type in Enum.GetValues(typeof(CurrencyTypes)))
            {
                CurrencySliderPresenter currencySliderPresenter = _presentersFactory.CreateCurrencySliderPresenter(type, _screenView.SlidersContainer);
                currencySliderPresenter.Initialize();
                _childPresenters.Add(currencySliderPresenter);
            }
        }
    }
}