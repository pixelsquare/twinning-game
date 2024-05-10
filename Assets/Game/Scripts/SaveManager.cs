namespace PxlSq.Game
{
    public class SaveManager
    {
        private static SaveManager _instance = new();
        public static SaveManager Instance => _instance;

        private ISaveData<GameData> _localSaveData = new LocalSaveData<GameData>();

        public void Save(GameData data)
        {
            _localSaveData.Save(data);
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
