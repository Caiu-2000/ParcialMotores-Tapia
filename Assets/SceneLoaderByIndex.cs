using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderByIndex : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Load(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
