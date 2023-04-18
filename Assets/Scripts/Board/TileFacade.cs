using System;
using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    public class TileFacade : MonoBehaviour, IPoolable<TileIndex, IMemoryPool>, IDisposable
    {
        [SerializeField] private Renderer _renderer;
        [Inject] private GameInstaller.Settings _settings;
        [Inject] private TilesRegistry _registry;
        
        public PawnFacade PawnFacade { get; set; }
        // private PawnFacade.Factory _pawnFactory;

        // [Inject]
        // public void Construct(PawnFacade.Factory pawnFactory)
        // {
        //     _pawnFactory = pawnFactory;
        // }
        
        public Tile Tile { get; private set; }
        private IMemoryPool _pool;

        public TileIndex Index => Tile.Index; 

        [Inject]
        public void Construct(Tile tile)
        {
            Tile = tile;
        }
        
        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(TileIndex index, IMemoryPool pool)
        {
            _pool = pool;
            Tile.Index = index;
            transform.Translate(index.Col, 0, index.Row);
            _renderer.material = Tile.Color == ColorType.White 
                ? _settings.WhiteTileMaterial 
                : _settings.BlackTileMaterial;
            _registry.Add(this);
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        // public void CreatePawn(ColorType color)
        // {
        //     _pawnFactory.Create(Index, color);
        // }
        
        public class Factory : PlaceholderFactory<TileIndex, TileFacade>
        {
        }
    }
}