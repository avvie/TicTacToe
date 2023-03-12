using System;
using System.Collections;
using System.Collections.Generic;
using AI.Helper;
using Board;
using Game;

namespace AI
{
    public sealed class NormalAi : Agent
    {
        private readonly List<MoveObj> moves = new();

        public override IEnumerator ChooseMove(ITicTacToePlace[,] gameBoard)
        {
            base.ChooseMove(gameBoard);

            moves.Clear();
            AIBoardTile[,] mirrorBoard = MirrorBoard();
            List<ITicTacToePlace> availableMoves = AIHelper.GetNonSetTilesFromBoard(gameBoard);
            if(availableMoves.Count <= 0) yield break;
            
            
            // Check if there's a winning move for the AI player
            ITicTacToePlace winningMove = AIHelper.FindWinningMove(mirrorBoard, PlayerType.AI, availableMoves);
            if (winningMove != null)
            {
                winningMove.SetPlayerControl(PlayerType.AI);
                yield break;
            }

            // Check if there's a blocking move for the opponent
            ITicTacToePlace blockingMove = AIHelper.FindWinningMove(mirrorBoard, PlayerType.Human, availableMoves);
            if (blockingMove != null)
            {
                blockingMove.SetPlayerControl(PlayerType.AI);
                yield break;
            }
            
            
            ITicTacToePlace randomChoice = availableMoves[new Random().Next(availableMoves.Count)];
            randomChoice.SetPlayerControl(PlayerType.AI);
        }

        
    }
}