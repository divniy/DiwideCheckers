using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    public class PawnFacade : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private MeshRenderer _renderer;

        [Inject] private GameInstaller.Settings _settings;
        private TileFacade _tileFacade;
        [Inject] private ColorType _color;
        [Inject] private TilesRegistry _registry;
        [Inject] private PathFinder _pathFinder;

        public TileIndex TileIndex => _tileFacade.TileIndex;
        public ColorType Color => _color;

        public List<IMovable> ValidMoves => _pathFinder.ValidMoves;

        public void GenerateValidMoves()
        {
            _pathFinder.GenerateValidMoves();
        }

        // [Inject]
        // public void Construct(TileIndex index, ColorType color)
        // {
            // _index = index;
            // _color = color;
        // }

        public void Initialize()
        {
            // _registry.GetTileFacade(Index).PawnFacade = this;
            _renderer.material = _color == ColorType.Black 
                ? _settings.BlackPawnMaterial 
                : _settings.WhitePawnMaterial;
        }
        
        public void Dispose()
        {
            _tileFacade.PawnFacade = null;
            Destroy(gameObject);
        }

        public void PlaceOnTile(TileFacade tileFacade)
        {
            if (_tileFacade != null) _tileFacade.PawnFacade = null;
            _tileFacade = tileFacade;
            _tileFacade.PawnFacade = this;
            transform.SetParent(_tileFacade.transform, false);
        }

        public class Factory : PlaceholderFactory<ColorType, PawnFacade>
        {
        }
    }
}