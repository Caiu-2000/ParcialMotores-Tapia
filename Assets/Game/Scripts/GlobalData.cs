public static class GlobalData
{

    public static int CurrentCharacterSkinIndex { get; set; } = 0;
    public static int CurrentMoney { get; set; } = 0;

    public static string SkinsCobra { get; set; } = "100000";
    public static string SkinsDavo { get; set; } = "100000";

    public static Dificltys CurrentDificulty = Dificltys.EASY;
    public static CharacterNames SelectedCharacter;
    
    public static void Load()
    {
        CurrentMoney = SaveManager.GetSavedMoney();
        SkinsCobra = SaveManager.GetSavedSkins(CharacterNames.Cobra);
        SkinsDavo = SaveManager.GetSavedSkins(CharacterNames.Davo);
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


