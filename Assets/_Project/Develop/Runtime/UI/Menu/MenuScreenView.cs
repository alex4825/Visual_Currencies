using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Wallet;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    public class MenuScreenView : MonoBehaviour
    {
        [SerializeField] private List<CurrencyButton> _buttons;

        [field: SerializeField] public IconTextListView WalletView { get; private set; }

        public IReadOnlyList<CurrencyButton> Buttons => _buttons;
    }
}