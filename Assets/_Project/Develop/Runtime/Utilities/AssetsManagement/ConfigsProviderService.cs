using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.ConfigsManagement
{
    public class ConfigsProviderService
    {
        private readonly Dictionary<Type, object> _configs = new();

        public T GetConfig<T>() where T : ScriptableObject
        {
            string configPath = null;

            if (_configs.ContainsKey(typeof(T)))
                return (T)_configs[typeof(T)];
            else
                if (ConfigsResourcesPaths.TryGet<T>(out configPath) == false)
                    throw new Exception($"Config {configPath} doesn't exist");

            T config = Resources.Load<T>(configPath);

            _configs.Add(typeof(T), config);

            return config;
        }
    }
}