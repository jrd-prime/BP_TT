using System;
using System.Collections.Generic;
using Code.Core.Data.SO;
using UnityEngine;
using VContainer;

namespace Code.Core.Managers
{
    public class SettingsManager : ISettingsManager
    {
        public Dictionary<Type, object> ConfigsCache { get; } = new();

        private MainSettings _mainSettings;

        [Inject]
        private void Construct(MainSettings mainSettings) => _mainSettings = mainSettings;

        public void Initialize()
        {
            if (_mainSettings == null) throw new NullReferenceException("Main Settings is null");

            CheckAndAddToCache(_mainSettings.InventoryMainSettings);
        }

        private void CheckAndAddToCache<T>(T settings) where T : SettingsBase
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            if (!ConfigsCache.TryAdd(typeof(T), settings))
                Debug.Log($"Error. When adding to cache {typeof(T)}");
        }

        public T GetConfig<T>() where T : SettingsBase => ConfigsCache[typeof(T)] as T;

        public void Dispose()
        {
        }
    }
}
