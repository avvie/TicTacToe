using System;

namespace Game
{
    public interface IActorSelectedEvent
    {
        event EventHandler<PlayerType> OnActorSelectedPlace;
    }
}