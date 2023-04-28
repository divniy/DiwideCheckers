using System;
using Zenject;

namespace Diwide.Checkers
{
    public class RelativePawnMoveFactory : IFactory<PawnFacade, TileIndex, IMovable>
    {
        public IMovable Create(PawnFacade pawnFacade, TileIndex relativeTo)
        {
            var from = pawnFacade.TileIndex;
            var to = from + relativeTo;
            return Math.Abs(relativeTo.Row) == 1
                ? new PawnMove(from, to)
                : new PawnAttack(from, to);
        }
    }

    /*public class MoveFromStringFactory : IFactory<string, PawnMove>
    {
        public PawnMove Create(string strMove)
        {
            // string[] data = strMove.(" -> ");
            // throw new NotImplementedException();
        }
    }*/
}