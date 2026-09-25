using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
        }
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
}


public enum CharacterNames
{
    Cobra,
    Davo
    
}

