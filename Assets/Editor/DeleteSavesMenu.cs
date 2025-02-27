#if UNITY_EDITOR
using System.IO;
using Code.Core.Data.Constants;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class DeleteSavesMenu
    {
        [MenuItem("Game Tools/Delete saves")]
        public static void DeleteSaves()
        {
            var directory = SavePath.Save;
            if (Directory.Exists(directory))
            {
                var files = Directory.GetFiles(directory);
                foreach (var file in files) File.Delete(file);

                UnityEngine.Debug.LogWarning("All saves deleted.");
            }
            else UnityEngine.Debug.LogWarning($"Directory {directory} not exists.");
        }

        [MenuItem("Game Tools/Reset Settings", true)]
        private static bool ValidateDeleteSaves() => !Application.isPlaying;
    }
}
#endif
