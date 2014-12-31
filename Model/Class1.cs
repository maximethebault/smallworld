using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Game
    {
        public Game(GameBuilder gb)
        {
            throw new System.NotImplementedException();
        }
    
        public List<Player> Players
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Unit> Units
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Map Map
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }

    public class UnitFactory
    {
        public Unit FactoryMethod()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Unit1
    {
    }

    public class Map
    {
        public Race RaceTypeOnTile()
        {
            throw new System.NotImplementedException();
        }

        public Tile GetTileAtPosition(Coordinate position)
        {
            throw new System.NotImplementedException();
        }
    
        enum Strategy { STRATEGY_DEMO, STRATEGY_SMALL, STRATEGY_STANDARD };

        public Map(Strategy strategy)
        {
            throw new System.NotImplementedException();
        }
    
        public DifficultyStrategy DifficultyStrategy
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Tile> Tiles
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }

    public class Turn
    {
    }

    public class DemoMapStrategy : DifficultyStrategy
    {
    }

    public class StandardMapStrategy : DifficultyStrategy
    {
    }

    public class SmallMapStrategy : DifficultyStrategy
    {
    }

    public class Desert : Tile
    {
    }

    public class Mountain : Tile
    {
    }

    public class Forest : Tile
    {
    }

    public class Plain : Tile
    {
    }

    public class UnitElf : Unit
    {
    }

    public class UnitOrc : Unit
    {
    }

    public class UnitDwarf : Unit
    {
    }

    public class Player
    {
        public String Name
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Race Race
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
