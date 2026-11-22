using TMPro;
using UnityEngine;
[DefaultExecutionOrder(-250)]
public class stamUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textStamina;
    private void OnEnable()
    {
        EventManager<GameEvent>.Subscribe<int>(GameEvent.StammUpdated, UpdateText);
    }
    private void OnDisable()
    {
        EventManager<GameEvent>.Subscribe<int>(GameEvent.StammUpdated, UpdateText);
    }

    private void UpdateText(int obj)
    {
        textStamina.text = obj.ToString();
    }

    private void Start()
    {
        textStamina.text = GlobalData.Stamina.ToString();
    }
}
