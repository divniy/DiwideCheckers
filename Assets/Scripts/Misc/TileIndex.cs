namespace Diwide.Checkers
{
    public class TileIndex
    {
        public int Row { get; }
        public int Col { get; }

        public TileIndex(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public static TileIndex operator +(TileIndex firstTileIndex, TileIndex secondTileIndex)
        {
            return new TileIndex(firstTileIndex.Row + secondTileIndex.Row,
                firstTileIndex.Col + secondTileIndex.Col);
        }

        public static TileIndex operator -(TileIndex firstTileIndex, TileIndex secondTileIndex)
        {
            return new TileIndex(firstTileIndex.Row - secondTileIndex.Row,
                firstTileIndex.Col - secondTileIndex.Col);
        }

        public static bool operator !=(TileIndex firstTileIndex, TileIndex secondTileIndex)
        {
            return !(firstTileIndex == secondTileIndex);
        }

        public static bool operator ==(TileIndex firstTileIndex, TileIndex secondTileIndex)
        {
            if (ReferenceEquals(null, firstTileIndex)) return false;
            return firstTileIndex.Equals(secondTileIndex);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TileIndex)obj);
        }

        protected bool Equals(TileIndex other)
        {
            return Row == other.Row && Col == other.Col;
        }
        
        public override string ToString()
        {
            return $"(R: {Row}, C: {Col})";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Col * 397) ^ Row;
            }
        }
    }
}