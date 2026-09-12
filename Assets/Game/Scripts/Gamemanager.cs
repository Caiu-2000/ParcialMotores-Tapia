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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


public enum CharacterNames
{
    Cobra,
    Davo
    
}