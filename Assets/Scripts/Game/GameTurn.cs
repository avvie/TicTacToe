using System;
using AI;
using Board;
using Game;
using Game.GameHelper;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameTurn : MonoBehaviour
{
    [SerializeReference] private GameObject inputBlocker;
    [SerializeReference] private TextMeshProUGUI AiText;
    [SerializeReference] private PlayerType playerTypeTurn;

    private IAIAgent agent;

    private GameStateCheck gameStateCheck;

    void Start()
    {
        gameStateCheck = GetComponent<GameStateCheck>();
        GetComponentInChildren<ButtonContainer>().onBoardInitFinished += OnBoardInitFinished;
        gameStateCheck.OnActorPlayedMove += OnActorPlayedMove;
        gameStateCheck.OnGameHasEnded += StageEnd;
        gameStateCheck.OnStageHasEnded += GameStateCheckOnOnStageHasEnded;
        
        BeginGame(GameConfig.GetCurrentDiffAi());
    }

    private void GameStateCheckOnOnStageHasEnded(object sender, GameEndArgs e)
    {
        // Go to main menu
        SceneManager.LoadScene(0);
    }

    public void BeginGame(Func<GameObject, TextMeshProUGUI, IAIAgent> addAgent)
    {
        agent = addAgent(gameObject, AiText);
        // Chose who goes first 
        if (Random.value > 0.5f)
        {
            playerTypeTurn = PlayerType.Human;
            inputBlocker.SetActive(false);
        }
        else UpdateToActivePlayerTurn(PlayerType.AI);
    }

    // Start is called before the first frame update

    private void StageEnd(object sender, EventArgs e)
    {
        agent.SetAgentState(false);
    }

    public PlayerType GetOtherPlayer()
    {
        if (IsHuman()) return PlayerType.AI;
        return PlayerType.Human;
    }

    private void OnBoardInitFinished(object sender, ITicTacToePlace[,] e)
    {
        agent.InitializeAgent(e);
        if(!IsHuman()) agent.SetAgentState(true);
    }

    private void OnActorPlayedMove(object sender, EventArgs e) => UpdateToActivePlayerTurn(GetOtherPlayer());


    private void UpdateToActivePlayerTurn(PlayerType type)
    {
        inputBlocker.SetActive(IsHuman());
        if(IsHuman()) agent.SetAgentState(true);
        playerTypeTurn = type;
    }

    private bool IsHuman() => playerTypeTurn == PlayerType.Human;
}
