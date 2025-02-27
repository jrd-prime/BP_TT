using System.Collections.Generic;
using Code.Core.Data.Enums;
using Code.SaveLoad;
using MessagePack;

namespace Code.Game.Inventory
{
    [MessagePackObject]
    public sealed class InventoryData : ISavableData
    {
        [Key(0)] public Dictionary<int, InventoryItemData> Items { get; private set; }
        [Key(1)] public Dictionary<int, SlotType> Slots { get; private set; }

        public InventoryData(Dictionary<int, InventoryItemData> items, Dictionary<int, SlotType> slots)
        {
            Items = items;
            Slots = slots;
        }
    }
}
