namespace Diwide.Checkers
{
    public class TileIndex
    {
        public int Row { get; private set; }
        public int Col { get; private set; }

        public TileIndex(int row, int col)
        {
            Row = row;
            Col = col;
        }
        
        public override string ToString()
        {
            return $"Row = {Row} || Col = {Col}";
        }
    }
}