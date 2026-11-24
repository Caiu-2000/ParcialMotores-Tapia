using UnityEngine;

public class PointCollectable : PlayerAffecter
{
    [SerializeField] private int Points = 1;

    public override void applySelf(Player player)
    {
        EventManager<GameEvent>.Publish<int>(GameEvent.AddPoints,Points);
    }
}
