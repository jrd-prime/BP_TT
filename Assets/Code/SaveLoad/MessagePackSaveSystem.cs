using System;
using System.Diagnostics;
using System.IO;
using Code.Core.Data.Constants;
using Cysharp.Threading.Tasks;
using MessagePack;
using MessagePack.Resolvers;
using R3;
using Debug = UnityEngine.Debug;

namespace Code.SaveLoad
{
    public class MessagePackSaveSystem : ISaveSystem
    {
        public ReactiveProperty<int> LastSaveTime { get; } = new(0);

        public void Initialize()
        {
        }

        public async UniTask LoadDataAsync<TSavableData>(Action<TSavableData> onDataLoadedCallback,
            TSavableData defaultData)
        {
            var filePath = SavePath.Save + typeof(TSavableData).Name + SavePath.FileExtension;

            if (!File.Exists(filePath))
            {
                Debug.LogWarning(" File not found. Save default data. Using default data.");
                await SaveToFileAsync(defaultData);
                onDataLoadedCallback.Invoke(defaultData);
                return;
            }

            try
            {
                var loadedData = await LoadFromFileAsync<TSavableData>(filePath);
                onDataLoadedCallback.Invoke(loadedData);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load data: {ex.Message}");
            }
        }

        //TODO optimize
        public async UniTask SaveToFileAsync<T>(T data)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var filePath = SavePath.Save + typeof(T).Name + SavePath.FileExtension;
            var directoryPath = Path.GetDirectoryName(filePath);

            if (directoryPath != null)
            {
                if (File.Exists(directoryPath))
                    throw new NullReferenceException($"Path {directoryPath} is a file, not a directory.");

                if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            }

            await using var fileStream =
                new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 1024 * 4, true);

            var options = MessagePackSerializerOptions.Standard.WithResolver(StandardResolver.Instance);

            var dataBytes = MessagePackSerializer.Serialize(data, options);

            await fileStream.WriteAsync(dataBytes, 0, dataBytes.Length);

            stopwatch.Stop();
            LastSaveTime.Value = (int)stopwatch.ElapsedMilliseconds;
        }

        private static async UniTask<T> LoadFromFileAsync<T>(string filePath)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException($"File not found: {filePath}");

            await using var fileStream =
                new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);

            var fileData = new byte[fileStream.Length];
            var bytesRead = await fileStream.ReadAsync(fileData, 0, fileData.Length);

            if (bytesRead < fileData.Length) Debug.LogWarning($"Failed to read all bytes from file: {filePath}");

            var options = MessagePackSerializerOptions.Standard.WithResolver(StandardResolver.Instance);

            try
            {
                var loadedData = MessagePackSerializer.Deserialize<T>(fileData, options);
                return loadedData;
            }
            catch (MessagePackSerializationException e)
            {
                Debug.LogError($"Deserialization failed: {e.Message}");
                throw;
            }
        }

        public void Dispose()
        {
        }
    }
}
