using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AI.Helper;
using Board;
using Game;

namespace AI
{
    public sealed class ExpertAi : Agent
    {
        private readonly List<MoveObj> moves = new();
        
        public override IEnumerator ChooseMove(ITicTacToePlace[,] gameBoard)
        {
            yield return base.ChooseMove(gameBoard);
            
            moves.Clear();
            AIBoardTile[,] mirrorBoard = MirrorBoard();
            List<ITicTacToePlace> availableMoves = AIHelper.GetNonSetTilesFromBoard(gameBoard);
            
            BeSmug(); // Most important line in the code
            
            // Minimax for maximum smugness, best case for human is a tie
            foreach (ITicTacToePlace ticTacToePlace in availableMoves)
            {
                AIBoardTile tile = mirrorBoard[ticTacToePlace.Coords.x, ticTacToePlace.Coords.y];
                tile.SetPlayerControl(PlayerType.AI);
                yield return null;
                moves.Add(new MoveObj(){
                    value = AIHelper.miniMax(mirrorBoard, PlayerType.Human, AIHelper.GetGameState(tile, mirrorBoard), 9), 
                    move = ticTacToePlace
                    
                });
                // Always undo move (in Minimax as well) to avoid needing extra copies 
                tile.SetPlayerControl(PlayerType.None);
            }

            var maxGroup = moves.GroupBy(x => x.value).OrderByDescending(g => g.Key).FirstOrDefault();
            maxGroup.ElementAt(new Random().Next(maxGroup.Count())).move.SetPlayerControl(PlayerType.AI);

        }
        
    }
}