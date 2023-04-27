using Zenject;

namespace Diwide.Checkers
{
    public interface IMovable
    {
        TileIndex From { get; }
        TileIndex To { get; }
        
        TileIndex Middle { get; }
    }

    public class PawnMoveFactory : PlaceholderFactory<PawnFacade, TileIndex, IMovable>
    {
    }

    public class RelativePawnMoveFactory : IFactory<PawnFacade, TileIndex, IMovable>
    {
        public IMovable Create(PawnFacade pawnFacade, TileIndex relativeTo)
        {
            var from = pawnFacade.TileIndex;
            var to = from + relativeTo;
            if (relativeTo.Row == 1 || relativeTo.Row == -1)
            {
                return new PawnMove(from, to);
            }
            else
            {
                return new PawnAttack(from, to);
            }
        }
    }
}