using System;
using UnityEngine;


public static class SaveManager
{
    public static void Save()
    {
        PlayerPrefs.SetInt("Money", GlobalData.CurrentMoney);
        PlayerPrefs.SetString("Skins_Cobra", GlobalData.GenerateString(GlobalData.SkinsCobra));
        PlayerPrefs.SetString("Skins_Davo", GlobalData.GenerateString(GlobalData.SkinsDavo));
        PlayerPrefs.Save();
    }
    public static void SaveStamina()
    {
        PlayerPrefs.SetInt("Stamina", GlobalData.Stamina);
        PlayerPrefs.Save();
    }
    public static void SaveTime()
    {
        PlayerPrefs.SetString("lastTime", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
        PlayerPrefs.Save();
    }
    public static int GetSavedMoney()
    {
        return PlayerPrefs.GetInt("Money", 0);
    }

    public static int GetSavedStamina()
    {
        return PlayerPrefs.GetInt("Stamina", 100);
    }
    public static long GetTime()
    {
        return long.Parse(PlayerPrefs.GetString("lastTime", "0"));
    }
    public static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString($"Skins_{CharacterNames.Davo}", "1000000000");
        PlayerPrefs.SetString($"Skins_{CharacterNames.Cobra}", "100000000");

        PlayerPrefs.SetInt("Money", 0);


        GlobalData.Load();
    }

    internal static string GetSavedSkins(CharacterNames character)
    {

        string data = PlayerPrefs.GetString($"Skins_{character}", "10000000");
        if (data == "VACIA") return "10000000";
        return data;
    }

    public static bool IsSkinUnlocked(Skinset set , int index)
    {
        if (set.CharacterName == CharacterNames.Davo)
        {
            return GlobalData.SkinsDavo[index]; 
        }
        else if (set.CharacterName == CharacterNames.Cobra)
        {
            return GlobalData.SkinsCobra[index];
        }
        return false;
    }
}
