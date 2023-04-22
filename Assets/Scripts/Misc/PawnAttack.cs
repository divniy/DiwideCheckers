namespace Diwide.Checkers
{
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