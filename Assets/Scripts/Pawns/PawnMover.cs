using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    public class PawnMover
    {
        [Inject] private TilesRegistry _registry;
        [Inject] private PlayerManager _playerManager;
        private PawnFacade _pawn;
        private TileFacade _destinationTile;

        public void PerformMove(IMovable move)
        {
            Debug.LogFormat("Perform move {0}", move);
            _pawn = _registry.GetPawnFacade(move.From);
            _destinationTile = _registry.GetTileFacade(move.To);
            _pawn.PlaceOnTile(_destinationTile);
            _playerManager.EndPlayerTurn();
        }
    }
}