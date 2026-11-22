using UnityEngine;

public class AdTester : MonoBehaviour
{
    [SerializeField] MonoBehaviour adServiceSource;
    IAdService adService;
    void Awake()
    {
        adService = adServiceSource as IAdService;
    }

    public void OnShowAdButton()
    {
        adService.ShowRewardedAd(result =>
        {
            if (result)
                Debug.Log("Reward Earned");
            else
                Debug.Log("No reward");
        });
    }
}
