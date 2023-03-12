using System;
using System.Collections;
using System.Collections.Generic;
using AI.Helper;
using Board;
using Game;

namespace AI
{
    public sealed class EasyIAi : Agent
    {
        // Choose a move at random 
        public override IEnumerator ChooseMove(ITicTacToePlace[,] gameBoard)
        {
            base.ChooseMove(gameBoard);

            List<ITicTacToePlace> list = AIHelper.GetNonSetTilesFromBoard(gameBoard);

            // Random choice of tile 
            if(list.Count <= 0) yield return null;
            ITicTacToePlace randomChoice = list[new Random().Next(list.Count)];
            randomChoice.SetPlayerControl(PlayerType.AI);
        }
        
    }
}