using Zenject;

namespace Diwide.Checkers
{
    public class BoardGenerator : IInitializable
    {
        [Inject] private GameInstaller.Settings _settings;
        [Inject] private TileFacade.Factory _tileFactory;
        
        public void Initialize()
        {
            for (var row = 0; row < _settings.BoardSize; row++)
            {
                for (int col = 0; col < _settings.BoardSize; col++)
                {
                    CreateTile(row, col);
                }
            }
        }

        private void CreateTile(int row, int col)
        {
            var index = new TileIndex(row, col);
            _tileFactory.Create(index);
        }
    }
}