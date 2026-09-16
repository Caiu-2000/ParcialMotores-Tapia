using System;
using UnityEngine;

[System.Serializable]
public class BuffCollectable : PlayerAffecter
{
    [SerializeField] private float BuffTime;

    [SerializeField] private Sprite[] sprites = new Sprite[2];
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[(int)GlobalData.SelectedCharacter];
    }

    public override void applySelf(Player player)
    {
        player.BuffPlayer(BuffTime);
        Destroy(gameObject);
    }
}