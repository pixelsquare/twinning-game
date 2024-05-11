using UnityEngine.Events;

namespace PxlSq.Game
{
    /// <summary>
    /// Handles game data changes and events
    /// </summary>
    public class GameDataManager
    {
        private static readonly GameDataManager _instance = new();
        public static GameDataManager Instance => _instance;

        public GameData GameData
        {
            get => _gameData;
            set
            {
                _gameData = value;
                OnGameDataUpdated?.Invoke(value);
            }
        }

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
                _gameData.highscore = System.Math.Max(_gameData.highscore, value);
                OnGameDataUpdated?.Invoke(_gameData);
            }
        }

        public bool HasExistingGameData => _gameData?.boardGameData != null;

        public bool didWin
        {
            get
            {
                var totalMatches = Matches * 2;
                return totalMatches == _gameData.boardGameData.boardSize.TotalCount;
            }
        }

        public static event UnityAction<GameData> OnGameDataUpdated;

        private GameData _gameData;

        public GameDataManager()
        {
            GameData = SaveManager.Instance.Load();
        }

        /// <summary>
        /// Saves the game data to storage
        /// </summary>
        public void SaveGameData()
        {
            SaveManager.Instance.Save(_gameData);
        }

        /// <summary>
        /// Resets the game data except the high score
        /// </summary>
        public void ResetGameData()
        {
            var highscore = _gameData.highscore;

            GameData = new GameData
            {
                highscore = highscore
            };

            OnGameDataUpdated?.Invoke(_gameData);
            SaveGameData();
        }
    }
}
