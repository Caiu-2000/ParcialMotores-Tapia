using TMPro;
using UnityEngine;

public class UiIndicatorBombs : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        EventManager<GameEvent>.Subscribe<int>(GameEvent.BombTroued, UpdateBombs);
    }

    private void UpdateBombs(int currentLife)
    {
        text.text = currentLife.ToString();
    }

    private void OnDisable()
    {
        EventManager<GameEvent>.Unsubscribe<int>(GameEvent.BombTroued, UpdateBombs);

    }
}
