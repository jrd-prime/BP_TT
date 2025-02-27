using System;
using Code.Game.Equipment;
using Code.Game.Inventory;
using Code.Game.Weapon;
using Code.UI.MainUI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core
{
    public class GameContext : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.LogWarning("game context");

            builder.Register<InventoryModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.Register<WeaponManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<InventoryManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<EquipmentManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();


            builder.Register<IMainUIViewModel, MainUIViewModel>(Lifetime.Singleton).As<IInitializable, IDisposable>();
            builder.Register<IMainUIModel, MainUIModel>(Lifetime.Singleton).As<IInitializable, IDisposable>();
        }
    }
}
