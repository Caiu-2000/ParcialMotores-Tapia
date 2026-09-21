using UnityEngine;

public class ExitButton : ButtonBase
{
    public override void ClickedLogic()
    {
        Application.Quit();
    }
}
