namespace Diwide.Checkers
{
    public class Tile
    {
        public TileIndex Index = new(0, 0);
        
        public ColorType Color => (Index.Col + Index.Row) % 2 != 0 ? ColorType.White : ColorType.Black;
    }
}