using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Wallet.Animation;
using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(fileName = "CurrencyButtonsConfig", menuName = "Configs/CurrencyButtonsConfig")]
    public class CurrencyButtonsConfig : ScriptableObject
    {
        [SerializeField]
        [SerializedDictionary("Currency", "Effect")]
        private SerializedDictionary<CurrencyTypes, ButtonInfo> _values;
        
        public string GetNameFor(CurrencyTypes currencyType) => _values[currencyType].Name;
        public Sprite GetSpriteFor(CurrencyTypes currencyType) => _values[currencyType].Sprite;
        public Color GetColorFor(CurrencyTypes currencyType) => _values[currencyType].Color;

        //public CurrencyEffectTypes GetAnimationFor(CurrencyTypes currencyType) => _values[currencyType];

        [Serializable]
        private class ButtonInfo
        {
            public string Name;
            public Sprite Sprite;
            public Color Color;
        }
    }
}
