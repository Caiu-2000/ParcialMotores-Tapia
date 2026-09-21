using TMPro;
using UnityEngine;


public class UiIndicatorLife : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        EventManager<GameEvent>.Subscribe<int>(GameEvent.DamagePlayer, UpdateLife);
    }

    private void UpdateLife(int currentLife)
    {
        text.text = currentLife.ToString();
    }

    private void OnDisable()
    {
        EventManager<GameEvent>.Unsubscribe<int>(GameEvent.DamagePlayer, UpdateLife);

    }
}
