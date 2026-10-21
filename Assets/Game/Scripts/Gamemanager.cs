using System.Collections;
    using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance { get; private set; }


    [SerializeField]
    public Sprite DefaultTexture;



    public Player[] CharactersList = new Player[1];

    void Start()
    {
       

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        GlobalData.Load();

    }

    public void PlayerDied()
    {
        SceneManager.LoadScene("DeathScene");
    }
    public static void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");     
    }

    
    public void UniversalTimer(float time, ITimable Caller)
    {
        StartCoroutine(count(time, Caller));
    }
    public IEnumerator count(float time, ITimable Caller)
    {
      
        yield return new WaitForSeconds(time);
        Caller.TimeStopped();
    }


    private void OnApplicationQuit()
    {
        SaveManager.Save();
    }

}


public enum CharacterNames
{
    Cobra,
    Davo
    
}

