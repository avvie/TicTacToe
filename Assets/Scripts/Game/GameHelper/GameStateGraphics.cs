using Game;
using UnityEngine;

public class GameStateGraphics : MonoBehaviour
{
    public static GameStateGraphics instance;
    
    public Sprite GetSprite(PlayerType playerType)
    {
        if (playerType == PlayerType.Human) return Player;
        return AI;
    }
    
    [SerializeReference]
    private Sprite Player;
    [SerializeReference]
    private Sprite AI;

    private void Start()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }
}
