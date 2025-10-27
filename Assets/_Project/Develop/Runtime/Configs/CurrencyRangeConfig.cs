using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(fileName = "CurrencyRangeConfig", menuName = "Configs/CurrencyRangeConfig")]
    public class CurrencyRangeConfig : ScriptableObject
    {
        [SerializeField]
        [SerializedDictionary("Currency", "Range")]
        private SerializedDictionary<CurrencyTypes, Vector2Int> _values;

        public Vector2Int GetRangeFor(CurrencyTypes currencyType) => _values[currencyType];

        public void OnValidate()
        {
            var keys = new List<CurrencyTypes>(_values.Keys);

            foreach (var key in keys)
            {
                Vector2Int currentValue = _values[key];
                Vector2Int newValue = currentValue;

                if (newValue.x < 0)
                    newValue.x = 0;

                if (newValue.y < 0)
                    newValue.y = 0;

                if (newValue.x > newValue.y)
                    newValue.x = newValue.y;

                if (newValue != currentValue)
                    _values[key] = newValue;
            }
        }
    }
}
