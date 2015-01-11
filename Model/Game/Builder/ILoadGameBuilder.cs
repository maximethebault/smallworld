namespace Model.Game.Builder
{
    public interface ILoadGameBuilder : IGameBuilder
    {
        string SaveFilepath { get; set; }
    }
}