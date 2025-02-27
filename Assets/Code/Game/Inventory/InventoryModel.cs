using System;
using System.Collections.Generic;
using Code.Core.Data.Enums;
using Code.Core.Data.SO.Inventory;
using Code.Core.Managers;
using Code.SaveLoad;
using JetBrains.Annotations;
using VContainer;
using Random = UnityEngine.Random;

namespace Code.Game.Inventory
{
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
            if (_settingsManager == null) throw new NullReferenceException("Settings manager is null");

            _inventorySettings = _settingsManager.GetConfig<InventoryMainSettings>();
        }

        protected override InventoryData GetDefaultModelData()
        {
            var items = new Dictionary<int, InventoryItemData>();
            var slots = new Dictionary<int, SlotType>();

            for (var i = 0; i < _inventorySettings.MaxSlots; i++)
            {
                if (i < _inventorySettings.DefaultAvailableSlots)
                {
                    // I'm stopped here
                    items.Add(i, new InventoryItemData() { itemSettings = null, count = Random.Range(0, 99) });
                    slots.Add(i, SlotType.Occupied);
                }
            }

            return new InventoryData(items, slots);
        }

        protected override string GetDebugLine()
        {
            return "inventory debug line";
        }
    }
}
