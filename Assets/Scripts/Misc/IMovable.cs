using System;
using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    public interface IMovable
    {
        TileIndex From { get; }
        TileIndex To { get; }
        TileIndex Middle { get; }
        
        public class RelativeFactory : PlaceholderFactory<PawnFacade, TileIndex, IMovable> {}
    }

    [Serializable]
    public abstract class BaseMovable : IMovable
    {
        [field: SerializeReference]
        public TileIndex From { get; protected set; }
        
        [field: SerializeReference]
        public TileIndex To { get; protected set; }
        
        public abstract TileIndex Middle { get; }
        
        public override string ToString()
        {
            return $"{From} -> {To}";
        }
    }
}