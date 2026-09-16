using UnityEngine;

public abstract class Bomb : MonoBehaviour 
{
    [SerializeField] protected Player player;
    protected RunManager manager;
    public abstract void Activate(); 


}

