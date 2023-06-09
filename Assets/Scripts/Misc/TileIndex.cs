using System;
using UnityEngine;
using Zenject;
using Object = System.Object;

namespace Diwide.Checkers
{
    [Serializable]
    public class TileIndex
    {
        // [SerializeField] private int _row;
        [field: SerializeField] public int Row { get; private set; }
        [field: SerializeField] public int Col { get; private set; }

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

        public static TileIndex operator /(TileIndex tileIndex, int del)
        {
            Func<int, int> calculate = (value) => Mathf.FloorToInt((float) value / del);
            return new TileIndex(calculate(tileIndex.Row), calculate(tileIndex.Col));
        }

        public static bool operator !=(TileIndex firstTileIndex, TileIndex secondTileIndex)
        {
            return !(firstTileIndex == secondTileIndex);
        }

        public static bool operator ==(TileIndex firstTileIndex, TileIndex secondTileIndex)
        {
            if (ReferenceEquals(null, firstTileIndex) && ReferenceEquals(null, secondTileIndex)) return true;
            if (ReferenceEquals(null, firstTileIndex)) return false;
            return firstTileIndex.Equals(secondTileIndex);
        }

        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            // TileIndex other = (TileIndex)obj;
            // return  Row == other.Row && Col == other.Col;
            return Equals((TileIndex)obj);
        }

        protected bool Equals(TileIndex other)
        {
            if (ReferenceEquals(null, other)) return false;
            return Row == other.Row && Col == other.Col;
        }
        
        public override string ToString()
        {
            // return $"(R: {Row}, C: {Col})";
            return $"{Row}, {Col}";
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