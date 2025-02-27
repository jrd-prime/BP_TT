using Code.Core.Data.Constants;
using Code.Core.Data.Enums;
using Code.Core.Data.SO.Inventory;
using MessagePack;
using UnityEngine;

namespace Code.Core.Data.SO.Items.Equipment
{
    [MessagePackObject]
    [CreateAssetMenu(fileName = "New HeadArmor", menuName = SOPathConst.InventoryItemPath, order = 100)]
    public class HeadArmorSettings : InventoryItemSettings
    {
        [Key(6)] public int absorption = 50;

        private void OnValidate() => equipmentType = EquipmentType.HeadArmor;
    }
}
