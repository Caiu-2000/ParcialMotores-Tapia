
using System.Collections.Generic;

using UnityEngine;



public static class GlobalData
{

    public static int CurrentCharacterSkinIndex { get; set; } = 0;
    public static int CurrentMoney { get; set; } = 0;

  

    public static List<bool> SkinsDavo = new List<bool>();
    public static Dificltys CurrentDificulty = Dificltys.EASY;
    public static CharacterNames SelectedCharacter;

    public static bool IsLoaded = false;

    public static void Load()
    {
        SkinsDavo.Clear();
        CurrentMoney = SaveManager.GetSavedMoney();
        // SkinsCobraSave = SaveManager.GetSavedSkins(CharacterNames.Cobra);
        IsLoaded = true;
        SkinsDavo = GenerateSkinList(SaveManager.GetSavedSkins(CharacterNames.Davo));
        Debug.Log("De la carga se recibio : " + SaveManager.GetSavedSkins(CharacterNames.Davo) + " Lista : " + SkinsDavo.Count);
    }


    private static List<bool> GenerateSkinList(string skinsSave)
    {
        Debug.Log("Se cargo la lista con : " + skinsSave);
        List<bool> skinsList = new List<bool>() ;
        if (skinsSave == string.Empty) skinsSave = "10000000";
        foreach (char c in skinsSave)
        {
            if (c == '1')
            {
                skinsList.Add(true);
            }
            else
            {
                skinsList.Add(false);
            }
        }
        
        return skinsList;
    }
    public static string GenerateString(List<bool> list)
    {
        string newString = string.Empty;

        foreach (bool unlocked in list)
        {
            if (unlocked)
            {
                newString += '1';
            }
            else
            {
                newString += '0';
            }
        }
        if (newString == string.Empty) return "VACIA";
        return newString;
    }


    public static void Addmoney(int amount)
    {
        CurrentMoney += amount;
        EventManager<GameEvent>.Publish<int>(GameEvent.MoneyChanged, CurrentMoney);
    }
    public static void RemoveMoney(int amount)
    {
        CurrentMoney -= amount;
        EventManager<GameEvent>.Publish<int>(GameEvent.MoneyChanged, CurrentMoney);
    }

}


