using System;
using R3;
using System.Collections.Generic;
using System.Linq;

namespace Assets._Project.Develop.Runtime.Meta.Features.Wallet
{
    public class WalletService
    {
        private readonly Dictionary<CurrencyTypes, ReactiveProperty<int>> _currencies = new();

        public WalletService()
        {
            foreach (CurrencyTypes type in Enum.GetValues(typeof(CurrencyTypes)))
                _currencies[type] = new ReactiveProperty<int>();
        }

        public List<CurrencyTypes> AvailableCurrencies => _currencies.Keys.ToList();

        public ReactiveProperty<int> GetCurrency(CurrencyTypes currencyType) => _currencies[currencyType];

        public bool Enough(CurrencyTypes currencyType, int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            return _currencies[currencyType].Value >= amount;
        }

        public void Add(CurrencyTypes currencyType, int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _currencies[currencyType].Value += amount;
        }

        public void Spend(CurrencyTypes currencyType, int amount)
        {
            if (Enough(currencyType, amount) == false)
                throw new InvalidOperationException($"Not enough {currencyType} in wallet");

            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _currencies[currencyType].Value -= amount;
        }
    }
}
