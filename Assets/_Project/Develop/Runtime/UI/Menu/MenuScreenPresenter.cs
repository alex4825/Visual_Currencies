using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Wallet;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    public class MenuScreenPresenter : IPresenter
    {
        private readonly MenuScreenView _screenView;
        private readonly PresentersFactory _presentersFactory;
        private readonly WalletService _walletService;
        private readonly CurrencyRandomizer _currencyRandomizer;

        private List<IPresenter> _childPresenters = new();

        public MenuScreenPresenter(
            MenuScreenView screenView,
            PresentersFactory presentersFactory,
            WalletService walletService,
            CurrencyRandomizer currencyRandomizer)
        {
            _screenView = screenView;
            _presentersFactory = presentersFactory;
            _walletService = walletService;
            _currencyRandomizer = currencyRandomizer;
        }

        public void Initialize()
        {
            _screenView.GoldButtonClicked += OnGoldButtonClicked;
            _screenView.DiamondButtonClicked += OnDiamondButtonClicked;
            _screenView.EnergyButtonClicked += OnEnergyButtonClicked;

            CreateWallet();

            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Initialize();
        }

        public void Dispose()
        {
            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Dispose();

            _childPresenters.Clear();

            _screenView.GoldButtonClicked -= OnGoldButtonClicked;
        }

        private void OnGoldButtonClicked()
        {
            _walletService.Add(CurrencyTypes.Gold, _currencyRandomizer.RandomGold);
        }

        private void OnDiamondButtonClicked()
        {
            _walletService.Add(CurrencyTypes.Diamond, _currencyRandomizer.RandomDiamond);
        }

        private void OnEnergyButtonClicked()
        {
            _walletService.Add(CurrencyTypes.Energy, _currencyRandomizer.RandomEnergy);
        }

        private void CreateWallet()
        {
            WalletPresenter walletPresenter = _presentersFactory.CreateWalletPresenter(_screenView.WalletView);
            _childPresenters.Add(walletPresenter);
        }

    }
}