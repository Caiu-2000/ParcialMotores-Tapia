// Cada valor representa un evento disponible en el juego.
public enum GameEvent
{
    StartGame,
    StopGame,
    
    AddPoints,

    EnemyKilled, 
    BossProgress,

    DamagePlayer,
    
    GameStateChanged,

    BombTroued,
    GamePaused,
    GameResumed,
   
    MoneyChanged
}

// En caso de agregar una pausa iria aca 
public enum GameState
{
    OnHold,
    Running

}


public enum InputEvents
{
    BombPressed
}

public enum CombatEvents
{
    PlayerFired,
    EnemyFired
}
