using System;
using Code.Core.Data.SO;
using Code.Core.Managers;
using Code.SaveLoad;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Context
{
    public class RootContext : LifetimeScope
    {
        [SerializeField] private MainSettings mainSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            if (!mainSettings) throw new NullReferenceException("Main Settings is null");

            builder.RegisterComponent(mainSettings);

            builder.Register<ISettingsManager, SettingsManager>(Lifetime.Singleton).As<IInitializable, IDisposable>();

            builder.Register<ISaveSystem, MessagePackSaveSystem>(Lifetime.Singleton).As<IInitializable, IDisposable>();

            builder.RegisterEntryPoint<AppStarter>();
        }
    }
}
