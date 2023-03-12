using Game.GameHelper;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUICallbacks : MonoBehaviour
{
    [SerializeReference]
    private TextMeshProUGUI LastWinnerLabel;
    
    public void StartGameEasy()
    {
        GameConfig.SetStageDifficulty(GameDifficulty.Easy);
        SceneManager.LoadScene(1);
    }
    
    public void StartGameNormal()
    {
        GameConfig.SetStageDifficulty(GameDifficulty.Normal);
        SceneManager.LoadScene(1);
    }
    
    public void StartGameExpert()
    {
        GameConfig.SetStageDifficulty(GameDifficulty.Hard);
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        LastWinnerLabel.text = GameConfig.GetLastWinnerText();
    }
}
