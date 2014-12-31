namespace Model.Game.Builder
{
    public interface ILoadGameBuilder : IGameBuilder
    {
        void SetSaveFilepath(string value);
    }
}