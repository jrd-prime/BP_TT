using System;
using Code.Game.Equipment;
using Code.Game.Inventory;
using Code.Game.Weapon;
using Code.UI.MainUI;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Context
{
    public class GameContext : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InventoryModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.Register<WeaponManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<InventoryManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<EquipmentManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();


            builder.Register<IMainUIViewModel, MainUIViewModel>(Lifetime.Singleton).As<IInitializable, IDisposable>();
            builder.Register<IMainUIModel, MainUIModel>(Lifetime.Singleton).As<IInitializable, IDisposable>();
        }
    }
}
