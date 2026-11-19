using UnityEngine;

public abstract class PlayerAffecter : MonoBehaviour
{
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TryGetComponent<Player>(out Player player))
        {
            applySelf(player);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.TryGetComponent<Player>(out Player player))
        {
            applySelf(player);
            Destroy(this.gameObject);
        }
    }
    public abstract void applySelf(Player player);



}
