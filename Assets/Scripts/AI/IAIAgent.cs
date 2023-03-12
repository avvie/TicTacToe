using System.Collections;
using Board;
using TMPro;

namespace AI
{
    public interface IAIAgent
    {
        IEnumerator ChooseMove(ITicTacToePlace[,] gameBoard);
        void SetAgentState(bool state);
        void InitializeAgent(ITicTacToePlace[,] board);
        void SetChatLabel(TextMeshProUGUI label);
    }
}