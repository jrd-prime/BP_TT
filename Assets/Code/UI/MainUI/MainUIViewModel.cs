using System;
using Code.Game.Inventory;
using R3;
using VContainer;
using VContainer.Unity;

namespace Code.UI.MainUI
{
    public interface IMainUIViewModel : IInitializable, IDisposable
    {
        public Subject<Unit> ShootBtnClick { get; }
        public Subject<Unit> AddAmmoBtnClick { get; }
        public Subject<Unit> AddEquipmentBtnClick { get; }
        public Subject<Unit> RemoveEquipmentBtnClick { get; }
        public ReadOnlyReactiveProperty<InventoryData> InventoryData { get; }
    }


    public class MainUIViewModel : IMainUIViewModel
    {
        public Subject<Unit> ShootBtnClick { get; } = new();
        public Subject<Unit> AddAmmoBtnClick { get; } = new();
        public Subject<Unit> AddEquipmentBtnClick { get; } = new();
        public Subject<Unit> RemoveEquipmentBtnClick { get; } = new();
        public ReadOnlyReactiveProperty<InventoryData> InventoryData => _model.InventoryData;

        private IMainUIModel _model;
        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(IMainUIModel model) => _model = model;

        public void Initialize()
        {
            if (_model == null) throw new NullReferenceException("Main UI Model is null");

            ShootBtnClick.Subscribe(OnShoot).AddTo(_disposables);
            AddAmmoBtnClick.Subscribe(OnAddAmmo).AddTo(_disposables);
            AddEquipmentBtnClick.Subscribe(OnAddEquipment).AddTo(_disposables);
            RemoveEquipmentBtnClick.Subscribe(OnRemoveEquipment).AddTo(_disposables);
        }

        private void OnShoot(Unit _) => _model.FireWithRandomBullet();
        private void OnAddAmmo(Unit _) => _model.RefillFullAmmoForAllTypes();
        private void OnAddEquipment(Unit _) => _model.AddFullRandomEquipment();
        private void OnRemoveEquipment(Unit _) => _model.RemoveAllItemsFromRandomSlot();

        public void Dispose()
        {
        }
    }
}
