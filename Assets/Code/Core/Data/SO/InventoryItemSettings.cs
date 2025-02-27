using MessagePack;
using UnityEngine;

namespace Code.Core.Data.SO
{
    [MessagePackObject]
    public class InventoryItemSettings : SettingsBase
    {
        [Key(0)] public string itemName = "NotSet";
        [Key(1)] public EquipmentType equipmentType = EquipmentType.NotSet;
        [Key(2)] public Sprite sprite;
    }

    public enum EquipmentType
    {
        NotSet,
        Weapon,
        HeadArmor,
        BodyArmor,
        Ammo
    }
}
