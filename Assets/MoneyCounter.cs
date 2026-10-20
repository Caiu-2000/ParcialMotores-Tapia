using TMPro;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    TextMeshProUGUI text = null;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        EventManager<GameEvent>.Subscribe<int>(GameEvent.MoneyChanged, OnMoneyChanged);
    }

    private void OnMoneyChanged(int newMoney)
    {
        text.text = newMoney.ToString();
    }
}
