using System.Collections.Generic;
using Zenject;

namespace Diwide.Checkers
{
    public class PlayerManager : IInitializable
    {
        private ColorType _currentPlayerIndex = ColorType.Black;
        // private Player[] _players = new Player[2];
        private Dictionary<ColorType, Player> _players = new();
        [Inject] private Player.Factory _playerFactory;
        [Inject] private SignalBus _signalBus;

        public Player CurrentPlayer => _players[_currentPlayerIndex];
        
        public void Initialize()
        {
            _players[ColorType.Black] = _playerFactory.Create(ColorType.Black);
            _players[ColorType.White] = _playerFactory.Create(ColorType.White);
            // StartPlayerTurn();
        }

        public Player GetPlayer(ColorType colorType)
        {
            return _players[colorType];
        }

        public void StartPlayerTurn()
        {
            CurrentPlayer.StartTurn();
            _signalBus.Fire<WaitingForMovementSignal>();
        }

        public void EndPlayerTurn()
        {
            SwitchPlayers();
            StartPlayerTurn();
        }

        private void SwitchPlayers()
        {
            _currentPlayerIndex = (ColorType) (((int) _currentPlayerIndex + 1) % 2);
        } 
    }
}