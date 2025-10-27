using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Wallet;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    public class MenuButtonsPresenter : IPresenter
    {
        private IReadOnlyList<CurrencyButton> _buttons;
        private readonly WalletService _walletService;
        private readonly CurrencyRandomizer _currencyRandomizer;

        public MenuButtonsPresenter(
            IReadOnlyList<CurrencyButton> buttons,
            WalletService walletService,
            CurrencyRandomizer currencyRandomizer)
        {
            _buttons = buttons;
            _walletService = walletService;
            _currencyRandomizer = currencyRandomizer;
        }

        public void Initialize()
        {
            foreach (CurrencyButton button in _buttons)
                button.Clicked += OnButtonClicked;
        }

        public void Dispose()
        {
            foreach (CurrencyButton button in _buttons)
                button.Clicked -= OnButtonClicked;
        }

        private void OnButtonClicked(CurrencyTypes currency)
        {
            _walletService.Add(currency, _currencyRandomizer.GetFor(currency));
        }
    }
}