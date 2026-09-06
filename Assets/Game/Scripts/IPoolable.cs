using UnityEngine;

public interface IPoolable
{
    void Spawned(Vector3 Position);
    void ReturnToPool(System.Action<IPoolable> returnaction);
}