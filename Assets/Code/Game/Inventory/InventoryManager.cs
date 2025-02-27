using System;
using JetBrains.Annotations;
using VContainer;
using VContainer.Unity;

namespace Code.Game.Inventory
{
    [UsedImplicitly]
    public sealed class InventoryManager : IInitializable, IDisposable
    {
        private InventoryModel _model;

        [Inject]
        private void Construct(InventoryModel model)
        {
            _model = model;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}
