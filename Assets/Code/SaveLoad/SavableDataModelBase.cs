using System;
using Code.Core.Data.SO;
using Code.Core.Managers;
using Code.Core.Tools;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.SaveLoad
{
    // TODO : refact, optimize & etc
    public abstract class SavableDataModelBase<TSettings, TSavableDto> : IInitializable, IDisposable
        where TSettings : SettingsBase
    {
        public ReactiveProperty<TSavableDto> ModelData { get; } = new();
        public ReactiveProperty<bool> IsModelLoaded { get; } = new(false);

        protected TSettings ModelSettings { get; private set; }

        protected TSavableDto CachedModelData;

        private ISettingsManager _settingsManager;
        private ISaveSystem _saveSystem;
        private const float SaveDelay = 10f;
        private DateTime _lastSaveTime;
        private TSavableDto _defaultModelData;

        [Inject]
        private void Construct(ISaveSystem iSaveSystem, ISettingsManager settingsManager)
        {
            _saveSystem = iSaveSystem;
            _settingsManager = settingsManager;
        }

        public void Initialize()
        {
            if (_saveSystem == null) throw new NullReferenceException("SaveSystem is null");
            if (_settingsManager == null) throw new NullReferenceException("SettingsManager is null");

            _lastSaveTime = DateTime.UtcNow;

            ModelSettings = _settingsManager.GetConfig<TSettings>();

            InitializeDataModel();
            _defaultModelData = GetDefaultModelData();
            _saveSystem.LoadDataAsync(OnModelDataLoaded, _defaultModelData).Forget();
        }

        protected void OnModelDataUpdated()
        {
            ModelData.Value = CachedModelData;

            ModelData.NotifyIfDataIsClass(); // notify if it is a class

            AutoSave();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void AutoSave()
        {
            var currentTime = DateTime.UtcNow;
            var timeElapsed = (currentTime - _lastSaveTime).TotalSeconds;

            if (timeElapsed < SaveDelay) return;
            _lastSaveTime = currentTime;
            _saveSystem.SaveToFileAsync(CachedModelData).Forget();
        }

        private void Notify() => ModelData.ForceNotify();

        private void OnModelDataLoaded(TSavableDto data)
        {
            CachedModelData = data;
            Debug.LogWarning($"Loaded {GetDebugLine()}");
            OnModelDataUpdated();
            IsModelLoaded.Value = true;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void ShowDebug() => Debug.LogWarning($"{typeof(TSavableDto).Name}: {GetDebugLine()}");


        // ReSharper disable Unity.PerformanceAnalysis
        public async void Dispose()
        {
            try
            {
                await _saveSystem.SaveToFileAsync(ModelData.CurrentValue);
                ModelData?.Dispose();
                IsModelLoaded?.Dispose();
                ShowDebug();
            }
            catch (Exception e)
            {
                Debug.LogWarning("Disposing failed. " + typeof(TSavableDto).Name + " / " + e.Message);
            }
        }

        protected abstract void InitializeDataModel();
        protected abstract TSavableDto GetDefaultModelData();
        protected abstract string GetDebugLine();
    }
}
