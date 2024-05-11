using UnityEngine.Events;

namespace PxlSq.Game
{
    /// <summary>
    /// Handles the saving and loading of game data
    /// </summary>
    public class SaveManager
    {
        private static readonly SaveManager _instance = new();
        public static SaveManager Instance => _instance;

        public GameData GameData => _localSaveData.Data;

        public static event UnityAction<GameData> OnGameDataUpdated;

        private ISaveData<GameData> _localSaveData = new LocalSaveData<GameData>();

        /// <summary>
        /// Writes the game data to storage
        /// </summary>
        /// <param name="data"></param>
        public void Save(GameData data)
        {
            _localSaveData.Save(data);
            OnGameDataUpdated?.Invoke(data);
        }

        /// <summary>
        /// Reads the game data to storage
        /// </summary>
        /// <returns></returns>
        public GameData Load()
        {
            return _localSaveData.Load();
        }

        /// <summary>
        /// Removes the game data to storage
        /// </summary>
        public void Delete()
        {
            _localSaveData.Delete();
        }

        /// <summary>
        /// Checks whether the game data is already stored
        /// </summary>
        /// <returns></returns>
        public bool HasSaveData()
        {
            return _localSaveData.HasSaveData();
        }
    }
}
