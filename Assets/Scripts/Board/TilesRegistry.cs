using System.Collections.Generic;
using System.Linq;

namespace Diwide.Checkers
{
    public class TilesRegistry
    {
        private List<TileFacade> _tileFacades = new();

        public IEnumerable<TileFacade> Facades => _tileFacades;

        public IEnumerable<TileFacade> InitialBlackPawnTiles => _tileFacades
            .Where(_ => _.Index.Row <= 2).Where(_ => _.Tile.Color == ColorType.Black);

        public IEnumerable<TileFacade> InitialWhitePawnTiles => _tileFacades
            .Where(_ => _.Index.Row >= 5).Where(_ => _.Tile.Color == ColorType.Black);
        
        public void Add(TileFacade tile)
        {
            _tileFacades.Add(tile);
        }

        public TileFacade GetTileFacade(TileIndex index)
        {
            return _tileFacades.Find(_ => _.Index.Equals(index));
        }

        public PawnFacade GetPawnFacade(TileIndex index)
        {
            return GetTileFacade(index).PawnFacade;
        }

        public bool IsTileExists(TileIndex index)
        {
            return _tileFacades.Exists(_ => _.Index == index);
        }
    }
}