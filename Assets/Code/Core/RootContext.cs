using System;
using Code.Core.Data.SO;
using Code.Core.Managers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core
{
    public class RootContext : LifetimeScope
    {
        [SerializeField] private MainSettings mainSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.LogWarning("RootContext");
            if (!mainSettings) throw new NullReferenceException("Main Settings is null");

            builder.RegisterComponent(mainSettings);

            // Managers
            builder.Register<ISettingsManager, SettingsManager>(Lifetime.Singleton).As<IInitializable, IDisposable>();

            builder.RegisterEntryPoint<AppStarter>();
        }
    }
}
