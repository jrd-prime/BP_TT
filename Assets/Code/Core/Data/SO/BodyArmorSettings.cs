using Code.Core.Data.Constants;
using MessagePack;
using UnityEngine;

namespace Code.Core.Data.SO
{
    [MessagePackObject]
    [CreateAssetMenu(fileName = "New BodyArmor", menuName = SOPathConst.InventoryItemPath, order = 100)]
    public class BodyArmorSettings : InventoryItemSettings
    {
        [Key(9)] public int absorption = 100;

        private void OnValidate() => equipmentType = EquipmentType.BodyArmor;
    }
}
