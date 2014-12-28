namespace ClassLibrary.GameEngine.Builder
{
    public interface ILoadGameBuilder : IGameBuilder
    {
        void SetSaveFilepath(string value);
    }
}