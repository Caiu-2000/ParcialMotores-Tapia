
using UnityEngine;


public static class SaveManager
{
    public static void Save()
    {
        PlayerPrefs.SetInt("Money", GlobalData.CurrentMoney);
        //PlayerPrefs.SetString("Skins_Cobra", GlobalData.SkinsCobraSave);
        PlayerPrefs.SetString("Skins_Davo", GlobalData.GenerateString(GlobalData.SkinsDavo));
        PlayerPrefs.Save();
    }
    public static int GetSavedMoney()
    {
        return PlayerPrefs.GetInt("Money", 0);
    }



    public static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString($"Skins_{CharacterNames.Davo}", "10000");
        PlayerPrefs.SetInt("Money", 0);


        GlobalData.Load();
    }

    internal static string GetSavedSkins(CharacterNames character)
    {
        string data = PlayerPrefs.GetString($"Skins_{character}", "10");
        if (data == "VACIA") return "10000000";
        return data;
    }
}
