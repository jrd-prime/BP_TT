using System;
using Code.Core.Data.SO.Inventory;
using MessagePack;

namespace Code.Game.Inventory
{
    [Serializable]
    [MessagePackObject]
    public struct InventoryItemData
    {
        [Key(0)] public InventoryItemSettings itemSettings;
        [Key(1)] public int count;
    }
}
