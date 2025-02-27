using System;
using Code.Core.Data.Constants;
using Code.Game.Inventory;
using UnityEngine;

namespace Code.Core.Data.SO
{
    [CreateAssetMenu(fileName = "MainSettings", menuName = SOPathConst.ConfigPath + "Main Settings", order = 100)]
    public class MainSettings : SettingsBase
    {
        [SerializeField] private InventoryMainSettings inventoryMainSettings;

        public InventoryMainSettings InventoryMainSettings => inventoryMainSettings;

        private void OnValidate()
        {
            if (!inventoryMainSettings) throw new NullReferenceException("Inventory main settings is null");
        }
    }
}
