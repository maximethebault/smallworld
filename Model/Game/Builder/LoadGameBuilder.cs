using System;

namespace Model.Game.Builder
{
    class LoadGameBuilder : GameBuilder, ILoadGameBuilder
    {
        private string SaveFilepath
        {
            get;
            set;
        }

        public void SetSaveFilepath(string value)
        {
            SaveFilepath = value;
        }

        public string GetSaveFilepath()
        {
            return SaveFilepath;
        }

        public override void BuildPlayer()
        {
            throw new NotImplementedException();
        }

        public override void BuildUnits()
        {
            throw new NotImplementedException();
        }

        public override void BuildMap()
        {
            throw new NotImplementedException();
        }

        public override void BuildDifficulty()
        {
            throw new NotImplementedException();
        }

        public override void Create()
        {
            throw new NotImplementedException();
        }
    }
}
