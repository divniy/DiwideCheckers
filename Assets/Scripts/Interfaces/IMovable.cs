using System;
using Zenject;

namespace Diwide.Checkers
{
    public interface IMovable
    {
        TileIndex From { get; }
        TileIndex To { get; }
        
        TileIndex Middle { get; }
    }

    [Serializable]
    public class PawnMove : IMovable
    {
        public TileIndex From { get; }
        public TileIndex To { get; }

        public TileIndex Middle { get; protected set; }

        public PawnMove(TileIndex from, TileIndex to)
        {
            From = from;
            To = to;
            Middle = null;
        }

        public PawnMove(PawnFacade pawnFacade, TileIndex relativeTo) : this(pawnFacade.Index, pawnFacade.Index + relativeTo)
        {
        }

        public override string ToString()
        {
            return $"Move: {From} => {To}";
        }
    }
    
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
            var pawn = _registry.GetTileFacade(move.Middle)?.PawnFacade;
            return pawn != null && pawn.Color != _playerManager.CurrentPlayer.PawnsColor;;
        }
    }

    public class PawnAttack : PawnMove
    {
        public PawnAttack(TileIndex from, TileIndex to) : base(from, to)
        {
            var delta = To - From;
            Middle = From + new TileIndex(delta.Row / 2, delta.Col / 2);
        }

        public PawnAttack(PawnFacade pawnFacade, TileIndex relativeTo) : this(pawnFacade.Index, pawnFacade.Index + relativeTo)
        {
        }
    }
}