using Code.Core.Data.Constants;
using Code.Core.Data.SO;
using UnityEngine;

namespace Code.Game.Inventory
{
    [CreateAssetMenu(fileName = "New Inventory Item", menuName = SOPathConst.InventoryItemPath, order = 100)]
    public class InventoryItemSettings : SettingsBase
    {
        public string name = "NotSet";
    }
}
