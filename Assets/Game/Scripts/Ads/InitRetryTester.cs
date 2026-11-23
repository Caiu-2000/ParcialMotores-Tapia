using UnityEngine;
using Unity.Services.LevelPlay;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InitRetryTester : MonoBehaviour
{
    [SerializeField] private string appKey;

    // Hook this to a UI Button's OnClick
    public void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("[Retry] calling Init again");
            LevelPlay.Init(appKey);
        }
    }
}