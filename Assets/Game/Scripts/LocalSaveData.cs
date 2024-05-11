using Newtonsoft.Json;
using UnityEngine;

namespace PxlSq.Game
{
    /// <summary>
    /// Save data implementation
    /// </summary>
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

        /// <summary>
        /// Serializes and object to string and writes to player prefs.
        /// </summary>
        /// <param name="data"></param>
        public void Save(T data)
        {
            Data = data;
            var content = JsonConvert.SerializeObject(data);
            StringData = content;
            PrintLog($"Saving game data.\n{content}");
        }

        /// <summary>
        /// Deserializes the string to an object data
        /// </summary>
        /// <returns></returns>
        public T Load()
        {
            Data = JsonConvert.DeserializeObject<T>(StringData) ?? new();
            PrintLog($"Loading game data.\n{StringData}");
            return Data;
        }

        /// <summary>
        /// Removes the stored data
        /// </summary>
        public void Delete()
        {
            PlayerPrefs.DeleteKey(PlayerPrefsKey);
        }

        /// <summary>
        /// Checks whether a game data is stored
        /// </summary>
        /// <returns></returns>
        public bool HasSaveData()
        {
            return PlayerPrefs.HasKey(PlayerPrefsKey);
        }

        /// <summary>
        /// Prints a log on the console during debug mode
        /// </summary>
        /// <param name="message"></param>
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
