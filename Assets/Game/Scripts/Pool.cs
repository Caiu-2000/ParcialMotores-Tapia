using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pool <T> where T : Component , IPoolable
{
    public Stack<T> poolStack = new Stack<T>();
    private T prefab;
    protected static GameObject PoolHolder;



    public Pool(T prefab, int initialSize)
    {
        if (PoolHolder == null) 
        { 
            PoolHolder = new(); 
            PoolHolder.name = "PoolHolder";
        }
        this.prefab = prefab;

        for (int i = 0; i < initialSize; i++)
        {
            var obj = Create();
            obj.gameObject.SetActive(false);
            poolStack.Push(obj);
        }
    }

    public T GetPrefab()
    {

        if (poolStack.Count > 0)
        {
            return poolStack.Pop();
        }
        
        return Create();
    }
    public virtual void ReturnPoolable(T obj)
    {
        Debug.Log("Se llamo ReturnPoolable  " + obj.gameObject.activeSelf);
        obj.gameObject.SetActive(false);
        Debug.Log("Se desactivo " + obj.gameObject.activeSelf);
        poolStack.Push(obj);
        
    }
    protected T Create()
    {
        var ins = UnityEngine.Object.Instantiate(prefab);
        ins.ReturnToPool(p => ReturnPoolable((T)p));
        ins.transform.SetParent(PoolHolder.transform, false);
        return ins;
    }

    
}

public class BulletPool : Pool<Bullet>
{
    public BulletPool(Bullet prefab, int initialSize) : base(prefab, initialSize)
    {
    }

    public void EmtpyPool()
    {
        while (poolStack.Count > 0)
        {
            
            Bullet bullet = poolStack.Pop();
            Debug.Log("Se llamo destruir  " + bullet.name);


            UnityEngine.Object.Destroy(bullet.gameObject);
        }
       
    }

}