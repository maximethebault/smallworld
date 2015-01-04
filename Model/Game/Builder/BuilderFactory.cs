using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
