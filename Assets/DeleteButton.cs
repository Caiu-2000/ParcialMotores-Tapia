using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    public void Pressed()
    {
        SaveManager.DeleteData();
        GlobalData.CurrentMoney = 0;
        GlobalData.SkinsDavo = new System.Collections.Generic.List<bool>();
        
    }
}
