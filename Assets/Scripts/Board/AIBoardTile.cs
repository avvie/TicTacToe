using System;
using Game;

namespace Board
{
    public class AIBoardTile : ITicTacToePlace, IInitializeGameTile
    {
        public Coords Coords { get; private set; }
        public bool IsSet { get; private set; }
        public bool isInitialized { get; private set; }

        public void SetPlayerControl(PlayerType playerType)
        {
            playerControl = playerType;
            if (playerType == PlayerType.None) IsSet = false;
            else IsSet = true;
        }

        public void Initialize(Coords coords) => throw new InvalidOperationException();

        public PlayerType playerControl { get; private set; }

        public void MirrorTile(ITicTacToePlace place)
        {
            isInitialized = true;
            IsSet = place.IsSet;
            Coords = place.Coords;
            playerControl = place.playerControl;
        }
    }
}