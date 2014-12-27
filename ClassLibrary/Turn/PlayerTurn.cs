using System;
using System.Collections.Generic;
using ClassLibrary.Player;

namespace ClassLibrary.Turn
{
    public class PlayerTurn : IPlayerTurn
    {
        private IEnumerator<Player.Player> _playerOrderIterator;

        public PlayerTurn(IEnumerator<Player.Player> playerOrderIterator)
        {
            _playerOrderIterator = playerOrderIterator;
        }

        public UnitTurn CurrentUnitTurn
        {
            get;
            set;
        }

        public Player.Player CurrentPlayer
        {
            get;
            set;
        }

        public IUnitTurn GetCurrentUnitTurn()
        {
            return CurrentUnitTurn;
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