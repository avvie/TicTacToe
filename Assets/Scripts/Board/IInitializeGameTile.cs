using Game;

namespace Board
{
    public interface IInitializeGameTile
    {
        void Initialize(Coords coords);
        PlayerType playerControl { get; }
    }
}