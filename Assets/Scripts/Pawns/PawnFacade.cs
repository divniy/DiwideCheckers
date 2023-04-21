using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    public class PawnFacade : MonoBehaviour, IInitializable
    {
        [SerializeField] private MeshRenderer _renderer;

        [Inject] private GameInstaller.Settings _settings;
        [Inject] private TileFacade _tileFacade;
        [Inject] private ColorType _color;
        [Inject] private TilesRegistry _registry;
        [Inject] private PathFinder _pathFinder;

        public TileIndex Index => _tileFacade.Index;
        public ColorType Color => _color;

        public List<PawnMove> ValidMoves => _pathFinder.ValidMoves;

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
            _registry.GetTileFacade(Index).PawnFacade = this;
            _renderer.material = _color == ColorType.Black 
                ? _settings.BlackPawnMaterial 
                : _settings.WhitePawnMaterial;
        }

        public class Factory : PlaceholderFactory<TileFacade, ColorType, PawnFacade>
        {
        }
    }
}