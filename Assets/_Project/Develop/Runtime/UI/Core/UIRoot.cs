using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public class UIRoot : MonoBehaviour
    {
        [field: SerializeField] public Transform HUDLayer { get; private set; }
        [field: SerializeField] public Transform VFXLayer { get; private set; }
    }
}