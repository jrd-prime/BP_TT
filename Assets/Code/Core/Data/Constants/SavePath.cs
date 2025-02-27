using UnityEngine;

namespace Code.Core.Data.Constants
{
    public static class SavePath
    {
        public static readonly string Save = Application.persistentDataPath + "/SaveData/";
        public const string FileExtension = ".dat";
    }
}
