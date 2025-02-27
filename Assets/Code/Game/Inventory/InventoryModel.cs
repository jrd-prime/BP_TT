using System;
using System.Collections.Generic;
using Code.Core.Managers;
using Code.UI.MainUI;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Game.Inventory
{
    [UsedImplicitly]
    public sealed class InventoryModel : IInitializable
    {
        public ReactiveProperty<InventoryData> InventoryData { get; } = new();

        private readonly Dictionary<int, InventoryItem> cache = new();

        private ISettingsManager _settingsManager;
        private InventoryMainSettings _inventorySettings;

        [Inject]
        private void Construct(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public void Initialize()
        {
            Debug.LogWarning("inv model initialized");
            if (_settingsManager == null) throw new NullReferenceException("Settings manager is null");

            _inventorySettings = _settingsManager.GetConfig<InventoryMainSettings>();

            InventoryData.Value = new InventoryData();
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public InventoryItemSettings itemSettings;
        public int count;
    }
}
