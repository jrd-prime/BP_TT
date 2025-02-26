using JetBrains.Annotations;
using UnityEngine;

namespace Code.Game.Weapon
{
    [UsedImplicitly]
    public sealed class WeaponManager
    {
        public void FireWithRandomBullet()
        {
            //тратит случайный патрон любого типа
            Debug.LogWarning("Fire with random bullet");
        }
    }
}
