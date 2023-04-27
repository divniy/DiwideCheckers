using Zenject;

namespace Diwide.Checkers
{
    public class MoveValidator
    {
        [Inject] private TilesRegistry _registry;
        [Inject] private PlayerManager _playerManager;

        public bool IsValid(IMovable move)
        {
            return IsTilesExists(move)
                   && AvailableToPlayer(move)
                   && DestinationEmpty(move)
                   && MiddleHasValidPawn(move);
        }
        
        public bool IsTilesExists(IMovable move)
        {
            return _registry.IsTileExists(move.From) && _registry.IsTileExists(move.To);
        }

        public bool AvailableToPlayer(IMovable move)
        {
            var pawn = _registry.GetPawnFacade(move.From);
            return pawn != null && pawn.Color == _playerManager.CurrentPlayer.PawnsColor;
        }

        public bool DestinationEmpty(IMovable move)
        {
            return _registry.GetPawnFacade(move.To) == null;
        }

        private bool MiddleHasValidPawn(IMovable move)
        {
            if (move.Middle == null) return true;
            // var pawn = _registry.GetPawnFacade(move.Middle);
            var pawn = _registry[move.Middle].PawnFacade;
            return pawn != null && pawn.Color != _playerManager.CurrentPlayer.PawnsColor;;
        }
    }
}