using System;
using UnityEngine;

[System.Serializable]
public class BuffCollectable : Collectable
{
    [SerializeField] private float BuffTime;
    public override void applySelf(Player player)
    {
        player.BuffPlayer(BuffTime);
        Destroy(gameObject);
    }
}