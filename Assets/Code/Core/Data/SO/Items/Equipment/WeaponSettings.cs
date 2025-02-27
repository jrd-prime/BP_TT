using System;
using Code.Core.Data.Constants;
using Code.Core.Data.Enums;
using Code.Core.Data.SO.Inventory;
using MessagePack;
using UnityEngine;

namespace Code.Core.Data.SO.Items.Equipment
{
    [MessagePackObject]
    [CreateAssetMenu(fileName = "New Weapon", menuName = SOPathConst.InventoryItemPath, order = 100)]
    public class WeaponSettings : InventoryItemSettings
    {
        [Key(7)] public WeaponType weaponType = WeaponType.NotSet;

        private void OnValidate()
        {
            equipmentType = EquipmentType.Weapon;

            if (weaponType == WeaponType.NotSet) throw new NullReferenceException("Weapon type is not set");
        }
    }
}
