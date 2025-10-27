using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    public class MenuButtonsPresenter : IPresenter
    {
        private readonly MenuScreenView _screenView;
        private readonly WalletService _walletService;
        private readonly CurrencyRandomizer _currencyRandomizer;

        public MenuButtonsPresenter(
            MenuScreenView screenView,
            WalletService walletService,
            CurrencyRandomizer currencyRandomizer)
        {
            _screenView = screenView;
            _walletService = walletService;
            _currencyRandomizer = currencyRandomizer;
        }

        public void Initialize()
        {
            _screenView.GoldButtonClicked += OnGoldButtonClicked;
            _screenView.DiamondButtonClicked += OnDiamondButtonClicked;
            _screenView.EnergyButtonClicked += OnEnergyButtonClicked;
        }

        public void Dispose()
        {
            _screenView.GoldButtonClicked -= OnGoldButtonClicked; 
            _screenView.DiamondButtonClicked -= OnDiamondButtonClicked;
            _screenView.EnergyButtonClicked -= OnEnergyButtonClicked;
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
    }
}