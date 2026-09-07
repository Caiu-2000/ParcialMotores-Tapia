
using System;
using System.Collections;
using System.Threading;
using UnityEngine;


public class Bullet : MonoBehaviour , IPoolable
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] private SpriteRenderer spriteRenderer;

    protected Entity who;
    protected System.Action<IPoolable> returnAction;
    public void Spawned(Vector3 position)
    {
        
        transform.position = position;
        StartCoroutine(DeleteTime());
    }
 
    public void RestartBullet(Entity who ,bool variation = false)
    {
        this.who = who;
        spriteRenderer.sprite = variation ? sprites[0] : sprites[1];
    }

    public void ReturnToPool(Action<IPoolable> returnaction)
    {
        returnAction = returnaction;
    }


    private void Update()
    {
        transform.position += Vector3.up * 5.0f * Time.deltaTime;
    }


    public virtual void DisableBullet()
    {
        returnAction?.Invoke(this);
    }

    private IEnumerator DeleteTime()
    {
        yield return new WaitForSeconds(1.2f);
        DisableBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IHittable hittable))
        {
            if (hittable is Entity entity && entity == who)
            {
                return;
            }
            DamageData data = new DamageData(1, who);
            hittable.OnHit(data);
            DisableBullet();
        }
    }

}

public enum SoundTypes
{
    menu,
    Start,
    Death

}