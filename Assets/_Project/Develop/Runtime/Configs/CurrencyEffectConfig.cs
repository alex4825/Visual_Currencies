using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Wallet.Animation;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(fileName = "CurrencyEffectConfig", menuName = "Configs/CurrencyEffectConfig")]
    public class CurrencyEffectConfig : ScriptableObject
    {
        [SerializeField]
        [SerializedDictionary("Currency", "Effect")]
        private SerializedDictionary<CurrencyTypes, CurrencyEffectTypes> _values;

        public CurrencyEffectTypes GetAnimationFor(CurrencyTypes currencyType) => _values[currencyType];
    }
}
