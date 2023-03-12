using Board;
using Game;

/// <summary>
/// Static Logic to solve 3x3 tic tac tow, can be extended to NxN
/// </summary>
public static class StateCheckLogic
{
    public static bool BoardFilled(ITicTacToePlace[,] boardButtons)
    {
        foreach (ITicTacToePlace boardButton in boardButtons)
        {
            if (!boardButton.IsSet) return false;
        }

        return true;
    }

    public static bool IsDiagonalWinner(Coords placed, PlayerType playerType, ITicTacToePlace[,] boardButtons)
    {
        if (placed.x == placed.y) return LeftDiagonal(new Coords(2, 2), playerType, boardButtons);
        if (placed.x + placed.y == 2) return RightDiagonal(new Coords(0, 2), playerType, boardButtons);

        return false;
    }

    private static bool RightDiagonal(Coords placed, PlayerType playerType, ITicTacToePlace[,] boardButtons)
    {
        if (!CheckPlayerControlAt(placed, playerType, boardButtons)) return false;
        if (placed is { x: 2, y: 0 } && CheckPlayerControlAt(placed, playerType, boardButtons)) return true;
        return RightDiagonal(new Coords(placed.x + 1, placed.y - 1), playerType, boardButtons);
    }

    private static bool LeftDiagonal(Coords placed, PlayerType playerType, ITicTacToePlace[,] boardButtons)
    {
        if (!CheckPlayerControlAt(placed, playerType, boardButtons)) return false;
        if (placed is { x: 0, y: 0 } && CheckPlayerControlAt(placed, playerType, boardButtons)) return true;
        return LeftDiagonal(new Coords(placed.x - 1, placed.y - 1), playerType, boardButtons);
    }

    public static bool IsVerticalWinner(Coords placed, PlayerType playerType, ITicTacToePlace[,] boardButtons)
    {
        if (!CheckPlayerControlAt(placed, playerType, boardButtons)) return false;
        if (placed.y == 0 && CheckPlayerControlAt(placed, playerType, boardButtons)) return true;
        return IsVerticalWinner(new Coords(placed.x, placed.y-1), playerType, boardButtons);
    }

    public static bool IsHorizontalWinner(Coords placed, PlayerType playerType, ITicTacToePlace[,] boardButtons)
    {
        if (!CheckPlayerControlAt(placed, playerType, boardButtons)) return false;
        if (placed.x == 0 && CheckPlayerControlAt(placed, playerType, boardButtons)) return true;
        return IsHorizontalWinner(new Coords(placed.x-1, placed.y), playerType, boardButtons);
    }

    private static bool CheckPlayerControlAt(Coords placed, PlayerType playerType, ITicTacToePlace[,] boardButtons)
    {
        return boardButtons[placed.x, placed.y].playerControl == playerType;
    }
    
    /// <summary>
    /// This is Valid logic per move per player
    /// </summary>
    /// <returns>Game State</returns>
    public static GameEndState HasGameEnded(Coords placed, PlayerType playerType, ITicTacToePlace[,] boardButtons)
    {
        bool winState = IsHorizontalWinner(new Coords(2, placed.y), playerType, boardButtons) || 
                        IsVerticalWinner(new Coords(placed.x, 2), playerType, boardButtons) || 
                        IsDiagonalWinner(placed, playerType, boardButtons);
        
        if (winState) return GameEndState.Win;
        if (!BoardFilled(boardButtons)) return GameEndState.NotEnded;
        return GameEndState.Draw;
    }
}