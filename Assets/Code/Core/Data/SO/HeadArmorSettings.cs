using Code.Core.Data.Constants;
using MessagePack;
using UnityEngine;

namespace Code.Core.Data.SO
{
    [MessagePackObject]
    [CreateAssetMenu(fileName = "New HeadArmor", menuName = SOPathConst.InventoryItemPath, order = 100)]
    public class HeadArmorSettings : InventoryItemSettings
    {
        [Key(6)] public int absorption = 50;

        private void OnValidate() => equipmentType = EquipmentType.HeadArmor;
    }
}
