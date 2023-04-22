using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    public class PawnMover
    {
        [Inject] private TilesRegistry _registry;
        [Inject] private PlayerManager _playerManager;
        private PawnFacade _pawn;
        private PawnFacade _enemy;
        private TileFacade _targetTile;

        public void PerformMove(IMovable move)
        {
            Debug.LogFormat("Perform move {0}", move);
            _pawn = _registry.GetPawnFacade(move.From);
            _targetTile = _registry.GetTileFacade(move.To);
            if (move.Middle != null)
            {
                _enemy = _registry.GetPawnFacade(move.Middle);
                _enemy.Dispose();
            }
            _pawn.PlaceOnTile(_targetTile);
            _playerManager.EndPlayerTurn();
        }
    }
}