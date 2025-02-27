using System;
using System.Collections.Generic;
using Code.Core.Data.SO;
using Code.Core.Managers;
using Code.SaveLoad;
using JetBrains.Annotations;
using MessagePack;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Code.Game.Inventory
{
    public enum SlotState
    {
        Empty,
        Occupied,
        Locked
    }

    [MessagePackObject]
    public sealed class InventoryData : ISavableData
    {
        [Key(0)] public Dictionary<int, InventoryItem> Items { get; private set; }
        [Key(1)] public Dictionary<int, SlotState> Slots { get; private set; }

        public InventoryData(Dictionary<int, InventoryItem> items, Dictionary<int, SlotState> slots)
        {
            Items = items;
            Slots = slots;
        }
    }

    [UsedImplicitly]
    public sealed class InventoryModel : SavableDataModelBase<InventoryMainSettings, InventoryData>
    {
        private ISettingsManager _settingsManager;
        private InventoryMainSettings _inventorySettings;

        [Inject]
        private void Construct(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        protected override void InitializeDataModel()
        {
            Debug.LogWarning("inv model initialized");
            if (_settingsManager == null) throw new NullReferenceException("Settings manager is null");

            _inventorySettings = _settingsManager.GetConfig<InventoryMainSettings>();
        }

        protected override InventoryData GetDefaultModelData()
        {
            var items = new Dictionary<int, InventoryItem>();
            var slots = new Dictionary<int, SlotState>();

            for (var i = 0; i < _inventorySettings.MaxSlots; i++)
            {
                if (i < _inventorySettings.DefaultAvailableSlots)
                {
                    // stopped here
                    items.Add(i, new InventoryItem() { itemSettings = null, count = Random.Range(0, 99) });
                    slots.Add(i, SlotState.Occupied);
                }
            }

            return new InventoryData(new Dictionary<int, InventoryItem>(), new Dictionary<int, SlotState>());
        }

        protected override string GetDebugLine()
        {
            return "inventory debug line";
        }
    }

    [Serializable]
    [MessagePackObject]
    public struct InventoryItem
    {
        [Key(0)] public InventoryItemSettings itemSettings;
        [Key(1)] public int count;
    }
}
