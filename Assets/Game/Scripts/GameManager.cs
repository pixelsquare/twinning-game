using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace PxlSq.Game
{
    public enum GameState
    {
        Menu,
        Game
    }

    /// <summary>
    /// Manages the game states
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;

        public bool IsGameContinued { get; set;}

        public static event UnityAction<GameState> OnGameStateChanged;

        private GameState _gameState;

        /// <summary>
        /// Sets the game state
        /// </summary>
        /// <param name="gameState"></param>
        public void SetGameState(GameState gameState)
        {
            _gameState = gameState;
            OnGameStateChanged?.Invoke(_gameState);
        }

        /// <summary>
        /// Starts the game
        /// Attached to button click event
        /// </summary>
        [Preserve]
        public void StartGame()
        {
            IsGameContinued = false;
            SetGameState(GameState.Game);
        }

        /// <summary>
        /// Continues a game from save data
        /// Attached to a button click event
        /// </summary>
        [Preserve]
        public void ContinueGame()
        {
            IsGameContinued = true;
            SetGameState(GameState.Game);
        }

        /// <summary>
        /// Quits the game and returns to main menu
        /// </summary>
        public void QuitGame()
        {
            SetGameState(GameState.Menu);
            _audioManager.PlaySfx(SfxType.GameOver);
        }
    }
}
