using Coffee.UIExtensions;
using DG.Tweening;
using R3;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Wallet.Animation
{
    [RequireComponent(typeof(ParticleSystem))]
    [RequireComponent(typeof(UIParticle))]
    public class CurrencyEffectVisualizer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private UIParticle _uiEffect;
        [SerializeField] private CurrencyEffectTypes _effectType;

        [SerializeField] private float _timeToEmit = 1;
        [SerializeField] private float _timeToAttract = 1;
        [SerializeField] private Ease _attractionEaseType = Ease.Linear;

        private UIParticleAttractor _attractor;
        private ReactiveProperty<float> _timeScale;

        private float _emitStartSpeed;
        private float _attractSpeed;
        private float _startLifetime;

        private IDisposable _timeScaleDisposable;

        public void Link(UIParticleAttractor attractor, ReactiveProperty<float> timeScale)
        {
            _attractor = attractor;
            _attractor.AddParticleSystem(_effect);
            _attractSpeed = _attractor.maxSpeed;
            _emitStartSpeed = _effect.main.startSpeed.constantMax;
            _startLifetime = _effect.main.startLifetime.constantMax;

            _timeScale = timeScale;
            _timeScaleDisposable = _timeScale.Subscribe(OnTimeScaleChanged);
            OnTimeScaleChanged(_timeScale.Value);
        }

        public void ShowEffect(int currencyCount)
        {
            //_effect.Emit(currencyCount < _maxParticles ? currencyCount : _maxParticles);

            var emission = _effect.emission;
            emission.rateOverTime = currencyCount;
            _effect.Play();

            //StartCoroutine(EffectRunProcess());
        }

        private void OnDestroy()
        {
            _timeScaleDisposable?.Dispose();
        }

        private IEnumerator EffectRunProcess()
        {
            yield return new WaitForSeconds(_timeToEmit);

            _effect.Pause();

            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[_effect.main.maxParticles];
            _effect.GetParticles(particles);

            foreach (var particle in particles)
            {
                //_effect.Emit
            }
        }

        private void OnTimeScaleChanged(float newValue)
        {
            var main = _effect.main;
            main.startSpeed = _emitStartSpeed * newValue;
            main.startLifetime = _startLifetime / newValue;

            _attractor.maxSpeed = _attractSpeed * newValue;
        }
    }
}