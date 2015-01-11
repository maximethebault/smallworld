using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Model.Game.Builder
{
    class LoadGameBuilder : GameBuilder, ILoadGameBuilder
    {
        public string SaveFilepath
        {
            get;
            set;
        }

        public override void Create()
        {
            var stream = File.Open(SaveFilepath, FileMode.Open);
            var bformatter = new BinaryFormatter();

            IDGame = (Game)bformatter.Deserialize(stream);
            stream.Close();
        }

        public override void BuildPlayer()
        {
            // well well well... kind of feel like that builder implementation defeats the purpose of the design pattern but we're gonna leave it so anyway
        }

        public override void BuildUnits()
        {
        }

        public override void BuildMap()
        {
        }

        public override void BuildDifficulty()
        {
        }
    }
}
