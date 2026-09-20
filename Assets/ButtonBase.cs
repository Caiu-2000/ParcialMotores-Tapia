using UnityEngine;

public abstract class ButtonBase : MonoBehaviour
{
   public void Onclicked()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.PlayRandom(SoundTypes.menu);
        ClickedLogic();
    }
    public abstract void ClickedLogic();

}
