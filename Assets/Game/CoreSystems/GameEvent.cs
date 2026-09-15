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
    
    GameStateChanged,

    BombTroued

}

// En caso de agregar una pausa iria aca 
public enum GameState
{
    OnHold,
    Running

}
