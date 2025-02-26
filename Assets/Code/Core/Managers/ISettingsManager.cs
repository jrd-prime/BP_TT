using System;
using System.Collections.Generic;
using Code.Core.Data.SO;
using VContainer.Unity;

namespace Code.Core.Managers
{
    public interface ISettingsManager : IInitializable, IDisposable
    {
        public Dictionary<Type, object> ConfigsCache { get; }
        public T GetConfig<T>() where T : SettingsBase;
    }
}
