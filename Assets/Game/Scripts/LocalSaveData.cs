using Newtonsoft.Json;
using UnityEngine;

namespace PxlSq.Game
{
    public class LocalSaveData<T> : ISaveData<T> where T : new()
    {
        public T Data { get; set; }

        private string StringData
        {
            get => PlayerPrefs.GetString(PlayerPrefsKey, "");
            set => PlayerPrefs.SetString(PlayerPrefsKey, value);
        }

        private bool _isDebugMode;

        private const string PlayerPrefsKey = "localsavedata";

        public LocalSaveData(bool isDebugMode = false)
        {
            _isDebugMode = isDebugMode;
        }

        public void Save(T data)
        {
            Data = data;
            var content = JsonConvert.SerializeObject(data);
            StringData = content;
            PrintLog($"Saving game data.\n{content}");
        }

        public T Load()
        {
            Data = JsonConvert.DeserializeObject<T>(StringData) ?? new();
            PrintLog($"Loading game data.\n{StringData}");
            return Data;
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(PlayerPrefsKey);
        }

        public bool HasSaveData()
        {
            return PlayerPrefs.HasKey(PlayerPrefsKey);
        }

        private void PrintLog(string message)
        {
            if (!_isDebugMode)
            {
                return;
            }

            Debug.Log($"{nameof(LocalSaveData<T>)} : {message}");
        }
    }
}
