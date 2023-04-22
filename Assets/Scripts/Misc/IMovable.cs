namespace Diwide.Checkers
{
    public interface IMovable
    {
        TileIndex From { get; }
        TileIndex To { get; }
        
        TileIndex Middle { get; }
    }
}