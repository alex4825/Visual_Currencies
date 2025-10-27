using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(fileName = "CurrencyIconsConfig", menuName = "Configs/CurrencyIconsConfig")]
    public class CurrencyIconsConfig : ScriptableObject
    {
        [SerializeField]
        [SerializedDictionary("Currency", "Sprite")]
        private SerializedDictionary<CurrencyTypes, Sprite> _values;

        public Sprite GetSpriteFor(CurrencyTypes currencyType) => _values[currencyType];
    }
}
