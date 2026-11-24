using UnityEngine;



[CreateAssetMenu(fileName = "CharacterSkin", menuName = "Scriptable Objects/SkinSet")]
public class Skinset : ScriptableObject
{
    public CharacterNames CharacterName;

    public Sprite SkinIcon;

    public string SkinName;

    public int SkinPrice;

    public Sprite NormalSprite;
    public Sprite BuffedSprite;
    public Sprite InvincibleSprite;

    public int SkinIndex;

    public bool IsBaseSkin = false;


    public SoundTypes SkinMusic = SoundTypes.GameMusic;

}