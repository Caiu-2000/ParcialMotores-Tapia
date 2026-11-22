using UnityEngine;
using Unity.Services.LevelPlay;

public class LevelPlayInitializer : MonoBehaviour
{
    [SerializeField] private string GameId;
    private void Awake()
    {
        LevelPlay.OnInitSuccess += OnInitSuccess;
        LevelPlay.OnInitFailed += OnInitFailed;
    }
    private void Start()
    {
        Debug.Log($"Calling Init, Key length{GameId?.Length}");
        LevelPlay.Init(GameId);
    }
    private void OnDestroy()
    {
        LevelPlay.OnInitFailed -= OnInitFailed;
        LevelPlay.OnInitSuccess -= OnInitSuccess;
    }
    private void OnInitSuccess(LevelPlayConfiguration config)
    {
        Debug.Log("Ads Initialized");
    }
    private void OnInitFailed(LevelPlayInitError error)
    {
        Debug.LogError($"Ads intialization failded, error: {error}");
    }
}
