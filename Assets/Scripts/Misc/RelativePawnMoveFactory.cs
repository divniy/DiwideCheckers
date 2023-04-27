using Zenject;

namespace Diwide.Checkers
{
    public class RelativePawnMoveFactory : IFactory<PawnFacade, TileIndex, PawnMove>
    {
        public PawnMove Create(PawnFacade pawnFacade, TileIndex relativeTo)
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