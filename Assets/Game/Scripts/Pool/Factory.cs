using UnityEngine;

public abstract class Factory <T> : FactoryBase where T : Component , ISpawnable
{
    [SerializeField] protected T objectPrefab;

    public T SpawnObject(Vector3 position , Quaternion rotation)
    {
        var instance = Instantiate(objectPrefab);
        instance.OnCreated(position, rotation);
        return instance;
    }

}


public interface ISpawnable
{
    void OnCreated(Vector3 position , Quaternion rotation);
}

public abstract class FactoryBase : MonoBehaviour
{

}