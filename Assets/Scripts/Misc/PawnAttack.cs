using System;
using UnityEngine;

namespace Diwide.Checkers
{
    [Serializable]
    public class PawnAttack : BaseMovable
    {
        [field: SerializeReference] 
        public override TileIndex Middle { get; }


        public PawnAttack(TileIndex from, TileIndex to)
        {
            From = from;
            To = to;
            Middle = From + (To - From) / 2;
        }

        // public PawnAttack(PawnFacade pawnFacade, TileIndex relativeTo) : this(pawnFacade.TileIndex, pawnFacade.TileIndex + relativeTo)
        // {
        // }
        
    }
}