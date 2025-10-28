using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Wallet.Animation;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(fileName = "EffectToParticleConfig", menuName = "Configs/EffectToParticleConfig")]
    public class EffectParticleConfig : ScriptableObject
    {
        [SerializeField]
        [SerializedDictionary("EffectType", "Particle System")]
        private SerializedDictionary<CurrencyEffectTypes, ParticleSystem> _values;

        public ParticleSystem GetParticlesFor(CurrencyEffectTypes effectType) => _values[effectType];
    }
}
