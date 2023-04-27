using System;
using Zenject;

namespace Diwide.Checkers
{
    [Serializable]
    public class PawnMove : IMovable
    {
        public TileIndex From { get; }
        public TileIndex To { get; }

        public TileIndex Middle { get; protected set; }

        // public PawnMove(PawnFacade pawnFacade, TileIndex relativeTo) : this(pawnFacade.TileIndex, pawnFacade.TileIndex + relativeTo)
        // {
        // }
        
        public PawnMove(TileIndex from, TileIndex to)
        {
            From = from;
            To = to;
            Middle = null;
        }

        public override string ToString()
        {
            return $"{From} => {To}";
        }
        
        // public class Factory : PlaceholderFactory<PawnFacade, TileIndex, PawnMove>
        // {
        // }
    }
}