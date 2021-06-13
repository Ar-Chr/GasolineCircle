using System;
using UnityEngine.Events;

public class Events
{
    [Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }
    [Serializable] public class EventPlayerPassedFinish : UnityEvent<Player> { }
    [Serializable] public class EventNextLevelSelected : UnityEvent<string> { }
    [Serializable] public class EventPlayerWon : UnityEvent<Player> { }
    [Serializable] public class EventFuelChanged : UnityEvent<Player, float> { }
    [Serializable] public class EventDurabilityChanged : UnityEvent<Player, float> { }
    [Serializable] public class EventCarBroke : UnityEvent<float, Player> { }
}
