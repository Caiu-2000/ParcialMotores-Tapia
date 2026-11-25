using TMPro;
using UnityEngine;
[DefaultExecutionOrder(150)]
public class MoneyCounter : MonoBehaviour
{
    TextMeshProUGUI text = null;
    [SerializeField]
    string ExtraText = "Money : ";
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
        text.text = ExtraText + newMoney.ToString();
    }
}
