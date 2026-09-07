// Cada valor representa un evento disponible en el juego.
public enum GameEvent
{
    StartGame,
    StopGame,
    
    AddPoints,
    RestPoints,

    PointsChanged,

    EnemyKilled,

    HealthChanged,
    HealPlayer,
    DamagePlayer,
    
    GameStateChanged

}

// En caso de agregar una pausa iria aca 
public enum GameState
{
    OnHold,
    Running

}
