using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Utilities.Other;
using DG.Tweening;
using R3;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Develop.Runtime.UI.Wallet.Animation
{
    public class CurrencyEffectVisualizer : IDisposable
    {
        private const float RandomTimeDivider = 2;

        private CurrencyEffectConfig _effectConfig;
        private ReactiveProperty<float> _timeScale;
        private Transform _emitter;
        private Transform _attractor;
        private Transform _vfxLayer;

        private ObjectPool<RectTransform> _particlesPool;

        private IDisposable _timeScaleDisposable;

        public CurrencyEffectVisualizer(
            CurrencyEffectConfig effectConfig,
            ReactiveProperty<float> timeScale,
            Transform emitter,
            Transform attractor,
            Transform vfxLayer)
        {
            _effectConfig = effectConfig;
            _timeScale = timeScale;
            _emitter = emitter;
            _attractor = attractor;
            _vfxLayer = vfxLayer;

            _particlesPool = new(_effectConfig.IconPrefab, _effectConfig.MaxParticles, _vfxLayer);

            _timeScaleDisposable = _timeScale.Subscribe(OnTimeScaleChanged);
            OnTimeScaleChanged(_timeScale.Value);
        }

        public void ShowEffect(int currencyCount)
        {
            Vector2 randomDirection = Vector2.right;

            for (int i = 0; i < currencyCount; i++)
            {
                RectTransform particle = _particlesPool.Get(_emitter.position);

                float randomDistance = Random.Range(_effectConfig.EmitMaxDistance / RandomTimeDivider, _effectConfig.EmitMaxDistance);

                randomDirection = GetOppositeDirectionWithOffset(randomDirection).normalized;

                float randomEmitTime = Random.Range(_effectConfig.TimeToEmit / RandomTimeDivider, _effectConfig.TimeToEmit);

                Vector2 movePoint = particle.anchoredPosition + randomDirection * randomDistance;

                particle
                    .DOAnchorPos(movePoint, randomEmitTime)
                    .SetEase(_effectConfig.EmitEaseType)
                    .OnComplete(() => MoveToAttractor(particle));
            }

            void MoveToAttractor(RectTransform particle)
            {
                float randomAttractTime = Random.Range(_effectConfig.TimeToAttract / RandomTimeDivider, _effectConfig.TimeToAttract);

                particle
                    .DOMove(_attractor.position, randomAttractTime)
                    .SetEase(_effectConfig.AttractEaseType)
                    .OnComplete(() =>
                    {
                        _particlesPool.Return(particle);

                    });
            }

        }

        public void Dispose()
        {
            _timeScaleDisposable?.Dispose();
        }

        private void OnTimeScaleChanged(float newValue)
        {

        }

        private Vector2 GetOppositeDirectionWithOffset(Vector2 lastDirection)
        {
            float lastAngle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;

            float newAngle = lastAngle + Random.Range(150f, 210f);

            return new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));
        }
    }
}