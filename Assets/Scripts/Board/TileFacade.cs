using System;
using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    public class TileFacade : MonoBehaviour, IInitializable
    {
        [SerializeField] private Renderer _renderer;
        [Inject] public TileIndex TileIndex{ get; private set; }
        
        [Inject] private GameInstaller.Settings _settings;
        [Inject] private TilesRegistry _registry;
        [Inject] private TilePointerHandler _pointerHandler;
        public bool IsValidMove => _pointerHandler.IsValidMove;
        
        public ColorType TileColor => (TileIndex.Row + TileIndex.Col) % 2 != 0 
            ? ColorType.White 
            : ColorType.Black;

        // public PawnFacade PawnFacade { get; set; } = null;
        public PawnFacade PawnFacade => GetComponentInChildren<PawnFacade>();
        // private PawnFacade.Factory _pawnFactory;

        // [Inject]
        // public void Construct(PawnFacade.Factory pawnFactory)
        // {
        //     _pawnFactory = pawnFactory;
        // }
        
        // public Tile Tile { get; private set; }
        public TilePointerHandler PointerHandler => _pointerHandler;

        
        private void Awake()
        {
            _registry.Add(this);
        }

        public void Initialize()
        {
            transform.Translate(TileIndex.Col, 0, TileIndex.Row);
            _renderer.material = TileColor == ColorType.White 
                ? _settings.WhiteTileMaterial 
                : _settings.BlackTileMaterial;
        }

        public class Factory : PlaceholderFactory<TileIndex, TileFacade>
        {
        }
    }
}