using System;

public interface IAdService
{
    void ShowRewardedAd(Action<bool> onResult);
}
