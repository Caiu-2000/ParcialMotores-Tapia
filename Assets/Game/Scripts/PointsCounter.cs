using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    private TextMeshProUGUI text;
    public void AddPoints(int points)
    {
        RunManager.RunPoints += points;
        text.text = points.ToString();
        
    }
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        EventManager<GameEvent>.Subscribe<int>(GameEvent.AddPoints, AddPoints);
    }
    private void OnDisable()
    {
        EventManager<GameEvent>.Unsubscribe<int>(GameEvent.AddPoints, AddPoints);
    }
}
