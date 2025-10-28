using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using R3;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Wallet.Animation
{
    public class CurrencyEffectPresenter : IPresenter
    {
        private CurrencyEffectConfig _currencyEffectConfig;
        private EffectParticleConfig _effectParticleConfig;

        private readonly ReactiveProperty<int> _originalCurrency;
        private readonly CurrencyTypes _currencyType;
        private readonly ReactiveProperty<int> _visualCurrency;

        private IDisposable _originalCurrencyDisposable;

        public CurrencyEffectPresenter(
            ConfigsProviderService configsProviderService,
            ReactiveProperty<int> originalCurrency,
            CurrencyTypes currencyType,
            ReactiveProperty<int> visualCurrency)
        {
            _currencyEffectConfig = configsProviderService.GetConfig<CurrencyEffectConfig>();
            _effectParticleConfig = configsProviderService.GetConfig<EffectParticleConfig>();
            _originalCurrency = originalCurrency;
            _currencyType = currencyType;
            _visualCurrency = visualCurrency;
        }

        public void Initialize()
        {
            _originalCurrencyDisposable = _originalCurrency.Subscribe(ShowEffect);
        }

        public void Dispose()
        {
            _originalCurrencyDisposable?.Dispose();
        }

        private void ShowEffect(int newValue)
        {
            CurrencyEffectTypes effectType = _currencyEffectConfig.GetAnimationFor(_currencyType);

            ParticleSystem prefab = _effectParticleConfig.GetParticlesFor(effectType);
        }
    }
}