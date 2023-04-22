using Zenject;

namespace Diwide.Checkers
{
    public class PlayerManager : IInitializable
    {
        private int _currentPlayerIndex = 0;
        private Player[] _players = new Player[2];
        [Inject] private Player.Factory _playerFactory;

        public Player CurrentPlayer => _players[_currentPlayerIndex];
        
        public void Initialize()
        {
            _players[0] = _playerFactory.Create(ColorType.Black);
            _players[1] = _playerFactory.Create(ColorType.White);
            StartPlayerTurn();
        }

        public void StartPlayerTurn()
        {
            CurrentPlayer.StartTurn();
        }

        public void EndPlayerTurn()
        {
            SwitchPlayers();
            StartPlayerTurn();
        }

        private void SwitchPlayers()
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % 2;
        } 
    }
}