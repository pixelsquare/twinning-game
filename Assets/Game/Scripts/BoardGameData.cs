namespace PxlSq.Game
{
    /// <summary>
    /// Board game model
    /// </summary>
    public class BoardGameData
    {
        public BoardSize boardSize;
        public int[] cardIds;

        public BoardGameData()
        {
        }

        public BoardGameData(BoardSize boardSize)
        {
            this.boardSize = boardSize;
        }
    }
}
