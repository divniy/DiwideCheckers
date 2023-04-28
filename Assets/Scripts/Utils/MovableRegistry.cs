using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Diwide.Checkers.Utils
{
    [Serializable]
    public class MovableRegistry : ISerializationCallbackReceiver
    {
        public List<IMovable> Movables = new();
        [SerializeField] private List<string> _movableStrings = new();

        public void Add(IMovable move)
        {
            Movables.Add(move);
        }

        public Queue<IMovable> GetQueue()
        {
            return new Queue<IMovable>(Movables);
        }

        public void Clear() => Movables.Clear();
        
        public void OnBeforeSerialize()
        {
            _movableStrings.Clear();
            foreach (var movable in Movables)
            {
                _movableStrings.Add(movable.ToString());
            }
        }

        public void OnAfterDeserialize()
        {
            Movables.Clear();
            foreach (var movableString in _movableStrings)
            {
                string[] indexes = movableString.Split(" -> ");
                string[] fromStrings = indexes[0].Split(", ");
                string[] toStrings = indexes[1].Split(", ");
                TileIndex from = new TileIndex(Int32.Parse(fromStrings[0]), Int32.Parse(fromStrings[1]));
                TileIndex to = new TileIndex(Int32.Parse(toStrings[0]), Int32.Parse(toStrings[1]));
                var deltaRow = (from - to).Row;
                IMovable movable;
                if (deltaRow == 1 || deltaRow == -1)
                {
                    movable = new PawnMove(from, to);
                }
                else
                {
                    movable = new PawnAttack(from, to);
                }
                Movables.Add(movable);
            }
        }
    }
}