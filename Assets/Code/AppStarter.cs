using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code
{
    public sealed class AppStarter : IInitializable
    {
        [Inject]
        private void Construct(IObjectResolver container)
        {
            Debug.LogWarning("Hello constructor");
        }

        public async void Initialize()
        {
            Debug.LogWarning("Hello");
        }
    }
}
