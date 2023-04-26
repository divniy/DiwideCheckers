using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Diwide.Checkers
{
    public class TilesRegistry : IInitializable
    {
        [Inject] private GameInstaller.Settings _settings;
        // private List<TileFacade> _tileFacades = new();
        private TileFacade[,] _tileFacades;
        
        public void Initialize()
        {
            _tileFacades = new TileFacade[_settings.BoardSize,_settings.BoardSize];
        }

        public TileFacade this[int row, int col]
        {
            get { return _tileFacades[row, col]; }
        }

        public TileFacade this[TileIndex tileIndex]
        {
            get { return _tileFacades[tileIndex.Row, tileIndex.Col]; }
        }
        public IEnumerable<TileFacade> TileFacades
        {
            get
            {
                List<TileFacade> list = new List<TileFacade>();
                foreach (TileFacade facade in _tileFacades)
                {
                    list.Add(facade);
                }

                return list;
            }
        }

        public IEnumerable<TileFacade> InitialBlackPawnTiles => TileFacades
            .Where(_ => _.TileIndex.Row <= 2).Where(_ => _.TileColor == ColorType.Black);

        public IEnumerable<TileFacade> InitialWhitePawnTiles => TileFacades
            .Where(_ => _.TileIndex.Row >= 5).Where(_ => _.TileColor == ColorType.Black);
        
        public void Add(TileFacade tile)
        {
            _tileFacades[tile.TileIndex.Row, tile.TileIndex.Col] = tile;
        }

        public TileFacade GetTileFacade(TileIndex index)
        {
            // return _tileFacades.Find(_ => _.TileIndex.Equals(index));
            return this[index];
        }

        public PawnFacade GetPawnFacade(TileIndex index)
        {
            return GetTileFacade(index).PawnFacade;
        }

        public bool IsTileExists(TileIndex index)
        {
            // return _tileFacades.Exists(_ => _.TileIndex == index);
            try
            {
                return this[index] != null;
            }
            catch (IndexOutOfRangeException e)
            {
                return false;
            }
        }
    }
}