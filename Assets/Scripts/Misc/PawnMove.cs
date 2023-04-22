using System;

namespace Diwide.Checkers
{
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
}