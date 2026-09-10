using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectionManager : MonoBehaviour
{
    [SerializeField] private List<CharacterStats> Characters = new List<CharacterStats>();

    public int selected = 0;


    [SerializeField] private TextMeshProUGUI CharName;
    [SerializeField] private TextMeshProUGUI SpecialDescription;
    [SerializeField] private Image CharFace;

    [SerializeField] private TextMeshProUGUI dificulty;

    [SerializeField] private Image FireExample;

    [SerializeField] private TextMeshProUGUI Power;
    [SerializeField] private TextMeshProUGUI Reach;
    [SerializeField] private TextMeshProUGUI Duration;
    [SerializeField] private TextMeshProUGUI Estability;



    private void Start()
    {
        UpdateUi();
    }



    public void UpdateUi()
    {
        CharName.text = Characters[selected].name;
        UpdateFace();
        SpecialDescription.text = Characters[selected].SpecialDescription;
        FireExample.sprite = Characters[selected].BulletsExample;
        Power.text = Characters[selected].Power.ToString();
        Reach.text = Characters[selected].Alcance.ToString();
        Duration.text = Characters[selected].Duracion.ToString();
        Estability.text = Characters[selected].Estabilidad.ToString();


    }
    
    public void UpdateFace()
    {
        CharFace.sprite = Characters[selected].dificultySprites[(int)GlobalData.CurrentDificulty];
    }

    public void NextDificulty()
    {

    }
    public void PreviousDificulty()
    {

    }
}
public static class GlobalData
{
    public static Dificltys CurrentDificulty = Dificltys.EASY;
}

public enum Dificltys
{
    EASY,

    MEDIUM,
    HARD
    
}