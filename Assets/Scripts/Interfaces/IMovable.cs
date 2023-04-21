using System;
using Zenject;

namespace Diwide.Checkers
{
    public interface IMovable
    {
        TileIndex From { get; }
        TileIndex To { get; }
    }

    [Serializable]
    public class PawnMove : IMovable
    {
        public TileIndex From { get; }
        public TileIndex To { get; }

        public PawnMove(TileIndex from, TileIndex to)
        {
            From = from;
            To = to;
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

        public bool IsValid(PawnMove move)
        {
            return IsTilesExists(move)
                && AvailableToPlayer(move)
                && DestinationEmpty(move);
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
    }

    public class PawnAttack : PawnMove
    {
        public PawnAttack(TileIndex from, TileIndex to) : base(from, to)
        {
            
        }
    }
}