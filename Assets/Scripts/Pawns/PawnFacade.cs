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
        // private TileFacade _tileFacade;
        [Inject] private ColorType _color;
        [Inject] private TilesRegistry _registry;
        [Inject] private PathFinder _pathFinder;
        [Inject] private PlayerManager _playerManager;

        public TileFacade TileFacade => GetComponentInParent<TileFacade>();
        public TileIndex TileIndex => TileFacade.TileIndex;
        public ColorType Color => _color;

        public Player Player => _playerManager.GetPlayer(_color);

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
            Player.Pawns.Add(this);
            _renderer.material = _color == ColorType.Black 
                ? _settings.BlackPawnMaterial 
                : _settings.WhitePawnMaterial;
        }
        
        public void Dispose()
        {
            // _tileFacade.PawnFacade = null;
            Player.Pawns.Remove(this);
            Destroy(gameObject);
        }

        public void AssignWithTile(TileFacade tileFacade, bool worldPositionStays = true)
        {
            // TileFacade oldTileFacade = _tileFacade;
            // tileFacade.PawnFacade = this;
            // _tileFacade = tileFacade;
            transform.SetParent(tileFacade.transform, worldPositionStays);
            // if (oldTileFacade != null) oldTileFacade.PawnFacade = null;
        }

        public class Factory : PlaceholderFactory<ColorType, PawnFacade>
        {
        }
    }
}