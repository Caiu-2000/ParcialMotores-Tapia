
using UnityEngine;

using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifesText;
    [SerializeField] private TextMeshProUGUI BombsText;


    private void OnEnable()
    {
        EventManager<GameEvent>.Subscribe<int>(GameEvent.BombTroued, UpdateBombs);
        EventManager<GameEvent>.Subscribe<int>(GameEvent.HealthChanged, UpdateLifes);

    }

    public void UpdateLifes(int cuantity)
    {
        lifesText.text = cuantity.ToString();
    }

    public void UpdateBombs(int cuantity)
    {
        BombsText.text = cuantity.ToString();
    }




    private void OnDisable()
    {
        EventManager<GameEvent>.Unsubscribe<int>(GameEvent.BombTroued, UpdateBombs);
        EventManager<GameEvent>.Unsubscribe<int>(GameEvent.HealthChanged, UpdateLifes);
    }

}
