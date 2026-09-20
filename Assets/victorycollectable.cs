using UnityEngine;
using UnityEngine.SceneManagement;

public class victorycollectable : PlayerAffecter
{
    public override void applySelf(Player player)
    {
        print("Toco player");
        SceneManager.LoadScene("WinScene");
    }

   
}
