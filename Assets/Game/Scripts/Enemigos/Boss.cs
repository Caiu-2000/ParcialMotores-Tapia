using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] SpriteRenderer bossRenderer;
    [SerializeField] Sprite idleSprite;
    [SerializeField] Sprite chargingSprite;
    [SerializeField] Sprite firingSprite;
    [SerializeField] public LaserBeam laserBeam;
    public void SetIdleVisual() => bossRenderer.sprite = idleSprite;
    public void SetChargingVisual() => bossRenderer.sprite = chargingSprite;
    public void SetFiringVisual() => bossRenderer.sprite = firingSprite;

}
