using UnityEngine;

namespace PxlSq.Game
{
    /// <summary>
    /// Responsible for managing the game board.
    /// </summary>
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private BoardSize _boardSize = new (2, 1);
        [SerializeField] private BoardGameView _boardGameView;

        private void Start()
        {
            var gameData = new BoardGameData(_boardSize);
            _ = new BoardController(gameData, _boardGameView);
        }
    }
}
