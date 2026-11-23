using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkinSelection : MonoBehaviour
{
    public string PathParaSkins = "SkinSets";
    public List<Skinset> characterSkins = new List<Skinset>();

    private int currentIndex = 0;

    [SerializeField] TextMeshProUGUI CharacterName;
    [SerializeField] Image skinImage;
    [SerializeField] Button PlayButton;


    [SerializeField] MainMenu menu;
    InputAction GoBack;

    private void Start()
    {
        GoBack = InputSystem.actions.FindAction("Cancel");
    }



    internal void LoadSkins()
    {
        characterSkins.Clear();
        characterSkins = new List<Skinset>();
        currentIndex = 0;

        Skinset[] skins = Resources.LoadAll<Skinset>(PathParaSkins);
        print("Se llamo load con resultado de: " + skins.Length + ". Personaje seleccionado: " + GlobalData.SelectedCharacter);

        foreach (Skinset skin in skins)
        {
            var Skininstance = Instantiate(skin);

            if (Skininstance.CharacterName == GlobalData.SelectedCharacter)
            {
                if (Skininstance.IsBaseSkin)
                {
                    characterSkins.Insert(0, Skininstance);
                    print(characterSkins.Count);
                }
                else
                {
                    print(characterSkins.Count);
                    characterSkins.Add(Skininstance);
                }
                print("Skin agregada: " + skin.SkinName);
            }
        }
        print(characterSkins.Count + "  Esto despues de cargar");

        UpdateImage(currentIndex);

    }



    public void ChangeSelection(int dif)
    {
        if (characterSkins.Count == 0) return;

        currentIndex += dif;
        if (currentIndex < 0) currentIndex = characterSkins.Count - 1;
        if (currentIndex >= characterSkins.Count) currentIndex = 0;
        UpdateImage(currentIndex);
    }

    private void UpdateImage(int index)
    {

        if (skinImage == null)
        {
            Debug.LogError("SkinSelection no tiene asignado el campo skinImage.");
            return;
        }
        if (!SaveManager.IsSkinUnlocked(characterSkins[index] , index))
        {
            skinImage.color = new Color(0.15f, 0.15f, 0.15f);
            PlayButton.enabled = false;
        }
        else
        {
            skinImage.color = Color.white;
            PlayButton.enabled = true;

        }

        skinImage.sprite = characterSkins[index].NormalSprite;
        CharacterName.text = characterSkins[index].SkinName;
    }

    public void UnloadScreen()
    {
        characterSkins.Clear();
        currentIndex = 0;
    }


    public void GoPlay()
    {
        GlobalData.CurrentSkin = characterSkins[currentIndex];
        GlobalData.RemoveStamina(20);
        SaveManager.SaveStamina();
        SoundManager.instance.PlayRandom(SoundTypes.menu);
        SceneManager.LoadScene(2);
    }

    private void Update()
    {
        if (GoBack.WasPressedThisFrame())
        {
            UnloadScreen();

            menu.GoSelection();
        }
    }
}