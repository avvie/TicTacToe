using System;
using Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Board
{
    public abstract class UIGameTile : MonoBehaviour, IPointerClickHandler, IInitializeGameTile, IActorSelectedEvent
    {
        protected Image ButtonImage { get; set; }
        public abstract void Initialize(Coords coords);
        public abstract PlayerType playerControl { get; }
        protected PlayerType _playerType = PlayerType.None;

        public abstract void OnPointerClick(PointerEventData eventData);
        public abstract event EventHandler<PlayerType> OnActorSelectedPlace;
    }
}