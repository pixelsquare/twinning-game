namespace PxlSq.Game
{
    /// <summary>
    /// Board size
    /// </summary>
    [System.Serializable]
    public class BoardSize
    {
        public uint width;
        public uint height;

        public uint TotalCount => width * height;

        public BoardSize(uint w, uint h)
        {
            width = w;
            height = h;
        }
    }
}
