using System;
using Code.Game.Equipment;
using Code.Game.Inventory;
using Code.Game.Weapon;
using R3;
using VContainer;
using VContainer.Unity;

namespace Code.UI.MainUI
{
    public interface IMainUIModel : IInitializable, IDisposable
    {
        public ReadOnlyReactiveProperty<InventoryData> InventoryData { get; }
        public void FireWithRandomBullet();
        public void RefillFullAmmoForAllTypes();
        public void AddFullRandomEquipment();
        public void RemoveAllItemsFromRandomSlot();
    }

    public class MainUIModel : IMainUIModel
    {
        public ReadOnlyReactiveProperty<InventoryData> InventoryData => _inventory.ModelData;

        private WeaponManager _weaponManager;
        private EquipmentManager _equipmentManager;
        private InventoryModel _inventory;

        [Inject]
        private void Construct(WeaponManager weaponManager, EquipmentManager equipmentManager, InventoryModel inventory)
        {
            _weaponManager = weaponManager;
            _equipmentManager = equipmentManager;
            _inventory = inventory;
        }

        public void Initialize()
        {
        }

        public void FireWithRandomBullet() => _weaponManager.FireWithRandomBullet();

        public void RefillFullAmmoForAllTypes() => _equipmentManager.RefillFullAmmoForAllTypes();

        public void AddFullRandomEquipment() => _equipmentManager.AddFullRandomEquipment();

        public void RemoveAllItemsFromRandomSlot() => _equipmentManager.RemoveAllItemsFromRandomSlot();

        public void Dispose()
        {
        }
    }
}
