using Code.Core.Data.Constants;
using Code.Core.Data.SO;
using UnityEngine;

namespace Code.Game.Inventory
{
    [CreateAssetMenu(fileName = "InventoryMainSettings", menuName = SOPathConst.ConfigPath + "Inventory Settings",
        order = 100)]
    public class InventoryMainSettings : SettingsBase
    {
        [SerializeField] private int maxSlots = 30;
        [SerializeField] private int defaultAvailableSlots = 15;
        [SerializeField] private int itemsInRow = 5;

        public int MaxSlots => maxSlots;
        public int DefaultAvailableSlots => defaultAvailableSlots;
        public int ItemsInRow => itemsInRow;
    }
}
