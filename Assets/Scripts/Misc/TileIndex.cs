namespace Diwide.Checkers
{
    public class TileIndex
    {
        public int Row { get; }
        public int Col { get; }

        public TileIndex(int col, int row)
        {
            Col = col;
            Row = row;
        }

        public static TileIndex operator +(TileIndex firstTileIndex, TileIndex secondTileIndex)
        {
            return new TileIndex(firstTileIndex.Col + secondTileIndex.Col,
                firstTileIndex.Row + secondTileIndex.Row);
        }

        public static TileIndex operator -(TileIndex firstTileIndex, TileIndex secondTileIndex)
        {
            return new TileIndex(firstTileIndex.Col - secondTileIndex.Col,
                firstTileIndex.Row - secondTileIndex.Row);
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
            return Col == other.Col && Row == other.Row;
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