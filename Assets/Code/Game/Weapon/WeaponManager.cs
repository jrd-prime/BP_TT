using System;
using JetBrains.Annotations;
using UnityEngine;
using VContainer.Unity;

namespace Code.Game.Weapon
{
    [UsedImplicitly]
    public sealed class WeaponManager : IInitializable, IDisposable
    {
        public void Initialize()
        {
        }

        public void FireWithRandomBullet()
        {
            //тратит случайный патрон любого типа
            Debug.LogWarning("Fire with random bullet");
        }

        public void Dispose()
        {
        }
    }
}
