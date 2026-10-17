using System;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[Serializable]
public class BulletManager : IUpdatable
{
    [SerializeField]
    protected Bullet[] Bullets;
    [SerializeField]
    protected int BulletCount;

    protected Entity Hittable;


    public BulletManager(int limit , Entity Objetctive)
    {
        BulletCount = -1;
        Bullets = new Bullet[limit];
        Hittable = Objetctive;
    }

    public void Update()
    {
        UpdateMovement();
        CheckCollision();
    }

    protected void UpdateMovement()
    {
        Bullet currentBullet;
        for (int x = 0; x <= BulletCount; x++)
        {
            currentBullet = Bullets[x];

            if (Time.time > currentBullet.TimeLimit) RemoveFromList(x);

            currentBullet.Move();
            
        }
    }
    protected void RemoveFromList(int index)
    { 
        Bullets[index].DisableBullet();
        Bullets[index] = Bullets[BulletCount];
        Bullets[BulletCount] = null;
        BulletCount--;
      
    }
    public void AddToList(Bullet bullet)
    {
 
        Bullets[BulletCount + 1] = bullet;
        BulletCount++;
        bullet.TimeLimit = Time.time + bullet.lifeTime;
    }

    protected  virtual void CheckCollision()
    {
        Bullet currentBulelt;
        for (int x = 0; x < BulletCount; x++)
        {
            currentBulelt = Bullets[x];
            if (DoesItHit(Hittable , currentBulelt , "BulletManager"))
            {
                currentBulelt.Hitted(Hittable);
                RemoveFromList(x);
            }
        }
    }

    public static bool DoesItHit( Entity entity , Bullet bullet , string whoCalled = " ")
    {
            return ExtraMath.ScuareDistance(entity.transform.position, bullet.transform.position) < ((entity.getSize() + bullet.size) * (entity.getSize() + bullet.size));
    }
}

[Serializable]
public class PlayerBulletManager : BulletManager
{
    [SerializeField]
    List<Enemy> EnemyList;

    public PlayerBulletManager(int Limit, List<Enemy> enemyList) : base(Limit, null)
    {
        EnemyList = enemyList;
    }


    protected override void CheckCollision()
    {
    
        Bullet currentBulelt;
        for (int x = 0; x < BulletCount; x++)
        {
            currentBulelt = Bullets[x];
            CheckIfDuped(currentBulelt);
            foreach (Enemy enemy in EnemyList)
            {
                if (enemy == null) continue;

                if (DoesItHit(enemy, currentBulelt, "PlayerBulletManager"))
                {
                    RemoveFromList(x);
                    currentBulelt.Hitted(enemy);
                    
                }
            }
        }
    }


    private void CheckIfDuped(Bullet bullet)
    {
        bool Checked = false;
        for (int x = 0; x < BulletCount; x++)
        {
            if (Bullets[x] == bullet)
            {
                if (!Checked)
                {
                    Checked = true;
                    continue;
                }
            
                Debug.LogError("Bullet esta duplicado  " + this);
                
            }
        }
    }

}





public interface IUpdatable
{
    void Update();
}




































