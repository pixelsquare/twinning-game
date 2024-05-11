using UnityEngine.Events;

namespace PxlSq.Game
{
    public class SaveManager
    {
        private static readonly SaveManager _instance = new();
        public static SaveManager Instance => _instance;

        public GameData GameData => _localSaveData.Data;

        public static event UnityAction<GameData> OnGameDataUpdated;

        private ISaveData<GameData> _localSaveData = new LocalSaveData<GameData>();

        public void Save(GameData data)
        {
            _localSaveData.Save(data);
            OnGameDataUpdated?.Invoke(data);
        }

        public GameData Load()
        {
            return _localSaveData.Load();
        }

        public void Delete()
        {
            _localSaveData.Delete();
        }
    }
}
