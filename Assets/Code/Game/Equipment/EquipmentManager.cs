using JetBrains.Annotations;
using UnityEngine;

namespace Code.Game.Equipment
{
    [UsedImplicitly]
    public sealed class EquipmentManager
    {
        public void RefillFullAmmoForAllTypes()
        {
            //добавляет полностью заполненный стак патронов каждого типa
            Debug.LogWarning("refill ammo for all types");
        }

        public void AddFullRandomEquipment()
        {
            //добавляет один случайный предмет каждого типа: оружие, голова, торс
            Debug.LogWarning("add full random equipment");
        }

        public void RemoveAllItemsFromRandomSlot()
        {
            //удаляет все предметы из случайного слота, если все слоты пустые: пишет ошибку в консоль
            Debug.LogWarning("remove all items from random slot");
        }
    }
}
