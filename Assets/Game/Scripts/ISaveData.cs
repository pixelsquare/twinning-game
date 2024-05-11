namespace PxlSq.Game
{
    public interface ISaveData<T>
    {
        T Data { get; set; }

        void Save(T data);

        T Load();

        void Delete();

        bool HasSaveData();
    }
}
