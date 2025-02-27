using System;
using Code.Core.Data.Constants;
using MessagePack;
using UnityEngine;

namespace Code.Core.Data.SO
{
    [MessagePackObject]
    [CreateAssetMenu(fileName = "New Ammo", menuName = SOPathConst.InventoryItemPath, order = 100)]
    public class AmmoSettings : InventoryItemSettings
    {
        [Key(4)] public int damage = 10;
        [Key(5)] public BulletType bulletType = BulletType.NotSet;

        private void OnValidate()
        {
            equipmentType = EquipmentType.Ammo;
            if (bulletType == BulletType.NotSet) throw new NullReferenceException("Bullet type is not set");
        }
    }

    public enum BulletType
    {
        NotSet,
        Ordinary,
        ArmorPiercing,
        Incendiary,
        Tracer
    }
}
