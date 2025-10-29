using DG.Tweening;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(fileName = "CurrencyEffectConfig", menuName = "Configs/CurrencyEffectConfig")]
    public class CurrencyEffectConfig : ScriptableObject
    {
        [field: SerializeField] public float TimeToEmit { get; private set; } = 1;
        [field: SerializeField] public float TimeToAttract { get; private set; } = 1;
        [field: SerializeField] public Ease EmitEaseType { get; private set; } = Ease.Linear;
        [field: SerializeField] public float EmitMaxDistance { get; private set; } = 10;
        [field: SerializeField] public Ease AttractEaseType { get; private set; } = Ease.InOutSine;
        [field: SerializeField] public int MaxParticles { get; private set; } = 100;
        [field: SerializeField] public RectTransform IconPrefab { get; private set; }
    }
}
