using System;
using System.Collections;
using Game;
using Game.GameHelper;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Board
{
    public class GameStateCheck : MonoBehaviour
    {
        [SerializeReference]
        private ButtonContainer container;
        [FormerlySerializedAs("EndGameText")] [SerializeReference]
        private TextMeshProUGUI endGameText;

        public event EventHandler OnActorPlayedMove;
        public event EventHandler<GameEndArgs> OnStageHasEnded;
        public event EventHandler OnGameHasEnded;

        private ITicTacToePlace[,] boardButtons { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            container.onBoardInitFinished += OnBoardInitFinished;
            endGameText.text = "";
        }

        private void OnBoardInitFinished(object sender, ITicTacToePlace[,] e)
        {
            boardButtons = e;

            foreach (ITicTacToePlace ticTacToePlace in boardButtons)
                ((UIGameTile)ticTacToePlace).OnActorSelectedPlace += TicTacToePlaceOnOnActorSelectedPlace;
        
            container.onBoardInitFinished -= OnBoardInitFinished;
        }

        private void TicTacToePlaceOnOnActorSelectedPlace(object sender, PlayerType e)
        {
            if (sender == null) throw new ArgumentNullException($"Argument {nameof(sender)} is null");
            if (sender is not ITicTacToePlace boardPlace) throw new ArgumentException($"Argument {nameof(sender)} is of wrong type");
        
            GameEndState gameEndState = StateCheckLogic.HasGameEnded(boardPlace.Coords, e, boardButtons);
            if(gameEndState is GameEndState.Win or GameEndState.Draw)
            {
                OnGameHasEnded?.Invoke(this, EventArgs.Empty);
                StartCoroutine(GameEnded(gameEndState, e));
            }
            else
                // Game has not finished
                OnActorPlayedMove?.Invoke(this, EventArgs.Empty);
        }

        private IEnumerator GameEnded(GameEndState gameEndState, PlayerType playerType)
        {
            DetachEvents();
            if(playerType == PlayerType.Human) endGameText.color = Color.blue;
            if(playerType == PlayerType.AI) endGameText.color = Color.red;
        
            if (gameEndState == GameEndState.Win)
            {
                endGameText.text = $"{Enum.GetName(typeof(PlayerType), playerType)} WON!";
                GameConfig.SetLastWinner(playerType);
            }
            if (gameEndState == GameEndState.Draw)
            {
                endGameText.color = Color.gray;
                GameConfig.SetLastWinner(PlayerType.None);
                endGameText.text = "Draw!";
            }
        
            yield return new WaitForSeconds(1);
            OnStageHasEnded?.Invoke(this, new GameEndArgs(playerType, gameEndState));
        }

        private void DetachEvents()
        {
            foreach (ITicTacToePlace ticTacToePlace in boardButtons)
                ((UIGameTile)ticTacToePlace).OnActorSelectedPlace -= TicTacToePlaceOnOnActorSelectedPlace;
        }

    
    }

    public enum GameEndState
    {
        NotEnded = 0,
        Win = 1, 
        Draw =2,
        Lose =3,
    }

    public class GameEndArgs
    {
        private PlayerType playerType { get; }
        private GameEndState gameEndState { get; }

        public GameEndArgs(PlayerType playerType, GameEndState gameEndState)
        {
            this.playerType = playerType;
            this.gameEndState = gameEndState;
        }
    
    }
}