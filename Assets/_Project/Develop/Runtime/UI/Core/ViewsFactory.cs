using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public class ViewsFactory
    {
        public TView Create<TView>(string viewID, Transform parent = null) where TView : MonoBehaviour, IView
        {
            if (ViewsResourcesPaths.TryGetBy(viewID, out string resourcePath) == false)
                throw new ArgumentException($"You didn't set resource path for {typeof(TView)}, searched ID is {viewID}");

            GameObject prefab = Resources.Load<GameObject>(resourcePath);
            GameObject instance = Object.Instantiate(prefab, parent);
            TView view = instance.GetComponent<TView>();

            if (view == null)
                throw new InvalidOperationException($"Not found {typeof(TView)} component on view instance");

            return view;
        }

        public void Release<TView>(TView view) where TView : MonoBehaviour, IView
        {
            if (view != null)
                Object.Destroy(view.gameObject);
        }
    }
}
