using System;
using Code.Game.Equipment;
using Code.Game.Inventory;
using Code.Game.Weapon;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.UI.MainUI
{
    public interface IMainUIModel : IInitializable, IDisposable
    {
        public ReadOnlyReactiveProperty<InventoryData> InventoryData { get; }
        public Subject<Unit> OnShootBtnClick { get; }
        public void FireWithRandomBullet();
        public void RefillFullAmmoForAllTypes();
        public void AddFullRandomEquipment();
        public void RemoveAllItemsFromRandomSlot();
    }

    public class MainUIModel : IMainUIModel
    {
        public ReadOnlyReactiveProperty<InventoryData> InventoryData => _inventoryModel.InventoryData;
        public Subject<Unit> OnShootBtnClick { get; } = new();

        private WeaponManager _weaponManager;
        private EquipmentManager _equipmentManager;
        private InventoryModel _inventoryModel;

        [Inject]
        private void Construct(WeaponManager weaponManager, EquipmentManager equipmentManager,
            InventoryModel inventoryModel)
        {
            _weaponManager = weaponManager;
            _equipmentManager = equipmentManager;
            _inventoryModel = inventoryModel;
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }

        public void Initialize()
        {
            Debug.LogWarning("main ui model initialized");
        }

        public void FireWithRandomBullet() => _weaponManager.FireWithRandomBullet();
        public void RefillFullAmmoForAllTypes() => _equipmentManager.RefillFullAmmoForAllTypes();
        public void AddFullRandomEquipment() => _equipmentManager.AddFullRandomEquipment();
        public void RemoveAllItemsFromRandomSlot() => _equipmentManager.RemoveAllItemsFromRandomSlot();
    }

    public enum BulletType
    {
        Ordinary,
        ArmorPiercing,
        Incendiary,
        Tracer
    }
}
