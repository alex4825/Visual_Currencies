using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Random = UnityEngine.Random;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    public class CurrencyRandomizer
    {
        private CurrencyRangeConfig _currencyRangeConfig;

        public CurrencyRandomizer(ConfigsProviderService configsProviderService)
        {
            _currencyRangeConfig = configsProviderService.GetConfig<CurrencyRangeConfig>();
        }

        public int RandomGold => GetFor(CurrencyTypes.Gold);
        public int RandomDiamond => GetFor(CurrencyTypes.Diamond);
        public int RandomEnergy => GetFor(CurrencyTypes.Energy);

        public int GetFor(CurrencyTypes currency)
        {
            int minValue = _currencyRangeConfig.GetRangeFor(currency).x;
            int maxValue = _currencyRangeConfig.GetRangeFor(currency).y;

            return Random.Range(minValue, maxValue);
        }
    }
}