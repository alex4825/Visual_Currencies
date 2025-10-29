using Assets._Project.Develop.Runtime.UI.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    [RequireComponent(typeof(Slider))]
    public class CurrencySliderView : MonoBehaviour, IView
    {
        public event Action<float> ValueChanged;

        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _koefText;

        public void SetKoefText(float value) => _koefText.text = value.ToString("0.00");

        private void Awake()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            ValueChanged?.Invoke(value);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }
    }
}