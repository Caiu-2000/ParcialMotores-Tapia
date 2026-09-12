using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class SelectionManager : MonoBehaviour
{
    [SerializeField] private List<CharacterStats> Characters = new List<CharacterStats>();

    public CharacterNames selected = 0;


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
        CharName.text = Characters[(int)selected].name;
        UpdateDificulty();
        SpecialDescription.text = Characters[(int)selected].SpecialDescription;
        FireExample.sprite = Characters[(int)selected].BulletsExample;
        Power.text = "Poder : " +Characters[(int)selected].Power.ToString();
        Reach.text = "Alcance : " + Characters[(int)selected].Alcance.ToString();
        Duration.text = "Duracion : " + Characters[(int)selected].Duracion.ToString();
        Estability.text = "Estabilidad : " + Characters[(int)selected].Estabilidad.ToString();


    }
    
    public void UpdateFace()
    {
        CharFace.sprite = Characters[(int)selected].dificultySprites[(int)GlobalData.CurrentDificulty];
    }

    public void UpdateDificulty(int diference = 0)
    {
        GlobalData.CurrentDificulty += diference;
        if (GlobalData.CurrentDificulty < 0) { GlobalData.CurrentDificulty = Dificltys.HARD; }
        if (GlobalData.CurrentDificulty > Dificltys.HARD) { GlobalData.CurrentDificulty = Dificltys.EASY; }
        dificulty.text = GlobalData.CurrentDificulty.ToString();
        UpdateFace();
    }






    public void ChangeCharacer(int diference)
    {
        selected += diference;
        if (selected < 0) { selected = CharacterNames.Davo; }
        if (selected > CharacterNames.Davo) { selected = CharacterNames.Cobra; }

        UpdateUi();
    }

    public void PlayPressed()
    {
        GlobalData.SelectedCharacter = selected;
        SceneManager.LoadScene(2);
    }


}
public static class GlobalData
{
    public static Dificltys CurrentDificulty = Dificltys.EASY;
    public static CharacterNames SelectedCharacter;
}

public enum Dificltys
{
    EASY,

    MEDIUM,
    HARD
    
}