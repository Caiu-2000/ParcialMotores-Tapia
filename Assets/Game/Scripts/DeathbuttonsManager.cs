using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathbuttonsManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void Restart()
    {
        SceneManager.LoadScene("CombatTemplate");
    }
    public void Meun()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
