using System;
using Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Board
{
    [RequireComponent(typeof(Image))]
    public class PlayButton : UIGameTile,  ITicTacToePlace
    {
        public Coords Coords { get; private set; }

        public bool IsSet { get; private set; }
        public bool isInitialized { get; private set; }

        public override PlayerType playerControl => _playerType;

        public override void Initialize(Coords coords)
        {
            this.Coords = coords;
            ButtonImage = gameObject.GetComponent<Image>();
            ButtonImage.sprite = null;
            ButtonImage.color = Color.clear;
            if (ButtonImage == null) 
                throw new ArgumentException("GameObject provided does not have required component");
            isInitialized = true;
        }
        

        public PlayerType GetPlayerControl() => playerControl;
        
        public void SetPlayerControl(PlayerType playerType)
        {
            IsSet = true;
            _playerType = playerType;
            ButtonImage.color = Color.white;
            ButtonImage.sprite = GameStateGraphics.instance.GetSprite(playerType);
            OnActorSelectedPlace?.Invoke(this, playerType);
        }

        public override event EventHandler<PlayerType> OnActorSelectedPlace;

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!isInitialized) throw new InvalidOperationException("Call Initialize first");
            if(IsSet) return;
            SetPlayerControl(PlayerType.Human);
        }
    }

    public interface ITicTacToePlace
    {
        Coords Coords { get; }
        bool IsSet { get; }
        bool isInitialized { get; }
        PlayerType playerControl { get; }
        void SetPlayerControl(PlayerType playerType);
    }

    
}