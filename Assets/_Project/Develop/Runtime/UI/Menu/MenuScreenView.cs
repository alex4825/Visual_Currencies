using Assets._Project.Develop.Runtime.UI.CommonViews;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    public class MenuScreenView : MonoBehaviour
    {
        public event Action GoldButtonClicked;
        public event Action DiamondButtonClicked;
        public event Action EnergyButtonClicked;

        [field: SerializeField] public IconTextListView WalletView { get; private set; }

        public void GoldButtonClick() => GoldButtonClicked?.Invoke();
        public void DiamondButtonClick() => DiamondButtonClicked?.Invoke();
        public void EnergyButtonClick() => EnergyButtonClicked?.Invoke();
    }
}