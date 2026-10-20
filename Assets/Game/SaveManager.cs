using System;
using UnityEngine;


public static class SaveManager
{
    public static void Save()
    {
        PlayerPrefs.SetInt("Money", GlobalData.CurrentMoney);
        PlayerPrefs.SetString("Skins_Cobra", GlobalData.SkinsCobra);
        PlayerPrefs.SetString("Skins_Davo", GlobalData.SkinsDavo);
        PlayerPrefs.Save();
    }
    public static int GetSavedMoney()
    {
        return PlayerPrefs.GetInt("Money", 0);
    }



    public static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }

    internal static string GetSavedSkins(CharacterNames character)
    {
        return PlayerPrefs.GetString($"Skins_{character}", "100000");
    }
}
/*
 Como guardar skins

Se hace un string con 0 y 1 
0 es skin bloqueada
1 es skin desbloqueada


 
 */