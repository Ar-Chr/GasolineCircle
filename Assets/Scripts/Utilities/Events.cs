using UnityEngine.Events;

public class Events
{
    [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }
    [System.Serializable] public class EventPlayerPassedFinish : UnityEvent<Player> { }
    [System.Serializable] public class EventNextLevelSelected : UnityEvent<string> { }
    [System.Serializable] public class EventPlayerWon : UnityEvent<Player> { }
    [System.Serializable] public class EventFuelChanged : UnityEvent<Player, float> { }
    [System.Serializable] public class EventDurabilityChanged : UnityEvent<Player, float> { }
}
