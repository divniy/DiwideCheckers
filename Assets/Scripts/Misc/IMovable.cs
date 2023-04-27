using Zenject;

namespace Diwide.Checkers
{
    public interface IMovable
    {
        TileIndex From { get; }
        TileIndex To { get; }
        
        TileIndex Middle { get; }
        
        public class RelativeFactory : PlaceholderFactory<PawnFacade, TileIndex, IMovable> {}
    }
}