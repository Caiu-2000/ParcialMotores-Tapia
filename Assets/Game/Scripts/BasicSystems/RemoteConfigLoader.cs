using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class RemoteConfigLoader : MonoBehaviour
{
    struct UserAttributes { }
    struct AppAttributes { }
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            await RemoteConfigService.Instance.FetchConfigsAsync(new UserAttributes(), new AppAttributes());
            var cfg = RemoteConfigService.Instance.appConfig;
            Debug.Log($"Remote config Ok -> health: {cfg.GetInt("defaultEnemyHealth", -1)}" + $"midBoss: {cfg.GetBool("midBossEnabled", false)}, spawnTimer: {cfg.GetFloat("spawnTimer", -1f)}");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Reomte Config failde, using default values: {e.Message}");
        }
    }
}
