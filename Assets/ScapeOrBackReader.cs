using UnityEngine;

public class ScapeOrBackReader : ExitReader
{
    protected override void GoBackPressed()
    {
        gameObject.SetActive(false);
    }
    public void Caller()
    {
        GoBackPressed();
    }
   
}
