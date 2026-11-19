using System;
using UnityEngine;

public static class StaminaManager 
{
    public static bool RechargeStamina()
    {
        long timeStamp = SaveManager.GetTime();
        DateTime saved = DateTimeOffset.FromUnixTimeSeconds(timeStamp).UtcDateTime;
        long timeNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        long substraction = timeStamp - timeNow;
        if(substraction < -30)
        {
            GlobalData.AddStamina(20);
            SaveManager.SaveStamina();
            SaveManager.SaveTime();
            return true;
        }
        return false;
    }
}
