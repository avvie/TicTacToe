using System;
using System.Collections;
using AI.Helper;
using Board;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace AI
{
    public abstract class Agent : MonoBehaviour, IAIAgent
    {
        private bool agentIsActive;
        private ITicTacToePlace[,] board;
        private TextMeshProUGUI chatLabel;
        private Color startColor;
        private Coroutine fadeCoroutine;
        
        public virtual IEnumerator ChooseMove(ITicTacToePlace[,] gameBoard)
        {
            if (gameBoard == null) throw new ArgumentNullException($"Argument {nameof(gameBoard) is null}");
            yield return null;
        }

        public void SetAgentState(bool sState)
        {
            agentIsActive = sState;
        }

        public void InitializeAgent(ITicTacToePlace[,] board)
        {
            this.board = board;
            if (chatLabel != null)
                startColor = chatLabel.color;
        }

        public void SetChatLabel(TextMeshProUGUI label) => chatLabel = label;

        private void Update()
        {
            if(!agentIsActive) return;
            // Cancel fade out 
            if (fadeCoroutine !=  null)
            {
                StopCoroutine(fadeCoroutine);
                chatLabel.text = String.Empty;
                chatLabel.color = startColor;
            }
            agentIsActive = false;
            StartCoroutine(ChooseMove(board));
        }

        protected virtual AIBoardTile[,] MirrorBoard()
        {
            if (board == null) throw new ArgumentNullException();
            AIBoardTile[,] copy = new AIBoardTile[board.GetLength(0), board.GetLength(1)];

            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    var temp = new AIBoardTile();
                    temp.MirrorTile(board[x,y]);
                    copy[x, y] = temp;
                }
            }

            return copy;
        }

        protected virtual void BeSmug()
        {
            chatLabel.text = AIHelper.wittyMessages[new Random().Next(AIHelper.wittyMessages.Count)];
            fadeCoroutine = StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            var fadeDuration = 2.5f;
            chatLabel.color = startColor;
            for (var t = 0.0f; t < fadeDuration; t += Time.deltaTime)
            {
                var alpha = Mathf.Lerp(1.0f, 0.0f, t / fadeDuration);

                chatLabel.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                yield return null;
            }

                // Ensure the text label is fully transparent at the end of the fade
                chatLabel.color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
        }
    }
}