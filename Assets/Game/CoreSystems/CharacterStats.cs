using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Scriptable Objects/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    public string Name;
    public string SpecialDescription;

    public Sprite[] dificultySprites = new Sprite[3];

    public Letras Power;
    public Letras Alcance;
    public Letras Duracion;

    public Letras Estabilidad;

    public Sprite BulletsExample;

    [Header("In game resources")]

    //public Character characterPrefab;

    public Bullet bullet;

    //public Special special;

    public Bomb bomb;

}
