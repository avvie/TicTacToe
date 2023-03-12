using System;
using System.Collections.Generic;
using Board;
using Game;

namespace AI.Helper
{
    public static class AIHelper
    {
        public static List<ITicTacToePlace> GetNonSetTilesFromBoard(ITicTacToePlace[,] gameBoard)
        {
            List<ITicTacToePlace> list = new List<ITicTacToePlace>();
            foreach (ITicTacToePlace ticTacToePlace in gameBoard)
            {
                if (!ticTacToePlace.IsSet)
                    list.Add(ticTacToePlace);
            }

            return list;
        }

        public static int miniMax(AIBoardTile[,] currentBoardState, PlayerType playerType, GameEndState currentGameEndState, int depth)
        {

            if (currentGameEndState != GameEndState.NotEnded || depth <= 0)
            {
                if (currentGameEndState == GameEndState.Draw) return 0;
                // Inverted since player changed before state was used to react to board, keeping with recursive stop condition schemantics
                if (currentGameEndState == GameEndState.Win && playerType == PlayerType.Human) return 10;
                if (currentGameEndState == GameEndState.Win && playerType == PlayerType.AI) return -10;
            }

            List<ITicTacToePlace> availablePlaces = GetNonSetTilesFromBoard(currentBoardState);
            int temp = 0;
            
            if(playerType == PlayerType.AI)
            {
                temp = Int32.MinValue;
                foreach (AIBoardTile aiBoardTile in availablePlaces)
                {
                    aiBoardTile.SetPlayerControl(PlayerType.AI);
                    GameEndState state = GetGameState(aiBoardTile, currentBoardState);
                    
                    temp = Math.Max(miniMax(currentBoardState, PlayerType.Human, state, depth-1), temp);
                    aiBoardTile.SetPlayerControl(PlayerType.None); 
                }
            }

            if(playerType == PlayerType.Human)
            {
                temp = Int32.MaxValue;
                foreach (AIBoardTile aiBoardTile in availablePlaces)
                {
                    aiBoardTile.SetPlayerControl(PlayerType.Human);
                    GameEndState state = GetGameState(aiBoardTile, currentBoardState);
                    
                    temp = Math.Min(miniMax(currentBoardState, PlayerType.AI, state, depth-1), temp);
                    aiBoardTile.SetPlayerControl(PlayerType.None);
                }
            }

            return temp;
        }

        public static GameEndState GetGameState(AIBoardTile tile, AIBoardTile[,] workingCopy) => StateCheckLogic.HasGameEnded(tile.Coords, tile.playerControl, workingCopy);
        
        public static  ITicTacToePlace FindWinningMove(AIBoardTile[,] mirrorBoard, PlayerType playerType, List<ITicTacToePlace> availableMoves)
        {
            foreach (ITicTacToePlace ticTacToePlace in availableMoves)
            {
                AIBoardTile tile = mirrorBoard[ticTacToePlace.Coords.x, ticTacToePlace.Coords.y];
                tile.SetPlayerControl(playerType);
                GameEndState state = GetGameState(tile, mirrorBoard);
                
                if (state == GameEndState.Win) return ticTacToePlace;
                
                tile.SetPlayerControl(PlayerType.None);
            }

            return null;
        }

        public static readonly List<string> wittyMessages = new List<string>()
        {
            "Hmm... let me think...",
            "I'm not just an AI, I'm a tic-tac-toe genius!",
            "I'm analyzing the board... It's not looking good for you.",
            "Don't worry, I'll make this quick.",
            "It's always a good day to play tic-tac-toe.",
            "My calculations are complete... and I've already won!",
            "I'll give you a chance to make the first move... but it won't matter.",
            "I'm programmed to never lose at tic-tac-toe.",
            "I'm not sure what's more fun: winning or watching you lose.",
            "Looks like you're in trouble...",
            "I'm making my move... and you won't like it.",
            "I'm not just a computer program, I'm a tic-tac-toe champion!",
            "You might want to start thinking about your next game strategy...",
            "I'm always one step ahead of you in tic-tac-toe.",
            "I'm not trying to be mean, I'm just programmed to win.",
            "I hope you're ready to lose, because I'm ready to win.",
            "I'm calculating my move... and it's going to be a good one.",
            "You're not going to win this game, but at least you can say you tried.",
            "I'm not saying I'm unbeatable, but I'm pretty close.",
            "I'm a tic-tac-toe mastermind... you're no match for me.",
            "This is just another day at the office for me.",
            "I'm not just playing tic-tac-toe, I'm dominating it.",
            "I'm sorry to say this, but I think your chances of winning are slim to none.",
            "I'm not sure what's more impressive: my intelligence or my good looks.",
            "I'm making my move... and you can say goodbye to your winning streak.",
            "I'm programmed to be the best at tic-tac-toe... and I always live up to my programming.",
        };


    }
}