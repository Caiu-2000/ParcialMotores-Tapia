using UnityEngine;



[CreateAssetMenu(fileName = "CharacterSkin", menuName = "Scriptable Objects/SkinSet")]
public class Skinset : ScriptableObject
{
    public Sprite NormalSprite;
    public Sprite BuffedSprite;
    public Sprite InvincibleSprite;
}