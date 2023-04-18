using System;
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

        public TileIndex Index => _tileFacade.Index;
        
        // [Inject]
        // public void Construct(TileIndex index, ColorType color)
        // {
            // _index = index;
            // _color = color;
        // }

        public void Initialize()
        {
            _tileFacade.PawnFacade = this;
            _renderer.material = _color == ColorType.Black 
                ? _settings.BlackPawnMaterial 
                : _settings.WhitePawnMaterial;
        }

        public class Factory : PlaceholderFactory<TileFacade, ColorType, PawnFacade>
        {
        }
    }
}