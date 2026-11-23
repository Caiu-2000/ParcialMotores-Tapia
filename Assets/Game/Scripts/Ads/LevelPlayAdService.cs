using System;
using UnityEngine;
using Unity.Services.LevelPlay;
using System.Collections;
public class LevelPlayAdService : MonoBehaviour, IAdService
{
    [SerializeField] private string rewardedAdUnitId;
    private LevelPlayRewardedAd rewardedAd;
    private Action<bool> pendingCallback;
    private bool rewardEarned;

    private void Awake()
    {
        LevelPlay.OnInitSuccess += OnInitSuccessHandler;
    }
    private void OnDestroy()
    {
        LevelPlay.OnInitSuccess -= OnInitSuccessHandler;
        if(rewardedAd != null)
        {
            rewardedAd.OnAdRewarded -= OnAdRewarded;
            rewardedAd.OnAdClosed -= OnAdClosed;
            rewardedAd.OnAdDisplayFailed -= OnAdDisplayFailed;
            rewardedAd.OnAdLoadFailed -= OnAdLoadFailed;
        }
    }
    private void OnInitSuccessHandler(LevelPlayConfiguration config)
    {
        rewardedAd = new LevelPlayRewardedAd(rewardedAdUnitId);
        rewardedAd.OnAdRewarded += OnAdRewarded;
        rewardedAd.OnAdClosed += OnAdClosed;
        rewardedAd.OnAdDisplayFailed += OnAdDisplayFailed;
        rewardedAd.OnAdLoadFailed += OnAdLoadFailed;
        rewardedAd.LoadAd();
        Debug.Log("intialized?");
    }
    public void ShowRewardedAd(Action<bool> onResult)
    {
        Debug.Log($"timeScale = {Time.timeScale}");
        if (rewardedAd == null || !rewardedAd.IsAdReady())
        {
            Debug.Log($"Not ready. rewardedAd null? {rewardedAd == null}");
            onResult?.Invoke(false);
            return;
        }
        pendingCallback = onResult;
        rewardEarned = false;
        rewardedAd.ShowAd();
    }
    void OnAdRewarded(LevelPlayAdInfo info, LevelPlayReward reward)
    {
        Debug.Log("OnAdRewarded fired");
        rewardEarned = true;
    }
    void OnAdClosed(LevelPlayAdInfo info)
    {
        StartCoroutine(ReportResultAfterClose());

    }
    void OnAdDisplayFailed(LevelPlayAdInfo info, LevelPlayAdError error)
    {
        pendingCallback?.Invoke(false);
        pendingCallback = null;
        rewardedAd.LoadAd();
    }
    void OnAdLoadFailed(LevelPlayAdError error)
    {
        Debug.Log($"Ad load failure: {error}");
    }

    IEnumerator ReportResultAfterClose()
    {
        yield return new WaitForSecondsRealtime(0.3f);

        pendingCallback?.Invoke(rewardEarned);
        pendingCallback = null;
        rewardedAd.LoadAd();
    }
}
