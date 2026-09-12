using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    [SerializeField] private float radius = 0.25f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("helo");
        if (TryGetComponent<Player>(out Player player))
        {
            applySelf(player);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("helo");
        if (collision.TryGetComponent<Player>(out Player player))
        {
            applySelf(player);
        }
    }
    public abstract void applySelf(Player player);



}
