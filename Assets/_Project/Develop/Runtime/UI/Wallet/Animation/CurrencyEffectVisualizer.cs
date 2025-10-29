using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.Other;
using DG.Tweening;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Develop.Runtime.UI.Wallet.Animation
{
    public class CurrencyEffectVisualizer : IDisposable
    {
        private const float MinTimeMultiplier = 0.5f;

        private CurrencyTypes _currencyType;
        private CurrencyEffectConfig _effectConfig;
        private ReactiveProperty<float> _timeScale;
        private Transform _emitter;
        private Transform _attractor;
        private WalletService _visualWalletService;
        private Transform _vfxLayer;

        private ObjectPool<RectTransform> _particlesPool;

        private List<Tween> _particleTweens = new();

        private IDisposable _timeScaleDisposable;

        public CurrencyEffectVisualizer(
            CurrencyTypes currencyType,
            CurrencyEffectConfig effectConfig,
            ReactiveProperty<float> timeScale,
            Transform emitter,
            Transform attractor,
            WalletService visualWalletService,
            Transform vfxLayer)
        {
            _currencyType = currencyType;
            _effectConfig = effectConfig;
            _timeScale = timeScale;
            _emitter = emitter;
            _attractor = attractor;
            _visualWalletService = visualWalletService;
            _vfxLayer = vfxLayer;

            _particlesPool = new(_effectConfig.IconPrefab, _effectConfig.MaxParticles, _vfxLayer);

            _timeScaleDisposable = _timeScale.Subscribe(OnTimeScaleChanged);
            OnTimeScaleChanged(_timeScale.Value);
        }

        public void ShowEffect(int currencyCount)
        {
            Vector2 direction = Vector2.right;

            int particlesCount = Math.Min(currencyCount, _effectConfig.MaxParticles);
            int particleCost = Math.Max(1, currencyCount / particlesCount);
            int costRemain = currencyCount - particleCost * particlesCount;

            int particlesCompletedCount = 0;

            for (int i = 0; i < particlesCount; i++)
            {
                RectTransform particle = _particlesPool.Get(_emitter.position);

                Tween emitTween;

                switch (_effectConfig.EmitType)
                {
                    case EmitTypes.Explosion:
                        emitTween = CreateExplosionEmitTweenFor(particle, ref direction);
                        break;

                    case EmitTypes.AroundCenter:
                        emitTween = CreateAroundCenterEmitTweenFor(particle, ref direction, particlesCount);
                        break;

                    case EmitTypes.SmoothAppearance:
                        emitTween = CreateSmoothAppearanceTweenFor(particle, ref direction);
                        break;

                    default:
                        throw new ArgumentException($"No settings for {_effectConfig.EmitType.ToString()}");
                }

                emitTween
                    .OnComplete(() =>
                        {
                            MoveToAttractor(particle);
                            _particleTweens.Remove(emitTween);
                        });

                _particleTweens.Add(emitTween);
            }

            void MoveToAttractor(RectTransform particle)
            {
                float randomAttractTime = Random.Range(_effectConfig.TimeToAttract * MinTimeMultiplier, _effectConfig.TimeToAttract) / _timeScale.Value;


                Tween attractTween = particle
                        .DOMove(_attractor.position, randomAttractTime)
                        .SetEase(_effectConfig.AttractEaseType);

                attractTween
                    .OnComplete(() =>
                    {
                        _particlesPool.Return(particle);
                        _visualWalletService.Add(_currencyType, particleCost);
                        particlesCompletedCount++;

                        if (particlesCompletedCount >= particlesCount)
                            _visualWalletService.Add(_currencyType, costRemain);

                        _particleTweens.Remove(attractTween);
                    });

                _particleTweens.Add(attractTween);
            }
        }

        public void Dispose()
        {
            _timeScaleDisposable?.Dispose();
        }

        private void OnTimeScaleChanged(float newValue)
        {
            foreach (Tween tween in _particleTweens)
            {
                tween.DOTimeScale(newValue, 0);
            }
        }

        private Tween CreateExplosionEmitTweenFor(RectTransform particle, ref Vector2 direction)
        {
            float randomDistance = Random.Range(_effectConfig.EmitMaxDistance * MinTimeMultiplier, _effectConfig.EmitMaxDistance);

            direction = GetOppositeDirectionWithOffset(direction).normalized;

            float randomEmitTime = Random.Range(_effectConfig.TimeToEmit * MinTimeMultiplier, _effectConfig.TimeToEmit) / _timeScale.Value;

            Vector2 movePoint = particle.anchoredPosition + direction * randomDistance;

            Tween emitTween = particle
                    .DOAnchorPos(movePoint, randomEmitTime)
                    .SetEase(_effectConfig.EmitEaseType);

            return emitTween;
        }

        private Tween CreateAroundCenterEmitTweenFor(RectTransform particle, ref Vector2 direction, int particlesCount)
        {
            direction = GetClockwiseDirectionWithOffset(direction, particlesCount).normalized;

            Vector2 movePoint = particle.anchoredPosition + direction * _effectConfig.EmitMaxDistance;

            Tween emitTween = particle
                    .DOAnchorPos(movePoint, _effectConfig.TimeToEmit)
                    .SetEase(_effectConfig.EmitEaseType);

            return emitTween;
        }

        private Tween CreateSmoothAppearanceTweenFor(RectTransform particle, ref Vector2 direction)
        {
            float randomDistance = Random.Range(_effectConfig.EmitMaxDistance * MinTimeMultiplier, _effectConfig.EmitMaxDistance);

            direction = GetOppositeDirectionWithOffset(direction).normalized;

            particle.anchoredPosition = particle.anchoredPosition + direction * randomDistance;
            //Vector2 movePoint = particle.anchoredPosition + direction * randomDistance;

            float randomShowTime = Random.Range(0, _effectConfig.TimeToEmit * MinTimeMultiplier) / _timeScale.Value;

            Tween emitTween = particle
                    .DOScale(1, randomShowTime).From(0)
                    .SetEase(_effectConfig.EmitEaseType);

            return emitTween;
        }


        private Vector2 GetOppositeDirectionWithOffset(Vector2 direction)
        {
            float lastAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            float newAngle = lastAngle + Random.Range(120f, 270f);

            return new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));
        }

        private Vector2 GetClockwiseDirectionWithOffset(Vector2 lastDirection, int particlesCount)
        {
            float lastAngle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;

            float newAngle = lastAngle + Mathf.Max(360f / particlesCount, 1);

            return new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));
        }
    }
}