using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Wallet;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    public class MenuScreenView : MonoBehaviour
    {
        [field: SerializeField] public IconTextListView WalletView { get; private set; }

        [field: SerializeField] public Transform ButtonsContainer { get; private set; }
        //[field: SerializeField] public Transform SlidersContainer { get; private set; }
    }
}