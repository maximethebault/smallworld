namespace Model.Game.Builder
{
    public class BuilderFactory
    {
        public static INewGameBuilder GetNewGameBuilder()
        {
            return new NewGameBuilder();
        }

        public static ILoadGameBuilder GetLoadGameBuilder()
        {
            return new LoadGameBuilder();
        }
        public static IGameCreator GetGameCreator(IGameBuilder gameBuilder)
        {
            return new GameCreator(gameBuilder);
        }
    }
}
