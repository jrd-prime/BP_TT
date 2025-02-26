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
        }
    }
}
