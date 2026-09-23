using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float tickRate = 0.3f;
    Entity owner;
    float tickTimer;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void Activate(Entity owner)
    {
        this.owner = owner;
        tickTimer = 0;
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        tickTimer += Time.deltaTime;
        if(tickTimer < tickRate) return;
        tickTimer = 0f;
        if(collision.TryGetComponent(out IHittable hittable))
        {
            if (hittable is Entity entity && entity == owner) return;
            hittable.OnHit(new DamageData(damage, owner));
        }
    }
}
