using Assets._Project.Develop.Runtime.Configs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.AssetsManagement
{
    public class ConfigsResourcesPaths
    {
        private static Dictionary<Type, string> _viewIDToResourcesPath = new Dictionary<Type, string>()
        {
            {typeof(CurrencyIconsConfig), "Configs/CurrencyIconsConfig" },
            {typeof(CurrencyRangeConfig), "Configs/CurrencyRangeConfig" },
            {typeof(CurrencyButtonsConfig), "Configs/CurrencyButtonsConfig" },
        };

        public static bool TryGet<T>(out string path) where T : ScriptableObject
        {
            if (_viewIDToResourcesPath.ContainsKey(typeof(T)))
            {
                path = _viewIDToResourcesPath[typeof(T)];
                return true;
            }

            path = null;
            return false;
        }
    }
}
