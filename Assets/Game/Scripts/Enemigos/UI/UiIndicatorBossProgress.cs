using UnityEngine;
using UnityEngine.UI;

public class UiIndicatorBossProgress : MonoBehaviour
{
    [SerializeField] Image fillImage;
    [SerializeField] int killsToSpawnBoss = 15;
    private void OnEnable()
    {
        EventManager<GameEvent>.Subscribe<int>(GameEvent.BossProgress, UpdateProgress);
    }
    private void UpdateProgress(int kills)
    {
        fillImage.fillAmount = (float)kills / killsToSpawnBoss;
    }
    private void OnDisable()
    {
        EventManager<GameEvent>.Unsubscribe<int>(GameEvent.BossProgress, UpdateProgress);
    }
}
