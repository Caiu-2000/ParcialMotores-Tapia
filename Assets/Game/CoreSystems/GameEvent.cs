// Cada valor representa un evento disponible en el juego.
public enum GameEvent
{
    StartGame,
    StopGame,
    
    AddPoints,
    RestPoints,

    PointsChanged,

    EnemyKilled, 
    BossProgress,

    HealthChanged,
    HealPlayer,
    DamagePlayer,
    
    GameStateChanged,

    BombTroued,
    GamePaused,
    GameResumed

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