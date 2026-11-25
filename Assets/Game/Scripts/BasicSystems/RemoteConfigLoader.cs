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
            GameConfig.DefaultEnemyHealth = cfg.GetInt("defaultEnemyHealth", GameConfig.DefaultEnemyHealth);
            GameConfig.KillsToSpawnBoss = cfg.GetInt("killsToSpawnBoss", GameConfig.KillsToSpawnBoss);
            GameConfig.MidBossEnabled = cfg.GetBool("midBossEnabled", GameConfig.MidBossEnabled);
            GameConfig.MinSpawnTimer = cfg.GetFloat("minSpawnTimer", GameConfig.MinSpawnTimer);
            GameConfig.SecondsToReachMinSpawnTimer = cfg.GetFloat("secondsToReachMinSpawnTimer", GameConfig.SecondsToReachMinSpawnTimer);
            GameConfig.SpawnTimer = cfg.GetFloat("spawnTimer", GameConfig.SpawnTimer);
            Debug.Log("Remote Config cargada en GameConfig");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Reomte Config failde, using default values: {e.Message}");
        }
    }
}
