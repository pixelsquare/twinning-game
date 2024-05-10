using Unity.Plastic.Newtonsoft.Json;
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

        private const string PlayerPrefsKey = "localsavedata";

        public void Save(T data)
        {
            Data = data;
            var content = JsonConvert.SerializeObject(data);
            StringData = content;
            // Debug.Log($"{nameof(LocalSaveData<T>)} : Saving game data.\n{content}");
        }

        public T Load()
        {
            Data = JsonConvert.DeserializeObject<T>(StringData) ?? new();
            // Debug.Log($"{nameof(LocalSaveData<T>)} : Loading game data.\n{StringData}");
            return Data;
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(PlayerPrefsKey);
        }
    }
}
