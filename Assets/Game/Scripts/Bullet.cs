
using System;
using System.Collections;

using UnityEngine;


public class Bullet : MonoBehaviour , IPoolable
{

    protected Entity who;
    protected System.Action<IPoolable> returnAction;

    public  float lifeTime = 2;
 
    public float Speed = 5;
    public float TimeLimit;
    public float size = 0.1f;

    public void Spawned(Vector3 position)
    {
        this.gameObject.SetActive(true);
        transform.rotation = Quaternion.identity;
        transform.position = position;
       

    }

    public void RestartBullet(Entity who , Sprite setSprite = null)
    {
        this.who = who;
   
    }

    public void ReturnToPool(Action<IPoolable> returnaction)
    {
        StopAllCoroutines();
            
        returnAction = returnaction;
    }

    public virtual void DisableBullet()
    {
        returnAction?.Invoke(this);
    }

    public void Hitted(Entity hitted)
    {
        hitted.OnHit(new DamageData(1, who));
        DisableBullet();
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, size);
    }


    public Entity fromWho() => who;

}
