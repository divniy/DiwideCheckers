using System;
using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    [Serializable]
    public class PawnMove : BaseMovable
    {
        

        // public PawnMove(PawnFacade pawnFacade, TileIndex relativeTo) : this(pawnFacade.TileIndex, pawnFacade.TileIndex + relativeTo)
        // {
        // }
        public override TileIndex Middle { get; } = null;
        
        public PawnMove(TileIndex from, TileIndex to)
        {
            From = from;
            To = to;
        }

        public override string ToString()
        {
            return $"{From} -> {To}";
        }
        
        // public class Factory : PlaceholderFactory<PawnFacade, TileIndex, PawnMove>
        // {
        // }
    }
}