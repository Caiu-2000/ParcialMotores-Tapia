using TMPro;
using UnityEngine;
[DefaultExecutionOrder(150)]
public class MoneyCounter : MonoBehaviour
{
    TextMeshProUGUI text = null;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        OnMoneyChanged(GlobalData.CurrentMoney);
    }

    private void OnEnable()
    {
        EventManager<GameEvent>.Subscribe<int>(GameEvent.MoneyChanged, OnMoneyChanged);
        
    }
    private void OnDisable()
    {
        EventManager<GameEvent>.Unsubscribe<int>(GameEvent.MoneyChanged, OnMoneyChanged);
    }

    private void OnMoneyChanged(int newMoney)
    {
        print(newMoney);
        text.text = "Money: " + newMoney.ToString();
    }
}
