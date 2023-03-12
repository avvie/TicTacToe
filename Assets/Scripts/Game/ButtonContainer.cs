using System;
using Board;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonContainer : MonoBehaviour
{
    private ITicTacToePlace[,] boardButtons = new ITicTacToePlace[3, 3];

    public EventHandler<ITicTacToePlace[,]> onBoardInitFinished;
    
    void Start()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            var childObject = gameObject.transform.GetChild(i);
            for (int y = 0; y < childObject.childCount; y++)
            {
                var button = childObject.GetChild(y).AddComponent<PlayButton>();
                button.Initialize(new Coords(i,y));
                boardButtons[i, y] = button;
            }
        }

        onBoardInitFinished?.Invoke(this, boardButtons);
    }

}
