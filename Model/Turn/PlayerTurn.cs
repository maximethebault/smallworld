using System;
using System.Collections.Generic;
using Model.Player;

namespace Model.Turn
{
    public class PlayerTurn : IDPlayerTurn
    {
        private IEnumerator<Player.Player> _playerOrderIterator;

        public PlayerTurn(IEnumerator<Player.Player> playerOrderIterator)
        {
            _playerOrderIterator = playerOrderIterator;
        }

        public IDUnitTurn CurrentIDUnitTurn
        {
            get;
            set;
        }

        public IPlayer CurrentPlayer
        {
            get;
            set;
        }

        public IPlayer GetCurrentPlayer()
        {
            return CurrentPlayer;
        }

        public void FinishPlayerTurn()
        {
            throw new NotImplementedException();
        }
    }
}