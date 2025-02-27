namespace Code.Core.Data.Constants
{
    public static class SOPathConst
    {
        // Names
        private const string MainMenu = "BP SO/";
        private const string Config = "Settings/";
        private const string InventoryItem = "Inventory Item/";

        // Paths
        public const string ConfigPath = MainMenu + Config;
        public const string InventoryItemPath = ConfigPath + InventoryItem;
    }
}
