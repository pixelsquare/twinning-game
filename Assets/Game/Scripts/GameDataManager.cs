using UnityEngine;
using UnityEngine.Events;

namespace PxlSq.Game
{
    public class GameDataManager
    {
        private static readonly GameDataManager _instance = new();
        public static GameDataManager Instance => _instance;

        public GameData GameData => _gameData;

        public uint Turns
        {
            get => _gameData.turns;
            set
            {
                _gameData.turns = value;
                OnGameDataUpdated?.Invoke(_gameData);
            }
        }

        public uint Matches
        {
            get => _gameData.matches;
            set
            {
                _gameData.matches = value;
                OnGameDataUpdated?.Invoke(_gameData);
            }
        }

        public uint Score
        {
            get => _gameData.score;
            set
            {
                _gameData.score = value;
                OnGameDataUpdated?.Invoke(_gameData);
            }
        }

        public static event UnityAction<GameData> OnGameDataUpdated;

        private GameData _gameData;

        public GameDataManager()
        {
            _gameData = SaveManager.Instance.Load();
        }

        public void SaveGameData()
        {
            SaveManager.Instance.Save(_gameData);
        }
    }
}
