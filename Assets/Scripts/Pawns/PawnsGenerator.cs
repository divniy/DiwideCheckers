using System.Linq;
using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    public class PawnsGenerator : IInitializable
    {
        [Inject] private TilesRegistry _tilesRegistry;
        [Inject] private PawnFacade.Factory _pawnsFactory;
        
        public void Initialize()
        {
            foreach (var tileFacade in _tilesRegistry.InitialBlackPawnTiles)
            {
                CreatePawn(tileFacade, ColorType.Black);
            }
            
            foreach (var tileFacade in _tilesRegistry.InitialWhitePawnTiles)
            {
                CreatePawn(tileFacade, ColorType.White);
            }
        }

        private void CreatePawn(TileFacade tileFacade, ColorType color)
        {
            Debug.LogFormat("Tile index {0}", tileFacade.Index);
            PawnFacade pawnFacade = _pawnsFactory.Create(tileFacade, color);
            pawnFacade.transform.SetParent(tileFacade.transform, false);
            
        }
    }
}