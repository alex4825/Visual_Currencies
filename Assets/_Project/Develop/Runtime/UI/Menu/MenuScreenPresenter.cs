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

        private List<IPresenter> _childPresenters = new();

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
            CreateButtonsPresenter();

            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Initialize();
        }

        public void Dispose()
        {
            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Dispose();

            _childPresenters.Clear();
        }

        private void CreateWallet()
        {
            WalletPresenter walletPresenter = _presentersFactory.CreateWalletPresenter(_screenView.WalletView);
            _childPresenters.Add(walletPresenter);
        }

        private void CreateButtonsPresenter()
        {
            MenuButtonsPresenter buttonsPresenter = _presentersFactory.CreateMenuButtonsPresenter(_screenView);
            _childPresenters.Add(buttonsPresenter);
        }

    }
}